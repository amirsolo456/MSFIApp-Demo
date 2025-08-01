using Microsoft.Extensions.Options;
using MSFIApp.Dtos.Common;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MSFIApp.Services.Common
{
    public class ApiClient : IApiCleint
    {
        static string BaseUrl;
        static string Token;
        int maxRetryAttempts = 3;
        int delayMilliseconds = 1000;

        private HttpClient _httpClient;
        private readonly ISecureStorageService _secureStorageService;
        public ApiClient(IOptions<ApiSettings> options, ISecureStorageService secureStorageService)
        {
            BaseUrl = options.Value.BaseUrl;
            _secureStorageService = secureStorageService;
            maxRetryAttempts = options.Value.maxRetryAttempts;
            delayMilliseconds = options.Value.delayMilliseconds;
        }

        public async Task<ApiResponse<C>> ApiSendRequest<T, C, D>(string resourceFileName) where T : ApiResponse<C>
        {

            try
            {
                var assembly = typeof(ApiClient).Assembly;
                var resourcePath = $"MSFIApp.Resources.FakeData.{resourceFileName}";

                using var stream = assembly.GetManifestResourceStream(resourcePath);
                if (stream == null)
                {
                    return new ApiResponse<C>()
                    {
                        Error = new ApiError
                        {
                            Code = "FileNotFound",
                            Message = $"فایل {resourceFileName} پیدا نشد."
                        }
                    };
                }

                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                // فرض کن C همون مدل داده‌ی داخل responses هست

                var jsonDoc = JsonDocument.Parse(json);
                var root = jsonDoc.RootElement;

                // آرایه responses رو بگیر
                var responsesElement = root.GetProperty("responses");

                // اولین عنصر آرایه responses رو به ApiResponse<C> تبدیل کن
                var firstResponseJson = responsesElement[0].GetRawText();

                var response = JsonSerializer.Deserialize<ApiResponse<C>>(firstResponseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return response ?? new ApiResponse<C>
                {
                    Error = new ApiError { Code = "NoResponse", Message = "هیچ پاسخی وجود ندارد." }
                };
            }
            catch (InvalidOperationException OpEx)
            {
                new ApiResponse<C>()
                {
                    Error = new ApiError
                    {
                        Code = "HttpRequestException",
                        Message = OpEx.Message
                    }
                };
            }
            catch (ArgumentNullException argEx)
            {
                new ApiResponse<C>()
                {
                    Error = new ApiError
                    {

                        Code = "HttpRequestException",
                        Message = argEx.Message
                    }
                };
            }
            catch (HttpRequestException httpEx)
            {
                new ApiResponse<C>()
                {
                    Error = new ApiError
                    {

                        Code = "HttpRequestException",
                        Message = httpEx.Message
                    }
                };
            }
            catch (TaskCanceledException ex)
            {
                return new ApiResponse<C>()
                {
                    Error = new ApiError
                    {
                        Code = "Timeout",
                        Message = "درخواست به دلیل تأخیر زیاد یا قطع اتصال لغو شد."
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<C>()
                {
                    Error = new ApiError
                    {
                        Code = "UnhandledException",
                        Message = ex.Message
                    }
                };
            }

            return new ApiResponse<C>()
            {
                Error = new ApiError
                {
                    Code = "MaxRetryExceeded",
                    Message = "تلاش‌های مکرر برای ارتباط با سرور ناموفق بود."
                }
            };
        }
    }



    public interface IApiCleint
    {
        Task<ApiResponse<C>> ApiSendRequest<T, C, D>(string resourceFileName) where T : ApiResponse<C>;

    }

    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public int Id { get; set; }
        public ApiError? Error { get; set; }
        public T Entity { get; set; }
        public int? Total { get; set; }
        public bool IsFailure => Error != null;
    }

    public class ApiError
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}

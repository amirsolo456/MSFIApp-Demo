using MSFIApp.Dtos.Autorize.Login;
using System.Globalization;
using System.Text.Json;

namespace MSFIApp.Services.Common
{
    public interface ISecureStorageService
    {
        protected Task SaveAsync(string key, string value);
        void SaveUser(string key, MSFIApp.Dtos.Autorize.Login.ResponseData value);
        Task<MSFIApp.Dtos.Autorize.Login.ResponseData> GetUser();
        protected void RemoveUser();
        protected Task<string> GetAsync(string key);
        Task<string> GetUserTokenAsync();
        Task<string> GetTokenExpireAsync();
        Task<bool> hasToken();
        Task<bool> hasExpiredToken();
    }

    public class SecureStorageService : ISecureStorageService
    {
        private readonly ISecureStorage SecureStorage;
        public SecureStorageService(ISecureStorage secureStorage)
        {
            SecureStorage = secureStorage;
        }
        public async Task SaveAsync(string key, string value)
        {
            await SecureStorage?.SetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            try
            {
                return await SecureStorage?.GetAsync(key);
            }
            catch (Exception)
            {
                // Handle exceptions like if device doesn't support secure storage
                return null;
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                SecureStorage?.Remove(key);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> hasToken()
        {
            string? token = await SecureStorage?.GetAsync("user_token");
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                if (await hasExpiredToken())
                {
                    SecureStorage?.Remove("user");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return false;
        }

        public async void SaveUser(string key, ResponseData value)
        {
            if (value == null) return;
            try
            {
                 var json = JsonSerializer.Serialize(value);
                 await SecureStorage?.SetAsync(key, json);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ResponseData> GetUser()
        {
            var json = await GetAsync("user");
            if (string.IsNullOrEmpty(json))
                return null;

            try
            {
                return JsonSerializer.Deserialize<ResponseData>(json);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public async Task<string> GetUserTokenAsync()
        {
            string token = "";
            try
            {
                ResponseData user=null;
                var json = await GetAsync("user");
                if (string.IsNullOrEmpty(json))
                    return null;

                try
                {
                    user =  JsonSerializer.Deserialize<ResponseData>(json);
                }
                catch (JsonException)
                {
                    return "";
                }

                if (user is null) return "";

                if (string.IsNullOrEmpty(user.Token))
                {
                    return "";
                }

                if (await hasExpiredToken())
                {
                    SecureStorage?.Remove("user");
                    return "";
                }

                return user.Token;
            }
            catch (Exception ex)
            {

            }

            return "";
        }


        public async Task<string> GetTokenExpireAsync()
        {
            var user = await GetUser();
            try
            {
                if (user == null) return "";
                return user.Expire;
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<bool> hasExpiredToken()
        {
            var expireStr = await GetTokenExpireAsync();

            if (string.IsNullOrWhiteSpace(expireStr))
                return false;

            try
            {

                var parts = expireStr.Split('/');
                if (parts.Length != 3)
                    return true;

                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);

                var pc = new PersianCalendar();
                var expireDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);

                return DateTime.Now > expireDate;
            }
            catch (Exception ex)
            {

                return true;
            }
        }

        public void RemoveUser()
        {
            try
            {
                SecureStorage?.Remove("user");
            }
            catch
            {


            }
        }
    }
}

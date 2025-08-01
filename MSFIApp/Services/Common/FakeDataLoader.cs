using MSFIApp.Services.Common;
using System.Reflection;
using System.Text.Json;

namespace MSFIApp.Services.Common
{
    public static class FakeDataLoader
    {
        public static async Task<T?> LoadFakeDataAsync<T>(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"YourAppNamespace.FakeData.{fileName}";

            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"Fake data file not found: {fileName}");

            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<T>(json);
        }
    }

}
//var authData = await FakeDataLoader.LoadFakeDataAsync<AuthResponseModel>("Auth.json");

using Newtonsoft.Json;
using System.Text;

namespace Comp2001.Authentication
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateUser(LoginDto loginDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://external-authentication-api.com/authenticate", content);

            if (!response.IsSuccessStatusCode)
            {
                return null; // Or handle the error as needed
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}

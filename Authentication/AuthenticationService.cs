using Comp2001.MISC;
using Newtonsoft.Json;
using System.Text;

namespace Comp2001.Authentication
{
    // Defines a service for authenticating users using an external API.
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        // Constructor injects an HttpClient instance.
        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Sends a login request to the external authentication API.
        public async Task<string> AuthenticateUser(LoginDto loginDto)
        {
            // Serialize the login credentials to JSON.
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            // Replace with the actual API URL.
            var response = await _httpClient.PostAsync("https://external-authentication-api.com/authenticate", content);

            // Return null for unsuccessful responses, or the response content for successful ones.
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }
    }

}

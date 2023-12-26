using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Comp2001.Models;
using Comp2001.MISC;

namespace Comp2001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        // Constructor for dependency injection of HttpClientFactory.
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        // Endpoint for user login.
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // Serialize login credentials and send a POST request to the authentication API.
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("https://web.socem.plymouth.ac.uk/COMP2001/auth/api/users", content);

            // Return the appropriate action based on the response status.
            return response.IsSuccessStatusCode ? Ok(await response.Content.ReadAsStringAsync()) : Unauthorized();
        }
    }
}
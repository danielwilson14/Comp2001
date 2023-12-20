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

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.example.com/v2/login", content);

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }

            var resultContent = await response.Content.ReadAsStringAsync();

            return Ok(resultContent); 
        }
    }
}
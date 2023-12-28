using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Comp2001.Models;
using Comp2001.MISC;
using Microsoft.Extensions.Configuration;
using Comp2001.Data;
using Microsoft.EntityFrameworkCore;

namespace Comp2001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context; 

        // Constructor for dependency injection
        public AuthController(
            IHttpClientFactory httpClientFactory,
            TokenService tokenService,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _tokenService = tokenService;
            _configuration = configuration;
            _context = context;
        }

        // Endpoint for user login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // Serialize login credentials and send a POST request to the authentication API
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://web.socem.plymouth.ac.uk/COMP2001/auth/api/users", content);

            // Check if authentication was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var authenticationResult = JsonConvert.DeserializeObject<List<string>>(responseContent);

                // Check if the user is verified
                if (authenticationResult[0] == "Verified" && authenticationResult[1] == "True")
                {
                    // User is verified, now get the user's role from your database
                    var user = await _context.Users
                                     .Where(u => u.Email == loginDto.Email)
                                     .FirstOrDefaultAsync();
                    if (user == null)
                    {
                        return Unauthorized("User does not exist in the database.");
                    }

                    // Determine the role based on the admin flag
                    string role = user.Admin ? "Admin" : "User";

                    // Generate a token with the user's email and role
                    var token = _tokenService.GenerateToken(loginDto.Email, role);

                    // Return the token
                    return Ok(new { Token = token });
                }
            }

            // If authentication is not successful, return Unauthorised
            return Unauthorized();
        }
    }
}
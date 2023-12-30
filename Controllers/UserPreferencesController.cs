using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comp2001.Data;
using Comp2001.Models;
using Comp2001.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Comp2001.Controllers
{
    // Controller for CRUD operations on 'UserPreferences' entities.
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor for dependency injection of the database context.
        public UserPreferencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET all User Preferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPreferences>>> GetUserPreferences()
        {
          if (_context.UserPreferences == null)
          {
              return NotFound();
          }
            return await _context.UserPreferences.ToListAsync();
        }

        // GET User preferences by specific ID
        [HttpGet("{id}", Name = "GetUserPreferences")]
        public async Task<ActionResult<UserPreferencesReadDTO>> GetUserPreferences(int id)
        {
          if (_context.UserPreferences == null)
          {
              return NotFound();
          }
            var userPreferences = await _context.UserPreferences.FindAsync(id);

            if (userPreferences == null)
            {
                return NotFound();
            }

            var userPreferencesDto = new UserPreferencesReadDTO
            {
                UserId = userPreferences.UserId,
                Units = userPreferences.Units,
                ActivityTimePreference = userPreferences.ActivityTimePreference,
                Height = userPreferences.Height,
                Weight = userPreferences.Weight,
                MarketingLanguage = userPreferences.MarketingLanguage
            };

            userPreferencesDto.Links.Add(new LinkDto(Url.Link("GetUserPreferences", new { id = id }), "view userPreferences information", "GET"));
            userPreferencesDto.Links.Add(new LinkDto(Url.Link("PutUserPreferences", new { userId = id }), "update userPreferences", "PUT"));
            userPreferencesDto.Links.Add(new LinkDto(Url.Link("DeleteUserPreferences", new { userId = id }), "delete userPreferences", "DELETE"));

            return userPreferencesDto;
        }

        // Update UserPreferences
        [HttpPut("{userId}", Name = "PutUserPreferences")]
        public async Task<IActionResult> PutUserPreferences(int userId, UserPreferencesUpdateDTO userPreferencesUpdateDTO)
        {

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var tokenUserId = int.Parse(userIdClaim.Value);

            // Check if the userId in the path matches the userId from the token
            if (userId != tokenUserId)
            {
                return Forbid(); // User is not authorized to edit these preferences
            }


            var userPreferences = await _context.UserPreferences.FindAsync(userId);
            if (userPreferences == null)
            {
                return NotFound();
            }

            userPreferences.Units = userPreferencesUpdateDTO.Units;
            userPreferences.ActivityTimePreference = userPreferencesUpdateDTO.ActivityTimePreference;
            userPreferences.Height = userPreferencesUpdateDTO.Height;
            userPreferences.Weight = userPreferencesUpdateDTO.Weight;
            userPreferences.MarketingLanguage = userPreferencesUpdateDTO.MarketingLanguage;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPreferencesExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(userPreferences);
        }

        [HttpPost]
        public async Task<ActionResult<UserPreferencesReadDTO>> PostUserPreferences(UserPreferencesCreateDTO userPreferencesCreateDTO)
        {
            var userPreference = new UserPreferences
            {
                UserId = userPreferencesCreateDTO.UserId,
                Units = userPreferencesCreateDTO.Units,
                ActivityTimePreference = userPreferencesCreateDTO.ActivityTimePreference,
                Height = userPreferencesCreateDTO.Height,
                Weight = userPreferencesCreateDTO.Weight,
                MarketingLanguage = userPreferencesCreateDTO.MarketingLanguage,
            };

            _context.UserPreferences.Add(userPreference);
            await _context.SaveChangesAsync();

            var userPreferencesReadDTO = new UserPreferencesReadDTO
            {
                UserId = userPreference.UserId,
                Units = userPreference.Units,
                ActivityTimePreference = userPreference.ActivityTimePreference,
                Height = userPreference.Height,
                Weight = userPreference.Weight,
            };

            return CreatedAtAction(nameof(GetUserPreferences), new { id = userPreference.UserId }, userPreferencesReadDTO);
        }

        // DELETE userPreferences
        [HttpDelete("{userId}", Name = "DeleteUserPreferences")]
        public async Task<IActionResult> DeleteUserPreferences(int userId)
        {
            var userPreferences = await _context.UserPreferences.FindAsync(userId);
            if (userPreferences == null)
            {
                return NotFound();
            }

            _context.UserPreferences.Remove(userPreferences);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPreferencesExists(int userId)
        {
            return (_context.UserPreferences?.Any(e => e.UserId == userId)).GetValueOrDefault();
        }
    }
}

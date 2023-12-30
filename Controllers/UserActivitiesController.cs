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
    // Controller for CRUD operations on 'UserActivities' entities.
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor for dependency injection of the database context.
        public UserActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET all User Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivity>>> GetUserActivity()
        {
          if (_context.UserActivity == null)
          {
              return NotFound();
          }
            return await _context.UserActivity.ToListAsync();
        }

        // GET User activity by specific ID
        [HttpGet("{id}", Name = "GetUserActivity")]
        public async Task<ActionResult<UserActivityReadDTO>> GetUserActivity(int id)
        {
          if (_context.UserActivity == null)
          {
              return NotFound();
          }
            var userActivity = await _context.UserActivity.FindAsync(id);

            if (userActivity == null)
            {
                return NotFound();
            }

            var userActivitiesDto = new UserActivityReadDTO
            {
                UserActivityId = userActivity.UserActivityId,
                UserId = userActivity.UserId,
                ActivityName = userActivity.ActivityName
            };

            userActivitiesDto.Links.Add(new LinkDto(Url.Link("GetUserActivity", new { id = id }), "view userActivity information", "GET"));
            userActivitiesDto.Links.Add(new LinkDto(Url.Link("PutUserActivity", new { UserActivityId = id }), "update userActivity", "PUT"));
            userActivitiesDto.Links.Add(new LinkDto(Url.Link("DeleteUserActivity", new { UserActivityId = id }), "delete userActivity", "DELETE"));


            return userActivitiesDto;
        }

        // Update user activities
        [HttpPut("{UserActivityId}", Name = "PutUserActivity")]
        public async Task<IActionResult> PutUserActivity(int UserActivityId, UserActivityUpdateDTO userActivityUpdateDTO)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var userActivity = await _context.UserActivity.FindAsync(UserActivityId);
            if (userActivity == null)
            {
                return NotFound();
            }

            if (userActivity.UserId != userId)
            {
                return Forbid();
            }

            userActivity.UserId = userActivityUpdateDTO.UserId;
            userActivity.ActivityName = userActivityUpdateDTO.ActivityName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserActivityExists(UserActivityId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(userActivity);
        }

        // POST: api/UserActivities
        [HttpPost]
        public async Task<ActionResult<UserActivityReadDTO>> PostUserActivity(UserActivityCreateDTO userActivityCreateDTO)
        {
            var userActivity = new UserActivity
            {
                UserId = userActivityCreateDTO.UserId,
                ActivityName = userActivityCreateDTO.ActivityName,
            };

            _context.UserActivity.Add(userActivity);
            await _context.SaveChangesAsync();

            var userActivityReadDTO = new UserActivityReadDTO
            {
                UserActivityId = userActivity.UserActivityId,
                UserId = userActivity.UserId,
                ActivityName = userActivity.ActivityName,
            };

            return CreatedAtAction(nameof(GetUserActivity), new { id = userActivity.UserId }, userActivityReadDTO);
        }

        // DELETE UserActivities
        [HttpDelete("{UserActivityid}", Name = "DeleteUserActivity")]
        public async Task<IActionResult> DeleteUserActivity(int UserActivityid)
        {
            var userActivity = await _context.UserActivity.FindAsync(UserActivityid);
            if (userActivity == null)
            {
                return NotFound();
            }

            _context.UserActivity.Remove(userActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserActivityExists(int UserActivityId)
        {
            return (_context.UserActivity?.Any(e => e.UserActivityId == UserActivityId)).GetValueOrDefault();
        }
    }
}

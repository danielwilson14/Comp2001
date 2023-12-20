using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comp2001.Data;
using Comp2001.Models;

namespace Comp2001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserPreferencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPreferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPreferences>>> GetUserPreferences()
        {
          if (_context.UserPreferences == null)
          {
              return NotFound();
          }
            return await _context.UserPreferences.ToListAsync();
        }

        // GET: api/UserPreferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPreferences>> GetUserPreferences(int id)
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

            return userPreferences;
        }

        // PUT: api/UserPreferences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPreferences(int id, UserPreferences userPreferences)
        {
            if (id != userPreferences.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userPreferences).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPreferencesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserPreferences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPreferences>> PostUserPreferences(UserPreferences userPreferences)
        {
          if (_context.UserPreferences == null)
          {
              return Problem("Entity set 'ApplicationDbContext.UserPreferences'  is null.");
          }
            _context.UserPreferences.Add(userPreferences);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPreferences", new { id = userPreferences.UserId }, userPreferences);
        }

        // DELETE: api/UserPreferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPreferences(int id)
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

            _context.UserPreferences.Remove(userPreferences);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPreferencesExists(int id)
        {
            return (_context.UserPreferences?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}

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
    // Controller for CRUD operations on 'Users' entities.
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor for dependency injection of the database context.
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET all Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET Users by specific ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // Update User
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(int userId, UserUpdateDTO userUpdateDTO)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = userUpdateDTO.FirstName;
            user.LastName = userUpdateDTO.LastName;
            user.Email = userUpdateDTO.Email;
            user.AboutMe = userUpdateDTO.AboutMe;
            user.Birthday = userUpdateDTO.Birthday;
            user.Password = userUpdateDTO.Password;
            user.Admin = userUpdateDTO.Admin;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        private bool UserExists(int userId)
        {
            return _context.Users.Any(e => e.UserId == userId);
        }


        // Create User
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> PostUser(UserCreateDTO userCreateDTO)
        {
            var user = new User
            {
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName,
                Email = userCreateDTO.Email,
                AboutMe = userCreateDTO.AboutMe,
                LocationID = userCreateDTO.LocationID,
                Birthday = userCreateDTO.Birthday,
                Password = userCreateDTO.Password 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userReadDTO = new UserReadDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AboutMe = user.AboutMe,
                LocationID = user.LocationID,
                Birthday = user.Birthday
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, userReadDTO);
        }



        // DELETE users
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

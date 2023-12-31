﻿using System;
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
using System.Security.Claims;

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
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
          
          var users = await _context.Users.Select(u => new UserReadDTO{
              UserId = u.UserId,
              FirstName = u.FirstName,
              LastName = u.LastName,
              Email = u.Email,
              AboutMe = u.AboutMe,
              LocationID = u.LocationID,
              Birthday = u.Birthday,
              Archived = u.Archived
            }).ToListAsync();
            return users;
            
        }

        // GET Users by specific ID
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserReadDTO>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserReadDTO
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    AboutMe = u.AboutMe,
                    LocationID = u.LocationID,
                    Birthday= u.Birthday,
                    Archived= u.Archived
                }).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            user.Links.Add(new LinkDto(Url.Link("GetUser", new { id = user.UserId }), "view user information", "GET"));
            user.Links.Add(new LinkDto(Url.Link("PutUser", new { userId = user.UserId }), "update user", "PUT"));
            user.Links.Add(new LinkDto(Url.Link("DeleteUser", new { id = user.UserId }), "delete user admin required", "DELETE"));

            return user;
        }

        // Update User
        [HttpPut("{userId}", Name = "PutUser")]
        public async Task<IActionResult> PutUser(int userId, UserUpdateDTO userUpdateDTO)
        {


            // Extract the email from the token
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (userEmail == null)
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Email != userEmail)
            {
                return Forbid();
            }

            user.FirstName = userUpdateDTO.FirstName;
            user.LastName = userUpdateDTO.LastName;
            user.Email = userUpdateDTO.Email;
            user.AboutMe = userUpdateDTO.AboutMe;
            user.LocationID = userUpdateDTO.LocationID;
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
        [HttpDelete("{id}", Name = "DeleteUser")]
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

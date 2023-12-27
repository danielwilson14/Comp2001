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


namespace Comp2001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocation()
        {
          if (_context.Location == null)
          {
              return NotFound();
          }
            return await _context.Location.ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
          if (_context.Location == null)
          {
              return NotFound();
          }
            var location = await _context.Location.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // Update location
        [HttpPut("{locationId}")]
        public async Task<IActionResult> PutLocation(int locationId, LocationUpdateDTO locationUpdateDTO)
        {
            var location = await _context.Location.FindAsync(locationId);
            if (location == null)
            {
                return NotFound();
            }

            location.City = locationUpdateDTO.City;
            location.Country = locationUpdateDTO.Country;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(locationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(location);
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<LocationReadDTO>> PostLocation(LocationCreateDTO locationCreateDTO)
        {
            var location = new Location
            {
                City = locationCreateDTO.City,
                Country = locationCreateDTO.Country
            };

            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            var locationReadDTO = new LocationReadDTO
            {
                LocationId = location.LocationId,
                City = location.City,
                Country = location.Country
            };

            return CreatedAtAction(nameof(GetLocation), new { id = location.LocationId }, locationReadDTO);
        }

        // DELETE locations
        [HttpDelete("{Locationid}")]
        public async Task<IActionResult> DeleteLocation(int LocationId)
        {
            var location = await _context.Location.FindAsync(LocationId);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int locationId)
        {
            return (_context.Location?.Any(e => e.LocationId == locationId)).GetValueOrDefault();
        }
    }
}

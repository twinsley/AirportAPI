using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportAPI.Entities;

namespace AirportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormdatumsController : ControllerBase
    {
        private readonly Wx1Context _context;

        public FormdatumsController(Wx1Context context)
        {
            _context = context;
        }

        // GET: api/Formdatums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formdatum>>> GetFormdata()
        {
          if (_context.Formdata == null)
          {
              return NotFound();
          }
            return await _context.Formdata.ToListAsync();
        }

        // GET: api/Formdatums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Formdatum>> GetFormdatum(int id)
        {
          if (_context.Formdata == null)
          {
              return NotFound();
          }
            var formdatum = await _context.Formdata.FindAsync(id);

            if (formdatum == null)
            {
                return NotFound();
            }

            return formdatum;
        }

        // PUT: api/Formdatums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormdatum(int id, Formdatum formdatum)
        {
            if (id != formdatum.Id)
            {
                return BadRequest();
            }

            _context.Entry(formdatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormdatumExists(id))
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

        // POST: api/Formdatums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Formdatum>> PostFormdatum(Formdatum formdatum)
        {
          if (_context.Formdata == null)
          {
              return Problem("Entity set 'Wx1Context.Formdata'  is null.");
          }
            _context.Formdata.Add(formdatum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormdatum", new { id = formdatum.Id }, formdatum);
        }

        // DELETE: api/Formdatums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormdatum(int id)
        {
            if (_context.Formdata == null)
            {
                return NotFound();
            }
            var formdatum = await _context.Formdata.FindAsync(id);
            if (formdatum == null)
            {
                return NotFound();
            }

            _context.Formdata.Remove(formdatum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormdatumExists(int id)
        {
            return (_context.Formdata?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportAPI.Entities;
using NuGet.Protocol;
using Microsoft.AspNetCore.Cors;
using AirportAPI.Areas.Identity.Data;

namespace AirportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WxController : ControllerBase
    {
        private readonly Wx1Context _context;

        public WxController(Wx1Context context)
        {
            _context = context;
        }

        // GET: api/Wx
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wxdatum>>> GetWxdata()
        {
          if (_context.Wxdata == null)
          {
              return NotFound();
          }
            return await _context.Wxdata.ToListAsync();
        }


        // GET: api/Wx/5
        [EnableCors]
        [HttpGet("{identifier}")]
        public async Task<ActionResult<IEnumerable<Wxdatum>>> GetWxdatum(string identifier)
        {
            if (_context.Wxdata == null)
            {
                return NotFound();
            }
            var wxdatum = await _context.Wxdata.Where(i => i.Identifier.Equals(identifier)).OrderByDescending(x => x.Id).Take(6).ToListAsync();

            if (wxdatum == null)
            {
                return NotFound();
            }

            return wxdatum;
        }

        // PUT: api/Wx/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWxdatum(long id, Wxdatum wxdatum)
        {
            if (id != wxdatum.Id)
            {
                return BadRequest();
            }

            _context.Entry(wxdatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WxdatumExists(id))
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

        // POST: api/Wx
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wxdatum>> PostWxdatum(Wxdatum wxdatum)
        {
          if (_context.Wxdata == null)
          {
              return Problem("Entity set 'Wx1Context.Wxdata'  is null.");
          }
            _context.Wxdata.Add(wxdatum);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("PostWxdatum", new { id = wxdatum.Id }, wxdatum);
            return Ok();
        }

        // DELETE: api/Wx/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWxdatum(long id)
        {
            if (_context.Wxdata == null)
            {
                return NotFound();
            }
            var wxdatum = await _context.Wxdata.FindAsync(id);
            if (wxdatum == null)
            {
                return NotFound();
            }

            _context.Wxdata.Remove(wxdatum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WxdatumExists(long id)
        {
            return (_context.Wxdata?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

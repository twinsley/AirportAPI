using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportAPI.Entities;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using NuGet.Protocol;

namespace AirportAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FormdataController : ControllerBase
    {
        private readonly Wx1Context _context;

        public FormdataController(Wx1Context context)
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
            String emailData = $"First name: {formdatum.FirstName} \nLast name: {formdatum.LastName} \nEmail: {formdatum.Email} \n Airport: {formdatum.Airport} \nComments: {formdatum.Comments}";
           // await SendEmail(emailData);

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

        static async Task SendEmail(String emailBody)
        {

            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            String SendgridApiKey = configuration.GetValue<String>("SendgridApiKey");
            String SenderEmail = configuration.GetValue<String>("SenderEmail");
            String RecipientEmail = configuration.GetValue<String>("EmailRecipient");

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Airport WX Site", SenderEmail));
            email.To.Add(new MailboxAddress("Tim Winsley", RecipientEmail));

            email.Subject = "New Airport Request Submitted";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = emailBody
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.sendgrid.net", 465, true);
                smtp.Authenticate("apikey", SendgridApiKey);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}

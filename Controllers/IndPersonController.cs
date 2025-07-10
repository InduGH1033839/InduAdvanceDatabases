using Hospital_Management_System_Indu.DbContextBoth;
using Hospital_Management_System_Indu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System_Indu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndPersonController : ControllerBase
    {

        private readonly DataConnect _context;

        public IndPersonController(DataConnect context)
        {
            _context = context;
        }


        // READ ALL
        [HttpGet("all")]
        public async Task<IActionResult> FetchAll()
        {
            var people = await _context.IndPerson.ToListAsync();
            return Ok(people);
        }

        // READ ONE
        [HttpGet("{id}")]
        public async Task<IActionResult> FetchById(int id)
        {
            var person = await _context.IndPerson.FindAsync(id);
            if (person == null) return NotFound("Individual not found.");
            return Ok(person);
        }

        // CREATE
        [HttpPost("create")]
        public async Task<IActionResult> CreateEntry(IndPerson person)
        {
            _context.IndPerson.Add(person);
            await _context.SaveChangesAsync();
            return Ok("New person entry created.");
        }

        // UPDATE
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> ModifyEntry(int id, IndPerson updatedPerson)
        {
            var original = await _context.IndPerson.FindAsync(id);
            if (original == null) return NotFound("Individual not found.");

            original.IndName = updatedPerson.IndName;
            original.Gender = updatedPerson.Gender;
            original.BirthDate = updatedPerson.BirthDate;
            original.MobileNo = updatedPerson.MobileNo;
            original.MailAddr = updatedPerson.MailAddr;

            await _context.SaveChangesAsync();
            return Ok("Details updated successfully.");
        }

        // DELETE
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveEntry(int id)
        {
            var record = await _context.IndPerson.FindAsync(id);
            if (record == null) return NotFound("Individual not found.");

            _context.IndPerson.Remove(record);
            await _context.SaveChangesAsync();
            return Ok("Record deleted successfully.");
        }
    }
}

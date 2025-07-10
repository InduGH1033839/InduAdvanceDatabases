using Hospital_Management_System_Indu.DbContextBoth;
using Hospital_Management_System_Indu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System_Indu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitNoteController : ControllerBase
    {

        private readonly DataConnect _context;

        public VisitNoteController(DataConnect context)
        {
            _context = context;
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetVisits(int id)
        {
            var visits = await _context.Visits
                .Where(v => v.IndID == id)
                .ToListAsync();
            return Ok(visits);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddVisit(VisitNote note)
        {
            _context.Visits.Add(note);
            await _context.SaveChangesAsync();
            return Ok("Visit saved");
        }


        // READ: Get single visit by VisitID
        [HttpGet("{visitId}")]
        public async Task<IActionResult> GetVisitById(int visitId)
        {
            var visit = await _context.Visits.FindAsync(visitId);
            if (visit == null) return NotFound("Visit record not found.");
            return Ok(visit);
        }

       

        // UPDATE: Edit existing visit
        [HttpPut("edit/{visitId}")]
        public async Task<IActionResult> EditVisit(int visitId, VisitNote updated)
        {
            var existing = await _context.Visits.FindAsync(visitId);
            if (existing == null) return NotFound("Visit not found.");

            existing.IndID = updated.IndID;
            existing.Consultant = updated.Consultant;
            existing.VisitDate = updated.VisitDate;
            existing.VisitSummary = updated.VisitSummary;

            await _context.SaveChangesAsync();
            return Ok("Visit record updated.");
        }

        // DELETE: Remove visit by VisitID
        [HttpDelete("remove/{visitId}")]
        public async Task<IActionResult> RemoveVisit(int visitId)
        {
            var record = await _context.Visits.FindAsync(visitId);
            if (record == null) return NotFound("Visit not found.");

            _context.Visits.Remove(record);
            await _context.SaveChangesAsync();
            return Ok("Visit deleted.");
        }

    }
}

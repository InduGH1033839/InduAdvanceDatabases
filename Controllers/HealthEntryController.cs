using Hospital_Management_System_Indu.DbContextBoth;
using Hospital_Management_System_Indu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Hospital_Management_System_Indu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthEntryController : ControllerBase
    {

        private readonly MongoLink _mongo;

        public HealthEntryController(MongoLink mongo)
        {
            _mongo = mongo;
        }

        [HttpPost("record")]
        public async Task<IActionResult> AddEntry(HealthEntry entry)
        {
            await _mongo.HealthEntries.InsertOneAsync(entry);
            return Ok("Entry saved");
        }

        [HttpGet("for/{indId}")]
        public async Task<IActionResult> GetEntries(int indId)
        {
            var entries = await _mongo.HealthEntries
                .Find(x => x.RefIndID == indId)
                .ToListAsync();
            return Ok(entries);
        }
    
        // READ ONE by ObjectId
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var objectId = new ObjectId(id);
            var entry = await _mongo.HealthEntries.Find(x => x.EntryID == objectId).FirstOrDefaultAsync();
            if (entry == null) return NotFound("Record not found.");
            return Ok(entry);
        }

        // UPDATE
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEntry(string id, HealthEntry updated)
        {
            var objectId = new ObjectId(id);
            var result = await _mongo.HealthEntries.ReplaceOneAsync(
                x => x.EntryID == objectId,
                updated
            );

            if (result.MatchedCount == 0)
                return NotFound("No matching record found.");

            return Ok("Health entry updated.");
        }

        // DELETE
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteEntry(string id)
        {
            var objectId = new ObjectId(id);
            var result = await _mongo.HealthEntries.DeleteOneAsync(x => x.EntryID == objectId);

            if (result.DeletedCount == 0)
                return NotFound("Entry not found.");

            return Ok("Health entry deleted.");
        }
    }
}

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
    public class LogRecordController : ControllerBase
    {
        private readonly MongoLink _mongo;

        public LogRecordController(MongoLink mongo)
        {
            _mongo = mongo;
        }

        // CREATE - Add a new log
        [HttpPost("write")]
        public async Task<IActionResult> WriteLog(LogRecord log)
        {
            await _mongo.Logs.InsertOneAsync(log);
            return Ok("Log added");
        }

        // READ - Latest 10 logs
        [HttpGet("latest")]
        public async Task<IActionResult> Latest()
        {
            var logs = await _mongo.Logs
                .Find(FilterDefinition<LogRecord>.Empty)
                .SortByDescending(l => l.ActionTime)
                .Limit(10)
                .ToListAsync();

            return Ok(logs);
        }

        // ✅ FIXED: READ - Single log by MongoDB ObjectId
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return BadRequest("Invalid MongoDB ObjectId format.");

            var log = await _mongo.Logs.Find(x => x.LogID == objectId).FirstOrDefaultAsync();
            if (log == null)
                return NotFound("Log not found.");

            return Ok(log);
        }

        // ✅ OPTIONAL: Get all logs for a patient by IndID (like 1, 2, etc.)
        [HttpGet("byPatient/{indId}")]
        public async Task<IActionResult> GetByPatientId(int indId)
        {
            var searchTerm = $"IndID (Patient) {indId}";
            var logs = await _mongo.Logs.Find(x => x.LogMessage.Contains(searchTerm)).ToListAsync();

            if (logs == null || logs.Count == 0)
                return NotFound($"No logs found for IndID {indId}.");

            return Ok(logs);
        }

        // UPDATE - Edit a log
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditLog(string id, LogRecord updated)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return BadRequest("Invalid MongoDB ObjectId format.");

            var result = await _mongo.Logs.ReplaceOneAsync(
                x => x.LogID == objectId,
                updated
            );

            if (result.MatchedCount == 0)
                return NotFound("No log found with given ID.");

            return Ok("Log updated successfully.");
        }

        // DELETE - Remove log by ID
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteLog(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return BadRequest("Invalid MongoDB ObjectId format.");

            var result = await _mongo.Logs.DeleteOneAsync(x => x.LogID == objectId);

            if (result.DeletedCount == 0)
                return NotFound("Log not found.");

            return Ok("Log removed.");
        }
    }
}

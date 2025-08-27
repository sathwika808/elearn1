using ELearnApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        ELearnDbContext db = null;
        public ReportController(ELearnDbContext c)
        {
            db = c; 
        }
        [HttpGet]

        public IActionResult GetReport()
        {
            var reports = db.Reports.ToList();
            if (reports == null)
            {
                return NotFound("No Feedbacks");
            }
            return Ok(reports);
        }



        [HttpGet("{id}")]

        public IActionResult GetReportById(int id)
        {
            var reports = db.Reports.Find(id);
            if (reports == null)
            {
                return NotFound($"Feedback with {id} is not found");
            }
            return Ok(reports);
        }



        [HttpPost]

        public IActionResult PostReport(Reports Postedreport)
        {
            if (Postedreport == null)
            {
                return BadRequest("Post a valid feedback");
            }
            db.Reports.Add(Postedreport);
            db.SaveChanges();
            return Ok("Posted Successfully");
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteReport(int id)
        {
            var ReportToBeDeleted = db.Reports.Find(id);
            if (ReportToBeDeleted == null)
            {
                return NotFound($"report with id {id} is not found");
            }
            db.Reports.Remove(ReportToBeDeleted);
            db.SaveChanges();
            return Ok("deleted Successfully");
        }
    }
}

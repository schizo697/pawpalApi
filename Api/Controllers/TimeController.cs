using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TimeController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("dbconnection")]
        public async Task<IActionResult> TestDbConnection()
        {
            try
            {
                var canConnect = await context.Database.CanConnectAsync();
                return Ok(new
                {
                    Success = canConnect,
                    Message = canConnect ? "Database connection successful" : "Could not connect to database"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Database connection failed",
                    Error = ex.Message,
                    Detailed = ex.ToString()
                });
            }
        }

        //Get all Times
        [HttpGet]
        public IActionResult GetTimes()
        {
            var times = context.Times.OrderByDescending(t => t.Id).ToList();

            if (times == null)
            {
                return NotFound();
            }

            return Ok(times);
        }

        //Get Time
        [HttpGet("{id}")]
        public IActionResult GetTime(int id)
        {
            var tm = context.Times.Find(id);
            if (tm == null)
            {
                return NotFound();
            }

            return Ok(tm);
        }

        //Create Time
        [HttpPost]
        public IActionResult CreateTime(TimeDto timeDto)
        {
            var tm = new Time
            {
                FeedingTime = timeDto.FeedingTime
            };

            context.Times.Add(tm);
            context.SaveChanges();

            return Ok(tm);
        }

        //Update Time
        [HttpPut("{id}")]
        public IActionResult EditTime(int id, TimeDto timeDto)
        {
            var tm = context.Times.Find(id);
            if (tm == null)
            {
                return NotFound();
            }

            tm.FeedingTime = timeDto.FeedingTime;

            context.SaveChanges();

            return Ok(tm);
        }

        //Dete Time
        [HttpDelete("{id}")]
        public IActionResult DeleteTime(int id)
        {
            var tm = context.Times.Find(id);
            if (tm == null)
            {
                return NotFound();
            }

            context.Times.Remove(tm);
            context.SaveChanges();

            return Ok(tm);
        }

    }
}

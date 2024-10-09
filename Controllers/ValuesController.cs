using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todelete.Controllers
{
    [Authorize]

    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly Random _rng = new Random();

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                TemperatureC = _rng.Next(-20, 55),
                Summary = Summaries[_rng.Next(Summaries.Length)]
            }).ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> GetById(int id)
        {
            if (id < 1 || id > 5)
            {
                return NotFound();
            }
            return new WeatherForecast
            {
                TemperatureC = _rng.Next(-20, 55),
                Summary = Summaries[_rng.Next(Summaries.Length)]
            };
        }

        [HttpPost]
        [Authorize("api1.write")]
        public IActionResult Post([FromBody] WeatherForecast weatherForecast)
        {
            // Save to database
            return Ok();
        }

        [HttpPut]
        [Authorize("api1.write")]
        public IActionResult Put([FromBody] WeatherForecast weatherForecast)
        {
            // Update in database
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize("api1.write")]
        public IActionResult Delete(int id)
        {
            // Delete from database
            return Ok();
        }
    }

    public class WeatherForecast
    {
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}

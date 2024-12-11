using Api.Database;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("v1")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MoviesDbContext _context;
        public MovieController(MoviesDbContext context)
        {
                _context = context;
        }

        [Route("worst-movies")]
        [HttpGet]
        public async Task<IActionResult> GetMovies() 
        {
            var producerPrizes = await _context.Movies
                      .Where(m => m.Winner == "yes")
                      .OrderBy(m => m.Producers)
                      .ThenBy(m => m.Year)
                      .GroupBy(m => m.Producers)
                      .Select(g => new
                      {
                          producer = g.Key,
                          prizes = g.ToList()
                      })
                      .ToListAsync();

            var intervals = producerPrizes.SelectMany(g =>
            {
                var intervalsForProducer = new List<ProducerInterval>();
                for (int i = 1; i < g.prizes.Count; i++)
                {
                    var previousWin = g.prizes[i - 1];
                    var followingWin = g.prizes[i];
                    var interval = followingWin.Year - previousWin.Year;

                    intervalsForProducer.Add(new ProducerInterval(g.producer, interval, previousWin.Year, followingWin.Year));
                }
                return intervalsForProducer;
            }).ToList();

            var minInterval = intervals.MinBy(i => i.Interval);
            var maxInterval = intervals.MaxBy(i => i.Interval);

            return Ok(new ProducerIntervalResponse(min: minInterval is not null ? [minInterval] : [], max: maxInterval is not null ? [maxInterval] : []));
        }
    }
}

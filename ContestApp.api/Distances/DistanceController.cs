using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ContestApp.api.Distances
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly ContestAppDbContext _context;

        public DistanceController(ContestAppDbContext context)
        {
            _context = context;
        }
        

        // POST: api/Distance
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Distance>> PostDistance(CreateDistanceDto createDistanceDto)
        {
            var distance = createDistanceDto.CreateDistanceDtoToDistance();

            _context.Distances.Add(distance);

            await _context.SaveChangesAsync();

            return Created("Created Distance", distance);
        }

        
    }
}

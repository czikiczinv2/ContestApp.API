using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace ContestApp.api.Players
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ContestAppDbContext _context;

        public PlayerController(ContestAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlayerDto>>> GetPlayers()
        {
            var playersWithDistances = await _context.Players.Include(d => d.Distances)
                .ToListAsync();

            return playersWithDistances.Select(x => x.PlayerToGetPlayerDto()).ToList();
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPlayer(Guid id, EditPlayerDto editPlayerDto)
        {
            if (id != editPlayerDto.Id)
            {
                return BadRequest("Ids do not match each other");
            }

            var player = await _context.Players.Where(t => t.Id == id).FirstOrDefaultAsync();

            if (player == null)
            {
                return NotFound();
            }

            editPlayerDto.EditPlayerDtoToPlayer(player);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(CreatePlayerDto createPlayerDto)
        {
            var player = createPlayerDto.CreatePlayerDtoToPlayer();

            player.Distances.AddRange(_context.Distances.Where(x =>
                createPlayerDto.DistanceIds.Contains(x.Id)));

            _context.Players.Add(player);

            await _context.SaveChangesAsync();

            return Created("Created Player", player);
        }

        
    }
}
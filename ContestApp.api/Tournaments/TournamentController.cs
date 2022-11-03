using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace ContestApp.api.Tournaments
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ContestAppDbContext _context;

        public TournamentController(ContestAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tournament
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTournamentDto>>> GetTournaments()
        {
            var tournamentsWithPlayers = await _context.Tournaments.Include(p => p.Players)
                .ToListAsync();
            
            return tournamentsWithPlayers.Select(x => x.TournamentToGetTournamentDto()).ToList();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTournamentDto>> GetTournament(Guid id)
        {
            var tournament = await _context.Tournaments
                .Where(t => t.Id == id)
                .Include(p => p.Players)
                .FirstOrDefaultAsync();
            
            if (tournament == null)
            {
                return NotFound();
            }
            
            return tournament.TournamentToGetTournamentDto();
        }

        // PUT: api/Tournament/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTournament(Guid id, EditTournamentDto editTournamentDto)
        {
            if (id != editTournamentDto.Id)
            {
                return BadRequest("Ids do not match each other");
            }

            var tournament = await _context.Tournaments.Where(t => t.Id == id).FirstOrDefaultAsync();

            if (tournament == null)
            {
                return NotFound();
            }

            editTournamentDto.EditTournamentDtoToTournament(tournament);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Tournament
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(CreateTournamentDto createTournamentDto)
        {
            var tournament = createTournamentDto.CreateTournamentDtoToTournament();

            tournament.Players.AddRange(_context.Players.Where(x =>
                createTournamentDto.PlayerIds.Contains(x.Id)));

            _context.Tournaments.Add(tournament);
            
            await _context.SaveChangesAsync();

            return Created("Created Tournament", tournament);
        }

    }
}

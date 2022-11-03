

namespace ContestApp.api.Tournaments;

public class GetTournamentDto : BaseTournamentDto
{
    public Guid Id { get; set; }
    public IList<Guid> PlayerIds { get; set; }
}
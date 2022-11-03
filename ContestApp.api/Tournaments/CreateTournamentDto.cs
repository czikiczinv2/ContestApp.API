namespace ContestApp.api.Tournaments;

public class CreateTournamentDto : BaseTournamentDto
{
    public IList<Guid> PlayerIds { get; set; }
}
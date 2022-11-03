namespace ContestApp.api.Players;

public class GetPlayerDto : BasePlayerDto
{
    public Guid Id { get; set; }

    public int StartNumber { get; set; }

    public bool IsDisqualified { get; set; }

    public IList<Guid> DistanceIds { get; set; }
    
    public IList<int> DistancePenaltyPoints { get; set; }

    public Guid TournamentId { get; set; }
}
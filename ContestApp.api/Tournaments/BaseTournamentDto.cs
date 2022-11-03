namespace ContestApp.api.Tournaments;

public class BaseTournamentDto
{
    public string Name { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public TournamentStatus Status { get; set; }
    
    public int DistancesAmount { get; set; }
}
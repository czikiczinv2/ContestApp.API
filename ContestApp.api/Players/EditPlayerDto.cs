namespace ContestApp.api.Players;

public class EditPlayerDto
{
    public Guid Id { get; set; }
    
    public bool IsDisqualified { get; set; }
}
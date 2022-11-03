namespace ContestApp.api.Players;

public abstract class BasePlayerDto
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public Group Group { get; set; }
}
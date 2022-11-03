using System.ComponentModel.DataAnnotations;

namespace ContestApp.api.Players;

public class CreatePlayerDto : BasePlayerDto
{
    public int StartNumber { get; set; }
    
    [Required]
    public Guid TournamentId { get; set; }

    public IList<Guid> DistanceIds { get; set; }
}
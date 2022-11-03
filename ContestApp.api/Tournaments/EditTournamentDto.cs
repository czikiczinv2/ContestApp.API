using System.ComponentModel.DataAnnotations;

namespace ContestApp.api.Tournaments;

public class EditTournamentDto
{
    public Guid Id { get; set; }
    
    [Required]
    public TournamentStatus Status { get; set; }
}
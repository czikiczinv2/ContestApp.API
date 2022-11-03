using System.ComponentModel.DataAnnotations;

namespace ContestApp.api.Distances;

public class CreateDistanceDto
{
    [Required]
    public int PenaltyPoints { get; set; }
    
    [Required]
    public Guid PlayerId { get; set; }
}
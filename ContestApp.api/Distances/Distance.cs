using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContestApp.api.Distances;

public class Distance
{
    public Guid Id { get; set; }

    [Required]
    public int PenaltyPoints { get; set; }
    
    [Required]
    [ForeignKey(nameof(PlayerId))]
    public Guid PlayerId { get; set; }
    
}
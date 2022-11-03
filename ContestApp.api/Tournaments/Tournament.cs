using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContestApp.api.Players;

namespace ContestApp.api.Tournaments;

public class Tournament
{
    public Guid Id { get; set; }
    
    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(50, MinimumLength = 5)]
    public string Name { get; set; }
    
    [Required]
    public DateTime CreationDate { get; set; }
    
    [Required]
    public TournamentStatus Status { get; set; }
    
    [Required]
    [Range(5,10, ErrorMessage = "Please enter a value bigger than 5 and fewer than 10")]
    public int DistancesAmount { get; set; }

    [Required]
    public IList<Player> Players { get; set; }
}
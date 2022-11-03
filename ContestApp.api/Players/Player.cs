using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContestApp.api.Distances;

namespace ContestApp.api.Players;

public class Player
{
    private static int _startNumber;
    public Guid Id { get; set; }
    
    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(20, MinimumLength = 2)]
    public string FirstName { get; set; }
    
    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(40, MinimumLength = 2)]
    public string LastName { get; set; }

    [Required]
    public int StartNumber { get; set; }

    [Required]
    public IList<Distance> Distances { get; set; }

    [Required]
    public Group Group { get; set; }

    [Required]
    [ForeignKey(nameof(TournamentId))]
    public Guid TournamentId { get; set; }

    public bool IsDisqualified { get; set; } = false;

}
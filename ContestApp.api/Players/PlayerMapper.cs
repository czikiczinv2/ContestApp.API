namespace ContestApp.api.Players;

public static class PlayerMapper
{

    public static GetPlayerDto PlayerToGetPlayerDto (this Player player)
    {
        return new GetPlayerDto()
        {
            Id = player.Id,
            FirstName = player.FirstName,
            LastName = player.LastName,
            Group = player.Group,
            IsDisqualified = player.IsDisqualified,
            TournamentId = player.TournamentId,
            StartNumber = player.StartNumber,
            DistanceIds = player.Distances.Select(d => d.Id).ToList(),
            DistancePenaltyPoints = player.Distances.Select(d => d.PenaltyPoints).ToList()
        };
    }
    
    public static Player CreatePlayerDtoToPlayer (this CreatePlayerDto createPlayerDto)
    {
        return new Player()
        {
            FirstName = createPlayerDto.FirstName,
            LastName = createPlayerDto.LastName,
            Group = createPlayerDto.Group,
            StartNumber = createPlayerDto.StartNumber,
            TournamentId = createPlayerDto.TournamentId,
        };
    }
    
    public static void EditPlayerDtoToPlayer(this EditPlayerDto editPlayerDto, Player player)
    {
        player.IsDisqualified = editPlayerDto.IsDisqualified;
    }
}
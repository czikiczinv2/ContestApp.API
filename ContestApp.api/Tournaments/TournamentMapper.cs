namespace ContestApp.api.Tournaments;

public static class TournamentMapper
{
    public static GetTournamentDto TournamentToGetTournamentDto (this Tournament tournament)
    {
        return new GetTournamentDto()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            DistancesAmount = tournament.DistancesAmount,
            Status = tournament.Status,
            CreationDate = tournament.CreationDate,
            PlayerIds = tournament.Players.Select(p => p.Id).ToList()
        };
    }
    
    public static Tournament CreateTournamentDtoToTournament (this CreateTournamentDto createTournamentDto)
    {
        return new Tournament()
        {
            Name = createTournamentDto.Name,
            DistancesAmount = createTournamentDto.DistancesAmount,
            Status = createTournamentDto.Status,
            CreationDate = createTournamentDto.CreationDate,
        };
    }

    public static void EditTournamentDtoToTournament(this EditTournamentDto editTournamentDto, Tournament tournament)
    {
        tournament.Status = editTournamentDto.Status;
    }
}
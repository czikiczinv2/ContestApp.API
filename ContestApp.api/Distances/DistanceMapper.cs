namespace ContestApp.api.Distances;

public static class DistanceMapper
{
    public static Distance CreateDistanceDtoToDistance (this CreateDistanceDto createDistanceDto)
    {
        return new Distance()
        {
            PenaltyPoints = createDistanceDto.PenaltyPoints,
            PlayerId = createDistanceDto.PlayerId
        };
    }
}
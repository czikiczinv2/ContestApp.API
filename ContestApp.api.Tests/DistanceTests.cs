using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContestApp.api.Distances;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContestApp.api.Tests;

public class DistanceTests : IClassFixture<AppFactory<Program>>
{
    private readonly HttpClient _client;

    public DistanceTests(AppFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task PostDistanceShouldReturnCreatedStatusCode()
    {
        HttpResponseMessage response = 
            await _client.PostAsJsonAsync(new HttpRequestMessage(HttpMethod.Post, "/api/Distance").RequestUri,
                CreateInMemoryDistanceDto());
        
        var responseStatusCode = response.StatusCode.ToString();

        Assert.Contains("Created", responseStatusCode);
    }

    private static CreateDistanceDto CreateInMemoryDistanceDto()
    {
        var context = GetInMemoryDatabaseContext();

        var inMemoryDistanceDto = new CreateDistanceDto()
        {
            PenaltyPoints = 3,
            PlayerId = new Guid("30b9d125-c7b8-4917-9a5e-5a943f1d42cd")
        };
        
        context.Database.EnsureDeleted();
        
        return inMemoryDistanceDto;
    }
    
    private static ContestAppDbContext GetInMemoryDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ContestAppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var databaseContext = new ContestAppDbContext(options);

        return databaseContext;
    }
}
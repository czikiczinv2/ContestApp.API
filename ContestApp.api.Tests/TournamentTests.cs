using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContestApp.api.Tournaments;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContestApp.api.Tests;

public class TournamentTests : IClassFixture<AppFactory<Program>>
{
    private readonly HttpClient _client;
    
    public TournamentTests(AppFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task PostTournamentShouldReturnCreatedStatusCode()
    {
        HttpResponseMessage response = 
            await _client.PostAsJsonAsync(new HttpRequestMessage(HttpMethod.Post, "/api/Tournament").RequestUri,
                CreateInMemoryTournamentDto());
        
        var responseStatusCode = response.StatusCode.ToString();

        Assert.Contains("Created", responseStatusCode);
    }
    
    [Fact]
    public async Task GetTournamentsShouldReturnTournamentsWithTheirNamesAndIds()
    {
        HttpResponseMessage response = 
            await _client.GetAsync(new HttpRequestMessage(HttpMethod.Get, $"/api/Tournament").RequestUri);
        
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Contains("9381fa39-22c3-4a1f-b707-21d1854d5dc2", responseContent);
        Assert.Contains("356726fe-ebdf-4af9-9c76-6f54fc764577", responseContent);
        Assert.Contains("Chicago Motorcycle Contest", responseContent);
        Assert.Contains("Warsaw Motorcycle Contest", responseContent);
    }
    
    [Fact]
    public async Task GetTournamentsShouldReturnTournamentsWithTheirPlayers()
    {
        HttpResponseMessage response = 
            await _client.GetAsync(new HttpRequestMessage(HttpMethod.Get, $"/api/Tournament").RequestUri);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        
        Assert.Contains("1265809a-e7c0-4c1c-9637-255971c4b4f2", responseContent);
        Assert.Contains("30b9d125-c7b8-4917-9a5e-5a943f1d42cd", responseContent);
    }
    [Fact]
    public async Task GetTournamentShouldReturnATournamentWithItsNameAndId()
    {
        HttpResponseMessage response = 
            await _client.GetAsync(new HttpRequestMessage(HttpMethod.Get, $"/api/Tournament/9381fa39-22c3-4a1f-b707-21d1854d5dc2").RequestUri);
        
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Contains("9381fa39-22c3-4a1f-b707-21d1854d5dc2", responseContent);
        Assert.Contains("Chicago Motorcycle Contest", responseContent);
    }
    
    [Fact]
    public async Task PutTournamentShouldReturnNoContent()
    {    
        var putRequest = await _client
            .PutAsJsonAsync("/api/Tournament/356726fe-ebdf-4af9-9c76-6f54fc764577", EditInMemoryTournamentDto());

        var responseStatusCodeString = putRequest.StatusCode.ToString();
        Assert.Contains("NoContent", responseStatusCodeString);
    }
    
    [Fact]
    public async Task PutTournamentShouldReturnUpdatedTournamentStatus()
    
    {    var putRequest = new HttpRequestMessage(HttpMethod.Put, "/api/Tournament/356726fe-ebdf-4af9-9c76-6f54fc764577");
        
        await _client.PutAsJsonAsync(putRequest.RequestUri, EditInMemoryTournamentDto());

        var response = await _client.GetAsync("/api/Tournament");
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains(":3", responseContent);
    }
    
    private static CreateTournamentDto CreateInMemoryTournamentDto()
    {
        var context = GetInMemoryDatabaseContext();

        var inMemoryTournamentDto = new CreateTournamentDto()
        {
            Name = "Warsaw Motorcycle Contest",
            Status = TournamentStatus.Finished,
            DistancesAmount = 7,
            CreationDate = DateTime.Now,
            PlayerIds = new List<Guid>()
        };
        
        context.Database.EnsureDeleted();
        
        return inMemoryTournamentDto;
    }
    
    private static EditTournamentDto EditInMemoryTournamentDto()
    {
        var context = GetInMemoryDatabaseContext();

        var inMemoryTournamentDto = new EditTournamentDto()
        {
            Id = new Guid("356726fe-ebdf-4af9-9c76-6f54fc764577"),
            Status = TournamentStatus.Finished,
        };
        
        context.Database.EnsureDeleted();
        
        return inMemoryTournamentDto;
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
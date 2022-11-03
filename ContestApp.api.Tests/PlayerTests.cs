using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContestApp.api.Players;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContestApp.api.Tests;

public class PlayerTests : IClassFixture<AppFactory<Program>>
{
    private readonly HttpClient _client;

    public PlayerTests(AppFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostPlayerShouldReturnCreatedStatusCode()
    {
        HttpResponseMessage response = 
            await _client.PostAsJsonAsync(new HttpRequestMessage(HttpMethod.Post, "/api/Player").RequestUri,
                CreateInMemoryPlayerDto());
        
        var responseStatusCode = response.StatusCode.ToString();

        Assert.Contains("Created", responseStatusCode);
    }
    
    [Fact]
    public async Task GetPlayersShouldReturnPlayersWithTheirNamesAndIds()
    {
        HttpResponseMessage response = 
            await _client.GetAsync(new HttpRequestMessage(HttpMethod.Get, $"/api/Player").RequestUri);
        
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Contains("1265809a-e7c0-4c1c-9637-255971c4b4f2", responseContent);
        Assert.Contains("30b9d125-c7b8-4917-9a5e-5a943f1d42cd", responseContent);
        Assert.Contains("Johny", responseContent);
        Assert.Contains("Smith", responseContent);
        Assert.Contains("Andrew", responseContent);
        Assert.Contains("Washington", responseContent);
    }
    
    [Fact]
    public async Task GetPlayersShouldReturnPlayersWithTheirTournamentId()
    {
        HttpResponseMessage response = 
            await _client.GetAsync(new HttpRequestMessage(HttpMethod.Get, $"/api/Player").RequestUri);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        
        Assert.Contains("9381fa39-22c3-4a1f-b707-21d1854d5dc2", responseContent);
    }
    
    [Fact]
    public async Task GetPlayersShouldReturnPlayersWithTheirDistances()
    {
        HttpResponseMessage response = 
            await _client.GetAsync(new HttpRequestMessage(HttpMethod.Get, $"/api/Player").RequestUri);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        
        Assert.Contains("b9a19092-25cb-47f9-9fe8-dfba539a1e69", responseContent);
        Assert.Contains("aada50ea-bc73-48db-8c2c-f732eb0f6d5d", responseContent);
    }
    
    [Fact]
    public async Task PutPlayerShouldReturnNoContent()
    {    
        var putRequest = await _client
            .PutAsJsonAsync("/api/Player/1265809a-e7c0-4c1c-9637-255971c4b4f2", EditInMemoryPlayerDto());

        var responseStatusCodeString = putRequest.StatusCode.ToString();
        Assert.Contains("NoContent", responseStatusCodeString);
    }
    
    [Fact]
    public async Task PutPlayerShouldReturnDisqualifiedPlayer()
    
    {    var putRequest = new HttpRequestMessage(HttpMethod.Put, "/api/Player/1265809a-e7c0-4c1c-9637-255971c4b4f2");
        
        await _client.PutAsJsonAsync(putRequest.RequestUri, EditInMemoryPlayerDto());

        var response = await _client.GetAsync("/api/Player");
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains(":true", responseContent);
    }
    
    
    private static CreatePlayerDto CreateInMemoryPlayerDto()
    {
        var context = GetInMemoryDatabaseContext();

        var inMemoryPlayerDto = new CreatePlayerDto()
        {
            FirstName = "Johny",
            LastName = "Smith",
            Group = Group.B,
            TournamentId = new Guid("9381fa39-22c3-4a1f-b707-21d1854d5dc2"),
            DistanceIds = new List<Guid>()
        };
        
        context.Database.EnsureDeleted();
        
        return inMemoryPlayerDto;
    }
    
    private static EditPlayerDto EditInMemoryPlayerDto()
    {
        var context = GetInMemoryDatabaseContext();

        var inMemoryTournamentDto = new EditPlayerDto()
        {
            Id = new Guid("1265809a-e7c0-4c1c-9637-255971c4b4f2"),
            IsDisqualified = true
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
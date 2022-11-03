using System;
using System.Collections.Generic;
using System.Linq;
using ContestApp.api.Distances;
using ContestApp.api.Players;
using ContestApp.api.Tournaments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContestApp.api.Tests;

public class AppFactory<T> : WebApplicationFactory<Program> where T : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ContestAppDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ContestAppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryContestAppDb");
            });

            var serviceProvider = services.BuildServiceProvider();
            
            var scope = serviceProvider.CreateScope();
            
            var appContext = scope.ServiceProvider.GetRequiredService<ContestAppDbContext>();
            
            try
            {
                appContext.Database.EnsureCreated();
            }
            catch (Exception exception)
            {
                throw;
            }

            appContext.Add(new Tournament()
            {
                Id = new Guid("9381fa39-22c3-4a1f-b707-21d1854d5dc2"),
                Name = "Chicago Motorcycle Contest",
                Status = TournamentStatus.Active,
                DistancesAmount = 9,
                CreationDate = DateTime.Now
            });
            
            appContext.Add(new Tournament()
            {
                Id = new Guid("356726fe-ebdf-4af9-9c76-6f54fc764577"),
                Name = "Warsaw Motorcycle Contest",
                Status = TournamentStatus.Active,
                DistancesAmount = 7,
                CreationDate = DateTime.Now
            });

            appContext.Add(new Player()
            {
                Id = new Guid("1265809a-e7c0-4c1c-9637-255971c4b4f2"),
                FirstName = "Johny",
                LastName = "Smith",
                Group = Group.B,
                IsDisqualified = false,
                TournamentId = new Guid("9381fa39-22c3-4a1f-b707-21d1854d5dc2")
            }); 
            appContext.Add(new Player()
            {
                Id = new Guid("30b9d125-c7b8-4917-9a5e-5a943f1d42cd"),
                FirstName = "Andrew",
                LastName = "Washington",
                Group = Group.B,
                IsDisqualified = false,
                TournamentId = new Guid("9381fa39-22c3-4a1f-b707-21d1854d5dc2")
            });

            appContext.Add(new Distance()
            {  
                Id=new Guid("b9a19092-25cb-47f9-9fe8-dfba539a1e69"),
                PenaltyPoints = 3,
                PlayerId = new Guid("1265809a-e7c0-4c1c-9637-255971c4b4f2")
            });
            appContext.Add(new Distance()
            {  
                Id = new Guid("aada50ea-bc73-48db-8c2c-f732eb0f6d5d"),
                PenaltyPoints = 4,
                PlayerId = new Guid("1265809a-e7c0-4c1c-9637-255971c4b4f2")
            });
            
            appContext.SaveChanges();
        });


    }
}
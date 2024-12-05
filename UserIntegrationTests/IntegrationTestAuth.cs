
using Core;
using Core.ViewModels;
using Data;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UserModule;

namespace UserIntegrationTests
{
    public class IntegrationTestAuth : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        //  private readonly ApplicationDbContext _context;
        //private readonly HttpClient _client;
        public IntegrationTestAuth(WebApplicationFactory<Program> factoryServer)
        {
            //  _factory = factoryServer;
            //  var scope = _factory.Services.CreateScope();
            //   _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _factory = factoryServer.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing ApplicationDbContext registration
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }



                    // Add ApplicationDbContext with InMemoryDatabase
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryTestDb");
                    });
                });
            });

        }

        [Fact]
        public async Task Register_shouldAddUserSuccessfullyAndReturnOk()
        {
            // Arrange
            var systemId = Guid.Parse("0062ce9c-c0ed-4769-97c0-be77c52275bb");
            var _client = _factory.CreateClient();

            RegisterViewModel registerModel = new RegisterViewModel()
            {
                Email = "test2@test.com",
                Fname = "Azza",
                Lname = "Mohamed",
                Password = "Password@12345",
                Mobile = "01234567890",
                Address = "6th October",
                SystemId = systemId
            };

            // Act
            var response = await _client.PostAsJsonAsync("https://localhost:7202/api/User/register", registerModel);

            // Assert HTTP Status Code
            Assert.True(response.IsSuccessStatusCode, "API call failed. HTTP Status Code: " + response.StatusCode);


            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();


            Assert.NotNull(apiResponse);
            Assert.Equal(CustomStatusCode.Success, apiResponse.StatusCode);


            Assert.Contains("Registration successful", apiResponse.Message);
        }



        [Fact]
        public async Task Login_Should_Return_Ok_For_Authenticated_User()
        {
            //Arrange 
            var _client = _factory.CreateClient();
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


            var systemId = Guid.Parse("0062ce9c-c0ed-4769-97c0-be77c52275bb");
            //dbContext.Users.Add(new User
            //{
            //    Email = "test@test.com",
            //    PasswordHash = "Password@12345"

            //});

            //await dbContext.SaveChangesAsync();

            var loginModel = new LoginViewModel
            {
                Email = "test@test.com",
                Password = "Password@12345"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(loginModel),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7202/api/User/Login", content);
            response.EnsureSuccessStatusCode();


            var responseString = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<TokenResponse>(responseString);

            Assert.NotNull(token);

            // //to remove only the user i created for test register or i can use inmemory database 
            //    var testUsers = _context.Users.Where(u => u.Email.StartsWith("test"));
            // _context.Users.RemoveRange(testUsers);

            //// _context.Users.RemoveRange(_context.Users);
            //  _context.SaveChanges();

        }

        [Fact]
        public async Task Login_Should_Return_401_For_Authenticated_User()
        {
            //Arrange 
            var _client = _factory.CreateClient();
            LoginViewModel loginModel = new LoginViewModel()
            {
                Email = "test",
                Password = "Password"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(loginModel),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7202/api/User/Login", content);

            var result = response.IsSuccessStatusCode;
            Assert.False(result);

        }
    }
}
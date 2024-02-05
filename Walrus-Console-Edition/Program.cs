using System;
using System.Threading.Tasks;
using Walrus_Class_Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Walrus_Console_Edition
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Setup configuration and dependency injection
            var host = CreateHostBuilder(args).Build();

            // Retrieve the Authentication service
            var authService = host.Services.GetRequiredService<Authentication>();

            // Example usage of RegisterUser and LoginUser
            string email = "user@example.com";
            string password = "user_password";

            // Register User
            var registerResponse = await authService.RegisterUser(email, password);
            Console.WriteLine(registerResponse.IsSuccessStatusCode
                ? "User registered successfully!"
                : "User registration failed!");

            // Login User
            var loginResponse = await authService.LoginUser(email, password);
            Console.WriteLine(loginResponse.IsSuccessStatusCode
                ? "User logged in successfully!"
                : "User login failed!");
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.AddUserSecrets<Program>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddSingleton<Authentication>(provider =>
                        new Authentication(configuration));
                });
    }
}
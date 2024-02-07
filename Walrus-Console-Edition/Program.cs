using Walrus_Class_Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Walrus_Class_Library.Input;
using Walrus_Class_Library.Repository;
using Firebase.Database;
using Grpc.Core;
using Walrus_Class_Library.Models;

namespace Walrus_Console_Edition
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var authService = host.Services.GetRequiredService<Authentication>();
            var initialiseFirebase = new InitialiseFirebase();
            var firebaseClient = initialiseFirebase.Initialise();
            var firebaseIntegration = host.Services.GetRequiredService<FirebaseIntegration>();
            var userInput = new UserInput();
            Console.WriteLine("Welcome to the Walrus Console Edition!");
            int option = 0;
            while (option != 1 && option != 2)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                option = userInput.Options("Enter 1 or 2:");
            }

            string email = "", password = "";
            bool success = false;

            if (option == 1)
            {
                while (!success)
                {
                    email = userInput.GetUserName("Enter email:");
                    password = userInput.GetPassword("Enter password:");
                    var response = await authService.RegisterUser(email, password);
                    success = response.IsSuccessStatusCode;
                    Console.WriteLine(success ? "Registered!" : "Try again.");
                }
            }

            AuthenticateUserViewModel dataModel = new AuthenticateUserViewModel();
            success = false; 

            while (!success)
            {
                if (option == 2) 
                {
                    email = userInput.GetUserName("Enter email:");
                    password = userInput.GetPassword("Enter password:");
                }
                dataModel = await authService.LoginUser(email, password);
                success = dataModel.StatusCode == System.Net.HttpStatusCode.OK;
                Console.WriteLine(success ? "Logged in!" : "Try again.");
            }
            Console.ReadLine();

            await firebaseIntegration.UpdateUserToOffline(firebaseClient, dataModel.User);
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((configuration) =>
                {
                    configuration.AddUserSecrets<Program>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    services.AddSingleton(_ =>
                    {
                        var initialiseFirebase = new InitialiseFirebase();
                        return initialiseFirebase; 
                    });
                    services.AddSingleton<FirebaseIntegration>();

                    services.AddSingleton<Authentication>(_ =>
                    {
                        var initialiseFirebase = new InitialiseFirebase();
                        var firebaseClient = initialiseFirebase.Initialise();
                        var firebaseIntegration = new FirebaseIntegration();
                        return new Authentication(configuration, firebaseClient, firebaseIntegration);
                    });

                });
    }
}
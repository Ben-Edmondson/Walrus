using Walrus_Class_Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Walrus_Class_Library.Input;
using Walrus_Class_Library.Repository;

namespace Walrus_Console_Edition
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var authService = host.Services.GetRequiredService<Authentication>();
            var initFirebase = host.Services.GetRequiredService<InitialiseFirebase>();
            var writeToDb = host.Services.GetRequiredService<WriteToDb>();
            var userInput = new UserInput();

            var firebaseClient = initFirebase.Initialise();
            await writeToDb.WriteData(firebaseClient);

            string email = userInput.GetUserName("Please enter a valid email.");
            string password = userInput.GetPassword("Please enter a valid password.");

            var registerResponse = await authService.RegisterUser(email, password);
            Console.WriteLine(registerResponse.IsSuccessStatusCode
                ? "User registered successfully!"
                : "User registration failed!");

            var loginResponse = await authService.LoginUser(email, password);
            Console.WriteLine(loginResponse.IsSuccessStatusCode
                ? "User logged in successfully!"
                : "User login failed!");

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
                    services.AddSingleton<Authentication>(_ =>
                        new Authentication(configuration));
                    services.AddSingleton<InitialiseFirebase>();
                    services.AddSingleton<WriteToDb>();
                });
    }
}
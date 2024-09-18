using NBomber.Contracts;
using NBomber.CSharp;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FleetManagmentApp.LoadTests
{
    public class AuthControllerLoadTests
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task RunTests()
        {
     
            var registerScenario = Scenario.Create("Register Scenario", async context =>
            {
                var registerData = new
                {
                    Email = $"user{context.InvocationNumber}@mail.com", 
                    Password = "StrongPassword1"
                };
                var content = new StringContent(JsonSerializer.Serialize(registerData), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/register", content);
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

  
            var loginScenario = Scenario.Create("Login Scenario", async context =>
            {
                var loginData = new
                {
                    Email = "user1@mail.com", 
                    Password = "StrongPassword1"
                };
                var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/login", content);
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

            var checkScenario = Scenario.Create("Check Auth Scenario", async context =>
            {
                var response = await httpClient.GetAsync("/check");
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

    
            var registerScenarioConfig = registerScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(10, TimeSpan.FromSeconds(30)));

            var loginScenarioConfig = loginScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(15, TimeSpan.FromSeconds(30)));

            var checkScenarioConfig = checkScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(20, TimeSpan.FromSeconds(30)));

            NBomberRunner
                .RegisterScenarios(registerScenarioConfig, loginScenarioConfig, checkScenarioConfig)
                .Run();
        }
    }
}

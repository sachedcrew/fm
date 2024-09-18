using NBomber.Contracts;
using NBomber.CSharp;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FleetManagmentApp.LoadTests
{
    public class RoutesControllerLoadTests
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task RunTests()
        {
            var getRoutesScenario = Scenario.Create("Get Routes Scenario", async context =>
            {
                var response = await httpClient.GetAsync("routes");
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

      
            var getRouteByIdScenario = Scenario.Create("Get Route by Id Scenario", async context =>
            {
                var response = await httpClient.GetAsync("routes/1"); 
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

       
            var postRouteScenario = Scenario.Create("Post Route Scenario", async context =>
            {
                var routeData = new
                {
                    CarId = 1, 
                    StartLocation = "Start Location",
                    EndLocation = "End Location",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    Distance = 150.5,
                    FuelUsed = 12.3
                };

                var content = new StringContent(JsonSerializer.Serialize(routeData), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("routes", content);
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });


            var putRouteScenario = Scenario.Create("Put Route Scenario", async context =>
            {
                var updatedRouteData = new
                {
                    Id = 1, 
                    CarId = 1, 
                    StartLocation = "Updated Start Location",
                    EndLocation = "Updated End Location",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(3),
                    Distance = 180.0,
                    FuelUsed = 14.5
                };

                var content = new StringContent(JsonSerializer.Serialize(updatedRouteData), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync("routes/1", content);
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });


            var deleteRouteScenario = Scenario.Create("Delete Route Scenario", async context =>
            {
                var response = await httpClient.DeleteAsync("routes/1");
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });


            var getRoutesScenarioConfig = getRoutesScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(10, TimeSpan.FromSeconds(30)));

            var getRouteByIdScenarioConfig = getRouteByIdScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(5, TimeSpan.FromSeconds(30)));

            var postRouteScenarioConfig = postRouteScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(5, TimeSpan.FromSeconds(30)));

            var putRouteScenarioConfig = putRouteScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(5, TimeSpan.FromSeconds(30)));

            var deleteRouteScenarioConfig = deleteRouteScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(5, TimeSpan.FromSeconds(30)));

            NBomberRunner
                .RegisterScenarios(getRoutesScenarioConfig, getRouteByIdScenarioConfig, postRouteScenarioConfig, putRouteScenarioConfig, deleteRouteScenarioConfig)
                .Run();
        }
    }
}

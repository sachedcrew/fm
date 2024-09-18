using NBomber.Contracts;
using NBomber.CSharp;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FleetManagmentApp.LoadTests
{
    public class CarsControllerLoadTests
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task RunTests()
        {
      
            var getCarsScenario = Scenario.Create("Get Cars Scenario", async context =>
            {
                var response = await httpClient.GetAsync("/cars");
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });


            var postCarScenario = Scenario.Create("Post Car Scenario", async context =>
            {
                var carData = new
                {
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2021,
                    VIN = $"JH4KA7650RC{context.InvocationNumber}"  
                };

                var content = new StringContent(JsonSerializer.Serialize(carData), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("/cars", content);
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

  
            var putCarScenario = Scenario.Create("Put Car Scenario", async context =>
            {
                var updatedCarData = new
                {
                    Id = 1,  
                    Make = "Toyota",
                    Model = "Camry",
                    Year = 2022,
                    VIN = "JH4KA7650RC123456"
                };

                var content = new StringContent(JsonSerializer.Serialize(updatedCarData), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync("/cars/1", content);
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

   
            var deleteCarScenario = Scenario.Create("Delete Car Scenario", async context =>
            {
                var response = await httpClient.DeleteAsync("/cars/1");
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            });

   
            var getCarsScenarioConfig = getCarsScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(10, TimeSpan.FromSeconds(30)));

            var postCarScenarioConfig = postCarScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(5, TimeSpan.FromSeconds(30)));

            var putCarScenarioConfig = putCarScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(5, TimeSpan.FromSeconds(30)));

            var deleteCarScenarioConfig = deleteCarScenario
                .WithoutWarmUp()
                .WithLoadSimulations(Simulation.KeepConstant(5, TimeSpan.FromSeconds(30)));

  
            NBomberRunner
                .RegisterScenarios(getCarsScenarioConfig, postCarScenarioConfig, putCarScenarioConfig, deleteCarScenarioConfig)
                .Run();
        }
    }
}

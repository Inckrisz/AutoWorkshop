using AutoWorkshop.AutoWorkshopClient.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AutoWorkshopClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Add HttpClient with the correct API base address
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7238") // Replace with your API base address
            });

            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IJobService,  JobService>();
            builder.Services.AddScoped<IClientsWithJobsService, ClientsWithJobsService>();


            await builder.Build().RunAsync();
        }
    }
}


using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AutoWorkshopContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
            });
            builder.Services.AddScoped<JobEstimationService>();
            builder.Services.AddRazorPages();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Allows any origin to access the API
                          .AllowAnyMethod()  // Allows any HTTP method (GET, POST, etc.)
                          .AllowAnyHeader(); // Allows any header
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


    }
}

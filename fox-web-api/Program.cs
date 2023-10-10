using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace fox_web_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");
                var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

                var connectionString = $"Data Source={dbHost}; Initial Catalog={dbName}; User Id=sa;Password={dbPassword};TrustServerCertificate=true;";
                options.UseSqlServer(connectionString);
            });

            
             
            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using(var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                    dbContext.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
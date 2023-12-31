using fox_web_api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace fox_web_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

            builder.Services.AddAuthorization();
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy
                                                .AllowAnyHeader()
                                                .AllowAnyMethod().AllowAnyOrigin();
                                      });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standart Auth using bearer (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });


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

                    // this will initialize the db with tow default roles
                    List<Role> roles = dbContext.Roles.ToList();
                    if(roles.Count() == 0)
                    {
                        dbContext.Roles.AddRange(new List<Role>() { new Role() { SystemRole = "Admin" }, new Role() {SystemRole = "Employee" } } );
                        dbContext.SaveChanges();                    }
                }
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.UseCors(MyAllowSpecificOrigins);

            app.Run();
        }
    }
}
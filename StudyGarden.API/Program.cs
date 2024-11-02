using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyGarden.API.Extensions;
using StudyGarden.DataAccess;
using StudyGarden.Infrastructure;
using StudyGarden.Infrastructure.Abstractions;
using StudyGarden.Logs;

namespace StudyGarden.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddApiAuthentication(configuration);
            services.AddSwaggerGen();
            
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            
            builder.Services.AddDbContext<StudyGardenDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

            services.AddEndpointsApiExplorer();
            
            services.AddScopedServices();
            
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("StudyGardenCors", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyHeader();
                });
            });
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    // c.RoutePrefix = string.Empty;
                });
            }
            
            app.UseHttpsRedirection();
            
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            app.UseCors("StudyGardenCors");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.AddMappedEndpoint();
            
            app.Run();
        }
    }
}
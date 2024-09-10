
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyGarden.Common;
using StudyGarden.Entities;
using StudyGarden.Hub;

namespace StudyGarden
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddSignalR();  

            builder.Services.AddControllers();
            
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            builder.Services.AddCors();
            
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("IsOwner", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier)));
            });

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer =  builder.Configuration["Jwt:Issuer"],
                        ValidAudience =  builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            
            app.MapHub<GardenHub>("/garden"); 
            app.MapHub<FriendHub>("/friend"); 
            
            app.UseCors(builder => builder.AllowAnyOrigin());


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
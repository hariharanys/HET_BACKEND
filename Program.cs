
using HET_BACKEND.Helper;
using HET_BACKEND.Middleware;
using HET_BACKEND.Services.AuthServices;
using HET_BACKEND.Services.ExpenseServices;
using HET_BACKEND.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HET_BACKEND
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

            //Adding DB context
            builder.Services.AddDbContext<HETDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            //Adding AuthService Dependency
            builder.Services.AddScoped<IAuthService, AuthService>();
            //Adding UserService Dependency
            builder.Services.AddScoped<IUserService,UserService>();
            //Adding ExpenseService Dependency
            builder.Services.AddScoped<IExpenseService,ExpenseService>();

            //Adding JWT services
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]??"JWT"))
                };
            });

            //Adding JWT Helper singleton
            builder.Services.AddSingleton<JWTHelper>();

            var app = builder.Build();

            //Added Exception Middleware
            app.UseMiddleware<ExceptionMiddleware>();

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

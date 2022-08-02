using API.Controllers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Services.Client;
using Application.Services.Contract;
using Application.Services.Employee;
using Application.Services.Position;
using Application.Services.WorkPlan;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebBuilder(args);
        }
        public static void WebBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            ConfigureServices(builder);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var services = builder.Services;


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<IDbContext, CrmContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ClientService>();
            builder.Services.AddScoped<ContractService>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<PositionService>();
            builder.Services.AddScoped<WorkPlanService>();
        }
    }
}
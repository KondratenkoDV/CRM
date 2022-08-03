using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Services.Client;
using Application.Services.Contract;
using Application.Services.Employee;
using Application.Services.Position;
using Application.Services.WorkPlan;
using FluentValidation;
using API.Helpers.Client;
using API.DTOs.Client;
using API.Helpers.Contract;
using API.DTOs.Contract;
using API.Helpers.Employee;
using API.DTOs.Employee;
using API.Helpers.Position;
using API.DTOs.Position;

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

            Validator(builder);

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

        public static void Validator(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateClientDto>, CreateClientDtoValidator>();
            builder.Services.AddScoped<IValidator<UpdateClientDto>, UpdateClientDtoValidator>();

            builder.Services.AddScoped<IValidator<CreateContractDto>, CreateContractDtoValidator>();
            builder.Services.AddScoped<IValidator<UpdateContractDto>, UpdateContractDtoValidator>();

            builder.Services.AddScoped<IValidator<CreateEmployeeDto>, CreateEmployeeDtoValidator>();
            builder.Services.AddScoped<IValidator<UpdateEmployeeDto>, UpdateEmployeeDtoValidator>();

            builder.Services.AddScoped<IValidator<CreatePositionDto>, CreatePositionDtoValidator>();
            builder.Services.AddScoped<IValidator<UpdatePositionDto>, UpdatePositionDtoValidator>();
        }
    }
}
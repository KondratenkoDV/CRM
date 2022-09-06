using FluentValidation;
using FluentValidation.AspNetCore;
using UI.Helpers;
using UI.Helpers.Client;
using UI.Helpers.Contract;
using UI.Helpers.Employee;
using UI.Helpers.Position;
using UI.Models.Client;
using UI.Models.Contract;
using UI.Models.Employee;
using UI.Models.Position;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebBuilder(args).Run();
        }

        public static WebApplication WebBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient();

            builder.Services.AddFluentValidationAutoValidation();

            ConfigureServices(builder);

            Validator(builder);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return app;
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("ConnectionStrings"));
        }

        public static void Validator(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateClientModel>, CreateClientModelValidator>();
            builder.Services.AddScoped<IValidator<UpdateClientModel>, UpdateClientModelValidator>();

            builder.Services.AddScoped<IValidator<CreateContractModel>, CreateContractModelValidator>();
            builder.Services.AddScoped<IValidator<UpdateContractModel>, UpdateContractModelValidator>();

            builder.Services.AddScoped<IValidator<CreateEmployeeModel>, CreateEmployeeModelValidator>();
            builder.Services.AddScoped<IValidator<UpdateEmployeeModel>, UpdateEmployeeModelValidator>();

            builder.Services.AddScoped<IValidator<CreatePositionModel>, CreatePositionModelValidator>();
            builder.Services.AddScoped<IValidator<UpdatePositionModel>, UpdatePositionModelValidator>();
        }
    }
}

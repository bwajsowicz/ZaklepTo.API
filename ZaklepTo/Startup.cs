using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZaklepTo.Core.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
using ZaklepTo.API.Extensions;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Encrypter;
using ZaklepTo.Infrastructure.Mappers;
using ZaklepTo.Infrastructure.Repositories.InMemory;
using ZaklepTo.Infrastructure.Services.Implementations;
using ZaklepTo.Infrastructure.Services.Interfaces;
using ZaklepTo.Infrastructure.Validators;

namespace ZaklepTo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
            services.AddScoped<IReservationRepository, InMemoryReservationRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            // Restaurant service isn't implemented yet.
            // services.AddScoped<IRestaurantService, RestaurantService>(); 
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddMvc().AddFluentValidation(fv => {});

            services.AddTransient<IValidator<CustomerOnCreateDTO>, CustomerOnCreateValidator>();
            services.AddTransient<IValidator<EmployeeOnCreateDTO>, EmployeeOnCreateValidator>();
            services.AddTransient<IValidator<CustomerOnCreateDTO>, CustomerOnCreateValidator>();
            services.AddTransient<IValidator<RestaurantOnCreateDTO>, RestaurantOnCreateValidator>();

            services.AddTransient<IValidator<PasswordChange>, PasswordChangeValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomExceptionHandler();
            app.UseMvc();
        }
    }
}

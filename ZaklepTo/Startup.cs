using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastucture.Mappers;
using ZaklepTo.Infrastucture.Services.Implementations;
using ZaklepTo.Infrastucture.Repositories.InMemory;
using ZaklepTo.Infrastucture.Encrypter;
using FluentValidation.AspNetCore;
using ZaklepTo.Infrastucture.Validators;
using FluentValidation;
using ZaklepTo.Infrastucture.Services.Interfaces;
using ZaklepTo.Infrastucture.DTO.OnUpdate;
using ZaklepTo.Infrastucture.DTO.OnCreate;
using ZaklepTo.Infrastructure.Encrypter;

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

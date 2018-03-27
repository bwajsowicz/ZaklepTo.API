using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastucture.Mappers;
using ZaklepTo.Infrastucture.Services.Implementations;
using ZaklepTo.Infrastucture.Repositories.InMemory;
using ZaklepTo.Infrastucture.Encrypter;
using ZaklepTo.Infrastructure;
using FluentValidation.AspNetCore;
using ZaklepTo.Infrastucture.Validators;
using ZaklepTo.Infrastucture.DTO;
using FluentValidation;
using ZaklepTo.Infrastucture.Services.Interfaces;

namespace ZaklepTo
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
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddMvc().AddFluentValidation(fv => {});

            services.AddTransient<IValidator<CustomerOnCreateDTO>, CustomerOnCreateValidator>();
            services.AddTransient<IValidator<EmployeeOnCreateDTO>, EmployeeOnCreateValidator>();
            services.AddTransient<IValidator<OwnerOnCreateDTO>, OwnerOnCreateValidator>();
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

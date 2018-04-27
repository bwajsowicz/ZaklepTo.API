using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZaklepTo.Core.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ZaklepTo.API.Extensions;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Encrypter;
using ZaklepTo.Infrastructure.Extensions;
using ZaklepTo.Infrastructure.Mappers;
using ZaklepTo.Infrastructure.Repositories;
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
            services.AddScoped<IEmployeeRepository, InMemoryEmployeeRepository>();
            services.AddScoped<IOwnerRepository, InMemoryOwnerRepository>();
            services.AddScoped<IReservationRepository, InMemoryReservationRepository>();
            services.AddScoped<IRestaurantRepository, InMemoryRestaurantRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IRestaurantService, RestaurantService>(); 
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddScoped<IDataInitializer, DataInitializer>();

            services.AddTransient<IValidator<CustomerOnCreateDto>, CustomerOnCreateValidator>();
            services.AddTransient<IValidator<EmployeeOnCreateDto>, EmployeeOnCreateValidator>();
            services.AddTransient<IValidator<CustomerOnCreateDto>, CustomerOnCreateValidator>();
            services.AddTransient<IValidator<RestaurantOnCreateDto>, RestaurantOnCreateValidator>();
            services.AddTransient<IValidator<PasswordChange>, PasswordChangeValidator>();

            services.AddSingleton<IJwtService, JwtService>();

            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddMvc().AddFluentValidation(fv => { });

            var connectionString = Configuration["ConnectionStrings:DataBaseConnectionString"];
            services.AddDbContext<DataBaseService>(options => options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDataInitializer dataInitializer, DataBaseService dataBaseService)
        {
            app.UseAuthentication();
            app.UseDeveloperExceptionPage();
            app.UseCustomExceptionHandler();
            app.UseMvc();

            dataInitializer.SeedAsync();
        }
    }
}

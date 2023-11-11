﻿using FluentValidation;
using FluentValidation.AspNetCore;
using HMS.Application.Abstraction.Repositories;
using HMS.Application.Abstraction.Repositories.IHotelManagerRepositories;
using HMS.Application.Abstraction.Repositories.IVisitorRepositories;
using HMS.Application.Abstraction.Services;
using HMS.Application.Validators.AuthValidators;
using HMS.Domain.Identity;
using HMS.Persistence.Context;
using HMS.Persistence.Implementations.Repositories;
using HMS.Persistence.Implementations.Repositories.HotelManagerRepositories;
using HMS.Persistence.Implementations.Repositories.VisitorRepositories;
using HMS.Persistence.Implementations.Services;
using HMS.Persistence.MapperProfiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Persistence.Extension_Methods
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            AddReadRepositories(services);
            AddWriteRepositories(services);
            services.AddScoped<IAuthService, AuthService>();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
            services.AddAutoMapper(typeof(AccountsProfile).Assembly);
            services.AddScoped<AppDbContextInitialiser>();
        }

        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;
                identityOptions.Password.RequiredLength = 8;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
            }).AddDefaultTokenProviders()
              .AddEntityFrameworkStores<AppDbContext>();
        }
        private static void AddReadRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped<IHotelManagerReadRepository, HotelManagerReadRepository>();
            services.AddScoped<IVisitorReadRepository, VisitorReadRepository>();
        }
        private static void AddWriteRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IHotelManagerWriteRepository, HotelManagerWriteRepository>();
            services.AddScoped<IVisitorWriteRepository, VisitorWriteRepository>();
        }
    }
}

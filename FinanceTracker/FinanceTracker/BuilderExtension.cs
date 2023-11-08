using Microsoft.AspNetCore.Authentication.Cookies;
using FinanceTracker.Repository.Interfaces;
using FinanceTracker.Repository.Implementations;
using FinanceTracker.Repository.Context;
using FinanceTracker.Service.Interfaces;
using FinanceTracker.Service.Implementations;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker
{
    public static class BuilderExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            //MVC
            builder.Services.AddControllersWithViews();

            //Repository
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IFinancialAccountRepository, FinancialAccountRepository>();

            builder.Services.AddScoped<IIncomeTypeRepository, IncomeTypeRepository>();
            builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();

            builder.Services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

            //Service
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IFinancialAccountService, FinancialAccountService>();
        }

        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Authentication";
                    options.LoginPath = "/Account/Authorization";
                    options.Cookie.HttpOnly = true;
                    options.SlidingExpiration = true;
                });
        }
        public static void AddDataBase(this WebApplicationBuilder builder)
        {
            var connStr = builder.Configuration.GetConnectionString("Default");

            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connStr).Options;

            builder.Services.AddScoped((s) => new AppDbContext(options));
            builder.Services.AddDbContext<AppDbContext>();
        }
    }
}
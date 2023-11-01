using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Domain.Models;

namespace FinanceTracker.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;

        public DbSet<FinancialAccountModel> FinancialAccounts { get; set; } = null!;

        public DbSet<IncomeTypeModel> IncomeTypes { get; set; } = null!;
        public DbSet<IncomeModel> Incomes { get; set; } = null!;

        public DbSet<ExpenseTypeModel> ExpenseTypes { get; set; } = null!;
        public DbSet<ExpenseModel> Expenses { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

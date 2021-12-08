using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrdering.Server.Data.Context
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<MealOrderingDbContext>
    {
        public MealOrderingDbContext CreateDbContext(string[] args)
        {
            String connectionString = "User ID=postgres;Password=852456Asd_?=;Host=localhost;Port=5432;Database=mealordering;";
            var builder = new DbContextOptionsBuilder<MealOrderingDbContext>();
            builder.UseNpgsql(connectionString);
            return new MealOrderingDbContext(builder.Options);
        }
    }
}

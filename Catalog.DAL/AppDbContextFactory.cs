using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {

        public AppDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db;Username=postgres;Password=12345678");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

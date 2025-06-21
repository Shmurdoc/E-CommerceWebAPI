using Microsoft.EntityFrameworkCore;
using eCommerceWebAPI.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace IntegrationTest.Data
{
    public class TestDbFactory
    {
        // Creates a new AppDataContext using the DefaultConnection string from appsettings.json.
        public AppDataContext CreateDbContext()
        {
            // Build configuration to read appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the connection string from configuration
            var connectionString = config.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<AppDataContext>();
            builder.UseSqlServer(connectionString);

            return new AppDataContext(builder.Options);
        }
    }
}
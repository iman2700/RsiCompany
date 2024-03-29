﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace RsiCompany.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {

        public RepositoryContext CreateDbContext(string[] args)
        {
            // Create a new configuration builder and set the base path to the current directory
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Create a new DbContextOptionsBuilder for the RepositoryContext
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("RsiCompany"));

            // Build the options and return a new instance of RepositoryContext with the configured options
            return new RepositoryContext(builder.Options);

        }

    }
}
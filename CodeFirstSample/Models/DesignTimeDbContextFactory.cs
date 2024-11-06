using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CodeFirstSample;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[] args)
    {
        args.ToList().ForEach(l => Console.WriteLine("ARGS: =>" + l));

        var connectionString = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>()
            .UseNpgsql(connectionString);

        return new MyDbContext(optionsBuilder.Options, true);
    }
}
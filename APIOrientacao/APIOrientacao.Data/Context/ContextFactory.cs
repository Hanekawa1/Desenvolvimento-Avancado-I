using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace APIOrientacao.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<Contexto>
    {
        Contexto IDesignTimeDbContextFactory<Contexto>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<Contexto>();
            var connectionString = configuration.GetConnectionString("ProjetoOrientacao");

            builder.UseSqlServer(connectionString);

            return new Contexto(builder.Options);
        }
    }
}

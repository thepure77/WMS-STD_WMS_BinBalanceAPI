using BinBalanceDataAccess.Models;
using GRDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class MasterDbContext : DbContext
    {

        public virtual DbSet<View_Location> View_Location { get; set; }
        public virtual DbSet<ms_Product> ms_Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("MasterDataAccess").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }


        }
    }

}

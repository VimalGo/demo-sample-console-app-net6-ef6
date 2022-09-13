using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sample_Console_App_Net_6.Context_Configuration;
using Sample_Console_App_Net_6.Models;
using Sample_Console_App_Net_6.Models.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sample_Console_App_Net_6.Context
{
    public partial class ReadTextFile_And_StoreData_Context : DbContext
    {
        public ReadTextFile_And_StoreData_Context()
        {

        }

        public ReadTextFile_And_StoreData_Context(DbContextOptions<ReadTextFile_And_StoreData_Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                                   // .AddEnvironmentVariables()
                                   ;

                var config = builder.Build();
                var connString = config.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString: connString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<TblStoreDataFromTextFile>(new TblStoreDataFromTextFileConfig());

            #region Stored Procedure
            modelBuilder.Entity<SP_Insert_Text_File_Data_Result>().HasNoKey();
            #endregion
        }
    }
}

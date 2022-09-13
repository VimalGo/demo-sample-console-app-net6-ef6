using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Sample_Console_App_Net_6.Context;
using Microsoft.EntityFrameworkCore;

namespace Sample_Console_App_Net_6
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var logHelper = LogManager.GetCurrentClassLogger();

            try
            {
                // Read appsettings.json file
                var config = new ConfigurationBuilder()
                                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())     // From NuGet Package Microsoft.Extenstions.Configuration.Json
                                                                                                //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                    .Build();

                using var serviceProvider = new ServiceCollection()
                                                .AddTransient<LogHelper>()
                                                .AddLogging(loggingBuilder =>
                                                {
                                                    // Configure Logging with NLog
                                                    loggingBuilder.ClearProviders();
                                                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                                                    loggingBuilder.AddNLog(config);
                                                }).BuildServiceProvider();

                var objLog = serviceProvider.GetRequiredService<LogHelper>();

                objLog.LogInformation("Hello World");

                ReadTextFile_And_StoreData_Context _context = new ReadTextFile_And_StoreData_Context();

                var filePath = config.GetSection("FilePath").Value;

                objLog.LogInformation($"File Path: {filePath}");

                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    string fileName = fileInfo.Name;
                    long fileSize = fileInfo.Length;
                    DateTime fileCreationTime = fileInfo.CreationTime;

                    objLog.LogInformation(string.Format("File Info: {0, -30:g} {1, -12:N0} {2} ", fileName, fileSize, fileCreationTime));

                    string[] fileLines = System.IO.File.ReadAllLines(filePath + fileName);
                    

                    DataTable dtTextFileDataTable = GetTextFileDataDataTable();

                    foreach (string line in fileLines)
                    {
                        // Use a tab to indent each line of the file.
                        objLog.LogInformation("\t" + line);

                        var a = line.Split('~')[0];

                        DataRow dtRow = dtTextFileDataTable.NewRow();
                        dtRow["Test_Column1_Data"] = Convert.ToInt64(line.Split('~')[0]);
                        dtRow["Test_Column2_Data"] = Convert.ToInt64(line.Split('~')[1]);
                        dtRow["Test_Column3_Data"] = line.Split('~')[2];
                        dtRow["Test_Column4_Data"] = line.Split('~')[3];
                        dtRow["Test_Column5_Data"] = line.Split('~')[4];

                        dtTextFileDataTable.Rows.Add(dtRow);
                    }

                    var param = new SqlParameter("@Text_File_Data", SqlDbType.Structured);
                    param.Value = dtTextFileDataTable;
                    param.TypeName = "[dbo].[UDTT_Text_File_Data]";     // Define your User Defined Data Table here.

                    var result = _context.SP_Insert_Text_File_Data_Results.FromSqlInterpolated($"EXEC [dbo].[INSERT_TEXT_FILE_DATA] @Text_File_Data = {param}").ToList();

                    objLog.LogInformation($"Max Text Raw Data Id: {result[0].MAX_Text_File_Data_Row_Id}");
                }

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logHelper.Error(ex, "Stopped program because of exception.");
                throw;
            }
            finally
            {
                // Ensure to flush and stop interval timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
            
        }

        public static DataTable GetTextFileDataDataTable()
        {
            DataTable dt = new DataTable();

            dt.Clear();

            dt.Columns.Add("Test_Column1_Data");
            dt.Columns.Add("Test_Column2_Data");
            dt.Columns.Add("Test_Column3_Data");
            dt.Columns.Add("Test_Column4_Data");
            dt.Columns.Add("Test_Column5_Data");

            return dt;
        }
    }
}
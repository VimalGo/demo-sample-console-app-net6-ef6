using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample_Console_App_Net_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Console_App_Net_6.Context_Configuration
{
    public class TblStoreDataFromTextFileConfig : IEntityTypeConfiguration<TblStoreDataFromTextFile>
    {
        public void Configure(EntityTypeBuilder<TblStoreDataFromTextFile> builder)
        {
            builder.HasKey(e => e.TextFileDataRowId);

            builder.ToTable("Tbl_Store_Data_From_Text_File");

            builder.Property(e => e.TextFileDataRowId).HasColumnName("Text_File_Data_Row_Id");

            builder.Property(e => e.TestColumn1Data).HasColumnName("Test_Column1_Data");

            builder.Property(e => e.TestColumn2Data).HasColumnName("Test_Column2_Data");

            builder.Property(e => e.TestColumn3Data).HasColumnName("Test_Column3_Data");

            builder.Property(e => e.TestColumn4Data).HasColumnName("Test_Column4_Data");

            builder.Property(e => e.TestColumn5Data).HasColumnName("Test_Column5_Data");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Sample_Console_App_Net_6.Models.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Console_App_Net_6.Context
{
    public partial class ReadTextFile_And_StoreData_Context : DbContext
    {
        public DbSet<SP_Insert_Text_File_Data_Result> SP_Insert_Text_File_Data_Results { get; set; }
    }
}

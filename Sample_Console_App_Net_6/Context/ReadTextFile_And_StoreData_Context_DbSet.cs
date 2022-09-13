using Microsoft.EntityFrameworkCore;
using Sample_Console_App_Net_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Console_App_Net_6.Context
{
    public partial class ReadTextFile_And_StoreData_Context : DbContext
    {
        public DbSet<TblStoreDataFromTextFile> TblStoreDataFromTextFiles { get; set; }
    }
}

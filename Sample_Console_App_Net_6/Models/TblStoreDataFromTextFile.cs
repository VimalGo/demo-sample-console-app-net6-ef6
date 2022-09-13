using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Console_App_Net_6.Models
{
    public partial class TblStoreDataFromTextFile
    {
        public long TextFileDataRowId { get; set; }
        public long? TestColumn1Data { get; set; }
        public long? TestColumn2Data { get; set; }
        public string? TestColumn3Data { get; set; }
        public string? TestColumn4Data { get; set; }
        public string? TestColumn5Data { get; set; }
    }
}

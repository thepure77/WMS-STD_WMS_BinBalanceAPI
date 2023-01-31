using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{
    public partial class View_CheckStock
    {
        [Key]
        public long? RowIndex { get; set; }
        
        public string Product_Id { get; set; }


        public string Product_Name { get; set; }

        public string Product_Lot { get; set; }

        public Guid ItemStatus_Index { get; set; }


        public string ERP_Location { get; set; }

        public Guid Location_Index { get; set; }

        public string remark { get; set; }

        public decimal? qty_Bal { get; set; }

        public decimal? qty_Reserve { get; set; }
    }
}

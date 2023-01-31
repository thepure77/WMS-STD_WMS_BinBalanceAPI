using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class SKUConversionViewModel : Pagination
    {
        [Key]
        public long RowIndex { get; set; }

        public string ProductId { get; set; }

        public string ProductConversionBarcode { get; set; }

        public string ProductName { get; set; }

        public string ProductSecondName { get; set; }

        public string ProductThirdName { get; set; }

        public string SKUConversionName { get; set; }

        public string ProductConversionName { get; set; }

        public decimal? ProductConversionRatio { get; set; }

        public string ColumnName { get; set; }

        public string Orderby { get; set; }
    }

    public class actionResultSKUConversionViewModel
    {
        public IList<SKUConversionViewModel> items { get; set; }
        public Pagination pagination { get; set; }
    }
}




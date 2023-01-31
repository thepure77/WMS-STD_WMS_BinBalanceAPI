using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class SKUSearchViewModel : Pagination
    {
        [Key]
        public long RowIndex { get; set; }

        public Guid? ProductIndex { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductSecondName { get; set; }

        public string LocationName { get; set; }

        public string GoodsReceiveNo { get; set; }

        public string ReceivingRef { get; set; }

        public string GoodsReceiveDate { get; set; }

        public string TagNo { get; set; }

        public string ItemStatusName { get; set; }

        public string MFGDate { get; set; }

        public string EXPDate { get; set; }

        public string ProductConversionName { get; set; }

        public decimal StockONHand { get; set; }

        public decimal StockAllocated { get; set; }

        public decimal StockAvailable { get; set; }

        public string ColumnName { get; set; }

        public string Orderby { get; set; }
        public string Type { get; set; }
        public Guid? owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public string productConversion_Ref1 { get; set; }

        public string productConversion_Ref2 { get; set; }
        public string productConversion_Ref3 { get; set; }
    }

    public class actionResultSKUViewModel
    {
        public IList<SKUSearchViewModel> items { get; set; }
        public Pagination pagination { get; set; }
    }
}




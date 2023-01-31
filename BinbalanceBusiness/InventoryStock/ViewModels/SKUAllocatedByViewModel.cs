using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class SKUAllocatedByViewModel : Pagination
    {
        [Key]
        public long RowIndex { get; set; }

        public string GoodsIssueNo { get; set; }

        public string RefDocumentNo { get; set; }

        public DateTime? PlanGoodsIssueDueDate { get; set; }

        public string RouteName { get; set; }

        public string RoundName { get; set; }

        public string ProductId { get; set; }

        public decimal BinCardReserveQtyBal { get; set; }

        public string ColumnName { get; set; }

        public string Orderby { get; set; }
    }

    public class actionResultSKUAllocatedByViewModel
    {
        public IList<SKUAllocatedByViewModel> items { get; set; }
        public Pagination pagination { get; set; }
    }

}


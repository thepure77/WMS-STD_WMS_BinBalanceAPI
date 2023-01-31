using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public partial class CheckStockViewModel
    {
        public string product_Id { get; set; }
        public string Product_Lot { get; set; }
        public string ERP_Location { get; set; }
        public Guid? ItemStatus_Index { get; set; }

    }
    
}


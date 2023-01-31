using System;
using BinBalanceDataAccess.Models;

namespace BinbalanceBusiness.ReplenishmentBalance
{
    public class ReplenishmentBalanceModel
    {
        public wm_BinBalance BinBalance { get; set; }

        public Guid Owner_Index { get; set; }

        public Guid Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public decimal Replenish_Qty { get; set; }
    }
}

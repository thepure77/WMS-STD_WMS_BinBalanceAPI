using System;
using System.Collections.Generic;
using BinBalanceDataAccess.Models;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class ReserveBinBalanceModel
    {
        public List<ReserveBinBalanceItemModel> Items { get; set; } = new List<ReserveBinBalanceItemModel>();
    }

    public class ReserveBinBalanceItemModel
    {
        public Guid BinBalance_Index { get; set; }

        public Guid Ref_Document_Index { get; set; }

        public Guid Ref_DocumentItem_Index { get; set; }

        public Guid Process_Index { get; set; }

        public string Ref_Document_No { get; set; }

        public string Ref_Wave_Index { get; set; }

        public decimal Reserve_Qty { get; set; }

        public string Reserve_By { get; set; }

        public bool IsReturnBinBalanceModel { get; set; }

        public bool IsReturninCardReserveModel { get; set; }
    }

    public class ReserveBinBalanceResultModel : Result
    {
        public List<ReserveBinBalanceResultItemModel> Items { get; set; }
    }

    public class ReserveBinBalanceResultItemModel
    {
        public Guid BinBalance_Index { get; set; }

        public Guid BinCardReserve_Index { get; set; }

        public wm_BinBalance BinBalance_Model { get; set; }

        public wm_BinCardReserve BinCardReserve_Model { get; set; }
    }
}

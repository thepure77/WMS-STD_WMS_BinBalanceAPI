using BinbalanceBusiness;
using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace binbalanceBusiness.BinServiceChargeViewModel
{
    public partial class BinServiceChargeViewModel
    {

        public BinServiceChargeViewModel()
        {
            listBinBalanceServiceCharge = new List<BinServiceChargeViewModel>();

        }

        public Guid? location_Index { get; set; }

        public string location_Id { get; set; }

        public string location_Name { get; set; }
        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string doc_Date { get; set; }
        public decimal? binBalance_QtyBal { get; set; }
        public decimal? binBalance_VolumeBal { get; set; }
        public decimal? binBalance_WeightBal { get; set; }
        public decimal? binBalance_NetWeightBal { get; set; }

        public decimal? binBalance_GrsWeightBal { get; set; }

        public decimal? rT { get; set; }
        public Guid? storageCharge_Index { get; set; }

        public Guid? serviceCharge_Index { get; set; }

        public string serviceCharge_Id { get; set; }

        public string serviceCharge_Name { get; set; }
        public decimal? rate { get; set; }
        public decimal? volumeCal { get; set; }
        public decimal? amount { get; set; }
        public string unitCharge_Name { get; set; }

        public List<BinServiceChargeViewModel> listBinBalanceServiceCharge { get; set; }

    }


}


using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class MovementViewModel
    {
        public string tag_No { get; set; }
        public string bincard_date { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string ref_Document_No { get; set; }
        public string documentType_Name { get; set; }
        public string product_Lot { get; set; }
        public decimal? binCard_QtyIn { get; set; }
        public decimal? binCard_QtyOut { get; set; }
        public decimal? binCard_QtySign { get; set; }
        public string productConversion_Name { get; set; }
        public string location_Name { get; set; }
        public string location_Name_To { get; set; }
        public string itemStatus_Name { get; set; }
        public string itemStatus_Name_To { get; set; }






    }

}


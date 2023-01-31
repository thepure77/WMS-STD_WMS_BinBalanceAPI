using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class actionResultMovementViewModel
    {
        public List<MovementViewModel> items { get; set; }
        public Pagination pagination { get; set; }
    }

    public class FilterSearchMovementViewModel : Pagination
    {
        public string owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string date_From { get; set; }
        public string date_To { get; set; }
        public string itemStatus_Index { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }
        public string productType_Index { get; set; }
        public string productType_Id { get; set; }
        public string productType_Name { get; set; }
        public string product_Lot { get; set; }
        public string ref_Document_Index { get; set; }
        public string ref_Document_Name { get; set; }
        public string advanceSearch_Date_From { get; set; }
        public string advanceSearch_Date_To { get; set; }
    }
}




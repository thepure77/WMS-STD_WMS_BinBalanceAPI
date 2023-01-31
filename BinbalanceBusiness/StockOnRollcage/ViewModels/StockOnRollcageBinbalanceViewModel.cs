using System;
using System.Collections.Generic;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public partial class StockOnRollcageBinbalanceViewModel : Pagination
    {
        public Guid? binBalance_Index { get; set; }
        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public Guid? location_Index { get; set; }


        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public Guid? zone_Index { get; set; }
        public string zone_Name { get; set; }
        public string zone_Id { get; set; }
        public Guid? goodsReceive_Index { get; set; }
        public string goodsReceive_No { get; set; }
        public string goodsReceive_Date { get; set; }
        public string goodsReceive_Date_To { get; set; }
        public Guid? goodsReceiveItem_Index { get; set; }
        public Guid? goodsReceiveItemLocation_Index { get; set; }
        public Guid? tagItem_Index { get; set; }
        public Guid? tag_Index { get; set; }
        public string tag_No { get; set; }
        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string product_SecondName { get; set; }
        public string product_ThirdName { get; set; }
        public string product_Lot { get; set; }

        public Guid? productType_Index { get; set; }

        public Guid? itemStatus_Index { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }
        public string goodsReceive_MFG_Date { get; set; }
        public string goodsReceive_MFG_Date_To { get; set; }

        public string goodsReceive_EXP_Date { get; set; }
        public string goodsReceive_EXP_Date_To { get; set; }

        public Guid? goodsReceive_ProductConversion_Index { get; set; }
        public string goodsReceive_ProductConversion_Id { get; set; }
        public string goodsReceive_ProductConversion_Name { get; set; }
        public decimal? sale_qty { get; set; }
        public string sale_unit { get; set; }
        public decimal? binBalance_Ratio { get; set; }
        public decimal? binBalance_QtyBegin { get; set; }
        public decimal? binBalance_WeightBegin { get; set; }
        public decimal? binBalance_VolumeBegin { get; set; }
        public decimal? binBalance_QtyBal { get; set; }
        public decimal? binBalance_WeightBal { get; set; }
        public decimal? binBalance_VolumeBal { get; set; }
        public decimal? binBalance_QtyReserve { get; set; }
        public decimal? binBalance_WeightReserve { get; set; }
        public decimal? binBalance_VolumeReserve { get; set; }
        public Guid? productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public string uDF_1 { get; set; }
        public string uDF_2 { get; set; }
        public string uDF_3 { get; set; }
        public string uDF_4 { get; set; }
        public string uDF_5 { get; set; }
        public string IsUse { get; set; }
        public string binBalance_Status { get; set; }
        public bool advanceSearch { get; set; }
        public decimal? total { get; set; }
        public decimal? amount { get; set; }
        public int? row { get; set; }
        public string productType_Name { get; set; }

        public decimal? productConversion_Width { get; set; }
        public decimal? productConversion_Length { get; set; }
        public decimal? productConversion_Height { get; set; }
        public decimal? productConversion_Volume { get; set; }
        public decimal? productConversion_ratio { get; set; }
        public string locationType_Name { get; set; }
        public string erp_location { get; set; }
        public decimal? cbm { get; set; }
        public bool isExportByProduct { get; set; }
        public bool isExportByLocation { get; set; }

        public int? dateDiffGetdate { get; set; }
        public int? productShelfLife_D { get; set; }
        public int? remainingShelfLife { get; set; }

        public string goodsIssue_No { get; set; }

        public string round_Name { get; set; }

        public string planGoodsIssue_Due_Date { get; set; }

        public class actionResultViewModel
        {
            public IList<StockOnRollcageBinbalanceViewModel> itemsStock { get; set; }
            public IList<StockOnRollcageBinbalanceViewModel> itemsStockLo { get; set; }

            public Pagination pagination { get; set; }
        }
    }

    public class View_GroupProductOnRollcageViewModel
    {

        public Guid? Product_Index { get; set; }
        public string Product_Name { get; set; }
        public string Product_Id { get; set; }
        public Guid Tag_Index { get; set; }
        public string Tag_No { get; set; }
        public Guid Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }
        public string Product_Lot { get; set; }
        public Guid? ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }

        public string ProductConversion_Name { get; set; }
        public decimal qty { get; set; }

        public decimal totalQty { get; set; }

        public List<View_GroupProductViewModel> ResultItem { get; set; }


    }

}


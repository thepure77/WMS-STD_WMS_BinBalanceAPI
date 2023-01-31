using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public partial class BinbalanceViewModel
    {
        public Guid? BinBalance_Index { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public Guid? GoodsReceive_Index { get; set; }
        public string GoodsReceive_No { get; set; }
        public string GoodsReceive_Date { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        public Guid? GoodsReceiveItemLocation_Index { get; set; }
        public Guid? TagItem_Index { get; set; }
        public Guid? Tag_Index { get; set; }
        public string Tag_No { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_SecondName { get; set; }
        public string Product_ThirdName { get; set; }
        public string Product_Lot { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }
        public string GoodsReceive_MFG_Date { get; set; }
        public string GoodsReceive_EXP_Date { get; set; }
        public Guid? GoodsReceive_ProductConversion_Index { get; set; }
        public string GoodsReceive_ProductConversion_Id { get; set; }
        public string GoodsReceive_ProductConversion_Name { get; set; }
        public decimal? BinBalance_Ratio { get; set; }
        public decimal? BinBalance_QtyBegin { get; set; }
        public decimal? BinBalance_WeightBegin { get; set; }
        public decimal? BinBalance_VolumeBegin { get; set; }
        public decimal? BinBalance_QtyBal { get; set; }
        public decimal? BinBalance_WeightBal { get; set; }
        public decimal? BinBalance_VolumeBal { get; set; }
        public decimal? BinBalance_QtyReserve { get; set; }
        public decimal? BinBalance_WeightReserve { get; set; }
        public decimal? BinBalance_VolumeReserve { get; set; }
        public Guid? ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public string UDF_1 { get; set; }
        public string UDF_2 { get; set; }
        public string UDF_3 { get; set; }
        public string UDF_4 { get; set; }
        public string UDF_5 { get; set; }
        public string IsUse { get; set; }
        public string BinBalance_Status { get; set; }
    }

    public partial class BinbalanceFilterViewModel
    {
        public Guid? binBalance_Index { get; set; }

    }

    public partial class goodsIssueItemLocationFilterViewModel
    {
        public Guid goodsIssueItemLocation_Index { get; set; }

        public Guid goodsIssue_Index { get; set; }


        public string lineNum { get; set; }

        public Guid? tagItem_Index { get; set; }

        public Guid? tag_Index { get; set; }


        public string tag_No { get; set; }

        public Guid product_Index { get; set; }



        public string product_Id { get; set; }



        public string product_Name { get; set; }


        public string product_SecondName { get; set; }


        public string product_ThirdName { get; set; }


        public string product_Lot { get; set; }

        public Guid? itemStatus_Index { get; set; }



        public string itemStatus_Id { get; set; }



        public string itemStatus_Name { get; set; }

        public Guid? location_Index { get; set; }


        public string location_Id { get; set; }


        public string location_Name { get; set; }


        public decimal qty { get; set; }


        public decimal ratio { get; set; }


        public decimal totalQty { get; set; }

        public Guid productConversion_Index { get; set; }



        public string productConversion_Id { get; set; }



        public string productConversion_Name { get; set; }


        public DateTime? mFG_Date { get; set; }


        public DateTime? eXP_Date { get; set; }


        public decimal? unitWeight { get; set; }


        public decimal? weight { get; set; }


        public decimal? unitWidth { get; set; }


        public decimal? unitLength { get; set; }


        public decimal? unitHeight { get; set; }


        public decimal? unitVolume { get; set; }


        public decimal? volume { get; set; }


        public decimal? unitPrice { get; set; }


        public decimal? price { get; set; }


        public string documentRef_No1 { get; set; }


        public string documentRef_No2 { get; set; }


        public string documentRef_No3 { get; set; }


        public string documentRef_No4 { get; set; }


        public string documentRef_No5 { get; set; }

        public int? document_Status { get; set; }


        public string uDF_1 { get; set; }


        public string uDF_2 { get; set; }


        public string uDF_3 { get; set; }


        public string uDF_4 { get; set; }


        public string uDF_5 { get; set; }

        public Guid? ref_Process_Index { get; set; }


        public string ref_Document_No { get; set; }


        public string ref_Document_LineNum { get; set; }

        public Guid? ref_Document_Index { get; set; }

        public Guid? ref_DocumentItem_Index { get; set; }

        public Guid? goodsReceiveItem_Index { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }

        public int? picking_Status { get; set; }


        public string picking_By { get; set; }


        public DateTime? picking_Date { get; set; }


        public string picking_Ref1 { get; set; }


        public string picking_Ref2 { get; set; }


        public decimal? picking_Qty { get; set; }


        public decimal? picking_Ratio { get; set; }


        public decimal? picking_TotalQty { get; set; }

        public Guid? picking_ProductConversion_Index { get; set; }

        public int? mashall_Status { get; set; }


        public decimal? mashall_Qty { get; set; }

        public int? cancel_Status { get; set; }


        public string goodsIssue_No { get; set; }

        public string message { get; set; }

        public Guid? binBalance_Index { get; set; }
        public Guid process_Index { get; set; }
        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public DateTime? goodsIssue_Date { get; set; }
        public Guid? documentType_Index { get; set; }
        public string documentType_Id { get; set; }
        public string documentType_Name { get; set; }
    }
}


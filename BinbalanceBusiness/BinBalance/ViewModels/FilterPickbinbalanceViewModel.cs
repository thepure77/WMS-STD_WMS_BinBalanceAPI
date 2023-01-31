using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public partial class FilterBinbalanceViewModel
    {
        public Guid Owner_Index { get; set; }
        public Guid? BinBalance_Index { get; set; }
        public Guid? Location_Index { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Lot { get; set; }
        public string Tag_No { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public DateTime? MFG_Date { get; set; }
        public DateTime? EXP_Date { get; set; }
        public string UDF_1 { get; set; }
        public string UDF_2 { get; set; }
        public string UDF_3 { get; set; }
        public string UDF_4 { get; set; }
        public string UDF_5 { get; set; }
        public bool isUseAttribute { get; set; }
        public string isuse { get; set; }
        public bool isActive { get; set; }
        public decimal? qtyPreTag { get; set; }
    }

    public partial class GoodsTransferItemViewModel
    {
        public Guid? goodsTransferItem_Index { get; set; }
        public Guid? goodsTransfer_Index { get; set; }
        public Guid? goodsReceive_Index { get; set; }
        public Guid? goodsReceiveItem_Index { get; set; }
        public Guid? goodsReceiveItemLocation_Index { get; set; }
        public string lineNum { get; set; }
        public Guid? tagItem_Index { get; set; }
        public Guid? tag_Index { get; set; }
        public string tag_No { get; set; }
        public Guid? tag_Index_To { get; set; }
        public Guid? product_Index { get; set; }
        public Guid? product_Index_To { get; set; }
        public string product_Lot { get; set; }
        public string product_Lot_To { get; set; }
        public Guid? itemStatus_Index { get; set; }
        public Guid? itemStatus_Index_To { get; set; }
        public Guid? productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public Guid? owner_Index { get; set; }
        public Guid? owner_Index_To { get; set; }
        public Guid? location_Index { get; set; }
        public Guid? location_Index_To { get; set; }
        public DateTime? goodsReceive_EXP_Date { get; set; }
        public DateTime? goodsReceive_EXP_Date_To { get; set; }
        public decimal? qty { get; set; }
        public decimal? ratio { get; set; }
        public decimal? totalQty { get; set; }
        public decimal? weight { get; set; }
        public decimal? volume { get; set; }
        public string documentRef_No1 { get; set; }
        public string documentRef_No2 { get; set; }
        public string documentRef_No3 { get; set; }
        public string documentRef_No4 { get; set; }
        public string documentRef_No5 { get; set; }
        public int? document_Status { get; set; }
        public string udf_1 { get; set; }
        public string udf_2 { get; set; }
        public string udf_3 { get; set; }
        public string udf_4 { get; set; }
        public string udf_5 { get; set; }
        public Guid? ref_Process_Index { get; set; }
        public string ref_Document_No { get; set; }
        public Guid? ref_Document_Index { get; set; }
        public Guid? ref_DocumentItem_Index { get; set; }
        public string tag_No_To { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string product_SecondName { get; set; }
        public string product_ThirdName { get; set; }
        public string product_Id_To { get; set; }
        public string product_Name_To { get; set; }
        public string product_SecondName_To { get; set; }
        public string product_ThirdName_To { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }
        public string itemStatus_Id_To { get; set; }
        public string itemStatus_Name_To { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string owner_Id_To { get; set; }
        public string owner_Name_To { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public string location_Id_To { get; set; }
        public string location_Name_To { get; set; }

        public DateTime? transfer_Date { get; set; }
        public Guid? binBalance_Index { get; set; }
        public string username { get; set; }
    }
}


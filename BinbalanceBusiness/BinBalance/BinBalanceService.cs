using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using BinBalanceBusiness;
using BinBalanceBusiness.ViewModels;
using BinBalanceDataAccess.Models;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;
using MasterDataBusiness.ViewModels;
using ProductViewModel = MasterDataBusiness.ViewModels.ProductViewModel;
using GIBusiness.GoodIssue;

namespace BinbalanceBusiness.BinBalanceService
{
    public class BinBalanceService
    {
        private BinbalanceDbContext db;
        private MasterDbContext dbMaster;

        public BinBalanceService()
        {
            db = new BinbalanceDbContext();
            dbMaster = new MasterDbContext();
        }
        public BinBalanceService(BinbalanceDbContext db , MasterDbContext dbMaster)
        {
            this.db = db;
            this.dbMaster = dbMaster;
        }

        public BinBalanceViewModel find(PickbinbalanceViewModel model)
        {
            try
            {
                var query = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));

                var item = new BinBalanceViewModel
                {
                    binBalance_Index = query.BinBalance_Index,
                    owner_Index = query.Owner_Index,
                    owner_Id = query.Owner_Id,
                    owner_Name = query.Owner_Name,
                    location_Index = query.Location_Index,
                    location_Id = query.Location_Id,
                    location_Name = query.Location_Name,
                    goodsReceive_Index = query.GoodsReceive_Index,
                    goodsReceive_No = query.GoodsReceive_No,
                    goodsReceive_Date = query.GoodsReceive_Date,
                    goodsReceiveItem_Index = query.GoodsReceiveItem_Index,
                    goodsReceiveItemLocation_Index = query.GoodsReceiveItemLocation_Index,
                    tagItem_Index = query.TagItem_Index,
                    tag_Index = query.Tag_Index,
                    tag_No = query.Tag_No,
                    product_Index = query.Product_Index,
                    product_Id = query.Product_Id,
                    product_Name = query.Product_Name,
                    product_SecondName = query.Product_SecondName,
                    product_ThirdName = query.Product_ThirdName,
                    product_Lot = query.Product_Lot,
                    itemStatus_Index = query.ItemStatus_Index,
                    itemStatus_Id = query.ItemStatus_Id,
                    itemStatus_Name = query.ItemStatus_Name,
                    goodsReceive_MFG_Date = query.GoodsReceive_MFG_Date,
                    goodsReceive_EXP_Date = query.GoodsReceive_EXP_Date,
                    goodsReceive_ProductConversion_Index = query.GoodsReceive_ProductConversion_Index,
                    goodsReceive_ProductConversion_Id = query.GoodsReceive_ProductConversion_Id,
                    goodsReceive_ProductConversion_Name = query.GoodsReceive_ProductConversion_Name,

                    binBalance_Ratio = query.BinBalance_Ratio,
                    binBalance_QtyBegin = query.BinBalance_QtyBegin,
                    binBalance_WeightBegin = query.BinBalance_WeightBegin,
                    binBalance_WeightBegin_Index = query.BinBalance_WeightBegin_Index,
                    binBalance_WeightBegin_Id = query.BinBalance_WeightBegin_Id,
                    binBalance_WeightBegin_Name = query.BinBalance_WeightBegin_Name,
                    binBalance_WeightBeginRatio = query.BinBalance_WeightBeginRatio,
                    binBalance_NetWeightBegin = query.BinBalance_NetWeightBegin,
                    binBalance_NetWeightBegin_Index = query.BinBalance_NetWeightBegin_Index,
                    binBalance_NetWeightBegin_Id = query.BinBalance_NetWeightBegin_Id,
                    binBalance_NetWeightBegin_Name = query.BinBalance_NetWeightBegin_Name,
                    binBalance_NetWeightBeginRatio = query.BinBalance_NetWeightBeginRatio,
                    binBalance_GrsWeightBegin = query.BinBalance_GrsWeightBegin,
                    binBalance_GrsWeightBegin_Index = query.BinBalance_GrsWeightBegin_Index,
                    binBalance_GrsWeightBegin_Id = query.BinBalance_GrsWeightBegin_Id,
                    binBalance_GrsWeightBegin_Name = query.BinBalance_GrsWeightBegin_Name,
                    binBalance_GrsWeightBeginRatio = query.BinBalance_GrsWeightBeginRatio,
                    binBalance_WidthBegin = query.BinBalance_WidthBegin,
                    binBalance_WidthBegin_Index = query.BinBalance_WidthBegin_Index,
                    binBalance_WidthBegin_Id = query.BinBalance_WidthBegin_Id,
                    binBalance_WidthBegin_Name = query.BinBalance_WidthBegin_Name,
                    binBalance_WidthBeginRatio = query.BinBalance_WidthBeginRatio,
                    binBalance_LengthBegin = query.BinBalance_LengthBegin,
                    binBalance_LengthBegin_Index = query.BinBalance_LengthBegin_Index,
                    binBalance_LengthBegin_Id = query.BinBalance_LengthBegin_Id,
                    binBalance_LengthBegin_Name = query.BinBalance_LengthBegin_Name,
                    binBalance_LengthBeginRatio = query.BinBalance_LengthBeginRatio,
                    binBalance_HeightBegin = query.BinBalance_HeightBegin,
                    binBalance_HeightBegin_Index = query.BinBalance_HeightBegin_Index,
                    binBalance_HeightBegin_Id = query.BinBalance_HeightBegin_Id,
                    binBalance_HeightBegin_Name = query.BinBalance_HeightBegin_Name,
                    binBalance_HeightBeginRatio = query.BinBalance_HeightBeginRatio,
                    binBalance_UnitVolumeBegin = query.BinBalance_UnitVolumeBegin,
                    binBalance_VolumeBegin = query.BinBalance_VolumeBegin,
                    binBalance_QtyBal = query.BinBalance_QtyBal,
                    binBalance_WeightBal = query.BinBalance_WeightBal,
                    binBalance_UnitWeightBal_Index = query.BinBalance_UnitWeightBal_Index,
                    binBalance_UnitWeightBal_Id = query.BinBalance_UnitWeightBal_Id,
                    binBalance_UnitWeightBal_Name = query.BinBalance_UnitWeightBal_Name,
                    binBalance_UnitWeightBalRatio = query.BinBalance_UnitWeightBalRatio,
                    binBalance_UnitWeightBal = query.BinBalance_UnitWeightBal,
                    binBalance_WeightBal_Index = query.BinBalance_WeightBal_Index,
                    binBalance_WeightBal_Id = query.BinBalance_WeightBal_Id,
                    binBalance_WeightBal_Name = query.BinBalance_WeightBal_Name,
                    binBalance_WeightBalRatio = query.BinBalance_WeightBalRatio,
                    binBalance_UnitNetWeightBal = query.BinBalance_UnitNetWeightBal,
                    binBalance_UnitNetWeightBal_Index = query.BinBalance_UnitNetWeightBal_Index,
                    binBalance_UnitNetWeightBal_Id = query.BinBalance_UnitNetWeightBal_Id,
                    binBalance_UnitNetWeightBal_Name = query.BinBalance_UnitNetWeightBal_Name,
                    binBalance_UnitNetWeightBalRatio = query.BinBalance_UnitNetWeightBalRatio,
                    binBalance_NetWeightBal = query.BinBalance_NetWeightBal,
                    binBalance_NetWeightBal_Index = query.BinBalance_NetWeightBal_Index,
                    binBalance_NetWeightBal_Id = query.BinBalance_NetWeightBal_Id,
                    binBalance_NetWeightBal_Name = query.BinBalance_NetWeightBal_Name,
                    binBalance_NetWeightBalRatio = query.BinBalance_NetWeightBalRatio,
                    binBalance_UnitGrsWeightBal = query.BinBalance_UnitGrsWeightBal,
                    binBalance_UnitGrsWeightBal_Index = query.BinBalance_UnitGrsWeightBal_Index,
                    binBalance_UnitGrsWeightBal_Id = query.BinBalance_UnitGrsWeightBal_Id,
                    binBalance_UnitGrsWeightBal_Name = query.BinBalance_UnitGrsWeightBal_Name,
                    binBalance_UnitGrsWeightBalRatio = query.BinBalance_UnitGrsWeightBalRatio,
                    binBalance_GrsWeightBal = query.BinBalance_GrsWeightBal,
                    binBalance_GrsWeightBal_Index = query.BinBalance_GrsWeightBal_Index,
                    binBalance_GrsWeightBal_Id = query.BinBalance_GrsWeightBal_Id,
                    binBalance_GrsWeightBal_Name = query.BinBalance_GrsWeightBal_Name,
                    binBalance_GrsWeightBalRatio = query.BinBalance_GrsWeightBalRatio,
                    binBalance_UnitWidthBal = query.BinBalance_UnitWidthBal,
                    binBalance_UnitWidthBal_Index = query.BinBalance_UnitWidthBal_Index,
                    binBalance_UnitWidthBal_Id = query.BinBalance_UnitWidthBal_Id,
                    binBalance_UnitWidthBal_Name = query.BinBalance_UnitWidthBal_Name,
                    binBalance_UnitWidthBalRatio = query.BinBalance_UnitWidthBalRatio,
                    binBalance_WidthBal = query.BinBalance_WidthBal,
                    binBalance_WidthBal_Index = query.BinBalance_WidthBal_Index,
                    binBalance_WidthBal_Id = query.BinBalance_WidthBal_Id,
                    binBalance_WidthBal_Name = query.BinBalance_WidthBal_Name,
                    binBalance_WidthBalRatio = query.BinBalance_WidthBalRatio,
                    binBalance_UnitLengthBal = query.BinBalance_UnitLengthBal,
                    binBalance_UnitLengthBal_Index = query.BinBalance_UnitLengthBal_Index,
                    binBalance_UnitLengthBal_Id = query.BinBalance_UnitLengthBal_Id,
                    binBalance_UnitLengthBal_Name = query.BinBalance_UnitLengthBal_Name,
                    binBalance_UnitLengthBalRatio = query.BinBalance_UnitLengthBalRatio,
                    binBalance_LengthBal = query.BinBalance_LengthBal,
                    binBalance_LengthBal_Index = query.BinBalance_LengthBal_Index,
                    binBalance_LengthBal_Id = query.BinBalance_LengthBal_Id,
                    binBalance_LengthBal_Name = query.BinBalance_LengthBal_Name,
                    binBalance_LengthBalRatio = query.BinBalance_LengthBalRatio,
                    binBalance_UnitHeightBal = query.BinBalance_UnitHeightBal,
                    binBalance_UnitHeightBal_Index = query.BinBalance_UnitHeightBal_Index,
                    binBalance_UnitHeightBal_Id = query.BinBalance_UnitHeightBal_Id,
                    binBalance_UnitHeightBal_Name = query.BinBalance_UnitHeightBal_Name,
                    binBalance_UnitHeightBalRatio = query.BinBalance_UnitHeightBalRatio,
                    binBalance_HeightBal = query.BinBalance_HeightBal,
                    binBalance_HeightBal_Index = query.BinBalance_HeightBal_Index,
                    binBalance_HeightBal_Id = query.BinBalance_HeightBal_Id,
                    binBalance_HeightBal_Name = query.BinBalance_HeightBal_Name,
                    binBalance_HeightBalRatio = query.BinBalance_HeightBalRatio,
                    binBalance_UnitVolumeBal = query.BinBalance_UnitVolumeBal,
                    binBalance_VolumeBal = query.BinBalance_VolumeBal,
                    binBalance_QtyReserve = query.BinBalance_QtyReserve,
                    binBalance_WeightReserve = query.BinBalance_WeightReserve,
                    binBalance_WeightReserve_Index = query.BinBalance_WeightReserve_Index,
                    binBalance_WeightReserve_Id = query.BinBalance_WeightReserve_Id,
                    binBalance_WeightReserve_Name = query.BinBalance_WeightReserve_Name,
                    binBalance_WeightReserveRatio = query.BinBalance_WeightReserveRatio,
                    binBalance_NetWeightReserve = query.BinBalance_NetWeightReserve,
                    binBalance_NetWeightReserve_Index = query.BinBalance_NetWeightReserve_Index,
                    binBalance_NetWeightReserve_Id = query.BinBalance_NetWeightReserve_Id,
                    binBalance_NetWeightReserve_Name = query.BinBalance_NetWeightReserve_Name,
                    binBalance_NetWeightReserveRatio = query.BinBalance_NetWeightReserveRatio,
                    binBalance_GrsWeightReserve = query.BinBalance_GrsWeightReserve,
                    binBalance_GrsWeightReserve_Index = query.BinBalance_GrsWeightReserve_Index,
                    binBalance_GrsWeightReserve_Id = query.BinBalance_GrsWeightReserve_Id,
                    binBalance_GrsWeightReserve_Name = query.BinBalance_GrsWeightReserve_Name,
                    binBalance_GrsWeightReserveRatio = query.BinBalance_GrsWeightReserveRatio,
                    binBalance_WidthReserve = query.BinBalance_WidthReserve,
                    binBalance_WidthReserve_Index = query.BinBalance_WidthReserve_Index,
                    binBalance_WidthReserve_Id = query.BinBalance_WidthReserve_Id,
                    binBalance_WidthReserve_Name = query.BinBalance_WidthReserve_Name,
                    binBalance_WidthReserveRatio = query.BinBalance_WidthReserveRatio,
                    binBalance_LengthReserve = query.BinBalance_LengthReserve,
                    binBalance_LengthReserve_Index = query.BinBalance_LengthReserve_Index,
                    binBalance_LengthReserve_Id = query.BinBalance_LengthReserve_Id,
                    binBalance_LengthReserve_Name = query.BinBalance_LengthReserve_Name,
                    binBalance_LengthReserveRatio = query.BinBalance_LengthReserveRatio,
                    binBalance_HeightReserve = query.BinBalance_HeightReserve,
                    binBalance_HeightReserve_Index = query.BinBalance_HeightReserve_Index,
                    binBalance_HeightReserve_Id = query.BinBalance_HeightReserve_Id,
                    binBalance_HeightReserve_Name = query.BinBalance_HeightReserve_Name,
                    binBalance_HeightReserveRatio = query.BinBalance_HeightReserveRatio,
                    binBalance_UnitVolumeReserve = query.BinBalance_UnitVolumeReserve,
                    binBalance_VolumeReserve = query.BinBalance_VolumeReserve,

                    productConversion_Index = query.ProductConversion_Index,
                    productConversion_Id = query.ProductConversion_Id,
                    productConversion_Name = query.ProductConversion_Name,

                    unitPrice = query.UnitPrice,
                    unitPrice_Index = query.UnitPrice_Index,
                    unitPrice_Id = query.UnitPrice_Id,
                    unitPrice_Name = query.UnitPrice_Name,
                    price = query.Price,
                    price_Index = query.Price_Index,
                    price_Id = query.Price_Id,
                    price_Name = query.Price_Name,


                    uDF_1 = query.UDF_1,
                    uDF_2 = query.UDF_2,
                    uDF_3 = query.UDF_3,
                    uDF_4 = query.UDF_4,
                    uDF_5 = query.UDF_5,
                    create_By = query.Create_By,
                    create_Date = query.Create_Date,
                    update_By = query.Update_By,
                    update_Date = query.Update_Date,
                    cancel_By = query.Cancel_By,
                    cancel_Date = query.Cancel_Date,
                    isUse = query.IsUse,
                    binBalance_Status = query.BinBalance_Status,

                    invoice_No = query.Invoice_No,
                    declaration_No = query.Declaration_No,
                    hs_Code = query.HS_Code,
                    conutry_of_Origin = query.Conutry_of_Origin,
                    tax1 = query.Tax1,
                    tax1_Currency_Index = query.Tax1_Currency_Index,
                    tax1_Currency_Id = query.Tax1_Currency_Id,
                    tax1_Currency_Name = query.Tax1_Currency_Name,
                    tax2 = query.Tax2,
                    tax2_Currency_Index = query.Tax2_Currency_Index,
                    tax2_Currency_Id = query.Tax2_Currency_Id,
                    tax2_Currency_Name = query.Tax2_Currency_Name,
                    tax3 = query.Tax3,
                    tax3_Currency_Index = query.Tax3_Currency_Index,
                    tax3_Currency_Id = query.Tax3_Currency_Id,
                    tax3_Currency_Name = query.Tax3_Currency_Name,
                    tax4 = query.Tax4,
                    tax4_Currency_Index = query.Tax4_Currency_Index,
                    tax4_Currency_Id = query.Tax4_Currency_Id,
                    tax4_Currency_Name = query.Tax4_Currency_Name,
                    tax5 = query.Tax5,
                    tax5_Currency_Index = query.Tax5_Currency_Index,
                    tax5_Currency_Id = query.Tax5_Currency_Id,
                    tax5_Currency_Name = query.Tax5_Currency_Name,
                    erp_Location = query.ERP_Location,
                };

                item.resultIsUse = true;
                return item;
            }
            catch (Exception ex)
            {
                var item = new BinBalanceViewModel();
                item.resultIsUse = false;
                item.resultMsg = ex.Message;
                return item;
            }
        }

        public BinBalanceViewModel findLocationNow(PickbinbalanceViewModel model)
        {
            try
            {
                var query = db.wm_BinBalance.FirstOrDefault(c=> c.Tag_No == model.tag_No);

                var item = new BinBalanceViewModel
                {
                    binBalance_Index = query.BinBalance_Index,
                    owner_Index = query.Owner_Index,
                    owner_Id = query.Owner_Id,
                    owner_Name = query.Owner_Name,
                    location_Index = query.Location_Index,
                    location_Id = query.Location_Id,
                    location_Name = query.Location_Name,
                    goodsReceive_Index = query.GoodsReceive_Index,
                    goodsReceive_No = query.GoodsReceive_No,
                    goodsReceive_Date = query.GoodsReceive_Date,
                    goodsReceiveItem_Index = query.GoodsReceiveItem_Index,
                    goodsReceiveItemLocation_Index = query.GoodsReceiveItemLocation_Index,
                    tagItem_Index = query.TagItem_Index,
                    tag_Index = query.Tag_Index,
                    tag_No = query.Tag_No,
                    product_Index = query.Product_Index,
                    product_Id = query.Product_Id,
                    product_Name = query.Product_Name,
                    product_SecondName = query.Product_SecondName,
                    product_ThirdName = query.Product_ThirdName,
                    product_Lot = query.Product_Lot,
                    itemStatus_Index = query.ItemStatus_Index,
                    itemStatus_Id = query.ItemStatus_Id,
                    itemStatus_Name = query.ItemStatus_Name,
                    goodsReceive_MFG_Date = query.GoodsReceive_MFG_Date,
                    goodsReceive_EXP_Date = query.GoodsReceive_EXP_Date,
                    goodsReceive_ProductConversion_Index = query.GoodsReceive_ProductConversion_Index,
                    goodsReceive_ProductConversion_Id = query.GoodsReceive_ProductConversion_Id,
                    goodsReceive_ProductConversion_Name = query.GoodsReceive_ProductConversion_Name,
                    
                };

                item.resultIsUse = true;
                return item;
            }
            catch (Exception ex)
            {
                var item = new BinBalanceViewModel();
                item.resultIsUse = false;
                item.resultMsg = ex.Message;
                return item;
            }
        }

        public BinBalanceViewModel getCheckStock(CheckStockViewModel model)
        {
            try
            {
                var queryBin = db.View_CheckStock.Where(c => c.Product_Id == model.product_Id  && c.ItemStatus_Index == model.ItemStatus_Index && c.ERP_Location == model.ERP_Location ).ToList(); ;

                var result = new BinBalanceViewModel();
                foreach (var query in queryBin)
                {
                    var item = new BinBalanceViewModel
                    {
                        product_Id = query.Product_Id,
                        product_Name = query.Product_Name.ToUpper(),
                        product_Lot = query.Product_Lot,
                        itemStatus_Index = query.ItemStatus_Index,
                        location_Index = query.Location_Index,
                        erp_Location = query.ERP_Location,
                        binBalance_QtyBal = query.qty_Bal,
                        binBalance_QtyReserve = query.qty_Reserve,
                        binBalance_QtyBegin = query.qty_Bal - query.qty_Reserve,
                        remark = query.remark
                    };
                    result = item;
                }

                return result;
            }
            catch (Exception ex)
            {
                var item = new BinBalanceViewModel();
                item.resultIsUse = false;
                item.resultMsg = ex.Message;
                return item;
            }
        }

        public List<BinBalanceViewModel> getBinbalance(FilterBinbalanceViewModel model)
        {
            try
            {
                var items = new List<BinBalanceViewModel>();
                db.Database.SetCommandTimeout(360);
                var querys = db.wm_BinBalance.AsQueryable();

                if (model.Owner_Index.ToString() != "" && model.Owner_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Owner_Index == model.Owner_Index);
                }
                if (!string.IsNullOrEmpty(model.Tag_No))
                {
                    querys = querys.Where(c => c.Tag_No == model.Tag_No);
                }
                if (!string.IsNullOrEmpty(model.Product_Index.ToString()) && model.Product_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Product_Index == model.Product_Index);
                }

                if (model.Product_Lot != null)
                {
                    if (model.Product_Lot.ToString() != "")
                    {
                        querys = querys.Where(c => c.Product_Lot == model.Product_Lot);
                    }
                }
                if (!string.IsNullOrEmpty(model.ItemStatus_Index.ToString()) && model.ItemStatus_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.ItemStatus_Index == model.ItemStatus_Index);
                }
                if (model.MFG_Date != null)
                {
                    if (model.MFG_Date.ToString() != "")
                    {

                    }
                }
                if (model.EXP_Date != null)
                {
                    if (model.EXP_Date.ToString() != "")
                    {

                    }
                }
                if (model.isUseAttribute == true)
                {
                    // ADD UDF 1-5 
                    if (model.UDF_1 != null)
                    {
                        querys = querys.Where(c => c.UDF_1 == model.UDF_1);
                    }

                    if (model.UDF_2 != null)
                    {
                        querys = querys.Where(c => c.UDF_2 == model.UDF_2);
                    }

                    if (model.UDF_3 != null)
                    {
                        querys = querys.Where(c => c.UDF_3 == model.UDF_3);
                    }

                    if (model.UDF_4 != null)
                    {
                        querys = querys.Where(c => c.UDF_4 == model.UDF_4);
                    }

                    if (model.UDF_5 != null)
                    {
                        querys = querys.Where(c => c.UDF_5 == model.UDF_5);
                    }
                }

                if (!string.IsNullOrEmpty(model.isuse))
                {
                    querys = querys.Where(c => c.IsUse == model.isuse);
                }

                var listdata = querys.ToList();

                foreach (var query in listdata)
                {
                    var item = new BinBalanceViewModel
                    {
                        binBalance_Index = query.BinBalance_Index,
                        owner_Index = query.Owner_Index,
                        owner_Id = query.Owner_Id,
                        owner_Name = query.Owner_Name,
                        location_Index = query.Location_Index,
                        location_Id = query.Location_Id,
                        location_Name = query.Location_Name,
                        goodsReceive_Index = query.GoodsReceive_Index,
                        goodsReceive_No = query.GoodsReceive_No,
                        goodsReceive_Date = query.GoodsReceive_Date,
                        goodsReceiveItem_Index = query.GoodsReceiveItem_Index,
                        goodsReceiveItemLocation_Index = query.GoodsReceiveItemLocation_Index,
                        tagItem_Index = query.TagItem_Index,
                        tag_Index = query.Tag_Index,
                        tag_No = query.Tag_No,
                        product_Index = query.Product_Index,
                        product_Id = query.Product_Id,
                        product_Name = query.Product_Name,
                        product_SecondName = query.Product_SecondName,
                        product_ThirdName = query.Product_ThirdName,
                        product_Lot = query.Product_Lot,
                        itemStatus_Index = query.ItemStatus_Index,
                        itemStatus_Id = query.ItemStatus_Id,
                        itemStatus_Name = query.ItemStatus_Name,
                        goodsReceive_MFG_Date = query.GoodsReceive_MFG_Date,
                        goodsReceive_EXP_Date = query.GoodsReceive_EXP_Date,
                        goodsReceive_ProductConversion_Index = query.GoodsReceive_ProductConversion_Index,
                        goodsReceive_ProductConversion_Id = query.GoodsReceive_ProductConversion_Id,
                        goodsReceive_ProductConversion_Name = query.GoodsReceive_ProductConversion_Name,

                        binBalance_Ratio = query.BinBalance_Ratio,
                        binBalance_QtyBegin = query.BinBalance_QtyBegin,
                        binBalance_WeightBegin = query.BinBalance_WeightBegin,
                        binBalance_WeightBegin_Index = query.BinBalance_WeightBegin_Index,
                        binBalance_WeightBegin_Id = query.BinBalance_WeightBegin_Id,
                        binBalance_WeightBegin_Name = query.BinBalance_WeightBegin_Name,
                        binBalance_WeightBeginRatio = query.BinBalance_WeightBeginRatio,
                        binBalance_NetWeightBegin = query.BinBalance_NetWeightBegin,
                        binBalance_NetWeightBegin_Index = query.BinBalance_NetWeightBegin_Index,
                        binBalance_NetWeightBegin_Id = query.BinBalance_NetWeightBegin_Id,
                        binBalance_NetWeightBegin_Name = query.BinBalance_NetWeightBegin_Name,
                        binBalance_NetWeightBeginRatio = query.BinBalance_NetWeightBeginRatio,
                        binBalance_GrsWeightBegin = query.BinBalance_GrsWeightBegin,
                        binBalance_GrsWeightBegin_Index = query.BinBalance_GrsWeightBegin_Index,
                        binBalance_GrsWeightBegin_Id = query.BinBalance_GrsWeightBegin_Id,
                        binBalance_GrsWeightBegin_Name = query.BinBalance_GrsWeightBegin_Name,
                        binBalance_GrsWeightBeginRatio = query.BinBalance_GrsWeightBeginRatio,
                        binBalance_WidthBegin = query.BinBalance_WidthBegin,
                        binBalance_WidthBegin_Index = query.BinBalance_WidthBegin_Index,
                        binBalance_WidthBegin_Id = query.BinBalance_WidthBegin_Id,
                        binBalance_WidthBegin_Name = query.BinBalance_WidthBegin_Name,
                        binBalance_WidthBeginRatio = query.BinBalance_WidthBeginRatio,
                        binBalance_LengthBegin = query.BinBalance_LengthBegin,
                        binBalance_LengthBegin_Index = query.BinBalance_LengthBegin_Index,
                        binBalance_LengthBegin_Id = query.BinBalance_LengthBegin_Id,
                        binBalance_LengthBegin_Name = query.BinBalance_LengthBegin_Name,
                        binBalance_LengthBeginRatio = query.BinBalance_LengthBeginRatio,
                        binBalance_HeightBegin = query.BinBalance_HeightBegin,
                        binBalance_HeightBegin_Index = query.BinBalance_HeightBegin_Index,
                        binBalance_HeightBegin_Id = query.BinBalance_HeightBegin_Id,
                        binBalance_HeightBegin_Name = query.BinBalance_HeightBegin_Name,
                        binBalance_HeightBeginRatio = query.BinBalance_HeightBeginRatio,
                        binBalance_UnitVolumeBegin = query.BinBalance_UnitVolumeBegin,
                        binBalance_VolumeBegin = query.BinBalance_VolumeBegin,
                        binBalance_QtyBal = query.BinBalance_QtyBal,
                        binBalance_WeightBal = query.BinBalance_WeightBal,
                        binBalance_UnitWeightBal_Index = query.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = query.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = query.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = query.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitWeightBal = query.BinBalance_UnitWeightBal,
                        binBalance_WeightBal_Index = query.BinBalance_WeightBal_Index,
                        binBalance_WeightBal_Id = query.BinBalance_WeightBal_Id,
                        binBalance_WeightBal_Name = query.BinBalance_WeightBal_Name,
                        binBalance_WeightBalRatio = query.BinBalance_WeightBalRatio,
                        binBalance_UnitNetWeightBal = query.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = query.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = query.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = query.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = query.BinBalance_UnitNetWeightBalRatio,
                        binBalance_NetWeightBal = query.BinBalance_NetWeightBal,
                        binBalance_NetWeightBal_Index = query.BinBalance_NetWeightBal_Index,
                        binBalance_NetWeightBal_Id = query.BinBalance_NetWeightBal_Id,
                        binBalance_NetWeightBal_Name = query.BinBalance_NetWeightBal_Name,
                        binBalance_NetWeightBalRatio = query.BinBalance_NetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = query.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = query.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = query.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = query.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = query.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_GrsWeightBal = query.BinBalance_GrsWeightBal,
                        binBalance_GrsWeightBal_Index = query.BinBalance_GrsWeightBal_Index,
                        binBalance_GrsWeightBal_Id = query.BinBalance_GrsWeightBal_Id,
                        binBalance_GrsWeightBal_Name = query.BinBalance_GrsWeightBal_Name,
                        binBalance_GrsWeightBalRatio = query.BinBalance_GrsWeightBalRatio,
                        binBalance_UnitWidthBal = query.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = query.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = query.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = query.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = query.BinBalance_UnitWidthBalRatio,
                        binBalance_WidthBal = query.BinBalance_WidthBal,
                        binBalance_WidthBal_Index = query.BinBalance_WidthBal_Index,
                        binBalance_WidthBal_Id = query.BinBalance_WidthBal_Id,
                        binBalance_WidthBal_Name = query.BinBalance_WidthBal_Name,
                        binBalance_WidthBalRatio = query.BinBalance_WidthBalRatio,
                        binBalance_UnitLengthBal = query.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = query.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = query.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = query.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = query.BinBalance_UnitLengthBalRatio,
                        binBalance_LengthBal = query.BinBalance_LengthBal,
                        binBalance_LengthBal_Index = query.BinBalance_LengthBal_Index,
                        binBalance_LengthBal_Id = query.BinBalance_LengthBal_Id,
                        binBalance_LengthBal_Name = query.BinBalance_LengthBal_Name,
                        binBalance_LengthBalRatio = query.BinBalance_LengthBalRatio,
                        binBalance_UnitHeightBal = query.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = query.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = query.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = query.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = query.BinBalance_UnitHeightBalRatio,
                        binBalance_HeightBal = query.BinBalance_HeightBal,
                        binBalance_HeightBal_Index = query.BinBalance_HeightBal_Index,
                        binBalance_HeightBal_Id = query.BinBalance_HeightBal_Id,
                        binBalance_HeightBal_Name = query.BinBalance_HeightBal_Name,
                        binBalance_HeightBalRatio = query.BinBalance_HeightBalRatio,
                        binBalance_UnitVolumeBal = query.BinBalance_UnitVolumeBal,
                        binBalance_VolumeBal = query.BinBalance_VolumeBal,
                        binBalance_QtyReserve = query.BinBalance_QtyReserve,
                        binBalance_WeightReserve = query.BinBalance_WeightReserve,
                        binBalance_WeightReserve_Index = query.BinBalance_WeightReserve_Index,
                        binBalance_WeightReserve_Id = query.BinBalance_WeightReserve_Id,
                        binBalance_WeightReserve_Name = query.BinBalance_WeightReserve_Name,
                        binBalance_WeightReserveRatio = query.BinBalance_WeightReserveRatio,
                        binBalance_NetWeightReserve = query.BinBalance_NetWeightReserve,
                        binBalance_NetWeightReserve_Index = query.BinBalance_NetWeightReserve_Index,
                        binBalance_NetWeightReserve_Id = query.BinBalance_NetWeightReserve_Id,
                        binBalance_NetWeightReserve_Name = query.BinBalance_NetWeightReserve_Name,
                        binBalance_NetWeightReserveRatio = query.BinBalance_NetWeightReserveRatio,
                        binBalance_GrsWeightReserve = query.BinBalance_GrsWeightReserve,
                        binBalance_GrsWeightReserve_Index = query.BinBalance_GrsWeightReserve_Index,
                        binBalance_GrsWeightReserve_Id = query.BinBalance_GrsWeightReserve_Id,
                        binBalance_GrsWeightReserve_Name = query.BinBalance_GrsWeightReserve_Name,
                        binBalance_GrsWeightReserveRatio = query.BinBalance_GrsWeightReserveRatio,
                        binBalance_WidthReserve = query.BinBalance_WidthReserve,
                        binBalance_WidthReserve_Index = query.BinBalance_WidthReserve_Index,
                        binBalance_WidthReserve_Id = query.BinBalance_WidthReserve_Id,
                        binBalance_WidthReserve_Name = query.BinBalance_WidthReserve_Name,
                        binBalance_WidthReserveRatio = query.BinBalance_WidthReserveRatio,
                        binBalance_LengthReserve = query.BinBalance_LengthReserve,
                        binBalance_LengthReserve_Index = query.BinBalance_LengthReserve_Index,
                        binBalance_LengthReserve_Id = query.BinBalance_LengthReserve_Id,
                        binBalance_LengthReserve_Name = query.BinBalance_LengthReserve_Name,
                        binBalance_LengthReserveRatio = query.BinBalance_LengthReserveRatio,
                        binBalance_HeightReserve = query.BinBalance_HeightReserve,
                        binBalance_HeightReserve_Index = query.BinBalance_HeightReserve_Index,
                        binBalance_HeightReserve_Id = query.BinBalance_HeightReserve_Id,
                        binBalance_HeightReserve_Name = query.BinBalance_HeightReserve_Name,
                        binBalance_HeightReserveRatio = query.BinBalance_HeightReserveRatio,
                        binBalance_UnitVolumeReserve = query.BinBalance_UnitVolumeReserve,
                        binBalance_VolumeReserve = query.BinBalance_VolumeReserve,

                        productConversion_Index = query.ProductConversion_Index,
                        productConversion_Id = query.ProductConversion_Id,
                        productConversion_Name = query.ProductConversion_Name,

                        unitPrice = query.UnitPrice,
                        unitPrice_Index = query.UnitPrice_Index,
                        unitPrice_Id = query.UnitPrice_Id,
                        unitPrice_Name = query.UnitPrice_Name,
                        price = query.Price,
                        price_Index = query.Price_Index,
                        price_Id = query.Price_Id,
                        price_Name = query.Price_Name,

                        uDF_1 = query.UDF_1,
                        uDF_2 = query.UDF_2,
                        uDF_3 = query.UDF_3,
                        uDF_4 = query.UDF_4,
                        uDF_5 = query.UDF_5,
                        create_By = query.Create_By,
                        create_Date = query.Create_Date,
                        update_By = query.Update_By,
                        update_Date = query.Update_Date,
                        cancel_By = query.Cancel_By,
                        cancel_Date = query.Cancel_Date,
                        isUse = query.IsUse,
                        binBalance_Status = query.BinBalance_Status,
                        //ageRemain = query.AgeRemain

                        invoice_No = query.Invoice_No,
                        declaration_No = query.Declaration_No,
                        hs_Code = query.HS_Code,
                        conutry_of_Origin = query.Conutry_of_Origin,
                        tax1 = query.Tax1,
                        tax1_Currency_Index = query.Tax1_Currency_Index,
                        tax1_Currency_Id = query.Tax1_Currency_Id,
                        tax1_Currency_Name = query.Tax1_Currency_Name,
                        tax2 = query.Tax2,
                        tax2_Currency_Index = query.Tax2_Currency_Index,
                        tax2_Currency_Id = query.Tax2_Currency_Id,
                        tax2_Currency_Name = query.Tax2_Currency_Name,
                        tax3 = query.Tax3,
                        tax3_Currency_Index = query.Tax3_Currency_Index,
                        tax3_Currency_Id = query.Tax3_Currency_Id,
                        tax3_Currency_Name = query.Tax3_Currency_Name,
                        tax4 = query.Tax4,
                        tax4_Currency_Index = query.Tax4_Currency_Index,
                        tax4_Currency_Id = query.Tax4_Currency_Id,
                        tax4_Currency_Name = query.Tax4_Currency_Name,
                        tax5 = query.Tax5,
                        tax5_Currency_Index = query.Tax5_Currency_Index,
                        tax5_Currency_Id = query.Tax5_Currency_Id,
                        tax5_Currency_Name = query.Tax5_Currency_Name,

                    };
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BinBalanceViewModel> getViewBinbalance(FilterBinbalanceViewModel model)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            db.Database.SetCommandTimeout(180);
            try
            {
                olog.logging("getViewBinbalance", State + " " + model.sJson());

                var items = new List<BinBalanceViewModel>();

                var querys = db.View_WaveBinBalance.AsQueryable();

                if (model.Owner_Index.ToString() != "" && model.Owner_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Owner_Index == model.Owner_Index);
                }

                if (model.Location_Index.ToString() != "" && model.Location_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Location_Index == model.Location_Index);
                }

                if (!string.IsNullOrEmpty(model.Tag_No))
                {
                    querys = querys.Where(c => c.Tag_No == model.Tag_No);
                }
                if (!string.IsNullOrEmpty(model.Product_Index.ToString()) && model.Product_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Product_Index == model.Product_Index);
                }

                if (model.Product_Lot != null)
                {
                    if (model.Product_Lot.ToString() != "")
                    {
                        querys = querys.Where(c => (c.Product_Lot ?? "")  == model.Product_Lot);
                    }
                }
                if (!string.IsNullOrEmpty(model.ItemStatus_Index.ToString()) && model.ItemStatus_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.ItemStatus_Index == model.ItemStatus_Index);
                }
                if ((model.qtyPreTag ?? 0) > 0)
                {
                    querys = querys.Where(c => (c.BinBalance_QtyBal - c.BinBalance_QtyReserve) == model.qtyPreTag);
                }
                if (model.MFG_Date != null)
                {
                    if (model.MFG_Date.ToString() != "")
                    {

                    }
                }
                if (model.EXP_Date != null)
                {
                    if (model.EXP_Date.ToString() != "")
                    {

                    }
                }
                if (model.isUseAttribute == true)
                {
                    // ADD UDF 1-5 
                    if (model.UDF_1 != null)
                    {
                     
                        querys = querys.Where(c => (c.UDF_1 ?? "")  == model.UDF_1);
                    }

                    if (model.UDF_2 != null)
                    {
                        querys = querys.Where(c => (c.UDF_2 ?? "") == model.UDF_2);
                    }

                    if (model.UDF_3 != null)
                    {
                        querys = querys.Where(c => (c.UDF_3 ?? "") == model.UDF_3);
                    }

                    if (model.UDF_4 != null)
                    {
                        querys = querys.Where(c => (c.UDF_4 ?? "") == model.UDF_4);
                    }

                    if (model.UDF_5 != null)
                    {
                        querys = querys.Where(c => (c.UDF_5 ?? "") == model.UDF_5);
                    }
                }

                if (!string.IsNullOrEmpty(model.isuse))
                {
                    querys = querys.Where(c => c.IsUse == model.isuse);
                }

                var listdata = querys.ToList();

                foreach (var query in listdata)
                {
                    var item = new BinBalanceViewModel
                    {
                        binBalance_Index = query.BinBalance_Index,
                        owner_Index = query.Owner_Index,
                        owner_Id = query.Owner_Id,
                        owner_Name = query.Owner_Name,
                        location_Index = query.Location_Index,
                        location_Id = query.Location_Id,
                        location_Name = query.Location_Name,
                        goodsReceive_Index = query.GoodsReceive_Index,
                        goodsReceive_No = query.GoodsReceive_No,
                        goodsReceive_Date = query.GoodsReceive_Date,
                        goodsReceiveItem_Index = query.GoodsReceiveItem_Index,
                        goodsReceiveItemLocation_Index = query.GoodsReceiveItemLocation_Index,
                        tagItem_Index = query.TagItem_Index,
                        tag_Index = query.Tag_Index,
                        tag_No = query.Tag_No,
                        product_Index = query.Product_Index,
                        product_Id = query.Product_Id,
                        product_Name = query.Product_Name,
                        product_SecondName = query.Product_SecondName,
                        product_ThirdName = query.Product_ThirdName,
                        product_Lot = query.Product_Lot,
                        itemStatus_Index = query.ItemStatus_Index,
                        itemStatus_Id = query.ItemStatus_Id,
                        itemStatus_Name = query.ItemStatus_Name,
                        goodsReceive_MFG_Date = query.GoodsReceive_MFG_Date,
                        goodsReceive_EXP_Date = query.GoodsReceive_EXP_Date,
                        goodsReceive_ProductConversion_Index = query.GoodsReceive_ProductConversion_Index,
                        goodsReceive_ProductConversion_Id = query.GoodsReceive_ProductConversion_Id,
                        goodsReceive_ProductConversion_Name = query.GoodsReceive_ProductConversion_Name,


                        binBalance_Ratio = query.BinBalance_Ratio,
                        binBalance_QtyBegin = query.BinBalance_QtyBegin,
                        binBalance_WeightBegin = query.BinBalance_WeightBegin,
                        binBalance_WeightBegin_Index = query.BinBalance_WeightBegin_Index,
                        binBalance_WeightBegin_Id = query.BinBalance_WeightBegin_Id,
                        binBalance_WeightBegin_Name = query.BinBalance_WeightBegin_Name,
                        binBalance_WeightBeginRatio = query.BinBalance_WeightBeginRatio,
                        binBalance_NetWeightBegin = query.BinBalance_NetWeightBegin,
                        binBalance_NetWeightBegin_Index = query.BinBalance_NetWeightBegin_Index,
                        binBalance_NetWeightBegin_Id = query.BinBalance_NetWeightBegin_Id,
                        binBalance_NetWeightBegin_Name = query.BinBalance_NetWeightBegin_Name,
                        binBalance_NetWeightBeginRatio = query.BinBalance_NetWeightBeginRatio,
                        binBalance_GrsWeightBegin = query.BinBalance_GrsWeightBegin,
                        binBalance_GrsWeightBegin_Index = query.BinBalance_GrsWeightBegin_Index,
                        binBalance_GrsWeightBegin_Id = query.BinBalance_GrsWeightBegin_Id,
                        binBalance_GrsWeightBegin_Name = query.BinBalance_GrsWeightBegin_Name,
                        binBalance_GrsWeightBeginRatio = query.BinBalance_GrsWeightBeginRatio,
                        binBalance_WidthBegin = query.BinBalance_WidthBegin,
                        binBalance_WidthBegin_Index = query.BinBalance_WidthBegin_Index,
                        binBalance_WidthBegin_Id = query.BinBalance_WidthBegin_Id,
                        binBalance_WidthBegin_Name = query.BinBalance_WidthBegin_Name,
                        binBalance_WidthBeginRatio = query.BinBalance_WidthBeginRatio,
                        binBalance_LengthBegin = query.BinBalance_LengthBegin,
                        binBalance_LengthBegin_Index = query.BinBalance_LengthBegin_Index,
                        binBalance_LengthBegin_Id = query.BinBalance_LengthBegin_Id,
                        binBalance_LengthBegin_Name = query.BinBalance_LengthBegin_Name,
                        binBalance_LengthBeginRatio = query.BinBalance_LengthBeginRatio,
                        binBalance_HeightBegin = query.BinBalance_HeightBegin,
                        binBalance_HeightBegin_Index = query.BinBalance_HeightBegin_Index,
                        binBalance_HeightBegin_Id = query.BinBalance_HeightBegin_Id,
                        binBalance_HeightBegin_Name = query.BinBalance_HeightBegin_Name,
                        binBalance_HeightBeginRatio = query.BinBalance_HeightBeginRatio,
                        binBalance_UnitVolumeBegin = query.BinBalance_UnitVolumeBegin,
                        binBalance_VolumeBegin = query.BinBalance_VolumeBegin,
                        binBalance_QtyBal = query.BinBalance_QtyBal,
                        binBalance_WeightBal = query.BinBalance_WeightBal,
                        binBalance_UnitWeightBal_Index = query.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = query.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = query.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = query.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitWeightBal = query.BinBalance_UnitWeightBal,
                        binBalance_WeightBal_Index = query.BinBalance_WeightBal_Index,
                        binBalance_WeightBal_Id = query.BinBalance_WeightBal_Id,
                        binBalance_WeightBal_Name = query.BinBalance_WeightBal_Name,
                        binBalance_WeightBalRatio = query.BinBalance_WeightBalRatio,
                        binBalance_UnitNetWeightBal = query.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = query.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = query.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = query.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = query.BinBalance_UnitNetWeightBalRatio,
                        binBalance_NetWeightBal = query.BinBalance_NetWeightBal,
                        binBalance_NetWeightBal_Index = query.BinBalance_NetWeightBal_Index,
                        binBalance_NetWeightBal_Id = query.BinBalance_NetWeightBal_Id,
                        binBalance_NetWeightBal_Name = query.BinBalance_NetWeightBal_Name,
                        binBalance_NetWeightBalRatio = query.BinBalance_NetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = query.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = query.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = query.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = query.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = query.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_GrsWeightBal = query.BinBalance_GrsWeightBal,
                        binBalance_GrsWeightBal_Index = query.BinBalance_GrsWeightBal_Index,
                        binBalance_GrsWeightBal_Id = query.BinBalance_GrsWeightBal_Id,
                        binBalance_GrsWeightBal_Name = query.BinBalance_GrsWeightBal_Name,
                        binBalance_GrsWeightBalRatio = query.BinBalance_GrsWeightBalRatio,
                        binBalance_UnitWidthBal = query.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = query.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = query.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = query.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = query.BinBalance_UnitWidthBalRatio,
                        binBalance_WidthBal = query.BinBalance_WidthBal,
                        binBalance_WidthBal_Index = query.BinBalance_WidthBal_Index,
                        binBalance_WidthBal_Id = query.BinBalance_WidthBal_Id,
                        binBalance_WidthBal_Name = query.BinBalance_WidthBal_Name,
                        binBalance_WidthBalRatio = query.BinBalance_WidthBalRatio,
                        binBalance_UnitLengthBal = query.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = query.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = query.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = query.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = query.BinBalance_UnitLengthBalRatio,
                        binBalance_LengthBal = query.BinBalance_LengthBal,
                        binBalance_LengthBal_Index = query.BinBalance_LengthBal_Index,
                        binBalance_LengthBal_Id = query.BinBalance_LengthBal_Id,
                        binBalance_LengthBal_Name = query.BinBalance_LengthBal_Name,
                        binBalance_LengthBalRatio = query.BinBalance_LengthBalRatio,
                        binBalance_UnitHeightBal = query.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = query.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = query.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = query.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = query.BinBalance_UnitHeightBalRatio,
                        binBalance_HeightBal = query.BinBalance_HeightBal,
                        binBalance_HeightBal_Index = query.BinBalance_HeightBal_Index,
                        binBalance_HeightBal_Id = query.BinBalance_HeightBal_Id,
                        binBalance_HeightBal_Name = query.BinBalance_HeightBal_Name,
                        binBalance_HeightBalRatio = query.BinBalance_HeightBalRatio,
                        binBalance_UnitVolumeBal = query.BinBalance_UnitVolumeBal,
                        binBalance_VolumeBal = query.BinBalance_VolumeBal,
                        binBalance_QtyReserve = query.BinBalance_QtyReserve,
                        binBalance_WeightReserve = query.BinBalance_WeightReserve,
                        binBalance_WeightReserve_Index = query.BinBalance_WeightReserve_Index,
                        binBalance_WeightReserve_Id = query.BinBalance_WeightReserve_Id,
                        binBalance_WeightReserve_Name = query.BinBalance_WeightReserve_Name,
                        binBalance_WeightReserveRatio = query.BinBalance_WeightReserveRatio,
                        binBalance_NetWeightReserve = query.BinBalance_NetWeightReserve,
                        binBalance_NetWeightReserve_Index = query.BinBalance_NetWeightReserve_Index,
                        binBalance_NetWeightReserve_Id = query.BinBalance_NetWeightReserve_Id,
                        binBalance_NetWeightReserve_Name = query.BinBalance_NetWeightReserve_Name,
                        binBalance_NetWeightReserveRatio = query.BinBalance_NetWeightReserveRatio,
                        binBalance_GrsWeightReserve = query.BinBalance_GrsWeightReserve,
                        binBalance_GrsWeightReserve_Index = query.BinBalance_GrsWeightReserve_Index,
                        binBalance_GrsWeightReserve_Id = query.BinBalance_GrsWeightReserve_Id,
                        binBalance_GrsWeightReserve_Name = query.BinBalance_GrsWeightReserve_Name,
                        binBalance_GrsWeightReserveRatio = query.BinBalance_GrsWeightReserveRatio,
                        binBalance_WidthReserve = query.BinBalance_WidthReserve,
                        binBalance_WidthReserve_Index = query.BinBalance_WidthReserve_Index,
                        binBalance_WidthReserve_Id = query.BinBalance_WidthReserve_Id,
                        binBalance_WidthReserve_Name = query.BinBalance_WidthReserve_Name,
                        binBalance_WidthReserveRatio = query.BinBalance_WidthReserveRatio,
                        binBalance_LengthReserve = query.BinBalance_LengthReserve,
                        binBalance_LengthReserve_Index = query.BinBalance_LengthReserve_Index,
                        binBalance_LengthReserve_Id = query.BinBalance_LengthReserve_Id,
                        binBalance_LengthReserve_Name = query.BinBalance_LengthReserve_Name,
                        binBalance_LengthReserveRatio = query.BinBalance_LengthReserveRatio,
                        binBalance_HeightReserve = query.BinBalance_HeightReserve,
                        binBalance_HeightReserve_Index = query.BinBalance_HeightReserve_Index,
                        binBalance_HeightReserve_Id = query.BinBalance_HeightReserve_Id,
                        binBalance_HeightReserve_Name = query.BinBalance_HeightReserve_Name,
                        binBalance_HeightReserveRatio = query.BinBalance_HeightReserveRatio,
                        binBalance_UnitVolumeReserve = query.BinBalance_UnitVolumeReserve,
                        binBalance_VolumeReserve = query.BinBalance_VolumeReserve,

                        productConversion_Index = query.ProductConversion_Index,
                        productConversion_Id = query.ProductConversion_Id,
                        productConversion_Name = query.ProductConversion_Name,

                        unitPrice = query.UnitPrice,
                        unitPrice_Index = query.UnitPrice_Index,
                        unitPrice_Id = query.UnitPrice_Id,
                        unitPrice_Name = query.UnitPrice_Name,
                        price = query.Price,
                        price_Index = query.Price_Index,
                        price_Id = query.Price_Id,
                        price_Name = query.Price_Name,

                        uDF_1 = query.UDF_1,
                        uDF_2 = query.UDF_2,
                        uDF_3 = query.UDF_3,
                        uDF_4 = query.UDF_4,
                        uDF_5 = query.UDF_5,
                        create_By = query.Create_By,
                        create_Date = query.Create_Date,
                        update_By = query.Update_By,
                        update_Date = query.Update_Date,
                        cancel_By = query.Cancel_By,
                        cancel_Date = query.Cancel_Date,
                        isUse = query.IsUse,
                        binBalance_Status = query.BinBalance_Status,
                        ageRemain = query.AgeRemain,

                        invoice_No = query.Invoice_No,
                        declaration_No = query.Declaration_No,
                        hs_Code = query.HS_Code,
                        conutry_of_Origin = query.Conutry_of_Origin,
                        tax1 = query.Tax1,
                        tax1_Currency_Index = query.Tax1_Currency_Index,
                        tax1_Currency_Id = query.Tax1_Currency_Id,
                        tax1_Currency_Name = query.Tax1_Currency_Name,
                        tax2 = query.Tax2,
                        tax2_Currency_Index = query.Tax2_Currency_Index,
                        tax2_Currency_Id = query.Tax2_Currency_Id,
                        tax2_Currency_Name = query.Tax2_Currency_Name,
                        tax3 = query.Tax3,
                        tax3_Currency_Index = query.Tax3_Currency_Index,
                        tax3_Currency_Id = query.Tax3_Currency_Id,
                        tax3_Currency_Name = query.Tax3_Currency_Name,
                        tax4 = query.Tax4,
                        tax4_Currency_Index = query.Tax4_Currency_Index,
                        tax4_Currency_Id = query.Tax4_Currency_Id,
                        tax4_Currency_Name = query.Tax4_Currency_Name,
                        tax5 = query.Tax5,
                        tax5_Currency_Index = query.Tax5_Currency_Index,
                        tax5_Currency_Id = query.Tax5_Currency_Id,
                        tax5_Currency_Name = query.Tax5_Currency_Name,

                        erp_Location = query.ERP_Location,

                    };
                    items.Add(item);
                }

                olog.logging("getViewBinbalance", "END");

                return items;
            }
            catch (Exception ex)
            {
                olog.logging("getViewBinbalance", "ex " + ex.Message);
                throw ex;
            }
        }

        public bool updateIsuseViewBinbalance(FilterBinbalanceViewModel model)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            try
            {

                olog.logging("updateIsuseBinbalance", State  + " " + model.sJson() );
                db.Database.SetCommandTimeout(360);
                var items = new List<BinBalanceViewModel>();

                if (model.isActive)
                {
                    olog.logging("updateIsuseBinbalance", "isActive - " + model.isuse.ToString());

                    var querys = db.wm_BinBalance.Where(c => c.IsUse == model.isuse).ToList();
                    var listdata = querys.ToList();
                    foreach (var l in listdata)
                    {
                        l.IsUse = "";
                    }
                }
                else
                {
                    olog.logging("updateIsuseBinbalance", "else - " + model.isuse.ToString());
                    olog.logging("updateIsuseBinbalance", "else - " + model.Product_Index.ToString());

                    var querys = db.View_WaveBinBalance.AsQueryable();
                    querys = querys.Where(c => c.BinBalance_QtyBal > 0);

                    if (model.Owner_Index.ToString() != "")
                    {
                        querys = querys.Where(c => c.Owner_Index == model.Owner_Index);
                    }
                    if (model.BinBalance_Index.ToString() != "")
                    {
                        querys = querys.Where(c => c.BinBalance_Index == model.BinBalance_Index);
                    }
                    if (model.Location_Index.ToString() != "")
                    {
                        querys = querys.Where(c => c.Location_Index == model.Location_Index);
                    }
                    if (model.Product_Index.ToString() != "")
                    {
                        querys = querys.Where(c => c.Product_Index == model.Product_Index);
                    }

                    if (model.Product_Lot != null)
                    {
                        if (model.Product_Lot.ToString() != "")
                        {
                            querys = querys.Where(c => (c.Product_Lot ?? "")  == model.Product_Lot);
                        }
                    }
                    if (model.ItemStatus_Index.ToString() != "")
                    {
                        querys = querys.Where(c => c.ItemStatus_Index == model.ItemStatus_Index);
                    }
                    if (model.MFG_Date != null)
                    {
                        if (model.MFG_Date.ToString() != "")
                        {

                        }
                    }
                    if (model.EXP_Date != null)
                    {
                        if (model.EXP_Date.ToString() != "")
                        {

                        }
                    }
                    if (model.isUseAttribute == true)
                    {
                        // ADD UDF 1-5 
                        if (model.UDF_1 != null)
                        {
  
                            querys = querys.Where(c => (c.UDF_1??"") == model.UDF_1);
                        }

                        if (model.UDF_2 != null)
                        {
                            querys = querys.Where(c => (c.UDF_2 ?? "") == model.UDF_2);
                        }

                        if (model.UDF_3 != null)
                        {
                            querys = querys.Where(c => (c.UDF_3 ?? "") == model.UDF_3);
                        }

                        if (model.UDF_4 != null)
                        {
                            querys = querys.Where(c => (c.UDF_4 ?? "") == model.UDF_4);
                        }

                        if (model.UDF_5 != null)
                        {
                            querys = querys.Where(c => (c.UDF_5 ?? "") == model.UDF_5);
                        }
                    }

                    var listdata = querys.ToList();
                    foreach (var l in listdata)
                    {
                        if (string.IsNullOrEmpty(l.IsUse))
                        {
                            l.IsUse = model.isuse;
                        }
                    }
                }


                olog.logging("updateIsuseBinbalance", "S.SaveChanges - " + model.isuse.ToString());

                var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transaction.Commit();

                    olog.logging("updateIsuseBinbalance", "E.SaveChanges - " + model.isuse.ToString());
                }
                catch (Exception exy)
                {
                    msglog = State + " exy Rollback " + exy.Message.ToString();
                    olog.logging("updateIsuseBinbalance", msglog);
 
                    transaction.Rollback();

                    olog.logging("updateIsuseBinbalance", "exy inner - " + exy.InnerException.Message.ToString());
                    throw exy;
                }

                return true;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("updateIsuseBinbalance", msglog);
                olog.logging("updateIsuseBinbalance", "ex inner - " + ex.InnerException.Message.ToString());

                return false;
            }
        }

        #region insertBinCardReserve

        #region insertBinCardReserve old
        //public actionResultPickbinbalanceFromGIViewModel insertBinCardReserve(PickbinbalanceFromGIViewModel model)
        //{
        //    String State = "Start " + model.sJson();
        //    String msglog = "";
        //    var xx = "";
        //    var olog = new logtxt();
        //    try
        //    {
        //        olog.logging("insertBinCardReserve", State);


        //        var result = new actionResultPickbinbalanceFromGIViewModel();
        //        var BinCardReserve_Index = Guid.NewGuid();
        //        //var BinCard_Index = Guid.NewGuid();
        //        olog.logging("insertBinCardReserve", " ref_DocumentItem_Index  - " + model.ref_DocumentItem_Index.ToString());
        //        db.Database.SetCommandTimeout(360);
        //        var itemBinReserve = db.wm_BinCardReserve.Where(c => c.Ref_Document_Index == new Guid(model.ref_Document_Index) && c.Ref_DocumentItem_Index == new Guid(model.ref_DocumentItem_Index)).ToList();
        //        if (itemBinReserve.Count == 0)
        //        {

        //            State = "itemBinReserve.Count == 0";
        //            olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());

        //            var itemBin = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));
        //            var BinCardReserve = new wm_BinCardReserve();

        //            BinCardReserve.BinCardReserve_Index = BinCardReserve_Index;
        //            BinCardReserve.BinBalance_Index = itemBin.BinBalance_Index;
        //            BinCardReserve.Process_Index = new Guid(model.process_Index);
        //            BinCardReserve.GoodsReceive_Index = itemBin.GoodsReceive_Index;
        //            BinCardReserve.GoodsReceive_No = itemBin.GoodsReceive_No;
        //            BinCardReserve.GoodsReceive_Date = itemBin.GoodsReceive_Date;
        //            BinCardReserve.GoodsReceiveItem_Index = itemBin.GoodsReceiveItem_Index;
        //            BinCardReserve.TagItem_Index = itemBin.TagItem_Index;
        //            BinCardReserve.Tag_Index = itemBin.Tag_Index;
        //            BinCardReserve.Tag_No = itemBin.Tag_No;
        //            BinCardReserve.Product_Index = itemBin.Product_Index;
        //            BinCardReserve.Product_Id = itemBin.Product_Id;
        //            BinCardReserve.Product_Name = itemBin.Product_Name;
        //            BinCardReserve.Product_SecondName = itemBin.Product_SecondName;
        //            BinCardReserve.Product_ThirdName = itemBin.Product_ThirdName;
        //            BinCardReserve.Product_Lot = itemBin.Product_Lot;
        //            BinCardReserve.ItemStatus_Index = itemBin.ItemStatus_Index;
        //            BinCardReserve.ItemStatus_Id = itemBin.ItemStatus_Id;
        //            BinCardReserve.ItemStatus_Name = itemBin.ItemStatus_Name;
        //            BinCardReserve.MFG_Date = itemBin.GoodsReceive_MFG_Date;
        //            BinCardReserve.EXP_Date = itemBin.GoodsReceive_EXP_Date;
        //            BinCardReserve.ProductConversion_Index = itemBin.ProductConversion_Index;
        //            BinCardReserve.ProductConversion_Id = itemBin.ProductConversion_Id;
        //            BinCardReserve.ProductConversion_Name = itemBin.ProductConversion_Name;
        //            BinCardReserve.Owner_Index = itemBin.Owner_Index;
        //            BinCardReserve.Owner_Id = itemBin.Owner_Id;
        //            BinCardReserve.Owner_Name = itemBin.Owner_Name;
        //            BinCardReserve.Location_Index = itemBin.Location_Index;
        //            BinCardReserve.Location_Id = itemBin.Location_Id;
        //            BinCardReserve.Location_Name = itemBin.Location_Name;
        //            BinCardReserve.BinCardReserve_QtyBal = model.pick;


        //            BinCardReserve.BinCardReserve_UnitWeightBal = itemBin.BinBalance_UnitWeightBal;
        //            BinCardReserve.BinCardReserve_UnitWeightBal_Index = itemBin.BinBalance_UnitWeightBal_Index;
        //            BinCardReserve.BinCardReserve_UnitWeightBal_Id = itemBin.BinBalance_UnitWeightBal_Id;
        //            BinCardReserve.BinCardReserve_UnitWeightBal_Name = itemBin.BinBalance_UnitWeightBal_Name;
        //            BinCardReserve.BinCardReserve_UnitWeightBalRatio = itemBin.BinBalance_UnitWeightBalRatio;
        //            BinCardReserve.BinCardReserve_WeightBal = model.pick * (itemBin.BinBalance_UnitWeightBal ?? 0);
        //            BinCardReserve.BinCardReserve_WeightBal_Index = itemBin.BinBalance_WeightBal_Index;
        //            BinCardReserve.BinCardReserve_WeightBal_Id = itemBin.BinBalance_WeightBal_Id;
        //            BinCardReserve.BinCardReserve_WeightBal_Name = itemBin.BinBalance_WeightBal_Name;
        //            BinCardReserve.BinCardReserve_WeightBalRatio = itemBin.BinBalance_WeightBalRatio;

        //            BinCardReserve.BinCardReserve_UnitNetWeightBal = itemBin.BinBalance_UnitNetWeightBal;
        //            BinCardReserve.BinCardReserve_UnitNetWeightBal_Index = itemBin.BinBalance_UnitNetWeightBal_Index;
        //            BinCardReserve.BinCardReserve_UnitNetWeightBal_Id = itemBin.BinBalance_UnitNetWeightBal_Id;
        //            BinCardReserve.BinCardReserve_UnitNetWeightBal_Name = itemBin.BinBalance_UnitNetWeightBal_Name;
        //            BinCardReserve.BinCardReserve_UnitNetWeightBalRatio = itemBin.BinBalance_UnitNetWeightBalRatio;
        //            BinCardReserve.BinCardReserve_NetWeightBal = model.pick * (itemBin.BinBalance_UnitNetWeightBal ?? 0);
        //            BinCardReserve.BinCardReserve_NetWeightBal_Index = itemBin.BinBalance_NetWeightBal_Index;
        //            BinCardReserve.BinCardReserve_NetWeightBal_Id = itemBin.BinBalance_NetWeightBal_Id;
        //            BinCardReserve.BinCardReserve_NetWeightBal_Name = itemBin.BinBalance_NetWeightBal_Name;
        //            BinCardReserve.BinCardReserve_NetWeightBalRatio = itemBin.BinBalance_NetWeightBalRatio;

        //            BinCardReserve.BinCardReserve_UnitGrsWeightBal = itemBin.BinBalance_UnitGrsWeightBal;
        //            BinCardReserve.BinCardReserve_UnitGrsWeightBal_Index = itemBin.BinBalance_UnitGrsWeightBal_Index;
        //            BinCardReserve.BinCardReserve_UnitGrsWeightBal_Id = itemBin.BinBalance_UnitGrsWeightBal_Id;
        //            BinCardReserve.BinCardReserve_UnitGrsWeightBal_Name = itemBin.BinBalance_UnitGrsWeightBal_Name;
        //            BinCardReserve.BinCardReserve_UnitGrsWeightBalRatio = itemBin.BinBalance_UnitGrsWeightBalRatio;
        //            BinCardReserve.BinCardReserve_GrsWeightBal = model.pick * (itemBin.BinBalance_UnitGrsWeightBal ?? 0);
        //            BinCardReserve.BinCardReserve_GrsWeightBal_Index = itemBin.BinBalance_GrsWeightBal_Index;
        //            BinCardReserve.BinCardReserve_GrsWeightBal_Id = itemBin.BinBalance_GrsWeightBal_Id;
        //            BinCardReserve.BinCardReserve_GrsWeightBal_Name = itemBin.BinBalance_GrsWeightBal_Name;
        //            BinCardReserve.BinCardReserve_GrsWeightBalRatio = itemBin.BinBalance_GrsWeightBalRatio;

        //            BinCardReserve.BinCardReserve_UnitWidthBal = itemBin.BinBalance_UnitWidthBal;
        //            BinCardReserve.BinCardReserve_UnitWidthBal_Index = itemBin.BinBalance_UnitWidthBal_Index;
        //            BinCardReserve.BinCardReserve_UnitWidthBal_Id = itemBin.BinBalance_UnitWidthBal_Id;
        //            BinCardReserve.BinCardReserve_UnitWidthBal_Name = itemBin.BinBalance_UnitWidthBal_Name;
        //            BinCardReserve.BinCardReserve_UnitWidthBalRatio = itemBin.BinBalance_UnitWidthBalRatio;
        //            BinCardReserve.BinCardReserve_WidthBal = model.pick * (itemBin.BinBalance_UnitWidthBal ?? 0);
        //            BinCardReserve.BinCardReserve_WidthBal_Index = itemBin.BinBalance_WidthBal_Index;
        //            BinCardReserve.BinCardReserve_WidthBal_Id = itemBin.BinBalance_WidthBal_Id;
        //            BinCardReserve.BinCardReserve_WidthBal_Name = itemBin.BinBalance_WidthBal_Name;
        //            BinCardReserve.BinCardReserve_WidthBalRatio = itemBin.BinBalance_WidthBalRatio;

        //            BinCardReserve.BinCardReserve_UnitLengthBal = itemBin.BinBalance_UnitLengthBal;
        //            BinCardReserve.BinCardReserve_UnitLengthBal_Index = itemBin.BinBalance_UnitLengthBal_Index;
        //            BinCardReserve.BinCardReserve_UnitLengthBal_Id = itemBin.BinBalance_UnitLengthBal_Id;
        //            BinCardReserve.BinCardReserve_UnitLengthBal_Name = itemBin.BinBalance_UnitLengthBal_Name;
        //            BinCardReserve.BinCardReserve_UnitLengthBalRatio = itemBin.BinBalance_UnitLengthBalRatio;
        //            BinCardReserve.BinCardReserve_LengthBal = model.pick * (itemBin.BinBalance_UnitLengthBal ?? 0);
        //            BinCardReserve.BinCardReserve_LengthBal_Index = itemBin.BinBalance_LengthBal_Index;
        //            BinCardReserve.BinCardReserve_LengthBal_Id = itemBin.BinBalance_LengthBal_Id;
        //            BinCardReserve.BinCardReserve_LengthBal_Name = itemBin.BinBalance_LengthBal_Name;
        //            BinCardReserve.BinCardReserve_LengthBalRatio = itemBin.BinBalance_LengthBalRatio;

        //            BinCardReserve.BinCardReserve_UnitHeightBal = itemBin.BinBalance_UnitHeightBal;
        //            BinCardReserve.BinCardReserve_UnitHeightBal_Index = itemBin.BinBalance_UnitHeightBal_Index;
        //            BinCardReserve.BinCardReserve_UnitHeightBal_Id = itemBin.BinBalance_UnitHeightBal_Id;
        //            BinCardReserve.BinCardReserve_UnitHeightBal_Name = itemBin.BinBalance_UnitHeightBal_Name;
        //            BinCardReserve.BinCardReserve_UnitHeightBalRatio = itemBin.BinBalance_UnitHeightBalRatio;
        //            BinCardReserve.BinCardReserve_HeightBal = model.pick * (itemBin.BinBalance_UnitHeightBal ?? 0);
        //            BinCardReserve.BinCardReserve_HeightBal_Index = itemBin.BinBalance_HeightBal_Index;
        //            BinCardReserve.BinCardReserve_HeightBal_Id = itemBin.BinBalance_HeightBal_Id;
        //            BinCardReserve.BinCardReserve_HeightBal_Name = itemBin.BinBalance_HeightBal_Name;
        //            BinCardReserve.BinCardReserve_HeightBalRatio = itemBin.BinBalance_HeightBalRatio;

        //            BinCardReserve.BinCardReserve_UnitVolumeBal = itemBin.BinBalance_UnitVolumeBal;
        //            BinCardReserve.BinCardReserve_VolumeBal = model.pick * (itemBin.BinBalance_UnitVolumeBal ?? 0);

        //            BinCardReserve.UnitPrice = itemBin.UnitPrice;
        //            BinCardReserve.UnitPrice_Index = itemBin.UnitPrice_Index;
        //            BinCardReserve.UnitPrice_Id = itemBin.UnitPrice_Id;
        //            BinCardReserve.UnitPrice_Name = itemBin.UnitPrice_Name;
        //            BinCardReserve.Price = model.pick * (itemBin.UnitPrice ?? 0);
        //            BinCardReserve.Price_Index = itemBin.UnitPrice_Index;
        //            BinCardReserve.Price_Id = itemBin.UnitPrice_Id;
        //            BinCardReserve.Price_Name = itemBin.UnitPrice_Name;



        //            BinCardReserve.Ref_Document_Index = Guid.Parse(model.ref_Document_Index);
        //            BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.ref_DocumentItem_Index);
        //            BinCardReserve.Ref_Document_No = model.goodsIssue_No;
        //            BinCardReserve.Ref_Wave_Index = model.wave_Index;

        //            BinCardReserve.Create_By = model.create_By;
        //            BinCardReserve.Create_Date = DateTime.Now;


        //            db.wm_BinCardReserve.Add(BinCardReserve);

        //            //itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyReserve + model.pick;

        //            string cmd1 = "";


        //            cmd1 += "       UPdate WMSDB_AMZ_Binbalance_V3 ..wm_BinBalance  set                          ";
        //            cmd1 += "           BinBalance_QtyReserve =   BinBalance_QtyReserve  +   " + model.pick.ToString();



        //            if (itemBin.BinBalance_WeightBegin != 0)
        //            {
        //                var WeightReserve = (model.pick * itemBin.BinBalance_UnitWeightBal);

        //                itemBin.BinBalance_WeightReserve = itemBin.BinBalance_WeightReserve + WeightReserve;

        //                //cmd1 += "          , BinBalance_WeightReserve  =   BinBalance_WeightReserve  +   " + WeightReserve.ToString();

        //            }

        //            if (itemBin.BinBalance_NetWeightBegin != 0)
        //            {
        //                var NetWeightReserve = (model.pick * itemBin.BinBalance_UnitNetWeightBal);
        //                itemBin.BinBalance_NetWeightReserve = itemBin.BinBalance_NetWeightReserve + NetWeightReserve;

        //                //cmd1 += "          , BinBalance_NetWeightReserve  =   BinBalance_NetWeightReserve  +   " + NetWeightReserve.ToString();

        //            }


        //            if (itemBin.BinBalance_GrsWeightBegin != 0)
        //            {
        //                var GrsWeightReserve = (model.pick * itemBin.BinBalance_UnitGrsWeightBal);
        //                itemBin.BinBalance_GrsWeightReserve = itemBin.BinBalance_GrsWeightReserve + GrsWeightReserve;


        //                //cmd1 += "          , BinBalance_GrsWeightReserve  =   BinBalance_GrsWeightReserve  +   " + GrsWeightReserve.ToString();

        //            }


        //            if (itemBin.BinBalance_WidthBegin != 0)
        //            {
        //                var WidthReserve = (model.pick * itemBin.BinBalance_UnitWidthBal);
        //                itemBin.BinBalance_WidthReserve = itemBin.BinBalance_WidthReserve + WidthReserve;

        //                //cmd1 += "          , BinBalance_WidthReserve  =   BinBalance_WidthReserve  +   " + WidthReserve.ToString();
        //            }


        //            if (itemBin.BinBalance_LengthBegin != 0)
        //            {
        //                var LengthReserve = (model.pick * itemBin.BinBalance_UnitLengthBal);
        //                itemBin.BinBalance_LengthReserve = itemBin.BinBalance_LengthReserve + LengthReserve;

        //                //cmd1 += "          , BinBalance_LengthReserve  =   BinBalance_LengthReserve  +   " + LengthReserve.ToString();


        //            }


        //            if (itemBin.BinBalance_HeightBegin != 0)
        //            {
        //                var HeightReserve = (model.pick * itemBin.BinBalance_UnitHeightBal);
        //                itemBin.BinBalance_HeightReserve = itemBin.BinBalance_HeightReserve + HeightReserve;


        //                //cmd1 += "          , BinBalance_HeightReserve  =   BinBalance_HeightReserve  +   " + HeightReserve.ToString();

        //            }

        //            if (itemBin.BinBalance_VolumeBegin != 0)
        //            {
        //                var VolReserve = (model.pick * itemBin.BinBalance_UnitVolumeBal);
        //                itemBin.BinBalance_VolumeReserve = itemBin.BinBalance_VolumeReserve + VolReserve;

        //                //cmd1 += "          , BinBalance_VolumeReserve  =   BinBalance_VolumeReserve  +   " + VolReserve.ToString();
        //            }


        //            if ((itemBin.UnitPrice ?? 0) != 0)
        //            {
        //                var VoltPrice = (model.pick * itemBin.UnitPrice);
        //                itemBin.Price = itemBin.Price - VoltPrice;

        //                //cmd1 += "          , Price  =   Price  +   " + VoltPrice.ToString();
        //            }



        //            cmd1 += "        where Binbalance_Index   = '" + model.binbalance_Index + "'";

        //            xx = cmd1;
        //            State = "s.SaveChanges";

        //            olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());




        //            //if (itemBin.BinBalance_WeightBegin != 0)
        //            //{
        //            //    var WeightReserve = (model.pick * itemBin.BinBalance_UnitWeightBal);
        //            //    itemBin.BinBalance_WeightReserve = itemBin.BinBalance_WeightReserve + WeightReserve;
        //            //}

        //            //if (itemBin.BinBalance_NetWeightBegin != 0)
        //            //{
        //            //    var NetWeightReserve = (model.pick * itemBin.BinBalance_UnitNetWeightBal);
        //            //    itemBin.BinBalance_NetWeightReserve = itemBin.BinBalance_NetWeightReserve + NetWeightReserve;
        //            //}


        //            //if (itemBin.BinBalance_GrsWeightBegin != 0)
        //            //{
        //            //    var GrsWeightReserve = (model.pick * itemBin.BinBalance_UnitGrsWeightBal);
        //            //    itemBin.BinBalance_GrsWeightReserve = itemBin.BinBalance_GrsWeightReserve + GrsWeightReserve;
        //            //}


        //            //if (itemBin.BinBalance_WidthBegin != 0)
        //            //{
        //            //    var WidthReserve = (model.pick * itemBin.BinBalance_UnitWidthBal);
        //            //    itemBin.BinBalance_WidthReserve = itemBin.BinBalance_WidthReserve + WidthReserve;
        //            //}


        //            //if (itemBin.BinBalance_LengthBegin != 0)
        //            //{
        //            //    var LengthReserve = (model.pick * itemBin.BinBalance_UnitLengthBal);
        //            //    itemBin.BinBalance_LengthReserve = itemBin.BinBalance_LengthReserve + LengthReserve;
        //            //}


        //            //if (itemBin.BinBalance_HeightBegin != 0)
        //            //{
        //            //    var HeightReserve = (model.pick * itemBin.BinBalance_UnitHeightBal);
        //            //    itemBin.BinBalance_HeightReserve = itemBin.BinBalance_HeightReserve + HeightReserve;
        //            //}

        //            //if (itemBin.BinBalance_VolumeBegin != 0)
        //            //{
        //            //    var VolReserve = (model.pick * itemBin.BinBalance_UnitVolumeBal);
        //            //    itemBin.BinBalance_VolumeReserve = itemBin.BinBalance_VolumeReserve + VolReserve;
        //            //}


        //            //if ((itemBin.UnitPrice ?? 0) != 0)
        //            //{
        //            //    var VoltPrice = (model.pick * itemBin.UnitPrice);
        //            //    itemBin.Price = itemBin.Price - VoltPrice;
        //            //}

        //            State = "s.SaveChanges";
        //            olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());


        //            var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
        //            try
        //            {
        //                db.SaveChanges();
        //                var r1 = db.Database.ExecuteSqlCommand(cmd1);
        //                transactionx.Commit();

        //                State = "e.SaveChanges";
        //                olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());


        //            }

        //            catch (Exception exy)
        //            {
        //                msglog = State + " ex Rollback " + exy.Message.ToString();
        //                olog.logging("insertBinCardReserve", msglog);
        //                transactionx.Rollback();

        //                throw exy;

        //            }
        //        }
        //        else
        //        {
        //            result.resultMsg = "สินค้าไม่พอ กรุณาลองใหม่อีกครั้ง";
        //            result.resultIsUse = false;
        //            return result;
        //        }

        //        model.binCardReserve_Index = BinCardReserve_Index.ToString();
        //        //model.binCard_Index = BinCard_Index.ToString();
        //        result.items = model;
        //        result.resultMsg = "รับสินค้าเรียบร้อยแล้ว";
        //        //result.resultIsUse = true;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        msglog = State + " ex Rollback " + ex.Message.ToString();
        //        olog.logging("insertBinCardReserve", xx);
        //        var result = new actionResultPickbinbalanceFromGIViewModel();
        //        result.resultIsUse = false;
        //        result.resultMsg = ex.Message;
        //        return result;
        //    }

        //}
        #endregion

        #region insertBinCardReserve new
        public actionResultPickbinbalanceFromGIViewModel insertBinCardReserve(PickbinbalanceFromGIViewModel model)
        {
            String State = "Start " + model.sJson();
            String msglog = "";
            var xx = "";
            var olog = new logtxt();
            try
            {
                olog.logging("insertBinCardReserve", State);


                var result = new actionResultPickbinbalanceFromGIViewModel();
                var BinCardReserve_Index = Guid.NewGuid();
                //var BinCard_Index = Guid.NewGuid();
                olog.logging("insertBinCardReserve", " ref_DocumentItem_Index  - " + model.ref_DocumentItem_Index.ToString());
                db.Database.SetCommandTimeout(360);
                var itemBinReserve = db.wm_BinCardReserve.Where(c => c.Ref_Document_Index == new Guid(model.ref_Document_Index) && c.Ref_DocumentItem_Index == new Guid(model.ref_DocumentItem_Index)).ToList();
                if (itemBinReserve.Count == 0)
                {

                    State = "itemBinReserve.Count == 0";
                    olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());

                    var itemBin = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));
                    var BinCardReserve = new wm_BinCardReserve();

                    BinCardReserve.BinCardReserve_Index = BinCardReserve_Index;
                    BinCardReserve.BinBalance_Index = itemBin.BinBalance_Index;
                    BinCardReserve.Process_Index = new Guid(model.process_Index);
                    BinCardReserve.GoodsReceive_Index = itemBin.GoodsReceive_Index;
                    BinCardReserve.GoodsReceive_No = itemBin.GoodsReceive_No;
                    BinCardReserve.GoodsReceive_Date = itemBin.GoodsReceive_Date;
                    BinCardReserve.GoodsReceiveItem_Index = itemBin.GoodsReceiveItem_Index;
                    BinCardReserve.TagItem_Index = itemBin.TagItem_Index;
                    BinCardReserve.Tag_Index = itemBin.Tag_Index;
                    BinCardReserve.Tag_No = itemBin.Tag_No;
                    BinCardReserve.Product_Index = itemBin.Product_Index;
                    BinCardReserve.Product_Id = itemBin.Product_Id;
                    BinCardReserve.Product_Name = itemBin.Product_Name;
                    BinCardReserve.Product_SecondName = itemBin.Product_SecondName;
                    BinCardReserve.Product_ThirdName = itemBin.Product_ThirdName;
                    BinCardReserve.Product_Lot = itemBin.Product_Lot;
                    BinCardReserve.ItemStatus_Index = itemBin.ItemStatus_Index;
                    BinCardReserve.ItemStatus_Id = itemBin.ItemStatus_Id;
                    BinCardReserve.ItemStatus_Name = itemBin.ItemStatus_Name;
                    BinCardReserve.MFG_Date = itemBin.GoodsReceive_MFG_Date;
                    BinCardReserve.EXP_Date = itemBin.GoodsReceive_EXP_Date;
                    BinCardReserve.ProductConversion_Index = itemBin.ProductConversion_Index;
                    BinCardReserve.ProductConversion_Id = itemBin.ProductConversion_Id;
                    BinCardReserve.ProductConversion_Name = itemBin.ProductConversion_Name;
                    BinCardReserve.Owner_Index = itemBin.Owner_Index;
                    BinCardReserve.Owner_Id = itemBin.Owner_Id;
                    BinCardReserve.Owner_Name = itemBin.Owner_Name;
                    BinCardReserve.Location_Index = itemBin.Location_Index;
                    BinCardReserve.Location_Id = itemBin.Location_Id;
                    BinCardReserve.Location_Name = itemBin.Location_Name;
                    BinCardReserve.BinCardReserve_QtyBal = model.pick;


                    BinCardReserve.BinCardReserve_UnitWeightBal = itemBin.BinBalance_UnitWeightBal;
                    BinCardReserve.BinCardReserve_UnitWeightBal_Index = itemBin.BinBalance_UnitWeightBal_Index;
                    BinCardReserve.BinCardReserve_UnitWeightBal_Id = itemBin.BinBalance_UnitWeightBal_Id;
                    BinCardReserve.BinCardReserve_UnitWeightBal_Name = itemBin.BinBalance_UnitWeightBal_Name;
                    BinCardReserve.BinCardReserve_UnitWeightBalRatio = itemBin.BinBalance_UnitWeightBalRatio;
                    BinCardReserve.BinCardReserve_WeightBal = model.pick * (itemBin.BinBalance_UnitWeightBal ?? 0);
                    BinCardReserve.BinCardReserve_WeightBal_Index = itemBin.BinBalance_WeightBal_Index;
                    BinCardReserve.BinCardReserve_WeightBal_Id = itemBin.BinBalance_WeightBal_Id;
                    BinCardReserve.BinCardReserve_WeightBal_Name = itemBin.BinBalance_WeightBal_Name;
                    BinCardReserve.BinCardReserve_WeightBalRatio = itemBin.BinBalance_WeightBalRatio;

                    BinCardReserve.BinCardReserve_UnitNetWeightBal = itemBin.BinBalance_UnitNetWeightBal;
                    BinCardReserve.BinCardReserve_UnitNetWeightBal_Index = itemBin.BinBalance_UnitNetWeightBal_Index;
                    BinCardReserve.BinCardReserve_UnitNetWeightBal_Id = itemBin.BinBalance_UnitNetWeightBal_Id;
                    BinCardReserve.BinCardReserve_UnitNetWeightBal_Name = itemBin.BinBalance_UnitNetWeightBal_Name;
                    BinCardReserve.BinCardReserve_UnitNetWeightBalRatio = itemBin.BinBalance_UnitNetWeightBalRatio;
                    BinCardReserve.BinCardReserve_NetWeightBal = model.pick * (itemBin.BinBalance_UnitNetWeightBal ?? 0);
                    BinCardReserve.BinCardReserve_NetWeightBal_Index = itemBin.BinBalance_NetWeightBal_Index;
                    BinCardReserve.BinCardReserve_NetWeightBal_Id = itemBin.BinBalance_NetWeightBal_Id;
                    BinCardReserve.BinCardReserve_NetWeightBal_Name = itemBin.BinBalance_NetWeightBal_Name;
                    BinCardReserve.BinCardReserve_NetWeightBalRatio = itemBin.BinBalance_NetWeightBalRatio;

                    BinCardReserve.BinCardReserve_UnitGrsWeightBal = itemBin.BinBalance_UnitGrsWeightBal;
                    BinCardReserve.BinCardReserve_UnitGrsWeightBal_Index = itemBin.BinBalance_UnitGrsWeightBal_Index;
                    BinCardReserve.BinCardReserve_UnitGrsWeightBal_Id = itemBin.BinBalance_UnitGrsWeightBal_Id;
                    BinCardReserve.BinCardReserve_UnitGrsWeightBal_Name = itemBin.BinBalance_UnitGrsWeightBal_Name;
                    BinCardReserve.BinCardReserve_UnitGrsWeightBalRatio = itemBin.BinBalance_UnitGrsWeightBalRatio;
                    BinCardReserve.BinCardReserve_GrsWeightBal = model.pick * (itemBin.BinBalance_UnitGrsWeightBal ?? 0);
                    BinCardReserve.BinCardReserve_GrsWeightBal_Index = itemBin.BinBalance_GrsWeightBal_Index;
                    BinCardReserve.BinCardReserve_GrsWeightBal_Id = itemBin.BinBalance_GrsWeightBal_Id;
                    BinCardReserve.BinCardReserve_GrsWeightBal_Name = itemBin.BinBalance_GrsWeightBal_Name;
                    BinCardReserve.BinCardReserve_GrsWeightBalRatio = itemBin.BinBalance_GrsWeightBalRatio;

                    BinCardReserve.BinCardReserve_UnitWidthBal = itemBin.BinBalance_UnitWidthBal;
                    BinCardReserve.BinCardReserve_UnitWidthBal_Index = itemBin.BinBalance_UnitWidthBal_Index;
                    BinCardReserve.BinCardReserve_UnitWidthBal_Id = itemBin.BinBalance_UnitWidthBal_Id;
                    BinCardReserve.BinCardReserve_UnitWidthBal_Name = itemBin.BinBalance_UnitWidthBal_Name;
                    BinCardReserve.BinCardReserve_UnitWidthBalRatio = itemBin.BinBalance_UnitWidthBalRatio;
                    BinCardReserve.BinCardReserve_WidthBal = model.pick * (itemBin.BinBalance_UnitWidthBal ?? 0);
                    BinCardReserve.BinCardReserve_WidthBal_Index = itemBin.BinBalance_WidthBal_Index;
                    BinCardReserve.BinCardReserve_WidthBal_Id = itemBin.BinBalance_WidthBal_Id;
                    BinCardReserve.BinCardReserve_WidthBal_Name = itemBin.BinBalance_WidthBal_Name;
                    BinCardReserve.BinCardReserve_WidthBalRatio = itemBin.BinBalance_WidthBalRatio;

                    BinCardReserve.BinCardReserve_UnitLengthBal = itemBin.BinBalance_UnitLengthBal;
                    BinCardReserve.BinCardReserve_UnitLengthBal_Index = itemBin.BinBalance_UnitLengthBal_Index;
                    BinCardReserve.BinCardReserve_UnitLengthBal_Id = itemBin.BinBalance_UnitLengthBal_Id;
                    BinCardReserve.BinCardReserve_UnitLengthBal_Name = itemBin.BinBalance_UnitLengthBal_Name;
                    BinCardReserve.BinCardReserve_UnitLengthBalRatio = itemBin.BinBalance_UnitLengthBalRatio;
                    BinCardReserve.BinCardReserve_LengthBal = model.pick * (itemBin.BinBalance_UnitLengthBal ?? 0);
                    BinCardReserve.BinCardReserve_LengthBal_Index = itemBin.BinBalance_LengthBal_Index;
                    BinCardReserve.BinCardReserve_LengthBal_Id = itemBin.BinBalance_LengthBal_Id;
                    BinCardReserve.BinCardReserve_LengthBal_Name = itemBin.BinBalance_LengthBal_Name;
                    BinCardReserve.BinCardReserve_LengthBalRatio = itemBin.BinBalance_LengthBalRatio;

                    BinCardReserve.BinCardReserve_UnitHeightBal = itemBin.BinBalance_UnitHeightBal;
                    BinCardReserve.BinCardReserve_UnitHeightBal_Index = itemBin.BinBalance_UnitHeightBal_Index;
                    BinCardReserve.BinCardReserve_UnitHeightBal_Id = itemBin.BinBalance_UnitHeightBal_Id;
                    BinCardReserve.BinCardReserve_UnitHeightBal_Name = itemBin.BinBalance_UnitHeightBal_Name;
                    BinCardReserve.BinCardReserve_UnitHeightBalRatio = itemBin.BinBalance_UnitHeightBalRatio;
                    BinCardReserve.BinCardReserve_HeightBal = model.pick * (itemBin.BinBalance_UnitHeightBal ?? 0);
                    BinCardReserve.BinCardReserve_HeightBal_Index = itemBin.BinBalance_HeightBal_Index;
                    BinCardReserve.BinCardReserve_HeightBal_Id = itemBin.BinBalance_HeightBal_Id;
                    BinCardReserve.BinCardReserve_HeightBal_Name = itemBin.BinBalance_HeightBal_Name;
                    BinCardReserve.BinCardReserve_HeightBalRatio = itemBin.BinBalance_HeightBalRatio;

                    BinCardReserve.BinCardReserve_UnitVolumeBal = itemBin.BinBalance_UnitVolumeBal;
                    BinCardReserve.BinCardReserve_VolumeBal = model.pick * (itemBin.BinBalance_UnitVolumeBal ?? 0);

                    BinCardReserve.UnitPrice = itemBin.UnitPrice;
                    BinCardReserve.UnitPrice_Index = itemBin.UnitPrice_Index;
                    BinCardReserve.UnitPrice_Id = itemBin.UnitPrice_Id;
                    BinCardReserve.UnitPrice_Name = itemBin.UnitPrice_Name;
                    BinCardReserve.Price = model.pick * (itemBin.UnitPrice ?? 0);
                    BinCardReserve.Price_Index = itemBin.UnitPrice_Index;
                    BinCardReserve.Price_Id = itemBin.UnitPrice_Id;
                    BinCardReserve.Price_Name = itemBin.UnitPrice_Name;



                    BinCardReserve.Ref_Document_Index = Guid.Parse(model.ref_Document_Index);
                    BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.ref_DocumentItem_Index);
                    BinCardReserve.Ref_Document_No = model.goodsIssue_No;
                    BinCardReserve.Ref_Wave_Index = model.wave_Index;

                    BinCardReserve.Create_By = model.create_By;
                    BinCardReserve.Create_Date = DateTime.Now;


                    db.wm_BinCardReserve.Add(BinCardReserve);
                    
                    itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyReserve + model.pick;

                    if (itemBin.BinBalance_WeightBegin != 0)
                    {
                        var WeightReserve = (model.pick * itemBin.BinBalance_UnitWeightBal);

                        itemBin.BinBalance_WeightReserve = itemBin.BinBalance_WeightReserve + WeightReserve;
                    }

                    if (itemBin.BinBalance_NetWeightBegin != 0)
                    {
                        var NetWeightReserve = (model.pick * itemBin.BinBalance_UnitNetWeightBal);
                        itemBin.BinBalance_NetWeightReserve = itemBin.BinBalance_NetWeightReserve + NetWeightReserve;
                    }


                    if (itemBin.BinBalance_GrsWeightBegin != 0)
                    {
                        var GrsWeightReserve = (model.pick * itemBin.BinBalance_UnitGrsWeightBal);
                        itemBin.BinBalance_GrsWeightReserve = itemBin.BinBalance_GrsWeightReserve + GrsWeightReserve;
                    }


                    if (itemBin.BinBalance_WidthBegin != 0)
                    {
                        var WidthReserve = (model.pick * itemBin.BinBalance_UnitWidthBal);
                        itemBin.BinBalance_WidthReserve = itemBin.BinBalance_WidthReserve + WidthReserve;
                    }


                    if (itemBin.BinBalance_LengthBegin != 0)
                    {
                        var LengthReserve = (model.pick * itemBin.BinBalance_UnitLengthBal);
                        itemBin.BinBalance_LengthReserve = itemBin.BinBalance_LengthReserve + LengthReserve;

                    }


                    if (itemBin.BinBalance_HeightBegin != 0)
                    {
                        var HeightReserve = (model.pick * itemBin.BinBalance_UnitHeightBal);
                        itemBin.BinBalance_HeightReserve = itemBin.BinBalance_HeightReserve + HeightReserve;
                    }

                    if (itemBin.BinBalance_VolumeBegin != 0)
                    {
                        var VolReserve = (model.pick * itemBin.BinBalance_UnitVolumeBal);
                        itemBin.BinBalance_VolumeReserve = itemBin.BinBalance_VolumeReserve + VolReserve;
                    }


                    if ((itemBin.UnitPrice ?? 0) != 0)
                    {
                        var VoltPrice = (model.pick * itemBin.UnitPrice);
                        itemBin.Price = itemBin.Price - VoltPrice;
                    }
                    
                    State = "s.SaveChanges";

                    olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());

                    
                    State = "s.SaveChanges";
                    olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());


                    var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transactionx.Commit();

                        State = "e.SaveChanges";
                        olog.logging("insertBinCardReserve", State + " - " + model.ref_DocumentItem_Index.ToString());


                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("insertBinCardReserve", msglog);
                        transactionx.Rollback();

                        throw exy;

                    }
                }
                else
                {
                    result.resultMsg = "สินค้าไม่พอ กรุณาลองใหม่อีกครั้ง";
                    result.resultIsUse = false;
                    return result;
                }

                model.binCardReserve_Index = BinCardReserve_Index.ToString();
                result.items = model;
                result.resultMsg = "รับสินค้าเรียบร้อยแล้ว";
                result.resultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("insertBinCardReserve", xx);
                var result = new actionResultPickbinbalanceFromGIViewModel();
                result.resultIsUse = false;
                result.resultMsg = ex.Message;
                return result;
            }

        }
        #endregion

        #endregion

        private ReserveBinBalanceModel GetReserveBinBalanceModel(string jsonData)
        {
            ReserveBinBalanceModel model = JsonConvert.DeserializeObject<ReserveBinBalanceModel>(jsonData ?? string.Empty);
            if (model is null)
            {
                throw new Exception("Invalid JSon : Can not Convert to Model");
            }

            if ((model.Items?.Count ?? 0) == 0)
            {
                throw new Exception("Invalid JSon : ReserveBinBalance Item not found");
            }

            return model;
        }

        public ReserveBinBalanceResultModel ReserveBinBalance(string jsonModel)
        {
            logtxt logging = new logtxt();
            ReserveBinBalanceResultModel result = new ReserveBinBalanceResultModel()
            {
                Items = new List<ReserveBinBalanceResultItemModel>()
            };

            try
            {
                ReserveBinBalanceModel models = GetReserveBinBalanceModel(jsonModel);

                wm_BinBalance binBalanceModel;
                wm_BinCardReserve binCardReserveModel;
                Guid binCardReserveIndex;
                bool alreadyInserted;

                DateTime reserveDate = DateTime.Now;
                foreach (ReserveBinBalanceItemModel item in models.Items)
                {
                    alreadyInserted = db.wm_BinCardReserve.Where(
                        s => s.Process_Index == item.Process_Index &&
                             s.Ref_Document_Index == item.Ref_Document_Index &&
                             s.Ref_DocumentItem_Index == item.Ref_DocumentItem_Index
                    ).Count() > 0;

                    if (alreadyInserted)
                    {
                        //Throw Exception ?
                        continue;
                    }

                    //Get BinBalance Model
                    binBalanceModel = db.wm_BinBalance.Find(item.BinBalance_Index);
                    if (binBalanceModel is null)
                    {
                        throw new Exception("BinBalance not found");
                    }

                    //Validate BinBalance
                    if (binBalanceModel.BinBalance_QtyBal <= 0)
                    {
                        throw new Exception(string.Format("BinBalance [ {0} ] QtyBal is out of stock", binBalanceModel.BinBalance_Index.ToString()));
                    }

                    if (binBalanceModel.BinBalance_QtyReserve < 0)
                    {
                        throw new Exception(string.Format("BinBalance [ {0} ] ReserveQty is Minus Value", binBalanceModel.BinBalance_Index.ToString()));
                    }

                    if (binBalanceModel.BinBalance_QtyReserve + item.Reserve_Qty > binBalanceModel.BinBalance_QtyBal)
                    {
                        throw new Exception(string.Format("BinBalance [ {0} ] ReserveQty is Over than BalanceQty", binBalanceModel.BinBalance_Index.ToString()));
                    }

                    binBalanceModel.BinBalance_QtyReserve += item.Reserve_Qty;
                    binBalanceModel.BinBalance_WeightReserve += decimal.Round((item.Reserve_Qty / binBalanceModel.BinBalance_QtyBal ?? 1) * (binBalanceModel.BinBalance_WeightBal ?? 0), 6);
                    binBalanceModel.BinBalance_VolumeReserve += decimal.Round((item.Reserve_Qty / binBalanceModel.BinBalance_QtyBal ?? 1) * (binBalanceModel.BinBalance_VolumeBal ?? 0), 6);

                    //Add BinCardReserve
                    binCardReserveIndex = Guid.NewGuid();
                    binCardReserveModel = new wm_BinCardReserve()
                    {
                        BinCardReserve_Index = binCardReserveIndex,
                        BinBalance_Index = binBalanceModel.BinBalance_Index,
                        Process_Index = item.Process_Index,
                        GoodsReceive_Index = binBalanceModel.GoodsReceive_Index,
                        GoodsReceive_No = binBalanceModel.GoodsReceive_No,
                        GoodsReceive_Date = binBalanceModel.GoodsReceive_Date,
                        GoodsReceiveItem_Index = binBalanceModel.GoodsReceiveItem_Index,
                        TagItem_Index = binBalanceModel.TagItem_Index,
                        Tag_Index = binBalanceModel.Tag_Index,
                        Tag_No = binBalanceModel.Tag_No,
                        Product_Index = binBalanceModel.Product_Index,
                        Product_Id = binBalanceModel.Product_Id,
                        Product_Name = binBalanceModel.Product_Name,
                        Product_SecondName = binBalanceModel.Product_SecondName,
                        Product_ThirdName = binBalanceModel.Product_ThirdName,
                        Product_Lot = binBalanceModel.Product_Lot,
                        ItemStatus_Index = binBalanceModel.ItemStatus_Index,
                        ItemStatus_Id = binBalanceModel.ItemStatus_Id,
                        ItemStatus_Name = binBalanceModel.ItemStatus_Name,
                        MFG_Date = binBalanceModel.GoodsReceive_MFG_Date,
                        EXP_Date = binBalanceModel.GoodsReceive_EXP_Date,
                        ProductConversion_Index = binBalanceModel.ProductConversion_Index,
                        ProductConversion_Id = binBalanceModel.ProductConversion_Id,
                        ProductConversion_Name = binBalanceModel.ProductConversion_Name,
                        Owner_Index = binBalanceModel.Owner_Index,
                        Owner_Id = binBalanceModel.Owner_Id,
                        Owner_Name = binBalanceModel.Owner_Name,
                        Location_Index = binBalanceModel.Location_Index,
                        Location_Id = binBalanceModel.Location_Id,
                        Location_Name = binBalanceModel.Location_Name,
                        BinCardReserve_QtyBal = item.Reserve_Qty,

                        BinCardReserve_UnitWeightBal = binBalanceModel.BinBalance_UnitWeightBal,
                        BinCardReserve_UnitWeightBal_Index = binBalanceModel.BinBalance_UnitWeightBal_Index,
                        BinCardReserve_UnitWeightBal_Id = binBalanceModel.BinBalance_UnitWeightBal_Id,
                        BinCardReserve_UnitWeightBal_Name = binBalanceModel.BinBalance_UnitWeightBal_Name,
                        BinCardReserve_UnitWeightBalRatio = binBalanceModel.BinBalance_UnitWeightBalRatio,

                        BinCardReserve_WeightBal = binBalanceModel.BinBalance_WeightBal,
                        BinCardReserve_WeightBal_Index = binBalanceModel.BinBalance_WeightBal_Index,
                        BinCardReserve_WeightBal_Id = binBalanceModel.BinBalance_WeightBal_Id,
                        BinCardReserve_WeightBal_Name = binBalanceModel.BinBalance_WeightBal_Name,
                        BinCardReserve_WeightBalRatio = binBalanceModel.BinBalance_WeightBalRatio,

                        BinCardReserve_NetWeightBal = binBalanceModel.BinBalance_NetWeightBal,
                        BinCardReserve_NetWeightBal_Index = binBalanceModel.BinBalance_NetWeightBal_Index,
                        BinCardReserve_NetWeightBal_Id = binBalanceModel.BinBalance_NetWeightBal_Id,
                        BinCardReserve_NetWeightBal_Name = binBalanceModel.BinBalance_NetWeightBal_Name,
                        BinCardReserve_NetWeightBalRatio = binBalanceModel.BinBalance_NetWeightBalRatio,

                        BinCardReserve_GrsWeightBal = binBalanceModel.BinBalance_GrsWeightBal,
                        BinCardReserve_GrsWeightBal_Index = binBalanceModel.BinBalance_GrsWeightBal_Index,
                        BinCardReserve_GrsWeightBal_Id = binBalanceModel.BinBalance_GrsWeightBal_Id,
                        BinCardReserve_GrsWeightBal_Name = binBalanceModel.BinBalance_GrsWeightBal_Name,
                        BinCardReserve_GrsWeightBalRatio = binBalanceModel.BinBalance_GrsWeightBalRatio,

                        BinCardReserve_UnitWidthBal = binBalanceModel.BinBalance_UnitWidthBal,
                        BinCardReserve_UnitWidthBal_Index = binBalanceModel.BinBalance_UnitWidthBal_Index,
                        BinCardReserve_UnitWidthBal_Id = binBalanceModel.BinBalance_UnitWidthBal_Id,
                        BinCardReserve_UnitWidthBal_Name = binBalanceModel.BinBalance_UnitWidthBal_Name,
                        BinCardReserve_UnitWidthBalRatio = binBalanceModel.BinBalance_UnitWidthBalRatio,

                        BinCardReserve_WidthBal = binBalanceModel.BinBalance_WidthBal,
                        BinCardReserve_WidthBal_Index = binBalanceModel.BinBalance_WidthBal_Index,
                        BinCardReserve_WidthBal_Id = binBalanceModel.BinBalance_WidthBal_Id,
                        BinCardReserve_WidthBal_Name = binBalanceModel.BinBalance_WidthBal_Name,
                        BinCardReserve_WidthBalRatio = binBalanceModel.BinBalance_WidthBalRatio,

                        BinCardReserve_UnitLengthBal = binBalanceModel.BinBalance_UnitLengthBal,
                        BinCardReserve_UnitLengthBal_Index = binBalanceModel.BinBalance_UnitLengthBal_Index,
                        BinCardReserve_UnitLengthBal_Id = binBalanceModel.BinBalance_UnitLengthBal_Id,
                        BinCardReserve_UnitLengthBal_Name = binBalanceModel.BinBalance_UnitLengthBal_Name,
                        BinCardReserve_UnitLengthBalRatio = binBalanceModel.BinBalance_UnitLengthBalRatio,

                        BinCardReserve_LengthBal = binBalanceModel.BinBalance_LengthBal,
                        BinCardReserve_LengthBal_Index = binBalanceModel.BinBalance_LengthBal_Index,
                        BinCardReserve_LengthBal_Id = binBalanceModel.BinBalance_LengthBal_Id,
                        BinCardReserve_LengthBal_Name = binBalanceModel.BinBalance_LengthBal_Name,
                        BinCardReserve_LengthBalRatio = binBalanceModel.BinBalance_LengthBalRatio,

                        BinCardReserve_UnitHeightBal = binBalanceModel.BinBalance_UnitHeightBal,
                        BinCardReserve_UnitHeightBal_Index = binBalanceModel.BinBalance_UnitHeightBal_Index,
                        BinCardReserve_UnitHeightBal_Id = binBalanceModel.BinBalance_UnitHeightBal_Id,
                        BinCardReserve_UnitHeightBal_Name = binBalanceModel.BinBalance_UnitHeightBal_Name,
                        BinCardReserve_UnitHeightBalRatio = binBalanceModel.BinBalance_UnitHeightBalRatio,

                        BinCardReserve_HeightBal = binBalanceModel.BinBalance_HeightBal,
                        BinCardReserve_HeightBal_Index = binBalanceModel.BinBalance_HeightBal_Index,
                        BinCardReserve_HeightBal_Id = binBalanceModel.BinBalance_HeightBal_Id,
                        BinCardReserve_HeightBal_Name = binBalanceModel.BinBalance_HeightBal_Name,
                        BinCardReserve_HeightBalRatio = binBalanceModel.BinBalance_HeightBalRatio,

                        BinCardReserve_VolumeBal = binBalanceModel.BinBalance_VolumeBal,

                        UnitPrice = binBalanceModel.UnitPrice,
                        UnitPrice_Index = binBalanceModel.UnitPrice_Index,
                        UnitPrice_Id = binBalanceModel.UnitPrice_Id,
                        UnitPrice_Name = binBalanceModel.UnitPrice_Name,

                        Price = binBalanceModel.Price,
                        Price_Index = binBalanceModel.Price_Index,
                        Price_Id = binBalanceModel.Price_Id,
                        Price_Name = binBalanceModel.Price_Name,

                        Ref_Document_Index = item.Ref_Document_Index,
                        Ref_DocumentItem_Index = item.Ref_DocumentItem_Index,
                        Ref_Document_No = item.Ref_Document_No,
                        Ref_Wave_Index = item.Ref_Wave_Index ?? string.Empty,

                        Create_By = item.Reserve_By,
                        Create_Date = reserveDate
                    };

                    db.wm_BinCardReserve.Add(binCardReserveModel);

                    result.Items.Add(
                        new ReserveBinBalanceResultItemModel()
                        {
                            BinBalance_Index = item.BinBalance_Index,
                            BinCardReserve_Index = binCardReserveIndex,
                            BinBalance_Model = item.IsReturnBinBalanceModel ? binBalanceModel : null,
                            BinCardReserve_Model = item.IsReturninCardReserveModel ? binCardReserveModel : null
                        }
                    );
                }

                var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception exSave)
                {
                    transaction.Rollback();
                    throw exSave;
                }

                result.resultIsUse = true;
                result.resultMsg = "ReserveBinBalance Successfully";
            }
            catch (Exception ex)
            {
                string errorMessage = "ReserveBinBalance Exception :" + ex.Message;
                logging.logging("ReserveBinBalance", errorMessage);

                result.Items.Clear();
                result.resultIsUse = false;
                result.resultMsg = errorMessage;
            }

            return result;
        }

        public bool CutSlotsBinBalance(goodsIssueItemLocationFilterViewModel model)
        {
            String State = "Start " + model.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var binbalance = db.wm_BinBalance.Find(model.binBalance_Index);
                var BinCardReserve = db.wm_BinCardReserve.FirstOrDefault(c => c.Ref_Document_Index == model.goodsIssue_Index && c.Ref_DocumentItem_Index == model.goodsIssueItemLocation_Index);

                if ((model.totalQty - model.picking_TotalQty) > 0)
                {
                    var clearReserveBinbalace = db.wm_BinBalance.Find(BinCardReserve.BinBalance_Index);
                    clearReserveBinbalace.BinBalance_QtyReserve = clearReserveBinbalace.BinBalance_QtyReserve - (model.totalQty - model.picking_TotalQty);
                    binbalance.Update_By = model.create_By;
                    binbalance.Update_Date = DateTime.Now;
                }

                BinCardReserve.BinCardReserve_Status = 2;

                var listTag = new List<DocumentViewModel> { new DocumentViewModel { document_No = model.tag_No } };
                var tag = new DocumentViewModel();
                tag.listDocumentViewModel = listTag;
                var CheckTag = utils.SendDataApi<List<LPNViewModel>>(new AppSettingConfig().GetUrl("CheckTag"), tag.sJson());


                var BinCard = new wm_BinCard();
                BinCard.BinCard_Index = Guid.NewGuid();
                BinCard.Process_Index = model.process_Index;
                BinCard.DocumentType_Index = model.documentType_Index;
                BinCard.DocumentType_Id = model.documentType_Id;
                BinCard.DocumentType_Name = model.documentType_Name;
                BinCard.GoodsReceive_Index = binbalance.GoodsReceive_Index;
                BinCard.GoodsReceiveItem_Index = binbalance.GoodsReceiveItem_Index;
                BinCard.GoodsReceiveItemLocation_Index = binbalance.GoodsReceiveItemLocation_Index;
                BinCard.BinCard_No = model.ref_Document_No;
                BinCard.BinCard_Date = model.goodsIssue_Date;
                BinCard.TagItem_Index = binbalance.TagItem_Index;
                BinCard.Tag_Index = CheckTag.Count() == 0 ? binbalance.Tag_Index : CheckTag.FirstOrDefault().tag_Index;
                BinCard.Tag_No = model.tag_No;
                BinCard.Tag_Index_To = CheckTag.Count() == 0 ? binbalance.Tag_Index : CheckTag.FirstOrDefault().tag_Index;
                BinCard.Tag_No_To = model.tag_No;
                BinCard.Product_Index = model.product_Index;
                BinCard.Product_Id = model.product_Id;
                BinCard.Product_Name = model.product_Name;
                BinCard.Product_SecondName = model.product_SecondName;
                BinCard.Product_ThirdName = model.product_ThirdName;
                BinCard.Product_Index_To = model.product_Index; //BinCardReserveItem.Product_Index_To;
                BinCard.Product_Id_To = model.product_Id;
                BinCard.Product_Name_To = model.product_Name;
                BinCard.Product_SecondName_To = model.product_SecondName;
                BinCard.Product_ThirdName_To = model.product_ThirdName;
                BinCard.Product_Lot = model.product_Lot;
                BinCard.Product_Lot_To = model.product_Lot;
                BinCard.ItemStatus_Index = model.itemStatus_Index;
                BinCard.ItemStatus_Id = model.itemStatus_Id;
                BinCard.ItemStatus_Name = model.itemStatus_Name;

                BinCard.ItemStatus_Index_To = model.itemStatus_Index;
                BinCard.ItemStatus_Id_To = model.itemStatus_Id;
                BinCard.ItemStatus_Name_To = model.itemStatus_Name;

                BinCard.ProductConversion_Index = model.productConversion_Index;
                BinCard.ProductConversion_Id = model.productConversion_Id;
                BinCard.ProductConversion_Name = model.productConversion_Name;

                BinCard.Owner_Index = model.owner_Index;
                BinCard.Owner_Id = model.owner_Id;
                BinCard.Owner_Name = model.owner_Name;

                BinCard.Owner_Index_To = model.owner_Index;
                BinCard.Owner_Id_To = model.owner_Id;
                BinCard.Owner_Name_To = model.owner_Name;

                BinCard.Location_Index = model.location_Index;//BinCardReserveItem.Location_Index;
                BinCard.Location_Id = model.location_Id; //BinCardReserveItem.Location_Id;
                BinCard.Location_Name = model.location_Name;//BinCardReserveItem.Location_Name;

                BinCard.Location_Index_To = model.location_Index;
                BinCard.Location_Id_To = model.location_Id;
                BinCard.Location_Name_To = model.location_Name;


                BinCard.GoodsReceive_EXP_Date = model.eXP_Date;
                BinCard.GoodsReceive_EXP_Date_To = model.eXP_Date;
                BinCard.BinCard_QtyIn = 0;
                BinCard.BinCard_QtyOut = model.picking_TotalQty;
                BinCard.BinCard_QtySign = model.picking_TotalQty * -1;

                //Out
                BinCard.BinCard_UnitWeightOut = binbalance.BinBalance_UnitWeightBal;
                BinCard.BinCard_UnitWeightOut_Index = binbalance.BinBalance_UnitWeightBal_Index;
                BinCard.BinCard_UnitWeightOut_Id = binbalance.BinBalance_UnitWeightBal_Id;
                BinCard.BinCard_UnitWeightOut_Name = binbalance.BinBalance_UnitWeightBal_Name;
                BinCard.BinCard_UnitWeightOutRatio = binbalance.BinBalance_UnitWeightBalRatio;

                BinCard.BinCard_WeightOut = (model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitWeightBal ?? 0);
                BinCard.BinCard_WeightOut_Index = binbalance.BinBalance_WeightBal_Index;
                BinCard.BinCard_WeightOut_Id = binbalance.BinBalance_WeightBal_Id;
                BinCard.BinCard_WeightOut_Name = binbalance.BinBalance_WeightBal_Name;
                BinCard.BinCard_WeightOutRatio = binbalance.BinBalance_WeightBalRatio;

                BinCard.BinCard_UnitNetWeightOut = binbalance.BinBalance_UnitNetWeightBal;
                BinCard.BinCard_UnitNetWeightOut_Index = binbalance.BinBalance_UnitNetWeightBal_Index;
                BinCard.BinCard_UnitNetWeightOut_Id = binbalance.BinBalance_UnitNetWeightBal_Id;
                BinCard.BinCard_UnitNetWeightOut_Name = binbalance.BinBalance_UnitNetWeightBal_Name;
                BinCard.BinCard_UnitNetWeightOutRatio = binbalance.BinBalance_UnitNetWeightBalRatio;

                BinCard.BinCard_NetWeightOut = (model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitNetWeightBal ?? 0);
                BinCard.BinCard_NetWeightOut_Index = binbalance.BinBalance_NetWeightBal_Index;
                BinCard.BinCard_NetWeightOut_Id = binbalance.BinBalance_NetWeightBal_Id;
                BinCard.BinCard_NetWeightOut_Name = binbalance.BinBalance_NetWeightBal_Name;
                BinCard.BinCard_NetWeightOutRatio = binbalance.BinBalance_NetWeightBalRatio;

                BinCard.BinCard_UnitGrsWeightOut = binbalance.BinBalance_UnitGrsWeightBal;
                BinCard.BinCard_UnitGrsWeightOut_Index = binbalance.BinBalance_UnitGrsWeightBal_Index;
                BinCard.BinCard_UnitGrsWeightOut_Id = binbalance.BinBalance_UnitGrsWeightBal_Id;
                BinCard.BinCard_UnitGrsWeightOut_Name = binbalance.BinBalance_UnitGrsWeightBal_Name;
                BinCard.BinCard_UnitGrsWeightOutRatio = binbalance.BinBalance_UnitGrsWeightBalRatio;

                BinCard.BinCard_GrsWeightOut = (model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitGrsWeightBal ?? 0);
                BinCard.BinCard_GrsWeightOut_Index = binbalance.BinBalance_GrsWeightBal_Index;
                BinCard.BinCard_GrsWeightOut_Id = binbalance.BinBalance_GrsWeightBal_Id;
                BinCard.BinCard_GrsWeightOut_Name = binbalance.BinBalance_GrsWeightBal_Name;
                BinCard.BinCard_GrsWeightOutRatio = binbalance.BinBalance_GrsWeightBalRatio;

                BinCard.BinCard_UnitWidthOut = (binbalance.BinBalance_UnitWidthBal ?? 0);
                BinCard.BinCard_UnitWidthOut_Index = binbalance.BinBalance_UnitWidthBal_Index;
                BinCard.BinCard_UnitWidthOut_Id = binbalance.BinBalance_UnitWidthBal_Id;
                BinCard.BinCard_UnitWidthOut_Name = binbalance.BinBalance_UnitWidthBal_Name;
                BinCard.BinCard_UnitWidthOutRatio = binbalance.BinBalance_UnitWidthBalRatio;

                BinCard.BinCard_WidthOut = (model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitWidthBal ?? 0);
                BinCard.BinCard_WidthOut_Index = binbalance.BinBalance_WidthBal_Index;
                BinCard.BinCard_WidthOut_Id = binbalance.BinBalance_WidthBal_Id;
                BinCard.BinCard_WidthOut_Name = binbalance.BinBalance_WidthBal_Name;
                BinCard.BinCard_WidthOutRatio = binbalance.BinBalance_WidthBalRatio;

                BinCard.BinCard_UnitLengthOut = (binbalance.BinBalance_UnitLengthBal ?? 0);
                BinCard.BinCard_UnitLengthOut_Index = binbalance.BinBalance_UnitLengthBal_Index;
                BinCard.BinCard_UnitLengthOut_Id = binbalance.BinBalance_UnitLengthBal_Id;
                BinCard.BinCard_UnitLengthOut_Name = binbalance.BinBalance_UnitLengthBal_Name;
                BinCard.BinCard_UnitLengthOutRatio = binbalance.BinBalance_UnitLengthBalRatio;

                BinCard.BinCard_LengthOut = (model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitLengthBal ?? 0);
                BinCard.BinCard_LengthOut_Index = binbalance.BinBalance_LengthBal_Index;
                BinCard.BinCard_LengthOut_Id = binbalance.BinBalance_LengthBal_Id;
                BinCard.BinCard_LengthOut_Name = binbalance.BinBalance_LengthBal_Name;
                BinCard.BinCard_LengthOutRatio = binbalance.BinBalance_LengthBalRatio;

                BinCard.BinCard_UnitHeightOut = (binbalance.BinBalance_UnitHeightBal ?? 0);
                BinCard.BinCard_UnitHeightOut_Index = binbalance.BinBalance_UnitHeightBal_Index;
                BinCard.BinCard_UnitHeightOut_Id = binbalance.BinBalance_UnitHeightBal_Id;
                BinCard.BinCard_UnitHeightOut_Name = binbalance.BinBalance_UnitHeightBal_Name;
                BinCard.BinCard_UnitHeightOutRatio = binbalance.BinBalance_UnitHeightBalRatio;

                BinCard.BinCard_HeightOut = (model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitHeightBal ?? 0);
                BinCard.BinCard_HeightOut_Index = binbalance.BinBalance_HeightBal_Index;
                BinCard.BinCard_HeightOut_Id = binbalance.BinBalance_HeightBal_Id;
                BinCard.BinCard_HeightOut_Name = binbalance.BinBalance_HeightBal_Name;
                BinCard.BinCard_HeightOutRatio = binbalance.BinBalance_HeightBalRatio;

                BinCard.BinCard_UnitVolumeOut = (binbalance.BinBalance_UnitVolumeBal ?? 0);
                BinCard.BinCard_VolumeOut = (model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitVolumeBal ?? 0);

                BinCard.BinCard_UnitPriceOut = binbalance.UnitPrice;
                BinCard.BinCard_UnitPriceOut_Index = binbalance.UnitPrice_Index;
                BinCard.BinCard_UnitPriceOut_Id = binbalance.UnitPrice_Id;
                BinCard.BinCard_UnitPriceOut_Name = binbalance.UnitPrice_Name;
                BinCard.BinCard_PriceOut = (model.picking_TotalQty ?? 0) * (binbalance.UnitPrice ?? 0);
                BinCard.BinCard_PriceOut_Index = binbalance.UnitPrice_Index;
                BinCard.BinCard_PriceOut_Id = binbalance.UnitPrice_Id;
                BinCard.BinCard_PriceOut_Name = binbalance.UnitPrice_Name;



                //Sign
                BinCard.BinCard_UnitWeightSign = binbalance.BinBalance_UnitWeightBal;
                BinCard.BinCard_UnitWeightSign_Index = binbalance.BinBalance_UnitWeightBal_Index;
                BinCard.BinCard_UnitWeightSign_Id = binbalance.BinBalance_UnitWeightBal_Id;
                BinCard.BinCard_UnitWeightSign_Name = binbalance.BinBalance_UnitWeightBal_Name;
                BinCard.BinCard_UnitWeightSignRatio = binbalance.BinBalance_UnitWeightBalRatio;

                BinCard.BinCard_WeightSign = (((model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitWeightBal ?? 0)) * -1);
                BinCard.BinCard_WeightSign_Index = binbalance.BinBalance_WeightBal_Index;
                BinCard.BinCard_WeightSign_Id = binbalance.BinBalance_WeightBal_Id;
                BinCard.BinCard_WeightSign_Name = binbalance.BinBalance_WeightBal_Name;
                BinCard.BinCard_WeightSignRatio = binbalance.BinBalance_WeightBalRatio;

                BinCard.BinCard_UnitNetWeightSign = binbalance.BinBalance_UnitNetWeightBal;
                BinCard.BinCard_UnitNetWeightSign_Index = binbalance.BinBalance_UnitNetWeightBal_Index;
                BinCard.BinCard_UnitNetWeightSign_Id = binbalance.BinBalance_UnitNetWeightBal_Id;
                BinCard.BinCard_UnitNetWeightSign_Name = binbalance.BinBalance_UnitNetWeightBal_Name;
                BinCard.BinCard_UnitNetWeightSignRatio = binbalance.BinBalance_UnitNetWeightBalRatio;

                BinCard.BinCard_NetWeightSign = (((model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitNetWeightBal ?? 0)) * -1);
                BinCard.BinCard_NetWeightSign_Index = binbalance.BinBalance_NetWeightBal_Index;
                BinCard.BinCard_NetWeightSign_Id = binbalance.BinBalance_NetWeightBal_Id;
                BinCard.BinCard_NetWeightSign_Name = binbalance.BinBalance_NetWeightBal_Name;
                BinCard.BinCard_NetWeightSignRatio = binbalance.BinBalance_NetWeightBalRatio;

                BinCard.BinCard_UnitGrsWeightSign = binbalance.BinBalance_UnitGrsWeightBal;
                BinCard.BinCard_UnitGrsWeightSign_Index = binbalance.BinBalance_UnitGrsWeightBal_Index;
                BinCard.BinCard_UnitGrsWeightSign_Id = binbalance.BinBalance_UnitGrsWeightBal_Id;
                BinCard.BinCard_UnitGrsWeightSign_Name = binbalance.BinBalance_UnitGrsWeightBal_Name;
                BinCard.BinCard_UnitGrsWeightSignRatio = binbalance.BinBalance_UnitGrsWeightBalRatio;

                BinCard.BinCard_GrsWeightSign = (((model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitGrsWeightBal ?? 0)) * -1);
                BinCard.BinCard_GrsWeightSign_Index = binbalance.BinBalance_GrsWeightBal_Index;
                BinCard.BinCard_GrsWeightSign_Id = binbalance.BinBalance_GrsWeightBal_Id;
                BinCard.BinCard_GrsWeightSign_Name = binbalance.BinBalance_GrsWeightBal_Name;
                BinCard.BinCard_GrsWeightSignRatio = binbalance.BinBalance_GrsWeightBalRatio;

                BinCard.BinCard_UnitWidthSign = (binbalance.BinBalance_UnitWidthBal ?? 0);
                BinCard.BinCard_UnitWidthSign_Index = binbalance.BinBalance_UnitWidthBal_Index;
                BinCard.BinCard_UnitWidthSign_Id = binbalance.BinBalance_UnitWidthBal_Id;
                BinCard.BinCard_UnitWidthSign_Name = binbalance.BinBalance_UnitWidthBal_Name;
                BinCard.BinCard_UnitWidthSignRatio = binbalance.BinBalance_UnitWidthBalRatio;

                BinCard.BinCard_WidthSign = (((model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitWidthBal ?? 0)) * -1);
                BinCard.BinCard_WidthSign_Index = binbalance.BinBalance_WidthBal_Index;
                BinCard.BinCard_WidthSign_Id = binbalance.BinBalance_WidthBal_Id;
                BinCard.BinCard_WidthSign_Name = binbalance.BinBalance_WidthBal_Name;
                BinCard.BinCard_WidthSignRatio = binbalance.BinBalance_WidthBalRatio;

                BinCard.BinCard_UnitLengthSign = (binbalance.BinBalance_UnitLengthBal ?? 0);
                BinCard.BinCard_UnitLengthSign_Index = binbalance.BinBalance_UnitLengthBal_Index;
                BinCard.BinCard_UnitLengthSign_Id = binbalance.BinBalance_UnitLengthBal_Id;
                BinCard.BinCard_UnitLengthSign_Name = binbalance.BinBalance_UnitLengthBal_Name;
                BinCard.BinCard_UnitLengthSignRatio = binbalance.BinBalance_UnitLengthBalRatio;

                BinCard.BinCard_LengthSign = (((model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitLengthBal ?? 0)) * -1);
                BinCard.BinCard_LengthSign_Index = binbalance.BinBalance_LengthBal_Index;
                BinCard.BinCard_LengthSign_Id = binbalance.BinBalance_LengthBal_Id;
                BinCard.BinCard_LengthSign_Name = binbalance.BinBalance_LengthBal_Name;
                BinCard.BinCard_LengthSignRatio = binbalance.BinBalance_LengthBalRatio;

                BinCard.BinCard_UnitHeightSign = (binbalance.BinBalance_UnitHeightBal ?? 0);
                BinCard.BinCard_UnitHeightSign_Index = binbalance.BinBalance_UnitHeightBal_Index;
                BinCard.BinCard_UnitHeightSign_Id = binbalance.BinBalance_UnitHeightBal_Id;
                BinCard.BinCard_UnitHeightSign_Name = binbalance.BinBalance_UnitHeightBal_Name;
                BinCard.BinCard_UnitHeightSignRatio = binbalance.BinBalance_UnitHeightBalRatio;

                BinCard.BinCard_HeightSign = (((model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitHeightBal ?? 0)) * -1);
                BinCard.BinCard_HeightSign_Index = binbalance.BinBalance_HeightBal_Index;
                BinCard.BinCard_HeightSign_Id = binbalance.BinBalance_HeightBal_Id;
                BinCard.BinCard_HeightSign_Name = binbalance.BinBalance_HeightBal_Name;
                BinCard.BinCard_HeightSignRatio = binbalance.BinBalance_HeightBalRatio;

                BinCard.BinCard_UnitVolumeSign = (binbalance.BinBalance_UnitVolumeBal ?? 0);
                BinCard.BinCard_VolumeSign = (((model.picking_TotalQty ?? 0) * (binbalance.BinBalance_UnitVolumeBal ?? 0)) * -1);

                BinCard.BinCard_UnitPriceSign = binbalance.UnitPrice;
                BinCard.BinCard_UnitPriceSign_Index = binbalance.UnitPrice_Index;
                BinCard.BinCard_UnitPriceSign_Id = binbalance.UnitPrice_Id;
                BinCard.BinCard_UnitPriceSign_Name = binbalance.UnitPrice_Name;
                BinCard.BinCard_PriceSign = (((model.picking_TotalQty ?? 0) * (binbalance.UnitPrice ?? 0)) * -1);
                BinCard.BinCard_PriceSign_Index = binbalance.UnitPrice_Index;
                BinCard.BinCard_PriceSign_Id = binbalance.UnitPrice_Id;
                BinCard.BinCard_PriceSign_Name = binbalance.UnitPrice_Name;

                //if (binbalance.BinBalance_WeightBegin != 0)
                //{
                //    var unitWeight = binbalance.BinBalance_WeightBegin / binbalance.BinBalance_QtyBegin;
                //    BinCard.BinCard_WeightIn = 0;
                //    BinCard.BinCard_WeightOut = model.totalQty * unitWeight;
                //    BinCard.BinCard_WeightSign = (model.totalQty * unitWeight) * -1;
                //}
                //else
                //{
                //    BinCard.BinCard_WeightIn = 0;
                //    BinCard.BinCard_WeightOut = 0;
                //    BinCard.BinCard_WeightSign = 0;
                //}

                //if (binbalance.BinBalance_VolumeBegin != 0)
                //{
                //    var unitVol = binbalance.BinBalance_VolumeBegin / binbalance.BinBalance_QtyBegin;

                //    BinCard.BinCard_VolumeIn = 0;
                //    BinCard.BinCard_VolumeOut = model.totalQty * unitVol;
                //    BinCard.BinCard_VolumeSign = (model.totalQty * unitVol) * -1;
                //}
                //else
                //{
                //    BinCard.BinCard_VolumeIn = 0;
                //    BinCard.BinCard_VolumeOut = 0;
                //    BinCard.BinCard_VolumeSign = 0;

                //}


                BinCard.Ref_Document_No = model.ref_Document_No;
                BinCard.Ref_Document_Index = model.ref_Document_Index; //tem.Ref_Document_Index;
                BinCard.Ref_DocumentItem_Index = model.ref_DocumentItem_Index;
                BinCard.Create_By = model.create_By;
                BinCard.Create_Date = DateTime.Now;
                BinCard.BinBalance_Index = binbalance.BinBalance_Index;
                BinCard.ERP_Location = binbalance.ERP_Location;
                BinCard.ERP_Location_To = binbalance.ERP_Location;

                binbalance.BinBalance_QtyBal = binbalance.BinBalance_QtyBal - model.picking_TotalQty;
                //binbalance.BinBalance_QtyReserve = binbalance.BinBalance_Index == BinCardReserve.BinBalance_Index ? 0 : binbalance.BinBalance_QtyReserve - model.picking_TotalQty;
                binbalance.BinBalance_QtyReserve = binbalance.BinBalance_QtyReserve - model.picking_TotalQty;
                //binbalance.BinBalance_WeightReserve = binbalance.BinBalance_WeightReserve - model.weight;
                //binbalance.BinBalance_VolumeReserve = binbalance.BinBalance_VolumeReserve - model.volume;

                if (binbalance.BinBalance_WeightBegin != 0)
                {
                    var WeightReserve = (model.picking_TotalQty * binbalance.BinBalance_UnitWeightBal);
                    binbalance.BinBalance_WeightReserve = binbalance.BinBalance_WeightReserve - WeightReserve;
                    binbalance.BinBalance_WeightBal = binbalance.BinBalance_WeightBal - WeightReserve;
                }

                if (binbalance.BinBalance_NetWeightBegin != 0)
                {
                    var NetWeightReserve = (model.picking_TotalQty * binbalance.BinBalance_UnitNetWeightBal);
                    binbalance.BinBalance_NetWeightReserve = binbalance.BinBalance_NetWeightReserve - NetWeightReserve;
                    binbalance.BinBalance_NetWeightBal = binbalance.BinBalance_NetWeightBal - NetWeightReserve;
                }


                if (binbalance.BinBalance_GrsWeightBegin != 0)
                {
                    var GrsWeightReserve = (model.picking_TotalQty * binbalance.BinBalance_UnitGrsWeightBal);
                    binbalance.BinBalance_GrsWeightReserve = binbalance.BinBalance_GrsWeightReserve - GrsWeightReserve;
                    binbalance.BinBalance_GrsWeightBal = binbalance.BinBalance_GrsWeightBal - GrsWeightReserve;
                }


                if (binbalance.BinBalance_WidthBegin != 0)
                {
                    var WidthReserve = (model.picking_TotalQty * binbalance.BinBalance_UnitWidthBal);
                    binbalance.BinBalance_WidthReserve = binbalance.BinBalance_WidthReserve - WidthReserve;
                    binbalance.BinBalance_WidthBal = binbalance.BinBalance_WidthBal - WidthReserve;
                }


                if (binbalance.BinBalance_LengthBegin != 0)
                {
                    var LengthReserve = (model.picking_TotalQty * binbalance.BinBalance_UnitLengthBal);
                    binbalance.BinBalance_LengthReserve = binbalance.BinBalance_LengthReserve - LengthReserve;
                    binbalance.BinBalance_LengthBal = binbalance.BinBalance_LengthBal - LengthReserve;
                }


                if (binbalance.BinBalance_HeightBegin != 0)
                {
                    var HeightReserve = (model.picking_TotalQty * binbalance.BinBalance_UnitHeightBal);
                    binbalance.BinBalance_HeightReserve = binbalance.BinBalance_HeightReserve - HeightReserve;
                    binbalance.BinBalance_HeightBal = binbalance.BinBalance_HeightBal - HeightReserve;
                }

                if (binbalance.BinBalance_VolumeBegin != 0)
                {
                    var VolReserve = (model.picking_TotalQty * binbalance.BinBalance_UnitVolumeBal);
                    binbalance.BinBalance_VolumeReserve = binbalance.BinBalance_VolumeReserve - VolReserve;
                    binbalance.BinBalance_VolumeBal = binbalance.BinBalance_VolumeBal - VolReserve;
                }

                if ((binbalance.UnitPrice ?? 0 ) != 0)
                {
                    var VoltPrice = (model.picking_TotalQty * binbalance.UnitPrice);
                    binbalance.Price = binbalance.Price - VoltPrice;
                }


                binbalance.Update_By = model.create_By;
                binbalance.Update_Date = DateTime.Now;

                var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.wm_BinCard.Add(BinCard);
                    db.SaveChanges();
                    transaction.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("CutSlotsBinBalance", msglog);
                    transaction.Rollback();
                    throw exy;
                }

                return true;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("CutSlotsBinBalance", msglog);
                return false;
            }

        }

        public bool UpdateBinbalanceQIareUU(GoodsTransferItemViewModel model)
        {
            String State = "Start " + model.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {

                if (model.binBalance_Index == null)
                {
                    return false;

                }
                var getitemStatus = utils.SendDataApi<List<ItemStatusDocViewModel>>(new AppSettingConfig().GetUrl("GetItemStatus"), new { }.sJson());

                var itemStatus = getitemStatus.FirstOrDefault(c => c.itemStatus_Index == Guid.Parse("C043169D-1D73-421B-9E33-69C770DCC3B4"));

                var binbalance = db.wm_BinBalance.Find(model.binBalance_Index);
                binbalance.ItemStatus_Index = itemStatus.itemStatus_Index;
                binbalance.ItemStatus_Id = itemStatus.itemStatus_Id;
                binbalance.ItemStatus_Name = itemStatus.itemStatus_Name;
                binbalance.GoodsReceive_MFG_Date = DateTime.Now;//model.transfer_Date;
                binbalance.Update_By = model.username;
                binbalance.Update_Date = DateTime.Now;

                var transaction = db.Database.BeginTransaction();
                try
                {
                    db.SaveChanges();
                    transaction.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("CutSlotsBinBalance", msglog);
                    transaction.Rollback();
                    throw exy;
                }
                return true;


            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("CutSlotsBinBalance", msglog);
                return false;
            }

        }

        public String checkProductLocation(chekcProductLocationViewModel data)
        {
            try
            {

                decimal? Sum = 0;

                var query = db.wm_BinBalance.Where(c => c.Tag_No == data.tagNo_New
                                                   && c.Owner_Index == data.owner_Index
                                                   && c.Product_Index == data.product_Index).ToList();

                var filterModel = new ProductViewModel();

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    filterModel.product_Index = data.product_Index;
                }
                var resultProduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProduct"), filterModel.sJson());

                //data.location_Name_To = data.location_Name;
                var resultLocation = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("GetLocationV2"), data.sJson());

                var checkLocation =  resultLocation.Where(c => c.blockPick != 1 && c.blockPut != 1).FirstOrDefault();

                if (checkLocation != null)
                {
                    if (query.Count > 0)
                    {
                        Sum = data.qty + query.Sum(s => s.BinBalance_QtyBal);

                        if (resultProduct.FirstOrDefault().qty_Per_Tag <= Sum)
                        {
                            return "ไม่สามารถย้ายได้ Qty เกิน Qty PerTag";
                        }
                    }
                    else
                    {
                        return "ไม่สามารถย้ายได้ พาเลทนี้เจ้าของสินค้าหรือสินค้าไม่ตรงกัน";
                    }
                }
                else
                {
                    return "ไม่สามารถย้ายได้ ตำแหน่งนี้มีการ Block";
                }




                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String checkLocation(chekcProductLocationViewModel data)
        {
            try
            {
                decimal? Sum = 0;

                var findLocationBin = db.wm_BinBalance.Where(c => c.Location_Name == data.location_Name_To).FirstOrDefault();

                if (findLocationBin != null)
                {
                    var query = db.wm_BinBalance.Where(c => c.Location_Name == data.location_Name_To
                   && c.Owner_Index == data.owner_Index
                   && c.Product_Index == data.product_Index).ToList();

                    if (query.Count <= 0)
                    {
                        return "ไม่สามารถย้ายได้ ตำแหน่งนี้เจ้าของสินค้าหรือสินค้าไม่ตรงกัน";
                    }

                    var filterModel = new ProductViewModel();

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                    {
                        filterModel.product_Index = data.product_Index;
                    }
                    var resultProduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProduct"), filterModel.sJson());

                    data.location_Name = data.location_Name_To;
                    data.location_Index = null;
                    var resultLocation = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("GetLocationV2"), data.sJson());

                    var checkLocation = resultLocation.Where(c => c.blockPick != 1 && c.blockPut != 1).FirstOrDefault();

                    if (checkLocation != null)
                    {
                        if (query.Count > 0)
                        {
                            Sum = data.qty + query.Sum(s => s.BinBalance_QtyBal);

                            if (resultProduct.FirstOrDefault().qty_Per_Tag <= Sum)
                            {
                                return "ไม่สามารถย้ายได้ Qty เกิน Qty PerTag";
                            }
                        }
                        else
                        {
                            return "ไม่สามารถย้ายได้ ตำแหน่งนี้เจ้าของสินค้าหรือสินค้าไม่ตรงกัน";
                        }
                        return "";
                    }
                    else
                    {
                        return "ไม่สามารถย้ายได้ ตำแหน่งนี้มีการ Block";
                    }
                }
                else
                {
                    return "";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BinBalanceViewModel> getBinbalanceGreaterThanZero(FilterBinbalanceViewModel model)
        {
            try
            {
                var items = new List<BinBalanceViewModel>();

                var querys = db.wm_BinBalance.Where(c => c.BinBalance_QtyBal > 0).AsQueryable();

                if (model.Owner_Index.ToString() != "" && model.Owner_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Owner_Index == model.Owner_Index);
                }
                if (!string.IsNullOrEmpty(model.Tag_No))
                {
                    querys = querys.Where(c => c.Tag_No == model.Tag_No);
                }
                if (!string.IsNullOrEmpty(model.Product_Index.ToString()) && model.Product_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Product_Index == model.Product_Index);
                }

                if (model.Product_Lot != null)
                {
                    if (model.Product_Lot.ToString() != "")
                    {
                        querys = querys.Where(c => c.Product_Lot == model.Product_Lot);
                    }
                }
                if (!string.IsNullOrEmpty(model.ItemStatus_Index.ToString()) && model.ItemStatus_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.ItemStatus_Index == model.ItemStatus_Index);
                }
                if (model.MFG_Date != null)
                {
                    if (model.MFG_Date.ToString() != "")
                    {

                    }
                }
                if (model.EXP_Date != null)
                {
                    if (model.EXP_Date.ToString() != "")
                    {

                    }
                }
                if (model.isUseAttribute == true)
                {
                    // ADD UDF 1-5 
                    if (model.UDF_1 != null)
                    {
                        querys = querys.Where(c => c.UDF_1 == model.UDF_1);
                    }

                    if (model.UDF_2 != null)
                    {
                        querys = querys.Where(c => c.UDF_2 == model.UDF_2);
                    }

                    if (model.UDF_3 != null)
                    {
                        querys = querys.Where(c => c.UDF_3 == model.UDF_3);
                    }

                    if (model.UDF_4 != null)
                    {
                        querys = querys.Where(c => c.UDF_4 == model.UDF_4);
                    }

                    if (model.UDF_5 != null)
                    {
                        querys = querys.Where(c => c.UDF_5 == model.UDF_5);
                    }
                }

                if (!string.IsNullOrEmpty(model.isuse))
                {
                    querys = querys.Where(c => c.IsUse == model.isuse);
                }

                var listdata = querys.ToList();

                foreach (var query in listdata)
                {
                    var item = new BinBalanceViewModel
                    {
                        binBalance_Index = query.BinBalance_Index,
                        owner_Index = query.Owner_Index,
                        owner_Id = query.Owner_Id,
                        owner_Name = query.Owner_Name,
                        location_Index = query.Location_Index,
                        location_Id = query.Location_Id,
                        location_Name = query.Location_Name,
                        goodsReceive_Index = query.GoodsReceive_Index,
                        goodsReceive_No = query.GoodsReceive_No,
                        goodsReceive_Date = query.GoodsReceive_Date,
                        goodsReceiveItem_Index = query.GoodsReceiveItem_Index,
                        goodsReceiveItemLocation_Index = query.GoodsReceiveItemLocation_Index,
                        tagItem_Index = query.TagItem_Index,
                        tag_Index = query.Tag_Index,
                        tag_No = query.Tag_No,
                        product_Index = query.Product_Index,
                        product_Id = query.Product_Id,
                        product_Name = query.Product_Name,
                        product_SecondName = query.Product_SecondName,
                        product_ThirdName = query.Product_ThirdName,
                        product_Lot = query.Product_Lot,
                        itemStatus_Index = query.ItemStatus_Index,
                        itemStatus_Id = query.ItemStatus_Id,
                        itemStatus_Name = query.ItemStatus_Name,
                        goodsReceive_MFG_Date = query.GoodsReceive_MFG_Date,
                        goodsReceive_EXP_Date = query.GoodsReceive_EXP_Date,
                        goodsReceive_ProductConversion_Index = query.GoodsReceive_ProductConversion_Index,
                        goodsReceive_ProductConversion_Id = query.GoodsReceive_ProductConversion_Id,
                        goodsReceive_ProductConversion_Name = query.GoodsReceive_ProductConversion_Name,

                        binBalance_Ratio = query.BinBalance_Ratio,
                        binBalance_QtyBegin = query.BinBalance_QtyBegin,
                        binBalance_WeightBegin = query.BinBalance_WeightBegin,
                        binBalance_WeightBegin_Index = query.BinBalance_WeightBegin_Index,
                        binBalance_WeightBegin_Id = query.BinBalance_WeightBegin_Id,
                        binBalance_WeightBegin_Name = query.BinBalance_WeightBegin_Name,
                        binBalance_WeightBeginRatio = query.BinBalance_WeightBeginRatio,
                        binBalance_NetWeightBegin = query.BinBalance_NetWeightBegin,
                        binBalance_NetWeightBegin_Index = query.BinBalance_NetWeightBegin_Index,
                        binBalance_NetWeightBegin_Id = query.BinBalance_NetWeightBegin_Id,
                        binBalance_NetWeightBegin_Name = query.BinBalance_NetWeightBegin_Name,
                        binBalance_NetWeightBeginRatio = query.BinBalance_NetWeightBeginRatio,
                        binBalance_GrsWeightBegin = query.BinBalance_GrsWeightBegin,
                        binBalance_GrsWeightBegin_Index = query.BinBalance_GrsWeightBegin_Index,
                        binBalance_GrsWeightBegin_Id = query.BinBalance_GrsWeightBegin_Id,
                        binBalance_GrsWeightBegin_Name = query.BinBalance_GrsWeightBegin_Name,
                        binBalance_GrsWeightBeginRatio = query.BinBalance_GrsWeightBeginRatio,
                        binBalance_WidthBegin = query.BinBalance_WidthBegin,
                        binBalance_WidthBegin_Index = query.BinBalance_WidthBegin_Index,
                        binBalance_WidthBegin_Id = query.BinBalance_WidthBegin_Id,
                        binBalance_WidthBegin_Name = query.BinBalance_WidthBegin_Name,
                        binBalance_WidthBeginRatio = query.BinBalance_WidthBeginRatio,
                        binBalance_LengthBegin = query.BinBalance_LengthBegin,
                        binBalance_LengthBegin_Index = query.BinBalance_LengthBegin_Index,
                        binBalance_LengthBegin_Id = query.BinBalance_LengthBegin_Id,
                        binBalance_LengthBegin_Name = query.BinBalance_LengthBegin_Name,
                        binBalance_LengthBeginRatio = query.BinBalance_LengthBeginRatio,
                        binBalance_HeightBegin = query.BinBalance_HeightBegin,
                        binBalance_HeightBegin_Index = query.BinBalance_HeightBegin_Index,
                        binBalance_HeightBegin_Id = query.BinBalance_HeightBegin_Id,
                        binBalance_HeightBegin_Name = query.BinBalance_HeightBegin_Name,
                        binBalance_HeightBeginRatio = query.BinBalance_HeightBeginRatio,
                        binBalance_UnitVolumeBegin = query.BinBalance_UnitVolumeBegin,
                        binBalance_VolumeBegin = query.BinBalance_VolumeBegin,
                        binBalance_QtyBal = query.BinBalance_QtyBal,
                        binBalance_WeightBal = query.BinBalance_WeightBal,
                        binBalance_UnitWeightBal_Index = query.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = query.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = query.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = query.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitWeightBal = query.BinBalance_UnitWeightBal,
                        binBalance_WeightBal_Index = query.BinBalance_WeightBal_Index,
                        binBalance_WeightBal_Id = query.BinBalance_WeightBal_Id,
                        binBalance_WeightBal_Name = query.BinBalance_WeightBal_Name,
                        binBalance_WeightBalRatio = query.BinBalance_WeightBalRatio,
                        binBalance_UnitNetWeightBal = query.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = query.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = query.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = query.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = query.BinBalance_UnitNetWeightBalRatio,
                        binBalance_NetWeightBal = query.BinBalance_NetWeightBal,
                        binBalance_NetWeightBal_Index = query.BinBalance_NetWeightBal_Index,
                        binBalance_NetWeightBal_Id = query.BinBalance_NetWeightBal_Id,
                        binBalance_NetWeightBal_Name = query.BinBalance_NetWeightBal_Name,
                        binBalance_NetWeightBalRatio = query.BinBalance_NetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = query.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = query.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = query.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = query.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = query.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_GrsWeightBal = query.BinBalance_GrsWeightBal,
                        binBalance_GrsWeightBal_Index = query.BinBalance_GrsWeightBal_Index,
                        binBalance_GrsWeightBal_Id = query.BinBalance_GrsWeightBal_Id,
                        binBalance_GrsWeightBal_Name = query.BinBalance_GrsWeightBal_Name,
                        binBalance_GrsWeightBalRatio = query.BinBalance_GrsWeightBalRatio,
                        binBalance_UnitWidthBal = query.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = query.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = query.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = query.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = query.BinBalance_UnitWidthBalRatio,
                        binBalance_WidthBal = query.BinBalance_WidthBal,
                        binBalance_WidthBal_Index = query.BinBalance_WidthBal_Index,
                        binBalance_WidthBal_Id = query.BinBalance_WidthBal_Id,
                        binBalance_WidthBal_Name = query.BinBalance_WidthBal_Name,
                        binBalance_WidthBalRatio = query.BinBalance_WidthBalRatio,
                        binBalance_UnitLengthBal = query.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = query.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = query.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = query.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = query.BinBalance_UnitLengthBalRatio,
                        binBalance_LengthBal = query.BinBalance_LengthBal,
                        binBalance_LengthBal_Index = query.BinBalance_LengthBal_Index,
                        binBalance_LengthBal_Id = query.BinBalance_LengthBal_Id,
                        binBalance_LengthBal_Name = query.BinBalance_LengthBal_Name,
                        binBalance_LengthBalRatio = query.BinBalance_LengthBalRatio,
                        binBalance_UnitHeightBal = query.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = query.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = query.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = query.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = query.BinBalance_UnitHeightBalRatio,
                        binBalance_HeightBal = query.BinBalance_HeightBal,
                        binBalance_HeightBal_Index = query.BinBalance_HeightBal_Index,
                        binBalance_HeightBal_Id = query.BinBalance_HeightBal_Id,
                        binBalance_HeightBal_Name = query.BinBalance_HeightBal_Name,
                        binBalance_HeightBalRatio = query.BinBalance_HeightBalRatio,
                        binBalance_UnitVolumeBal = query.BinBalance_UnitVolumeBal,
                        binBalance_VolumeBal = query.BinBalance_VolumeBal,
                        binBalance_QtyReserve = query.BinBalance_QtyReserve,
                        binBalance_WeightReserve = query.BinBalance_WeightReserve,
                        binBalance_WeightReserve_Index = query.BinBalance_WeightReserve_Index,
                        binBalance_WeightReserve_Id = query.BinBalance_WeightReserve_Id,
                        binBalance_WeightReserve_Name = query.BinBalance_WeightReserve_Name,
                        binBalance_WeightReserveRatio = query.BinBalance_WeightReserveRatio,
                        binBalance_NetWeightReserve = query.BinBalance_NetWeightReserve,
                        binBalance_NetWeightReserve_Index = query.BinBalance_NetWeightReserve_Index,
                        binBalance_NetWeightReserve_Id = query.BinBalance_NetWeightReserve_Id,
                        binBalance_NetWeightReserve_Name = query.BinBalance_NetWeightReserve_Name,
                        binBalance_NetWeightReserveRatio = query.BinBalance_NetWeightReserveRatio,
                        binBalance_GrsWeightReserve = query.BinBalance_GrsWeightReserve,
                        binBalance_GrsWeightReserve_Index = query.BinBalance_GrsWeightReserve_Index,
                        binBalance_GrsWeightReserve_Id = query.BinBalance_GrsWeightReserve_Id,
                        binBalance_GrsWeightReserve_Name = query.BinBalance_GrsWeightReserve_Name,
                        binBalance_GrsWeightReserveRatio = query.BinBalance_GrsWeightReserveRatio,
                        binBalance_WidthReserve = query.BinBalance_WidthReserve,
                        binBalance_WidthReserve_Index = query.BinBalance_WidthReserve_Index,
                        binBalance_WidthReserve_Id = query.BinBalance_WidthReserve_Id,
                        binBalance_WidthReserve_Name = query.BinBalance_WidthReserve_Name,
                        binBalance_WidthReserveRatio = query.BinBalance_WidthReserveRatio,
                        binBalance_LengthReserve = query.BinBalance_LengthReserve,
                        binBalance_LengthReserve_Index = query.BinBalance_LengthReserve_Index,
                        binBalance_LengthReserve_Id = query.BinBalance_LengthReserve_Id,
                        binBalance_LengthReserve_Name = query.BinBalance_LengthReserve_Name,
                        binBalance_LengthReserveRatio = query.BinBalance_LengthReserveRatio,
                        binBalance_HeightReserve = query.BinBalance_HeightReserve,
                        binBalance_HeightReserve_Index = query.BinBalance_HeightReserve_Index,
                        binBalance_HeightReserve_Id = query.BinBalance_HeightReserve_Id,
                        binBalance_HeightReserve_Name = query.BinBalance_HeightReserve_Name,
                        binBalance_HeightReserveRatio = query.BinBalance_HeightReserveRatio,
                        binBalance_UnitVolumeReserve = query.BinBalance_UnitVolumeReserve,
                        binBalance_VolumeReserve = query.BinBalance_VolumeReserve,

                        productConversion_Index = query.ProductConversion_Index,
                        productConversion_Id = query.ProductConversion_Id,
                        productConversion_Name = query.ProductConversion_Name,

                        unitPrice = query.UnitPrice,
                        unitPrice_Index = query.UnitPrice_Index,
                        unitPrice_Id = query.UnitPrice_Id,
                        unitPrice_Name = query.UnitPrice_Name,
                        price = query.Price,
                        price_Index = query.Price_Index,
                        price_Id = query.Price_Id,
                        price_Name = query.Price_Name,

                        uDF_1 = query.UDF_1,
                        uDF_2 = query.UDF_2,
                        uDF_3 = query.UDF_3,
                        uDF_4 = query.UDF_4,
                        uDF_5 = query.UDF_5,
                        create_By = query.Create_By,
                        create_Date = query.Create_Date,
                        update_By = query.Update_By,
                        update_Date = query.Update_Date,
                        cancel_By = query.Cancel_By,
                        cancel_Date = query.Cancel_Date,
                        isUse = query.IsUse,
                        binBalance_Status = query.BinBalance_Status,
                        //ageRemain = query.AgeRemain

                        invoice_No = query.Invoice_No,
                        declaration_No = query.Declaration_No,
                        hs_Code = query.HS_Code,
                        conutry_of_Origin = query.Conutry_of_Origin,
                        tax1 = query.Tax1,
                        tax1_Currency_Index = query.Tax1_Currency_Index,
                        tax1_Currency_Id = query.Tax1_Currency_Id,
                        tax1_Currency_Name = query.Tax1_Currency_Name,
                        tax2 = query.Tax2,
                        tax2_Currency_Index = query.Tax2_Currency_Index,
                        tax2_Currency_Id = query.Tax2_Currency_Id,
                        tax2_Currency_Name = query.Tax2_Currency_Name,
                        tax3 = query.Tax3,
                        tax3_Currency_Index = query.Tax3_Currency_Index,
                        tax3_Currency_Id = query.Tax3_Currency_Id,
                        tax3_Currency_Name = query.Tax3_Currency_Name,
                        tax4 = query.Tax4,
                        tax4_Currency_Index = query.Tax4_Currency_Index,
                        tax4_Currency_Id = query.Tax4_Currency_Id,
                        tax4_Currency_Name = query.Tax4_Currency_Name,
                        tax5 = query.Tax5,
                        tax5_Currency_Index = query.Tax5_Currency_Index,
                        tax5_Currency_Id = query.Tax5_Currency_Id,
                        tax5_Currency_Name = query.Tax5_Currency_Name,

                    };
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BinBalanceViewModel> getBinbalanceGreaterThanZeroV2(FilterBinbalanceViewModel model)
        {
            try
            {
                var items = new List<BinBalanceViewModel>();

                var querys = db.wm_BinBalance.Where(c => c.BinBalance_QtyBal > 0).AsQueryable();

                if (model.Owner_Index.ToString() != "" && model.Owner_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Owner_Index == model.Owner_Index);
                }
                if (!string.IsNullOrEmpty(model.Tag_No))
                {
                    querys = querys.Where(c => c.Tag_No == model.Tag_No);
                }
                if (!string.IsNullOrEmpty(model.Product_Index.ToString()) && model.Product_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Product_Index == model.Product_Index);
                }

                if (!string.IsNullOrEmpty(model.Location_Index.ToString()) && model.Location_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Location_Index == model.Location_Index);
                }

                if (model.Product_Lot != null)
                {
                    if (model.Product_Lot.ToString() != "")
                    {
                        querys = querys.Where(c => c.Product_Lot == model.Product_Lot);
                    }
                }
                if (!string.IsNullOrEmpty(model.ItemStatus_Index.ToString()) && model.ItemStatus_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.ItemStatus_Index == model.ItemStatus_Index);
                }
                if (model.MFG_Date != null)
                {
                    if (model.MFG_Date.ToString() != "")
                    {

                    }
                }
                if (model.EXP_Date != null)
                {
                    if (model.EXP_Date.ToString() != "")
                    {

                    }
                }
                if (model.isUseAttribute == true)
                {
                    // ADD UDF 1-5 
                    if (model.UDF_1 != null)
                    {
                        querys = querys.Where(c => c.UDF_1 == model.UDF_1);
                    }

                    if (model.UDF_2 != null)
                    {
                        querys = querys.Where(c => c.UDF_2 == model.UDF_2);
                    }

                    if (model.UDF_3 != null)
                    {
                        querys = querys.Where(c => c.UDF_3 == model.UDF_3);
                    }

                    if (model.UDF_4 != null)
                    {
                        querys = querys.Where(c => c.UDF_4 == model.UDF_4);
                    }

                    if (model.UDF_5 != null)
                    {
                        querys = querys.Where(c => c.UDF_5 == model.UDF_5);
                    }
                }

                if (!string.IsNullOrEmpty(model.isuse))
                {
                    querys = querys.Where(c => c.IsUse == model.isuse);
                }

                var listdata = querys.ToList();

                foreach (var query in listdata)
                {
                    var item = new BinBalanceViewModel
                    {
                        binBalance_Index = query.BinBalance_Index,
                        owner_Index = query.Owner_Index,
                        owner_Id = query.Owner_Id,
                        owner_Name = query.Owner_Name,
                        location_Index = query.Location_Index,
                        location_Id = query.Location_Id,
                        location_Name = query.Location_Name,
                        goodsReceive_Index = query.GoodsReceive_Index,
                        goodsReceive_No = query.GoodsReceive_No,
                        goodsReceive_Date = query.GoodsReceive_Date,
                        goodsReceiveItem_Index = query.GoodsReceiveItem_Index,
                        goodsReceiveItemLocation_Index = query.GoodsReceiveItemLocation_Index,
                        tagItem_Index = query.TagItem_Index,
                        tag_Index = query.Tag_Index,
                        tag_No = query.Tag_No,
                        product_Index = query.Product_Index,
                        product_Id = query.Product_Id,
                        product_Name = query.Product_Name,
                        product_SecondName = query.Product_SecondName,
                        product_ThirdName = query.Product_ThirdName,
                        product_Lot = query.Product_Lot,
                        itemStatus_Index = query.ItemStatus_Index,
                        itemStatus_Id = query.ItemStatus_Id,
                        itemStatus_Name = query.ItemStatus_Name,
                        goodsReceive_MFG_Date = query.GoodsReceive_MFG_Date,
                        goodsReceive_EXP_Date = query.GoodsReceive_EXP_Date,
                        goodsReceive_ProductConversion_Index = query.GoodsReceive_ProductConversion_Index,
                        goodsReceive_ProductConversion_Id = query.GoodsReceive_ProductConversion_Id,
                        goodsReceive_ProductConversion_Name = query.GoodsReceive_ProductConversion_Name,

                        binBalance_Ratio = query.BinBalance_Ratio,
                        binBalance_QtyBegin = query.BinBalance_QtyBegin,
                        binBalance_WeightBegin = query.BinBalance_WeightBegin,
                        binBalance_WeightBegin_Index = query.BinBalance_WeightBegin_Index,
                        binBalance_WeightBegin_Id = query.BinBalance_WeightBegin_Id,
                        binBalance_WeightBegin_Name = query.BinBalance_WeightBegin_Name,
                        binBalance_WeightBeginRatio = query.BinBalance_WeightBeginRatio,
                        binBalance_NetWeightBegin = query.BinBalance_NetWeightBegin,
                        binBalance_NetWeightBegin_Index = query.BinBalance_NetWeightBegin_Index,
                        binBalance_NetWeightBegin_Id = query.BinBalance_NetWeightBegin_Id,
                        binBalance_NetWeightBegin_Name = query.BinBalance_NetWeightBegin_Name,
                        binBalance_NetWeightBeginRatio = query.BinBalance_NetWeightBeginRatio,
                        binBalance_GrsWeightBegin = query.BinBalance_GrsWeightBegin,
                        binBalance_GrsWeightBegin_Index = query.BinBalance_GrsWeightBegin_Index,
                        binBalance_GrsWeightBegin_Id = query.BinBalance_GrsWeightBegin_Id,
                        binBalance_GrsWeightBegin_Name = query.BinBalance_GrsWeightBegin_Name,
                        binBalance_GrsWeightBeginRatio = query.BinBalance_GrsWeightBeginRatio,
                        binBalance_WidthBegin = query.BinBalance_WidthBegin,
                        binBalance_WidthBegin_Index = query.BinBalance_WidthBegin_Index,
                        binBalance_WidthBegin_Id = query.BinBalance_WidthBegin_Id,
                        binBalance_WidthBegin_Name = query.BinBalance_WidthBegin_Name,
                        binBalance_WidthBeginRatio = query.BinBalance_WidthBeginRatio,
                        binBalance_LengthBegin = query.BinBalance_LengthBegin,
                        binBalance_LengthBegin_Index = query.BinBalance_LengthBegin_Index,
                        binBalance_LengthBegin_Id = query.BinBalance_LengthBegin_Id,
                        binBalance_LengthBegin_Name = query.BinBalance_LengthBegin_Name,
                        binBalance_LengthBeginRatio = query.BinBalance_LengthBeginRatio,
                        binBalance_HeightBegin = query.BinBalance_HeightBegin,
                        binBalance_HeightBegin_Index = query.BinBalance_HeightBegin_Index,
                        binBalance_HeightBegin_Id = query.BinBalance_HeightBegin_Id,
                        binBalance_HeightBegin_Name = query.BinBalance_HeightBegin_Name,
                        binBalance_HeightBeginRatio = query.BinBalance_HeightBeginRatio,
                        binBalance_UnitVolumeBegin = query.BinBalance_UnitVolumeBegin,
                        binBalance_VolumeBegin = query.BinBalance_VolumeBegin,
                        binBalance_QtyBal = query.BinBalance_QtyBal,
                        binBalance_WeightBal = query.BinBalance_WeightBal,
                        binBalance_UnitWeightBal_Index = query.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = query.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = query.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = query.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitWeightBal = query.BinBalance_UnitWeightBal,
                        binBalance_WeightBal_Index = query.BinBalance_WeightBal_Index,
                        binBalance_WeightBal_Id = query.BinBalance_WeightBal_Id,
                        binBalance_WeightBal_Name = query.BinBalance_WeightBal_Name,
                        binBalance_WeightBalRatio = query.BinBalance_WeightBalRatio,
                        binBalance_UnitNetWeightBal = query.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = query.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = query.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = query.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = query.BinBalance_UnitNetWeightBalRatio,
                        binBalance_NetWeightBal = query.BinBalance_NetWeightBal,
                        binBalance_NetWeightBal_Index = query.BinBalance_NetWeightBal_Index,
                        binBalance_NetWeightBal_Id = query.BinBalance_NetWeightBal_Id,
                        binBalance_NetWeightBal_Name = query.BinBalance_NetWeightBal_Name,
                        binBalance_NetWeightBalRatio = query.BinBalance_NetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = query.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = query.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = query.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = query.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = query.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_GrsWeightBal = query.BinBalance_GrsWeightBal,
                        binBalance_GrsWeightBal_Index = query.BinBalance_GrsWeightBal_Index,
                        binBalance_GrsWeightBal_Id = query.BinBalance_GrsWeightBal_Id,
                        binBalance_GrsWeightBal_Name = query.BinBalance_GrsWeightBal_Name,
                        binBalance_GrsWeightBalRatio = query.BinBalance_GrsWeightBalRatio,
                        binBalance_UnitWidthBal = query.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = query.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = query.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = query.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = query.BinBalance_UnitWidthBalRatio,
                        binBalance_WidthBal = query.BinBalance_WidthBal,
                        binBalance_WidthBal_Index = query.BinBalance_WidthBal_Index,
                        binBalance_WidthBal_Id = query.BinBalance_WidthBal_Id,
                        binBalance_WidthBal_Name = query.BinBalance_WidthBal_Name,
                        binBalance_WidthBalRatio = query.BinBalance_WidthBalRatio,
                        binBalance_UnitLengthBal = query.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = query.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = query.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = query.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = query.BinBalance_UnitLengthBalRatio,
                        binBalance_LengthBal = query.BinBalance_LengthBal,
                        binBalance_LengthBal_Index = query.BinBalance_LengthBal_Index,
                        binBalance_LengthBal_Id = query.BinBalance_LengthBal_Id,
                        binBalance_LengthBal_Name = query.BinBalance_LengthBal_Name,
                        binBalance_LengthBalRatio = query.BinBalance_LengthBalRatio,
                        binBalance_UnitHeightBal = query.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = query.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = query.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = query.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = query.BinBalance_UnitHeightBalRatio,
                        binBalance_HeightBal = query.BinBalance_HeightBal,
                        binBalance_HeightBal_Index = query.BinBalance_HeightBal_Index,
                        binBalance_HeightBal_Id = query.BinBalance_HeightBal_Id,
                        binBalance_HeightBal_Name = query.BinBalance_HeightBal_Name,
                        binBalance_HeightBalRatio = query.BinBalance_HeightBalRatio,
                        binBalance_UnitVolumeBal = query.BinBalance_UnitVolumeBal,
                        binBalance_VolumeBal = query.BinBalance_VolumeBal,
                        binBalance_QtyReserve = query.BinBalance_QtyReserve,
                        binBalance_WeightReserve = query.BinBalance_WeightReserve,
                        binBalance_WeightReserve_Index = query.BinBalance_WeightReserve_Index,
                        binBalance_WeightReserve_Id = query.BinBalance_WeightReserve_Id,
                        binBalance_WeightReserve_Name = query.BinBalance_WeightReserve_Name,
                        binBalance_WeightReserveRatio = query.BinBalance_WeightReserveRatio,
                        binBalance_NetWeightReserve = query.BinBalance_NetWeightReserve,
                        binBalance_NetWeightReserve_Index = query.BinBalance_NetWeightReserve_Index,
                        binBalance_NetWeightReserve_Id = query.BinBalance_NetWeightReserve_Id,
                        binBalance_NetWeightReserve_Name = query.BinBalance_NetWeightReserve_Name,
                        binBalance_NetWeightReserveRatio = query.BinBalance_NetWeightReserveRatio,
                        binBalance_GrsWeightReserve = query.BinBalance_GrsWeightReserve,
                        binBalance_GrsWeightReserve_Index = query.BinBalance_GrsWeightReserve_Index,
                        binBalance_GrsWeightReserve_Id = query.BinBalance_GrsWeightReserve_Id,
                        binBalance_GrsWeightReserve_Name = query.BinBalance_GrsWeightReserve_Name,
                        binBalance_GrsWeightReserveRatio = query.BinBalance_GrsWeightReserveRatio,
                        binBalance_WidthReserve = query.BinBalance_WidthReserve,
                        binBalance_WidthReserve_Index = query.BinBalance_WidthReserve_Index,
                        binBalance_WidthReserve_Id = query.BinBalance_WidthReserve_Id,
                        binBalance_WidthReserve_Name = query.BinBalance_WidthReserve_Name,
                        binBalance_WidthReserveRatio = query.BinBalance_WidthReserveRatio,
                        binBalance_LengthReserve = query.BinBalance_LengthReserve,
                        binBalance_LengthReserve_Index = query.BinBalance_LengthReserve_Index,
                        binBalance_LengthReserve_Id = query.BinBalance_LengthReserve_Id,
                        binBalance_LengthReserve_Name = query.BinBalance_LengthReserve_Name,
                        binBalance_LengthReserveRatio = query.BinBalance_LengthReserveRatio,
                        binBalance_HeightReserve = query.BinBalance_HeightReserve,
                        binBalance_HeightReserve_Index = query.BinBalance_HeightReserve_Index,
                        binBalance_HeightReserve_Id = query.BinBalance_HeightReserve_Id,
                        binBalance_HeightReserve_Name = query.BinBalance_HeightReserve_Name,
                        binBalance_HeightReserveRatio = query.BinBalance_HeightReserveRatio,
                        binBalance_UnitVolumeReserve = query.BinBalance_UnitVolumeReserve,
                        binBalance_VolumeReserve = query.BinBalance_VolumeReserve,

                        productConversion_Index = query.ProductConversion_Index,
                        productConversion_Id = query.ProductConversion_Id,
                        productConversion_Name = query.ProductConversion_Name,

                        unitPrice = query.UnitPrice,
                        unitPrice_Index = query.UnitPrice_Index,
                        unitPrice_Id = query.UnitPrice_Id,
                        unitPrice_Name = query.UnitPrice_Name,
                        price = query.Price,
                        price_Index = query.Price_Index,
                        price_Id = query.Price_Id,
                        price_Name = query.Price_Name,

                        uDF_1 = query.UDF_1,
                        uDF_2 = query.UDF_2,
                        uDF_3 = query.UDF_3,
                        uDF_4 = query.UDF_4,
                        uDF_5 = query.UDF_5,
                        create_By = query.Create_By,
                        create_Date = query.Create_Date,
                        update_By = query.Update_By,
                        update_Date = query.Update_Date,
                        cancel_By = query.Cancel_By,
                        cancel_Date = query.Cancel_Date,
                        isUse = query.IsUse,
                        binBalance_Status = query.BinBalance_Status,
                        //ageRemain = query.AgeRemain

                        invoice_No = query.Invoice_No,
                        declaration_No = query.Declaration_No,
                        hs_Code = query.HS_Code,
                        conutry_of_Origin = query.Conutry_of_Origin,
                        tax1 = query.Tax1,
                        tax1_Currency_Index = query.Tax1_Currency_Index,
                        tax1_Currency_Id = query.Tax1_Currency_Id,
                        tax1_Currency_Name = query.Tax1_Currency_Name,
                        tax2 = query.Tax2,
                        tax2_Currency_Index = query.Tax2_Currency_Index,
                        tax2_Currency_Id = query.Tax2_Currency_Id,
                        tax2_Currency_Name = query.Tax2_Currency_Name,
                        tax3 = query.Tax3,
                        tax3_Currency_Index = query.Tax3_Currency_Index,
                        tax3_Currency_Id = query.Tax3_Currency_Id,
                        tax3_Currency_Name = query.Tax3_Currency_Name,
                        tax4 = query.Tax4,
                        tax4_Currency_Index = query.Tax4_Currency_Index,
                        tax4_Currency_Id = query.Tax4_Currency_Id,
                        tax4_Currency_Name = query.Tax4_Currency_Name,
                        tax5 = query.Tax5,
                        tax5_Currency_Index = query.Tax5_Currency_Index,
                        tax5_Currency_Id = query.Tax5_Currency_Id,
                        tax5_Currency_Name = query.Tax5_Currency_Name,

                    };
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BinBalanceViewModel> getBinbalanceGreaterThanZeroV3(FilterBinbalanceViewModel model)
        {
            try
            {
                var items = new List<BinBalanceViewModel>();

                #region locationtype
                List<Guid> index_locationtype = new List<Guid>
                {
                    Guid.Parse("E4F856EA-9685-45A4-995C-C05FF9E499C4"),
                    Guid.Parse("E77778D2-7A8E-448D-BA31-CD35FD938FC3"),
                    Guid.Parse("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
                    Guid.Parse("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
                    Guid.Parse("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
                    Guid.Parse("02F5CBFC-769A-411B-9146-1D27F92AE82D"),
                    Guid.Parse("8A545442-77A3-43A4-939A-6B9102DFE8C6"),
                    Guid.Parse("7F3E1BC2-F18B-4B16-80A9-2394EB8BBE63"),
                    Guid.Parse("1D2DF268-F004-4820-831F-B823FF9C7564"),
                    Guid.Parse("48F83BB5-7807-4B32-9E3C-74962CEF92E8")
                };
                #endregion

                List<Guid> location = dbMaster.View_Location.Where(C => index_locationtype.Contains(C.LocationType_Index.GetValueOrDefault())).Select(c=> c.Location_Index).ToList();

                var querys = db.wm_BinBalance.Where(c => location.Contains(c.Location_Index) && c.BinBalance_QtyBal > 0).AsQueryable();

                if (model.Owner_Index.ToString() != "" && model.Owner_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Owner_Index == model.Owner_Index);
                }
                if (!string.IsNullOrEmpty(model.Tag_No))
                {
                    querys = querys.Where(c => c.Tag_No == model.Tag_No);
                }
                if (!string.IsNullOrEmpty(model.Product_Index.ToString()) && model.Product_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Product_Index == model.Product_Index);
                }

                if (!string.IsNullOrEmpty(model.Location_Index.ToString()) && model.Location_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.Location_Index == model.Location_Index);
                }

                if (model.Product_Lot != null)
                {
                    if (model.Product_Lot.ToString() != "")
                    {
                        querys = querys.Where(c => c.Product_Lot == model.Product_Lot);
                    }
                }
                if (!string.IsNullOrEmpty(model.ItemStatus_Index.ToString()) && model.ItemStatus_Index != Guid.Empty)
                {
                    querys = querys.Where(c => c.ItemStatus_Index == model.ItemStatus_Index);
                }
                if (model.MFG_Date != null)
                {
                    if (model.MFG_Date.ToString() != "")
                    {

                    }
                }
                if (model.EXP_Date != null)
                {
                    if (model.EXP_Date.ToString() != "")
                    {

                    }
                }
                if (model.isUseAttribute == true)
                {
                    // ADD UDF 1-5 
                    if (model.UDF_1 != null)
                    {
                        querys = querys.Where(c => c.UDF_1 == model.UDF_1);
                    }

                    if (model.UDF_2 != null)
                    {
                        querys = querys.Where(c => c.UDF_2 == model.UDF_2);
                    }

                    if (model.UDF_3 != null)
                    {
                        querys = querys.Where(c => c.UDF_3 == model.UDF_3);
                    }

                    if (model.UDF_4 != null)
                    {
                        querys = querys.Where(c => c.UDF_4 == model.UDF_4);
                    }

                    if (model.UDF_5 != null)
                    {
                        querys = querys.Where(c => c.UDF_5 == model.UDF_5);
                    }
                }

                if (!string.IsNullOrEmpty(model.isuse))
                {
                    querys = querys.Where(c => c.IsUse == model.isuse);
                }

                var listdata = querys.ToList();

                foreach (var query in listdata)
                {
                    var item = new BinBalanceViewModel
                    {
                        binBalance_Index = query.BinBalance_Index,
                        owner_Index = query.Owner_Index,
                        owner_Id = query.Owner_Id,
                        owner_Name = query.Owner_Name,
                        location_Index = query.Location_Index,
                        location_Id = query.Location_Id,
                        location_Name = query.Location_Name,
                        goodsReceive_Index = query.GoodsReceive_Index,
                        goodsReceive_No = query.GoodsReceive_No,
                        goodsReceive_Date = query.GoodsReceive_Date,
                        goodsReceiveItem_Index = query.GoodsReceiveItem_Index,
                        goodsReceiveItemLocation_Index = query.GoodsReceiveItemLocation_Index,
                        tagItem_Index = query.TagItem_Index,
                        tag_Index = query.Tag_Index,
                        tag_No = query.Tag_No,
                        product_Index = query.Product_Index,
                        product_Id = query.Product_Id,
                        product_Name = query.Product_Name,
                        product_SecondName = query.Product_SecondName,
                        product_ThirdName = query.Product_ThirdName,
                        product_Lot = query.Product_Lot,
                        itemStatus_Index = query.ItemStatus_Index,
                        itemStatus_Id = query.ItemStatus_Id,
                        itemStatus_Name = query.ItemStatus_Name,
                        goodsReceive_MFG_Date = query.GoodsReceive_MFG_Date,
                        goodsReceive_EXP_Date = query.GoodsReceive_EXP_Date,
                        goodsReceive_ProductConversion_Index = query.GoodsReceive_ProductConversion_Index,
                        goodsReceive_ProductConversion_Id = query.GoodsReceive_ProductConversion_Id,
                        goodsReceive_ProductConversion_Name = query.GoodsReceive_ProductConversion_Name,

                        binBalance_Ratio = query.BinBalance_Ratio,
                        binBalance_QtyBegin = query.BinBalance_QtyBegin,
                        binBalance_WeightBegin = query.BinBalance_WeightBegin,
                        binBalance_WeightBegin_Index = query.BinBalance_WeightBegin_Index,
                        binBalance_WeightBegin_Id = query.BinBalance_WeightBegin_Id,
                        binBalance_WeightBegin_Name = query.BinBalance_WeightBegin_Name,
                        binBalance_WeightBeginRatio = query.BinBalance_WeightBeginRatio,
                        binBalance_NetWeightBegin = query.BinBalance_NetWeightBegin,
                        binBalance_NetWeightBegin_Index = query.BinBalance_NetWeightBegin_Index,
                        binBalance_NetWeightBegin_Id = query.BinBalance_NetWeightBegin_Id,
                        binBalance_NetWeightBegin_Name = query.BinBalance_NetWeightBegin_Name,
                        binBalance_NetWeightBeginRatio = query.BinBalance_NetWeightBeginRatio,
                        binBalance_GrsWeightBegin = query.BinBalance_GrsWeightBegin,
                        binBalance_GrsWeightBegin_Index = query.BinBalance_GrsWeightBegin_Index,
                        binBalance_GrsWeightBegin_Id = query.BinBalance_GrsWeightBegin_Id,
                        binBalance_GrsWeightBegin_Name = query.BinBalance_GrsWeightBegin_Name,
                        binBalance_GrsWeightBeginRatio = query.BinBalance_GrsWeightBeginRatio,
                        binBalance_WidthBegin = query.BinBalance_WidthBegin,
                        binBalance_WidthBegin_Index = query.BinBalance_WidthBegin_Index,
                        binBalance_WidthBegin_Id = query.BinBalance_WidthBegin_Id,
                        binBalance_WidthBegin_Name = query.BinBalance_WidthBegin_Name,
                        binBalance_WidthBeginRatio = query.BinBalance_WidthBeginRatio,
                        binBalance_LengthBegin = query.BinBalance_LengthBegin,
                        binBalance_LengthBegin_Index = query.BinBalance_LengthBegin_Index,
                        binBalance_LengthBegin_Id = query.BinBalance_LengthBegin_Id,
                        binBalance_LengthBegin_Name = query.BinBalance_LengthBegin_Name,
                        binBalance_LengthBeginRatio = query.BinBalance_LengthBeginRatio,
                        binBalance_HeightBegin = query.BinBalance_HeightBegin,
                        binBalance_HeightBegin_Index = query.BinBalance_HeightBegin_Index,
                        binBalance_HeightBegin_Id = query.BinBalance_HeightBegin_Id,
                        binBalance_HeightBegin_Name = query.BinBalance_HeightBegin_Name,
                        binBalance_HeightBeginRatio = query.BinBalance_HeightBeginRatio,
                        binBalance_UnitVolumeBegin = query.BinBalance_UnitVolumeBegin,
                        binBalance_VolumeBegin = query.BinBalance_VolumeBegin,
                        binBalance_QtyBal = query.BinBalance_QtyBal,
                        binBalance_WeightBal = query.BinBalance_WeightBal,
                        binBalance_UnitWeightBal_Index = query.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = query.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = query.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = query.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitWeightBal = query.BinBalance_UnitWeightBal,
                        binBalance_WeightBal_Index = query.BinBalance_WeightBal_Index,
                        binBalance_WeightBal_Id = query.BinBalance_WeightBal_Id,
                        binBalance_WeightBal_Name = query.BinBalance_WeightBal_Name,
                        binBalance_WeightBalRatio = query.BinBalance_WeightBalRatio,
                        binBalance_UnitNetWeightBal = query.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = query.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = query.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = query.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = query.BinBalance_UnitNetWeightBalRatio,
                        binBalance_NetWeightBal = query.BinBalance_NetWeightBal,
                        binBalance_NetWeightBal_Index = query.BinBalance_NetWeightBal_Index,
                        binBalance_NetWeightBal_Id = query.BinBalance_NetWeightBal_Id,
                        binBalance_NetWeightBal_Name = query.BinBalance_NetWeightBal_Name,
                        binBalance_NetWeightBalRatio = query.BinBalance_NetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = query.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = query.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = query.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = query.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = query.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_GrsWeightBal = query.BinBalance_GrsWeightBal,
                        binBalance_GrsWeightBal_Index = query.BinBalance_GrsWeightBal_Index,
                        binBalance_GrsWeightBal_Id = query.BinBalance_GrsWeightBal_Id,
                        binBalance_GrsWeightBal_Name = query.BinBalance_GrsWeightBal_Name,
                        binBalance_GrsWeightBalRatio = query.BinBalance_GrsWeightBalRatio,
                        binBalance_UnitWidthBal = query.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = query.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = query.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = query.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = query.BinBalance_UnitWidthBalRatio,
                        binBalance_WidthBal = query.BinBalance_WidthBal,
                        binBalance_WidthBal_Index = query.BinBalance_WidthBal_Index,
                        binBalance_WidthBal_Id = query.BinBalance_WidthBal_Id,
                        binBalance_WidthBal_Name = query.BinBalance_WidthBal_Name,
                        binBalance_WidthBalRatio = query.BinBalance_WidthBalRatio,
                        binBalance_UnitLengthBal = query.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = query.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = query.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = query.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = query.BinBalance_UnitLengthBalRatio,
                        binBalance_LengthBal = query.BinBalance_LengthBal,
                        binBalance_LengthBal_Index = query.BinBalance_LengthBal_Index,
                        binBalance_LengthBal_Id = query.BinBalance_LengthBal_Id,
                        binBalance_LengthBal_Name = query.BinBalance_LengthBal_Name,
                        binBalance_LengthBalRatio = query.BinBalance_LengthBalRatio,
                        binBalance_UnitHeightBal = query.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = query.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = query.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = query.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = query.BinBalance_UnitHeightBalRatio,
                        binBalance_HeightBal = query.BinBalance_HeightBal,
                        binBalance_HeightBal_Index = query.BinBalance_HeightBal_Index,
                        binBalance_HeightBal_Id = query.BinBalance_HeightBal_Id,
                        binBalance_HeightBal_Name = query.BinBalance_HeightBal_Name,
                        binBalance_HeightBalRatio = query.BinBalance_HeightBalRatio,
                        binBalance_UnitVolumeBal = query.BinBalance_UnitVolumeBal,
                        binBalance_VolumeBal = query.BinBalance_VolumeBal,
                        binBalance_QtyReserve = query.BinBalance_QtyReserve,
                        binBalance_WeightReserve = query.BinBalance_WeightReserve,
                        binBalance_WeightReserve_Index = query.BinBalance_WeightReserve_Index,
                        binBalance_WeightReserve_Id = query.BinBalance_WeightReserve_Id,
                        binBalance_WeightReserve_Name = query.BinBalance_WeightReserve_Name,
                        binBalance_WeightReserveRatio = query.BinBalance_WeightReserveRatio,
                        binBalance_NetWeightReserve = query.BinBalance_NetWeightReserve,
                        binBalance_NetWeightReserve_Index = query.BinBalance_NetWeightReserve_Index,
                        binBalance_NetWeightReserve_Id = query.BinBalance_NetWeightReserve_Id,
                        binBalance_NetWeightReserve_Name = query.BinBalance_NetWeightReserve_Name,
                        binBalance_NetWeightReserveRatio = query.BinBalance_NetWeightReserveRatio,
                        binBalance_GrsWeightReserve = query.BinBalance_GrsWeightReserve,
                        binBalance_GrsWeightReserve_Index = query.BinBalance_GrsWeightReserve_Index,
                        binBalance_GrsWeightReserve_Id = query.BinBalance_GrsWeightReserve_Id,
                        binBalance_GrsWeightReserve_Name = query.BinBalance_GrsWeightReserve_Name,
                        binBalance_GrsWeightReserveRatio = query.BinBalance_GrsWeightReserveRatio,
                        binBalance_WidthReserve = query.BinBalance_WidthReserve,
                        binBalance_WidthReserve_Index = query.BinBalance_WidthReserve_Index,
                        binBalance_WidthReserve_Id = query.BinBalance_WidthReserve_Id,
                        binBalance_WidthReserve_Name = query.BinBalance_WidthReserve_Name,
                        binBalance_WidthReserveRatio = query.BinBalance_WidthReserveRatio,
                        binBalance_LengthReserve = query.BinBalance_LengthReserve,
                        binBalance_LengthReserve_Index = query.BinBalance_LengthReserve_Index,
                        binBalance_LengthReserve_Id = query.BinBalance_LengthReserve_Id,
                        binBalance_LengthReserve_Name = query.BinBalance_LengthReserve_Name,
                        binBalance_LengthReserveRatio = query.BinBalance_LengthReserveRatio,
                        binBalance_HeightReserve = query.BinBalance_HeightReserve,
                        binBalance_HeightReserve_Index = query.BinBalance_HeightReserve_Index,
                        binBalance_HeightReserve_Id = query.BinBalance_HeightReserve_Id,
                        binBalance_HeightReserve_Name = query.BinBalance_HeightReserve_Name,
                        binBalance_HeightReserveRatio = query.BinBalance_HeightReserveRatio,
                        binBalance_UnitVolumeReserve = query.BinBalance_UnitVolumeReserve,
                        binBalance_VolumeReserve = query.BinBalance_VolumeReserve,

                        productConversion_Index = query.ProductConversion_Index,
                        productConversion_Id = query.ProductConversion_Id,
                        productConversion_Name = query.ProductConversion_Name,

                        unitPrice = query.UnitPrice,
                        unitPrice_Index = query.UnitPrice_Index,
                        unitPrice_Id = query.UnitPrice_Id,
                        unitPrice_Name = query.UnitPrice_Name,
                        price = query.Price,
                        price_Index = query.Price_Index,
                        price_Id = query.Price_Id,
                        price_Name = query.Price_Name,

                        uDF_1 = query.UDF_1,
                        uDF_2 = query.UDF_2,
                        uDF_3 = query.UDF_3,
                        uDF_4 = query.UDF_4,
                        uDF_5 = query.UDF_5,
                        create_By = query.Create_By,
                        create_Date = query.Create_Date,
                        update_By = query.Update_By,
                        update_Date = query.Update_Date,
                        cancel_By = query.Cancel_By,
                        cancel_Date = query.Cancel_Date,
                        isUse = query.IsUse,
                        binBalance_Status = query.BinBalance_Status,
                        //ageRemain = query.AgeRemain

                        invoice_No = query.Invoice_No,
                        declaration_No = query.Declaration_No,
                        hs_Code = query.HS_Code,
                        conutry_of_Origin = query.Conutry_of_Origin,
                        tax1 = query.Tax1,
                        tax1_Currency_Index = query.Tax1_Currency_Index,
                        tax1_Currency_Id = query.Tax1_Currency_Id,
                        tax1_Currency_Name = query.Tax1_Currency_Name,
                        tax2 = query.Tax2,
                        tax2_Currency_Index = query.Tax2_Currency_Index,
                        tax2_Currency_Id = query.Tax2_Currency_Id,
                        tax2_Currency_Name = query.Tax2_Currency_Name,
                        tax3 = query.Tax3,
                        tax3_Currency_Index = query.Tax3_Currency_Index,
                        tax3_Currency_Id = query.Tax3_Currency_Id,
                        tax3_Currency_Name = query.Tax3_Currency_Name,
                        tax4 = query.Tax4,
                        tax4_Currency_Index = query.Tax4_Currency_Index,
                        tax4_Currency_Id = query.Tax4_Currency_Id,
                        tax4_Currency_Name = query.Tax4_Currency_Name,
                        tax5 = query.Tax5,
                        tax5_Currency_Index = query.Tax5_Currency_Index,
                        tax5_Currency_Id = query.Tax5_Currency_Id,
                        tax5_Currency_Name = query.Tax5_Currency_Name,

                    };
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public String checkTransferLocation(chekcProductLocationViewModel data)
        {
            try
            {
                decimal? Sum = 0;

                var findLocationBin = db.wm_BinBalance.Where(c => c.Location_Name == data.location_Name_To && c.BinBalance_QtyBal != 0).FirstOrDefault();


                if (findLocationBin != null)
                {
                    return "ไม่สามารถย้ายได้ ตำแหน่งนี้สินค้าอยู่แล้ว";

                   // var query = db.wm_BinBalance.Where(c => c.Location_Name == data.location_Name_To
                   //&& c.Owner_Index == data.owner_Index
                   //&& c.Product_Index == data.product_Index).ToList();

                   // if (query.Count <= 0)
                   // {
                   //     return "ไม่สามารถย้ายได้ ตำแหน่งนี้เจ้าของสินค้าหรือสินค้าไม่ตรงกัน";
                   // }

                   // var filterModel = new ProductViewModel();

                   // if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                   // {
                   //     filterModel.product_Index = data.product_Index;
                   // }
                   // var resultProduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProduct"), filterModel.sJson());

                   // data.location_Name = data.location_Name_To;
                   // data.location_Index = null;
                   // var resultLocation = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("GetLocationV2"), data.sJson());

                   // var checkLocation = resultLocation.Where(c => c.blockPick != 1 && c.blockPut != 1).FirstOrDefault();

                   // if (checkLocation != null)
                   // {
                   //     if (query.Count > 0)
                   //     {
                   //         Sum = data.qty + query.Sum(s => s.BinBalance_QtyBal);

                   //         if (resultProduct.FirstOrDefault().qty_Per_Tag <= Sum)
                   //         {
                   //             return "ไม่สามารถย้ายได้ Qty เกิน Qty PerTag";
                   //         }
                   //     }
                   //     else
                   //     {
                   //         return "ไม่สามารถย้ายได้ ตำแหน่งนี้เจ้าของสินค้าหรือสินค้าไม่ตรงกัน";
                   //     }
                   //     return "";
                   // }
                   // else
                   // {
                   //     return "ไม่สามารถย้ายได้ ตำแหน่งนี้มีการ Block";
                   // }
                }
                else
                {
                    return "";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

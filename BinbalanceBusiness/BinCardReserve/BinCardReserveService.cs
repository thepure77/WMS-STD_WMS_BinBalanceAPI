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
using System.Text;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;

namespace BinbalanceBusiness.BinBalanceService
{
    public class BinCardReserveService
    {
        private BinbalanceDbContext db;

        public BinCardReserveService()
        {
            db = new BinbalanceDbContext();
        }
        public BinCardReserveService(BinbalanceDbContext db)
        {
            this.db = db;
        }

        public int chkBinCardReserve(BinCardReserveViewModel model)
        {
            try
            {
                var count = db.wm_BinCardReserve.Where(c => c.Ref_Document_Index == new Guid(model.ref_Document_Index) && c.Ref_DocumentItem_Index == new Guid(model.ref_DocumentItem_Index)).Count();
                return count;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<BinBalanceViewModel> getBinBalancearFromReserve(BinCardReserveViewModel model)
        {
            try
            {
                var BinCardReserve = db.wm_BinCardReserve.FirstOrDefault(c => c.Ref_Document_Index == new Guid(model.ref_Document_Index) && c.Ref_DocumentItem_Index == new Guid(model.ref_DocumentItem_Index));
                var items = new List<BinBalanceViewModel>();

                var querys = db.wm_BinBalance.Where(c => c.BinBalance_Index == BinCardReserve.BinBalance_Index);
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

                throw;
            }
        }
    }
}

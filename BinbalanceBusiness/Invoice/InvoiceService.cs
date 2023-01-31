using AspNetCore.Reporting;
using binbalanceBusiness.Binbalance.ViewModels;
using binbalanceBusiness.BinServiceChargeViewModel;
using BinBalanceBusiness;
using BinBalanceDataAccess.Models;
using Business.Library;
using Common.Utils;
using DataAccess;
using GRBusiness.AutoNumber;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace BinbalanceBusiness.BinBalanceService
{
    public class InvoiceService
    {
        private BinbalanceDbContext db;

        public InvoiceService()
        {
            db = new BinbalanceDbContext();
        }
        public InvoiceService(BinbalanceDbContext db)
        {
            this.db = db;
        }

        public List<BinServiceChargeViewModel> loadtransction(BinBalanceServiceChargeViewModel model)
        {
            try
            {
                var Index = Guid.NewGuid();

                //(DateTime)model.doc_Date.toDate()

                for (var day = (DateTime)model.doc_Date.toDate(); day.Date <= (DateTime)model.doc_DateTo.toDate(); day = day.AddDays(1))
                {
                    var Guid_ServiceCharge = new SqlParameter("Guid_ServiceCharge", Index);
                    var Owner_Index = new SqlParameter("Owner_Index", model.owner_Index);
                    var DocDate = new SqlParameter("DocDate", day);
                    var ServiceCharge_Index = new SqlParameter("ServiceCharge_Index", model.serviceCharge_Index);


                    var save = db.Database.ExecuteSqlCommand("sp_InsertBinBalanceServiceCharge  @Guid_ServiceCharge,@Owner_Index,@DocDate,@ServiceCharge_Index", Guid_ServiceCharge, Owner_Index, DocDate, ServiceCharge_Index);
                }


                #region selectTemplate

                var query = db.wm_BinBalanceServiceCharge.Where(c => c.Guid_ServiceCharge == Index).OrderBy(o => o.Doc_Date).ToList();

                var result = new List<BinServiceChargeViewModel>();

                var qureyGroup = query.GroupBy(c => new
                {
                    c.Location_Index,
                    c.Location_Id,
                    c.Location_Name,
                    c.Doc_Date
                })
                                        .Select(c => new
                                        {
                                            c.Key.Location_Index,
                                            c.Key.Location_Id,
                                            c.Key.Location_Name,
                                            c.Key.Doc_Date,
                                            binBalance_QtyBal = c.Sum(s => s.BinBalance_QtyBal),
                                            binBalance_VolumeBal = c.Sum(s => s.BinBalance_VolumeBal),
                                            binBalance_WeightBal = c.Sum(s => s.BinBalance_WeightBal),
                                            binBalance_NetWeightBal = c.Sum(s => s.BinBalance_NetWeightBal),
                                            binBalance_GrsWeightBal = c.Sum(s => s.BinBalance_GrsWeightBal),

                                        }).ToList();


                foreach (var item in qureyGroup)
                {
                    var resultItem = new BinServiceChargeViewModel();
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.doc_Date = item.Doc_Date.toString();
                    resultItem.binBalance_QtyBal = item.binBalance_QtyBal;
                    resultItem.binBalance_VolumeBal = item.binBalance_VolumeBal;
                    resultItem.binBalance_WeightBal = item.binBalance_WeightBal;
                    resultItem.binBalance_NetWeightBal = item.binBalance_NetWeightBal;
                    resultItem.binBalance_GrsWeightBal = item.binBalance_GrsWeightBal;

                    if (item.binBalance_VolumeBal < (item.binBalance_WeightBal / 1000))
                    {
                        resultItem.rT = (item.binBalance_WeightBal / 1000);
                    }
                    else
                    {
                        resultItem.rT = item.binBalance_VolumeBal;
                    }
                    result.Add(resultItem);
                }



                //foreach (var item in qureyGroup)
                //{
                //    var resultItem = new BinBalanceServiceChargeViewModel();
                //    resultItem.row_Index = item.Row_Index;
                //    resultItem.guid_ServiceCharge = item.Guid_ServiceCharge;
                //    resultItem.doc_Date = item.Doc_Date.toString();
                //    resultItem.binBalance_Index = item.BinBalance_Index;
                //    resultItem.owner_Index = item.Owner_Index;
                //    resultItem.owner_Id = item.Owner_Id;
                //    resultItem.owner_Name = item.Owner_Name;
                //    resultItem.location_Index = item.Location_Index;
                //    resultItem.location_Id = item.Location_Id;
                //    resultItem.location_Name = item.Location_Name;
                //    resultItem.goodsReceive_Index = item.GoodsReceive_Index;
                //    resultItem.goodsReceive_No = item.GoodsReceive_No;
                //    resultItem.goodsReceive_Date = item.GoodsReceive_Date.GetValueOrDefault();
                //    resultItem.goodsReceiveItem_Index = item.GoodsReceiveItem_Index;
                //    resultItem.goodsReceiveItemLocation_Index = item.GoodsReceiveItemLocation_Index;
                //    resultItem.tagItem_Index = item.TagItem_Index;
                //    resultItem.tag_Index = item.Tag_Index;
                //    resultItem.tag_No = item.Tag_No;
                //    resultItem.product_Index = item.Product_Index;
                //    resultItem.product_Id = item.Product_Id;
                //    resultItem.product_Name = item.Product_Name;
                //    resultItem.product_SecondName = item.Product_SecondName;
                //    resultItem.product_ThirdName = item.Product_ThirdName;
                //    resultItem.product_Lot = item.Product_Lot;
                //    resultItem.itemStatus_Index = item.ItemStatus_Index;
                //    resultItem.itemStatus_Id = item.ItemStatus_Id;
                //    resultItem.itemStatus_Name = item.ItemStatus_Name;
                //    resultItem.goodsReceive_MFG_Date = item.GoodsReceive_MFG_Date;
                //    resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date;
                //    resultItem.goodsReceive_ProductConversion_Index = item.GoodsReceive_ProductConversion_Index;
                //    resultItem.goodsReceive_ProductConversion_Id = item.GoodsReceive_ProductConversion_Id;
                //    resultItem.goodsReceive_ProductConversion_Name = item.GoodsReceive_ProductConversion_Name;
                //    resultItem.binBalance_Ratio = item.BinBalance_Ratio;
                //    resultItem.binBalance_QtyBegin = item.BinBalance_QtyBegin;
                //    resultItem.binBalance_WeightBegin = item.BinBalance_WeightBegin;
                //    resultItem.binBalance_WeightBegin_Index = item.BinBalance_WeightBegin_Index;
                //    resultItem.binBalance_WeightBegin_Id = item.BinBalance_WeightBegin_Id;
                //    resultItem.binBalance_WeightBegin_Name = item.BinBalance_WeightBegin_Name;
                //    resultItem.binBalance_WeightBeginRatio = item.BinBalance_WeightBeginRatio;
                //    resultItem.binBalance_NetWeightBegin = item.BinBalance_NetWeightBegin;
                //    resultItem.binBalance_NetWeightBegin_Index = item.BinBalance_NetWeightBegin_Index;
                //    resultItem.binBalance_NetWeightBegin_Id = item.BinBalance_NetWeightBegin_Id;
                //    resultItem.binBalance_NetWeightBegin_Name = item.BinBalance_NetWeightBegin_Name;
                //    resultItem.binBalance_NetWeightBeginRatio = item.BinBalance_NetWeightBeginRatio;
                //    resultItem.binBalance_GrsWeightBegin = item.BinBalance_GrsWeightBegin;
                //    resultItem.binBalance_GrsWeightBegin_Index = item.BinBalance_GrsWeightBegin_Index;
                //    resultItem.binBalance_GrsWeightBegin_Id = item.BinBalance_GrsWeightBegin_Id;
                //    resultItem.binBalance_GrsWeightBegin_Name = item.BinBalance_GrsWeightBegin_Name;
                //    resultItem.binBalance_GrsWeightBeginRatio = item.BinBalance_GrsWeightBeginRatio;
                //    resultItem.binBalance_WidthBegin = item.BinBalance_WidthBegin;
                //    resultItem.binBalance_WidthBegin_Index = item.BinBalance_WidthBegin_Index;
                //    resultItem.binBalance_WidthBegin_Id = item.BinBalance_WidthBegin_Id;
                //    resultItem.binBalance_WidthBegin_Name = item.BinBalance_WidthBegin_Name;
                //    resultItem.binBalance_WidthBeginRatio = item.BinBalance_WidthBeginRatio;
                //    resultItem.binBalance_LengthBegin = item.BinBalance_LengthBegin;
                //    resultItem.binBalance_LengthBegin_Index = item.BinBalance_LengthBegin_Index;
                //    resultItem.binBalance_LengthBegin_Id = item.BinBalance_LengthBegin_Id;
                //    resultItem.binBalance_LengthBegin_Name = item.BinBalance_LengthBegin_Name;
                //    resultItem.binBalance_LengthBeginRatio = item.BinBalance_LengthBeginRatio;
                //    resultItem.binBalance_HeightBegin = item.BinBalance_HeightBegin;
                //    resultItem.binBalance_HeightBegin_Index = item.BinBalance_HeightBegin_Index;
                //    resultItem.binBalance_HeightBegin_Id = item.BinBalance_HeightBegin_Id;
                //    resultItem.binBalance_HeightBegin_Name = item.BinBalance_HeightBegin_Name;
                //    resultItem.binBalance_HeightBeginRatio = item.BinBalance_HeightBeginRatio;
                //    resultItem.binBalance_UnitVolumeBegin = item.BinBalance_UnitVolumeBegin;
                //    resultItem.binBalance_VolumeBegin = item.BinBalance_VolumeBegin;
                //    resultItem.binBalance_QtyBal = item.BinBalance_QtyBal;
                //    resultItem.binBalance_WeightBal = item.BinBalance_WeightBal;
                //    resultItem.binBalance_UnitWeightBal_Index = item.BinBalance_UnitWeightBal_Index;
                //    resultItem.binBalance_UnitWeightBal_Id = item.BinBalance_UnitWeightBal_Id;
                //    resultItem.binBalance_UnitWeightBal_Name = item.BinBalance_UnitWeightBal_Name;
                //    resultItem.binBalance_UnitWeightBalRatio = item.BinBalance_UnitWeightBalRatio;
                //    resultItem.binBalance_UnitWeightBal = item.BinBalance_UnitWeightBal;
                //    resultItem.binBalance_WeightBal_Index = item.BinBalance_WeightBal_Index;
                //    resultItem.binBalance_WeightBal_Id = item.BinBalance_WeightBal_Id;
                //    resultItem.binBalance_WeightBal_Name = item.BinBalance_WeightBal_Name;
                //    resultItem.binBalance_WeightBalRatio = item.BinBalance_WeightBalRatio;
                //    resultItem.binBalance_UnitNetWeightBal = item.BinBalance_UnitNetWeightBal;
                //    resultItem.binBalance_UnitNetWeightBal_Index = item.BinBalance_UnitNetWeightBal_Index;
                //    resultItem.binBalance_UnitNetWeightBal_Id = item.BinBalance_UnitNetWeightBal_Id;
                //    resultItem.binBalance_UnitNetWeightBal_Name = item.BinBalance_UnitNetWeightBal_Name;
                //    resultItem.binBalance_UnitNetWeightBalRatio = item.BinBalance_UnitNetWeightBalRatio;
                //    resultItem.binBalance_NetWeightBal = item.BinBalance_NetWeightBal;
                //    resultItem.binBalance_NetWeightBal_Index = item.BinBalance_NetWeightBal_Index;
                //    resultItem.binBalance_NetWeightBal_Id = item.BinBalance_NetWeightBal_Id;
                //    resultItem.binBalance_NetWeightBal_Name = item.BinBalance_NetWeightBal_Name;
                //    resultItem.binBalance_NetWeightBalRatio = item.BinBalance_NetWeightBalRatio;
                //    resultItem.binBalance_UnitGrsWeightBal = item.BinBalance_UnitGrsWeightBal;
                //    resultItem.binBalance_UnitGrsWeightBal_Index = item.BinBalance_UnitGrsWeightBal_Index;
                //    resultItem.binBalance_UnitGrsWeightBal_Id = item.BinBalance_UnitGrsWeightBal_Id;
                //    resultItem.binBalance_UnitGrsWeightBal_Name = item.BinBalance_UnitGrsWeightBal_Name;
                //    resultItem.binBalance_UnitGrsWeightBalRatio = item.BinBalance_UnitGrsWeightBalRatio;
                //    resultItem.binBalance_GrsWeightBal = item.BinBalance_GrsWeightBal;
                //    resultItem.binBalance_GrsWeightBal_Index = item.BinBalance_GrsWeightBal_Index;
                //    resultItem.binBalance_GrsWeightBal_Id = item.BinBalance_GrsWeightBal_Id;
                //    resultItem.binBalance_GrsWeightBal_Name = item.BinBalance_GrsWeightBal_Name;
                //    resultItem.binBalance_GrsWeightBalRatio = item.BinBalance_GrsWeightBalRatio;
                //    resultItem.binBalance_UnitWidthBal = item.BinBalance_UnitWidthBal;
                //    resultItem.binBalance_UnitWidthBal_Index = item.BinBalance_UnitWidthBal_Index;
                //    resultItem.binBalance_UnitWidthBal_Id = item.BinBalance_UnitWidthBal_Id;
                //    resultItem.binBalance_UnitWidthBal_Name = item.BinBalance_UnitWidthBal_Name;
                //    resultItem.binBalance_UnitWidthBalRatio = item.BinBalance_UnitWidthBalRatio;
                //    resultItem.binBalance_WidthBal = item.BinBalance_WidthBal;
                //    resultItem.binBalance_WidthBal_Index = item.BinBalance_WidthBal_Index;
                //    resultItem.binBalance_WidthBal_Id = item.BinBalance_WidthBal_Id;
                //    resultItem.binBalance_WidthBal_Name = item.BinBalance_WidthBal_Name;
                //    resultItem.binBalance_WidthBalRatio = item.BinBalance_WidthBalRatio;
                //    resultItem.binBalance_UnitLengthBal = item.BinBalance_UnitLengthBal;
                //    resultItem.binBalance_UnitLengthBal_Index = item.BinBalance_UnitLengthBal_Index;
                //    resultItem.binBalance_UnitLengthBal_Id = item.BinBalance_UnitLengthBal_Id;
                //    resultItem.binBalance_UnitLengthBal_Name = item.BinBalance_UnitLengthBal_Name;
                //    resultItem.binBalance_UnitLengthBalRatio = item.BinBalance_UnitLengthBalRatio;
                //    resultItem.binBalance_LengthBal = item.BinBalance_LengthBal;
                //    resultItem.binBalance_LengthBal_Index = item.BinBalance_LengthBal_Index;
                //    resultItem.binBalance_LengthBal_Id = item.BinBalance_LengthBal_Id;
                //    resultItem.binBalance_LengthBal_Name = item.BinBalance_LengthBal_Name;
                //    resultItem.binBalance_LengthBalRatio = item.BinBalance_LengthBalRatio;
                //    resultItem.binBalance_UnitHeightBal = item.BinBalance_UnitHeightBal;
                //    resultItem.binBalance_UnitHeightBal_Index = item.BinBalance_UnitHeightBal_Index;
                //    resultItem.binBalance_UnitHeightBal_Id = item.BinBalance_UnitHeightBal_Id;
                //    resultItem.binBalance_UnitHeightBal_Name = item.BinBalance_UnitHeightBal_Name;
                //    resultItem.binBalance_UnitHeightBalRatio = item.BinBalance_UnitHeightBalRatio;
                //    resultItem.binBalance_HeightBal = item.BinBalance_HeightBal;
                //    resultItem.binBalance_HeightBal_Index = item.BinBalance_HeightBal_Index;
                //    resultItem.binBalance_HeightBal_Id = item.BinBalance_HeightBal_Id;
                //    resultItem.binBalance_HeightBal_Name = item.BinBalance_HeightBal_Name;
                //    resultItem.binBalance_HeightBalRatio = item.BinBalance_HeightBalRatio;
                //    resultItem.binBalance_UnitVolumeBal = item.BinBalance_UnitVolumeBal;
                //    resultItem.binBalance_VolumeBal = item.BinBalance_VolumeBal;
                //    resultItem.binBalance_QtyReserve = item.BinBalance_QtyReserve;
                //    resultItem.binBalance_WeightReserve = item.BinBalance_WeightReserve;
                //    resultItem.binBalance_WeightReserve_Index = item.BinBalance_WeightReserve_Index;
                //    resultItem.binBalance_WeightReserve_Id = item.BinBalance_WeightReserve_Id;
                //    resultItem.binBalance_WeightReserve_Name = item.BinBalance_WeightReserve_Name;
                //    resultItem.binBalance_WeightReserveRatio = item.BinBalance_WeightReserveRatio;
                //    resultItem.binBalance_NetWeightReserve = item.BinBalance_NetWeightReserve;
                //    resultItem.binBalance_NetWeightReserve_Index = item.BinBalance_NetWeightReserve_Index;
                //    resultItem.binBalance_NetWeightReserve_Id = item.BinBalance_NetWeightReserve_Id;
                //    resultItem.binBalance_NetWeightReserve_Name = item.BinBalance_NetWeightReserve_Name;
                //    resultItem.binBalance_NetWeightReserveRatio = item.BinBalance_NetWeightReserveRatio;
                //    resultItem.binBalance_GrsWeightReserve = item.BinBalance_GrsWeightReserve;
                //    resultItem.binBalance_GrsWeightReserve_Index = item.BinBalance_GrsWeightReserve_Index;
                //    resultItem.binBalance_GrsWeightReserve_Id = item.BinBalance_GrsWeightReserve_Id;
                //    resultItem.binBalance_GrsWeightReserve_Name = item.BinBalance_GrsWeightReserve_Name;
                //    resultItem.binBalance_GrsWeightReserveRatio = item.BinBalance_GrsWeightReserveRatio;
                //    resultItem.binBalance_WidthReserve = item.BinBalance_WidthReserve;
                //    resultItem.binBalance_WidthReserve_Index = item.BinBalance_WidthReserve_Index;
                //    resultItem.binBalance_WidthReserve_Id = item.BinBalance_WidthReserve_Id;
                //    resultItem.binBalance_WidthReserve_Name = item.BinBalance_WidthReserve_Name;
                //    resultItem.binBalance_WidthReserveRatio = item.BinBalance_WidthReserveRatio;
                //    resultItem.binBalance_LengthReserve = item.BinBalance_LengthReserve;
                //    resultItem.binBalance_LengthReserve_Index = item.BinBalance_LengthReserve_Index;
                //    resultItem.binBalance_LengthReserve_Id = item.BinBalance_LengthReserve_Id;
                //    resultItem.binBalance_LengthReserve_Name = item.BinBalance_LengthReserve_Name;
                //    resultItem.binBalance_LengthReserveRatio = item.BinBalance_LengthReserveRatio;
                //    resultItem.binBalance_HeightReserve = item.BinBalance_HeightReserve;
                //    resultItem.binBalance_HeightReserve_Index = item.BinBalance_HeightReserve_Index;
                //    resultItem.binBalance_HeightReserve_Id = item.BinBalance_HeightReserve_Id;
                //    resultItem.binBalance_HeightReserve_Name = item.BinBalance_HeightReserve_Name;
                //    resultItem.binBalance_HeightReserveRatio = item.BinBalance_HeightReserveRatio;
                //    resultItem.binBalance_UnitVolumeReserve = item.BinBalance_UnitVolumeReserve;
                //    resultItem.binBalance_VolumeReserve = item.BinBalance_VolumeReserve;
                //    resultItem.productConversion_Index = item.ProductConversion_Index;
                //    resultItem.productConversion_Id = item.ProductConversion_Id;
                //    resultItem.productConversion_Name = item.ProductConversion_Name;
                //    resultItem.unitPrice = item.UnitPrice;
                //    resultItem.unitPrice_Index = item.UnitPrice_Index;
                //    resultItem.unitPrice_Id = item.UnitPrice_Id;
                //    resultItem.unitPrice_Name = item.UnitPrice_Name;
                //    resultItem.price = item.Price;
                //    resultItem.price_Index = item.Price_Index;
                //    resultItem.price_Id = item.Price_Id;
                //    resultItem.price_Name = item.Price_Name;
                //    resultItem.uDF_1 = item.UDF_1;
                //    resultItem.uDF_2 = item.UDF_2;
                //    resultItem.uDF_3 = item.UDF_3;
                //    resultItem.uDF_4 = item.UDF_4;
                //    resultItem.uDF_5 = item.UDF_5;
                //    resultItem.create_By = item.Create_By;
                //    resultItem.create_Date = item.Create_Date;
                //    resultItem.update_By = item.Update_By;
                //    resultItem.update_Date = item.Update_Date;
                //    resultItem.cancel_By = item.Cancel_By;
                //    resultItem.cancel_Date = item.Cancel_Date;
                //    resultItem.isUse = item.IsUse;
                //    resultItem.binBalance_Status = item.BinBalance_Status;
                //    resultItem.invoice_No = item.Invoice_No;
                //    resultItem.declaration_No = item.Declaration_No;
                //    resultItem.hS_Code = item.HS_Code;
                //    resultItem.conutry_of_Origin = item.Conutry_of_Origin;
                //    resultItem.tax1 = item.Tax1;
                //    resultItem.tax1_Currency_Index = item.Tax1_Currency_Index;
                //    resultItem.tax1_Currency_Id = item.Tax1_Currency_Id;
                //    resultItem.tax1_Currency_Name = item.Tax1_Currency_Name;
                //    resultItem.tax2 = item.Tax2;
                //    resultItem.tax2_Currency_Index = item.Tax2_Currency_Index;
                //    resultItem.tax2_Currency_Id = item.Tax2_Currency_Id;
                //    resultItem.tax2_Currency_Name = item.Tax2_Currency_Name;
                //    resultItem.tax3 = item.Tax3;
                //    resultItem.tax3_Currency_Index = item.Tax3_Currency_Index;
                //    resultItem.tax3_Currency_Id = item.Tax3_Currency_Id;
                //    resultItem.tax3_Currency_Name = item.Tax3_Currency_Name;
                //    resultItem.tax4 = item.Tax4;
                //    resultItem.tax4_Currency_Index = item.Tax4_Currency_Index;
                //    resultItem.tax4_Currency_Id = item.Tax4_Currency_Id;
                //    resultItem.tax4_Currency_Name = item.Tax4_Currency_Name;
                //    resultItem.tax5 = item.Tax5;
                //    resultItem.tax5_Currency_Index = item.Tax5_Currency_Index;
                //    resultItem.tax5_Currency_Id = item.Tax5_Currency_Id;
                //    resultItem.tax5_Currency_Name = item.Tax5_Currency_Name;

                //    if (item.BinBalance_VolumeBal < (item.BinBalance_WeightBal / 1000))
                //    {
                //        resultItem.rT = (item.BinBalance_WeightBal / 1000);
                //    }
                //    else
                //    {
                //        resultItem.rT = item.BinBalance_VolumeBal;
                //    }
                //    result.Add(resultItem);

                //}
                #endregion

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BinServiceChargeViewModel> cal(BinServiceChargeViewModel model)
        {
            try
            {
                var result = new List<BinServiceChargeViewModel>();

                var resultstorageCharge = utils.SendDataApi<List<storageChargeModel>>(new AppSettingConfig().GetUrl("getConfigStorageCharge"), model.sJson());

                var resultlocationServiceCharge = utils.SendDataApi<List<locationServiceChargeViewModel>>(new AppSettingConfig().GetUrl("getConfigLocationServiceCharge"), model.sJson());


                var findLocation = model.listBinBalanceServiceCharge.Where(c => resultlocationServiceCharge.Select(s => s.location_Index).Contains(c.location_Index)).OrderBy(o => o.doc_Date).ToList();



                foreach (var item in findLocation)
                {

                    var resultItem = new BinServiceChargeViewModel();

                    if (resultstorageCharge[0].unitCharge_Name == "QTY")
                    {
                        resultItem.amount = (resultstorageCharge[0].rate * item.binBalance_QtyBal);
                        resultItem.volumeCal = item.binBalance_QtyBal;
                    }
                    else if (resultstorageCharge[0].unitCharge_Name == "Weight")
                    {
                        resultItem.amount = (resultstorageCharge[0].rate * item.binBalance_GrsWeightBal);
                        resultItem.volumeCal = item.binBalance_GrsWeightBal;

                    }
                    else if (resultstorageCharge[0].unitCharge_Name == "Vol")
                    {
                        resultItem.amount = (resultstorageCharge[0].rate * item.binBalance_VolumeBal);
                        resultItem.volumeCal = item.binBalance_QtyBal;
                    }
                    else if (resultstorageCharge[0].unitCharge_Name == "RT")
                    {
                        if (item.binBalance_VolumeBal < (item.binBalance_WeightBal / 1000))
                        {
                            resultItem.amount = (resultstorageCharge[0].rate * (item.binBalance_WeightBal / 1000));
                            resultItem.volumeCal = (item.binBalance_WeightBal / 1000);
                        }
                        else
                        {
                            resultItem.amount = (resultstorageCharge[0].rate * item.binBalance_VolumeBal);
                            resultItem.volumeCal = item.binBalance_VolumeBal;

                        }

                    }

                    //resultItem.row_Index = item.row_Index;
                    //resultItem.binBalance_Index = item.binBalance_Index;
                    resultItem.binBalance_QtyBal = item.binBalance_QtyBal;
                    resultItem.binBalance_GrsWeightBal = item.binBalance_GrsWeightBal;
                    resultItem.binBalance_WeightBal = item.binBalance_WeightBal;
                    resultItem.binBalance_NetWeightBal = item.binBalance_NetWeightBal;

                    resultItem.binBalance_VolumeBal = item.binBalance_VolumeBal;
                    resultItem.binBalance_QtyBal = item.binBalance_QtyBal;

                    if (item.binBalance_VolumeBal < (item.binBalance_WeightBal / 1000))
                    {
                        resultItem.rT = (item.binBalance_WeightBal / 1000);
                    }
                    else
                    {
                        resultItem.rT = item.binBalance_VolumeBal;
                    }
                    resultItem.unitCharge_Name = resultstorageCharge[0].unitCharge_Name;
                    resultItem.rate = resultstorageCharge[0].rate;
                    resultItem.doc_Date = item.doc_Date;
                    resultItem.location_Index = item.location_Index;
                    resultItem.location_Id = item.location_Id;
                    resultItem.location_Name = item.location_Name;

                    result.Add(resultItem);

                }


                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public actionResultInvoice saveInvoice(InvoiceViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            Guid index = new Guid();
            decimal Other = 0;
            try
            {
                var actionResult = new actionResultInvoice();

                var Header = new im_Invoice();

                if (data.invoice_Index != null)
                {
                    index = data.invoice_Index;
                }

                Header.Invoice_Index = Guid.NewGuid();

                var result = new List<GenDocumentTypeViewModel>();
                var filterModel = new GenDocumentTypeViewModel();
                filterModel.process_Index = new Guid("C06E0E4D-393D-43BC-B640-448C20832433");
                filterModel.documentType_Index = data.documentType_Index;
                result = utils.SendDataApi<List<GenDocumentTypeViewModel>>(new AppSettingConfig().GetUrl("dropDownDocumentType"), filterModel.sJson());

                var genDoc = new AutoNumberService();
                string DocNo = "";

                DateTime DocumentDate = (DateTime)data.invoice_Date.toDate();
                DocNo = genDoc.genAutoDocmentNumber(result, DocumentDate);

                Header.Invoice_No = DocNo;
                DateTime invoice_Date = (DateTime)data.invoice_Date.toDate();

                Header.Invoice_Date = invoice_Date;

                // DateTime start_Date = (DateTime)data.start_Date.toDate();
                Header.Start_Date = data.start_Date.toDate();

                //DateTime end_Date = (DateTime)data.end_Date.toDate();

                Header.End_Date = data.end_Date.toDate();
                Header.Due_Date = data.due_Date.toDate();
                Header.Owner_Index = data.owner_Index;
                Header.Owner_Id = data.owner_Id;
                Header.Owner_Name = data.owner_Name;
                Header.DocumentType_Index = data.documentType_Index;
                Header.DocumentType_Id = data.documentType_Id;
                Header.DocumentType_Name = data.documentType_Name;
                Header.Tax_No = data.tax_No;
                Header.Credit_Term = data.credit_Term;
                Header.Currency_Index = data.currency_Index;
                Header.Exchange_Rate = data.exchange_Rate;
                Header.PaymentMethod_Index = data.paymentMethod_Index;
                Header.Payment_Ref = data.payment_Ref;
                Header.FullPaid_Date = data.fullPaid_Date;
                Header.Amount = data.listInvoice.Sum(s => s.amount);
                Header.Discount_Amt = data.discount_Amt;
                Header.Discount_Percent = data.discount_Percent;
                Header.Total_Amt = data.total_Amt;
                Header.VAT_Percent = data.vAT_Percent;
                Header.VAT = data.vAT;
                Header.Net_Amt = data.net_Amt;
                Header.Billing_Address = data.billing_Address;
                Header.Billing_Tel = data.billing_Tel;
                Header.Billing_Fax = data.billing_Fax;
                Header.DocumentRef_No1 = data.documentRef_No1;
                Header.DocumentRef_No2 = data.documentRef_No2;
                Header.DocumentRef_No3 = data.documentRef_No3;
                Header.DocumentRef_No4 = data.documentRef_No4;
                Header.DocumentRef_No5 = data.documentRef_No5;
                Header.Document_Status = 0;
                Header.Document_Remark = data.document_Remark;
                Header.UDF_1 = data.uDF_1;
                Header.UDF_2 = data.uDF_2;
                Header.UDF_3 = data.uDF_3;
                Header.UDF_4 = data.uDF_4;
                Header.UDF_5 = data.uDF_5;
                Header.IsActive = 1;
                Header.IsDelete = 0;
                Header.Create_By = data.create_By;
                Header.Create_Date = DateTime.Now;

                var AmountOther = data.listInvoice.Where(c => c.serviceCharge_Index != new Guid("F2AAE1C0-16D6-44D2-BA65-CB9792D6B37D")).Sum(s => s.amount);
                if (AmountOther != null)
                {
                    Other = AmountOther.GetValueOrDefault();
                    Header.AmountOther = AmountOther;
                }
                Header.AmountStorage = (Header.Amount - Other);
                db.im_Invoice.Add(Header);

                if (data.listInvoice != null)
                {
                    foreach (var item in data.listInvoice)
                    {
                        var Detail = new im_InvoiceItem();

                        Detail.InvoiceItem_Index = Guid.NewGuid();
                        Detail.Invoice_Index = Header.Invoice_Index;
                        Detail.Item_Seq = item.item_Seq;
                        Detail.ServiceCharge_Index = item.serviceCharge_Index;
                        Detail.ServiceCharge_Id = item.serviceCharge_Id;
                        Detail.ServiceCharge_Name = item.serviceCharge_Name;
                        Detail.Qty = item.binBalance_QtyBal;
                        Detail.Weight = item.binBalance_WeightBal;
                        Detail.Volume = item.binBalance_VolumeBal;
                        Detail.RT = item.rT;
                        Detail.UnitCharge = item.volumeCal.GetValueOrDefault();
                        Detail.Rate = item.rate;
                        Detail.Amount = item.amount;
                        Detail.Currency_Index = item.currency_Index;
                        Detail.Memo_Index = item.memo_Index;
                        Detail.Memo_No = item.memo_No;
                        Detail.DocumentRef_No1 = item.documentRef_No1;
                        Detail.DocumentRef_No2 = item.documentRef_No2;
                        Detail.DocumentRef_No3 = item.documentRef_No3;
                        Detail.DocumentRef_No4 = item.documentRef_No4;
                        Detail.DocumentRef_No5 = item.documentRef_No5;
                        Detail.Document_Status = 0;
                        Detail.Document_Remark = item.document_Remark;
                        Detail.UDF_1 = item.uDF_1;
                        Detail.UDF_2 = item.uDF_2;
                        Detail.UDF_3 = item.uDF_3;
                        Detail.UDF_4 = item.uDF_4;
                        Detail.UDF_5 = item.uDF_5;
                        Detail.Ref_Process_Index = item.ref_Process_Index;
                        Detail.Ref_Document_No = item.ref_Document_No;
                        Detail.Ref_Document_Index = item.ref_Document_Index;
                        Detail.IsActive = 1;
                        Detail.IsDelete = 0;
                        Detail.Create_By = data.create_By;
                        Detail.Create_Date = DateTime.Now;
                        db.im_InvoiceItem.Add(Detail);
                    }

                }


                if (data.listStorage != null)
                {
                    foreach (var item in data.listStorage)
                    {
                        var Detail = new im_InvoiceStorageCharge();

                        Detail.InvoiceStorageCharge_Index = Guid.NewGuid();
                        Detail.Invoice_Index = Header.Invoice_Index;

                        DateTime Doc_Date = (DateTime)item.doc_Date.toDate();

                        Detail.Doc_Date = Doc_Date;
                        Detail.ServiceCharge_Index = item.serviceCharge_Index;
                        Detail.ServiceCharge_Id = item.serviceCharge_Id;
                        Detail.ServiceCharge_Name = item.serviceCharge_Name;
                        Detail.QtyBal = item.binBalance_QtyBal;
                        Detail.WeightBal = item.binBalance_WeightBal;
                        Detail.NetWeightBal = item.binBalance_NetWeightBal;
                        Detail.GrsWeightBal = item.binBalance_GrsWeightBal;
                        Detail.VolumeBal = item.binBalance_VolumeBal;
                        Detail.RTBal = item.rT;
                        Detail.DocumentRef_No1 = item.documentRef_No1;
                        Detail.DocumentRef_No2 = item.documentRef_No2;
                        Detail.DocumentRef_No3 = item.documentRef_No3;
                        Detail.DocumentRef_No4 = item.documentRef_No4;
                        Detail.DocumentRef_No5 = item.documentRef_No5;
                        Detail.Document_Status = 0;
                        Detail.Document_Remark = item.document_Remark;
                        Detail.UDF_1 = item.uDF_1;
                        Detail.UDF_2 = item.uDF_2;
                        Detail.UDF_3 = item.uDF_3;
                        Detail.UDF_4 = item.uDF_4;
                        Detail.UDF_5 = item.uDF_5;
                        Detail.IsActive = 1;
                        Detail.IsDelete = 0;
                        Detail.Create_By = data.create_By;
                        Detail.Create_Date = DateTime.Now;
                        Detail.Amount = item.amount;
                        Detail.UnitCharge = item.volumeCal.GetValueOrDefault();
                        db.im_InvoiceStorageCharge.Add(Detail);

                    }

                }


                var deleteItem = db.im_InvoiceItem.Where(c => !data.listInvoice.Select(s => s.invoiceItem_Index).Contains(c.InvoiceItem_Index)
                    && c.Invoice_Index == index).ToList();

                foreach (var c in deleteItem)
                {
                    var delete = db.im_InvoiceItem.Find(c.InvoiceItem_Index);

                    delete.IsActive = 0;
                    delete.IsDelete = 1;
                    delete.Cancel_By = data.create_By;
                    delete.Cancel_Date = DateTime.Now;

                }

                var deleteItemSto = db.im_InvoiceStorageCharge.Where(c => !data.listStorage.Select(s => s.invoiceStorageCharge_Index).Contains(c.InvoiceStorageCharge_Index)
                                    && c.Invoice_Index == index).ToList();

                foreach (var c in deleteItemSto)
                {
                    var delete = db.im_InvoiceStorageCharge.Find(c.InvoiceStorageCharge_Index);

                    delete.IsActive = 0;
                    delete.IsDelete = 1;
                    delete.Cancel_By = data.create_By;
                    delete.Cancel_Date = DateTime.Now;

                }

                var queryMemoItem = db.im_MemoItem.Where(c => data.listInvoice.Select(s => s.memo_Index).Contains(c.Memo_Index)).ToList();

                foreach (var item in queryMemoItem)
                {
                    var updateMemoItem = db.im_MemoItem.Find(item.Memo_Item_Index);

                    if (updateMemoItem != null)
                    {
                        updateMemoItem.Document_Status = 1;
                        updateMemoItem.Update_By = data.create_By;
                        updateMemoItem.Update_Date = DateTime.Now;
                    }
                }


                actionResult.msg = "MSG_Save_error";

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                    actionResult.msg = "MSG_Save_success";
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("saveInvoice", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return actionResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BinServiceChargeViewModel> groupInvoice(BinServiceChargeViewModel data)
        {
            try
            {
                var result = new List<BinServiceChargeViewModel>();

                var qureyGroup = data.listBinBalanceServiceCharge
                    .GroupBy(c => new
                    {
                        c.serviceCharge_Index
                    })
                    .Select(c => new
                    {
                        c.Key.serviceCharge_Index,
                        binBalance_QtyBal = c.Sum(s => s.binBalance_QtyBal),
                        binBalance_VolumeBal = c.Sum(s => s.binBalance_VolumeBal),
                        binBalance_WeightBal = c.Sum(s => s.binBalance_WeightBal),
                        binBalance_NetWeightBal = c.Sum(s => s.binBalance_NetWeightBal),
                        binBalance_GrsWeightBal = c.Sum(s => s.binBalance_GrsWeightBal),
                        rT = c.Sum(s => s.rT),
                        volumeCal = c.Sum(s => s.volumeCal),
                        amount = c.Sum(s => s.amount),

                    }).ToList();

                foreach (var item in qureyGroup)
                {
                    var resultItem = new BinServiceChargeViewModel();

                    resultItem.serviceCharge_Index = item.serviceCharge_Index;
                    resultItem.binBalance_QtyBal = item.binBalance_QtyBal;
                    resultItem.binBalance_VolumeBal = item.binBalance_VolumeBal;
                    resultItem.binBalance_GrsWeightBal = item.binBalance_GrsWeightBal;
                    resultItem.binBalance_NetWeightBal = item.binBalance_NetWeightBal;
                    resultItem.binBalance_WeightBal = item.binBalance_WeightBal;
                    resultItem.rT = item.rT;
                    resultItem.volumeCal = item.volumeCal;
                    resultItem.amount = item.amount;
                    result.Add(resultItem);
                }


                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BinServiceChargeViewModel> groupStorage(BinServiceChargeViewModel data)
        {
            try
            {
                var result = new List<BinServiceChargeViewModel>();

                var qureyGroup = data.listBinBalanceServiceCharge
                    .GroupBy(c => new
                    {
                        c.doc_Date
                    })
                    .Select(c => new
                    {
                        c.Key.doc_Date,
                        binBalance_QtyBal = c.Sum(s => s.binBalance_QtyBal),
                        binBalance_VolumeBal = c.Sum(s => s.binBalance_VolumeBal),
                        binBalance_WeightBal = c.Sum(s => s.binBalance_WeightBal),
                        binBalance_NetWeightBal = c.Sum(s => s.binBalance_NetWeightBal),
                        binBalance_GrsWeightBal = c.Sum(s => s.binBalance_GrsWeightBal),
                        rT = c.Sum(s => s.rT),
                        volumeCal = c.Sum(s => s.volumeCal),
                        amount = c.Sum(s => s.amount),


                    }).ToList();

                foreach (var item in qureyGroup)
                {
                    var resultItem = new BinServiceChargeViewModel();

                    resultItem.doc_Date = item.doc_Date;
                    resultItem.binBalance_QtyBal = item.binBalance_QtyBal;
                    resultItem.binBalance_VolumeBal = item.binBalance_VolumeBal;
                    resultItem.binBalance_GrsWeightBal = item.binBalance_GrsWeightBal;
                    resultItem.binBalance_NetWeightBal = item.binBalance_NetWeightBal;
                    resultItem.binBalance_WeightBal = item.binBalance_WeightBal;
                    resultItem.rT = item.rT;
                    resultItem.volumeCal = item.volumeCal;
                    resultItem.amount = item.amount;
                    result.Add(resultItem);
                }


                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public actionResultInvoice filterInvoice(InvoiceViewModel data)
        {
            try
            {
                var query = db.im_Invoice.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Invoice_No.Contains(data.key));
                }

                if (!string.IsNullOrEmpty(data.owner_Name))
                {
                    query = query.Where(c => c.Owner_Name.Contains(data.owner_Name));
                }

                if (!string.IsNullOrEmpty(data.invoice_Date) && !string.IsNullOrEmpty(data.invoice_DateTo))
                {
                    var dateStart = data.invoice_Date.toBetweenDate();
                    var dateEnd = data.invoice_DateTo.toBetweenDate();
                    query = query.Where(c => c.Invoice_Date >= dateStart.start && c.Invoice_Date <= dateEnd.end);
                }

                var Item = new List<im_Invoice>();
                var TotalRow = new List<im_Invoice>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }

                Item = query.OrderByDescending(o => o.Create_Date).ToList();
                var result = new List<InvoiceViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new InvoiceViewModel();

                    resultItem.invoice_Index = item.Invoice_Index;
                    resultItem.invoice_No = item.Invoice_No;
                    resultItem.invoice_Date = item.Invoice_Date.toString();
                    resultItem.start_Date = item.Start_Date.toString();
                    resultItem.end_Date = item.End_Date.toString();
                    resultItem.due_Date = item.Due_Date.toString();
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.documentType_Index = item.DocumentType_Index;
                    resultItem.documentType_Id = item.DocumentType_Id;
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.tax_No = item.Tax_No;
                    resultItem.credit_Term = item.Credit_Term;
                    resultItem.currency_Index = item.Currency_Index;
                    resultItem.exchange_Rate = item.Exchange_Rate;
                    resultItem.paymentMethod_Index = item.PaymentMethod_Index;
                    resultItem.payment_Ref = item.Payment_Ref;
                    resultItem.fullPaid_Date = item.FullPaid_Date;
                    resultItem.amount = item.Amount;
                    resultItem.discount_Amt = item.Discount_Amt;
                    resultItem.discount_Percent = item.Discount_Percent;
                    resultItem.total_Amt = item.Total_Amt;
                    resultItem.vAT_Percent = item.VAT_Percent;
                    resultItem.vAT = item.VAT;
                    resultItem.net_Amt = item.Net_Amt;
                    resultItem.billing_Address = item.Billing_Address;
                    resultItem.billing_Tel = item.Billing_Tel;
                    resultItem.billing_Fax = item.Billing_Fax;
                    resultItem.documentRef_No1 = item.DocumentRef_No1;
                    resultItem.documentRef_No2 = item.DocumentRef_No2;
                    resultItem.documentRef_No3 = item.DocumentRef_No3;
                    resultItem.documentRef_No4 = item.DocumentRef_No4;
                    resultItem.documentRef_No5 = item.DocumentRef_No5;
                    resultItem.document_Status = item.Document_Status;
                    resultItem.document_Remark = item.Document_Remark;
                    resultItem.uDF_1 = item.UDF_1;
                    resultItem.uDF_2 = item.UDF_2;
                    resultItem.uDF_3 = item.UDF_3;
                    resultItem.uDF_4 = item.UDF_4;
                    resultItem.uDF_5 = item.UDF_5;
                    resultItem.isActive = 1;
                    resultItem.isDelete = 0;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultl = new actionResultInvoice();
                actionResultl.items = result.ToList();
                actionResultl.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                return actionResultl;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public actionResultInvoice filterInvoiceItem(InvoiceViewModel data)
        {
            try
            {
                var actionResultl = new actionResultInvoice();

                var Invoice = db.im_InvoiceItem.Where(c => c.Invoice_Index == data.invoice_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();

                var ResultInvoiceItem = new List<InvoiceItemViewModel>();

                foreach (var item in Invoice)
                {
                    var ResultItemInvoiceItem = new InvoiceItemViewModel();

                    ResultItemInvoiceItem.invoiceItem_Index = item.InvoiceItem_Index;
                    ResultItemInvoiceItem.invoice_Index = item.Invoice_Index;
                    ResultItemInvoiceItem.serviceCharge_Index = item.ServiceCharge_Index;
                    ResultItemInvoiceItem.serviceCharge_Id = item.ServiceCharge_Id;
                    ResultItemInvoiceItem.serviceCharge_Name = item.ServiceCharge_Name;
                    ResultItemInvoiceItem.binBalance_QtyBal = item.Qty;
                    ResultItemInvoiceItem.binBalance_WeightBal = item.Weight;
                    ResultItemInvoiceItem.binBalance_VolumeBal = item.Volume;
                    ResultItemInvoiceItem.rT = item.RT;
                    ResultItemInvoiceItem.volumeCal = item.UnitCharge;
                    ResultItemInvoiceItem.rate = item.Rate;
                    ResultItemInvoiceItem.amount = item.Amount;
                    ResultItemInvoiceItem.currency_Index = item.Currency_Index;
                    ResultItemInvoiceItem.memo_Index = item.Memo_Index;
                    ResultItemInvoiceItem.memo_No = item.Memo_No;
                    ResultItemInvoiceItem.documentRef_No1 = item.DocumentRef_No1;
                    ResultItemInvoiceItem.documentRef_No2 = item.DocumentRef_No2;
                    ResultItemInvoiceItem.documentRef_No3 = item.DocumentRef_No3;
                    ResultItemInvoiceItem.documentRef_No4 = item.DocumentRef_No4;
                    ResultItemInvoiceItem.documentRef_No5 = item.DocumentRef_No5;
                    ResultItemInvoiceItem.document_Status = item.Document_Status;
                    ResultItemInvoiceItem.document_Remark = item.Document_Remark;
                    ResultItemInvoiceItem.uDF_1 = item.UDF_1;
                    ResultItemInvoiceItem.uDF_2 = item.UDF_2;
                    ResultItemInvoiceItem.uDF_3 = item.UDF_3;
                    ResultItemInvoiceItem.uDF_4 = item.UDF_4;
                    ResultItemInvoiceItem.uDF_5 = item.UDF_5;
                    ResultItemInvoiceItem.ref_Document_No = item.Ref_Document_No;
                    ResultItemInvoiceItem.ref_Document_Index = item.Ref_Document_Index;
                    ResultInvoiceItem.Add(ResultItemInvoiceItem);

                }


                var Storage = db.im_InvoiceStorageCharge.Where(c => c.Invoice_Index == data.invoice_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();

                var ResultStorage = new List<InvoiceStorageChargeViewModel>();

                foreach (var item in Storage)
                {
                    var ResultItemStorage = new InvoiceStorageChargeViewModel();

                    ResultItemStorage.invoiceStorageCharge_Index = item.InvoiceStorageCharge_Index;
                    ResultItemStorage.invoice_Index = item.Invoice_Index;
                    ResultItemStorage.doc_Date = item.Doc_Date.toString();
                    ResultItemStorage.serviceCharge_Index = item.ServiceCharge_Index;
                    ResultItemStorage.serviceCharge_Id = item.ServiceCharge_Id;
                    ResultItemStorage.serviceCharge_Name = item.ServiceCharge_Name;
                    ResultItemStorage.binBalance_QtyBal = item.QtyBal;
                    ResultItemStorage.binBalance_WeightBal = item.WeightBal;
                    ResultItemStorage.binBalance_NetWeightBal = item.NetWeightBal;
                    ResultItemStorage.binBalance_GrsWeightBal = item.GrsWeightBal;
                    ResultItemStorage.binBalance_VolumeBal = item.VolumeBal;
                    ResultItemStorage.rT = item.RTBal;
                    ResultItemStorage.documentRef_No1 = item.DocumentRef_No1;
                    ResultItemStorage.documentRef_No2 = item.DocumentRef_No2;
                    ResultItemStorage.documentRef_No3 = item.DocumentRef_No3;
                    ResultItemStorage.documentRef_No4 = item.DocumentRef_No4;
                    ResultItemStorage.documentRef_No5 = item.DocumentRef_No5;
                    ResultItemStorage.document_Status = item.Document_Status;
                    ResultItemStorage.document_Remark = item.Document_Remark;
                    ResultItemStorage.uDF_1 = item.UDF_1;
                    ResultItemStorage.uDF_2 = item.UDF_2;
                    ResultItemStorage.uDF_3 = item.UDF_3;
                    ResultItemStorage.uDF_4 = item.UDF_4;
                    ResultItemStorage.uDF_5 = item.UDF_5;
                    ResultItemStorage.amount = item.Amount;
                    ResultItemStorage.volumeCal = item.UnitCharge;
                    ResultStorage.Add(ResultItemStorage);
                }

                actionResultl.listInvoice = ResultInvoiceItem;
                actionResultl.listStorage = ResultStorage;

                return actionResultl;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean deleteInvoiceItem(InvoiceViewModel data)
        {
            Boolean Isdelete = false;
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var Invoice = db.im_InvoiceItem.Where(c => c.Invoice_Index == data.invoice_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();

                foreach (var item in Invoice)
                {
                    var delete = db.im_InvoiceItem.Find(item.InvoiceItem_Index);

                    if (delete != null)
                    {
                        delete.IsActive = 0;
                        delete.IsDelete = 1;
                        delete.Cancel_By = data.cancel_By;
                        delete.Cancel_Date = DateTime.Now;

                    }
                }


                var Storage = db.im_InvoiceStorageCharge.Where(c => c.Invoice_Index == data.invoice_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();


                foreach (var item in Storage)
                {
                    var delete = db.im_InvoiceStorageCharge.Find(item.InvoiceStorageCharge_Index);

                    if (delete != null)
                    {
                        delete.IsActive = 0;
                        delete.IsDelete = 1;
                        delete.Cancel_By = data.cancel_By;
                        delete.Cancel_Date = DateTime.Now;
                    }
                }


                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                    Isdelete = true;
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("deleteInvoice", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return Isdelete;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BinBalanceServiceChargeViewModel> loadMemo(BinBalanceServiceChargeViewModel data)
        {
            try
            {
                var query = db.View_MEMO.AsQueryable();

                query = query.Where(c => c.Owner_Index == data.owner_Index);

                if (!string.IsNullOrEmpty(data.doc_Date) && !string.IsNullOrEmpty(data.doc_DateTo))
                {
                    var dateStart = data.doc_Date.toBetweenDate();
                    var dateEnd = data.doc_DateTo.toBetweenDate();
                    query = query.Where(c => c.Memo_Date >= dateStart.start && c.Memo_Date <= dateEnd.end);
                }

                var ResultQuery = query.ToList();



                var result = new List<BinBalanceServiceChargeViewModel>();

                foreach (var item in ResultQuery)
                {
                    var resultItem = new BinBalanceServiceChargeViewModel();

                    resultItem.serviceCharge_Index = item.ServiceCharge_Index;
                    resultItem.binBalance_QtyBal = item.Qty;
                    resultItem.binBalance_VolumeBal = item.Volume;
                    resultItem.binBalance_WeightBal = item.Weight;
                    resultItem.amount = item.Amount;
                    resultItem.volumeCal = item.UnitCharge;
                    resultItem.rT = item.RT;
                    resultItem.ref_Process_Index = item.Ref_Process_Index;
                    resultItem.ref_Document_No = item.Ref_Document_No;
                    resultItem.ref_Document_Index = item.Ref_Document_Index;
                    resultItem.memo_Index = item.Memo_Index;
                    resultItem.memo_No = item.Memo_No;
                    resultItem.serviceCharge_Index = item.ServiceCharge_Index;
                    resultItem.serviceCharge_Id = item.ServiceCharge_Id;
                    resultItem.serviceCharge_Name = item.ServiceCharge_Name;
                    result.Add(resultItem);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string ExportStorage(ReportInvoiceStorageChargeViewModel data, string rootPath = "")
        {
            decimal fixamount = 0;

            try
            {
                var result = new List<ReportInvoiceStorageChargeViewModel>();
                rootPath = rootPath.Replace("\\BinbalanceAPI", "");
                //var reportPath = rootPath + "\\BinbalanceBusiness\\Reports\\Storage\\InvoiceStorage.rdlc";
                var reportPath = rootPath + "\\Reports\\Storage\\InvoiceStorage.rdlc";

                string pwhereFilter = "";
                string pwhereBeginFilter = "";

                var header = db.im_Invoice.Where(c => c.Invoice_Index == data.invoice_Index).FirstOrDefault();


                var ResultStorage = new ReportInvoiceStorageChargeViewModel();


                //var query = db.im_InvoiceStorageCharge.Where(c => c.Invoice_Index == data.invoice_Index).ToList();

                if (header != null)
                {
                    var ResultItemStorage = new ReportInvoiceStorageChargeViewModel();

                    ResultItemStorage.invoice_No = header.Invoice_No;
                    ResultItemStorage.owner_Id = header.Owner_Id;
                    ResultItemStorage.owner_Name = header.Owner_Name;

                    DateTime start_Date = (DateTime)header.Start_Date;
                    DateTime end_Date = (DateTime)header.End_Date;


                    ResultItemStorage.invoice_Date = start_Date.ToString("dd") + " - " + end_Date.ToString("dd MMMM yyyy");
                    //result.Add(ResultItemStorage);

                }

                var queryStorage = db.im_InvoiceStorageCharge.Where(c => c.Invoice_Index == data.invoice_Index)
                        .GroupBy(c => new
                        {
                            c.Doc_Date,
                            c.ServiceCharge_Index,
                            c.ServiceCharge_Id,
                            c.ServiceCharge_Name,
                        }).Select(c => new
                        {
                            c.Key.Doc_Date,
                            c.Key.ServiceCharge_Index,
                            c.Key.ServiceCharge_Id,
                            c.Key.ServiceCharge_Name,
                            QtyBal = c.Sum(s => s.QtyBal),
                            VolumeBal = c.Sum(s => s.VolumeBal),
                            Amount = c.Sum(s => s.Amount)
                        }).ToList();

                var queryInvoiceItem = db.im_InvoiceItem.Where(c => c.Invoice_Index == data.invoice_Index).ToList();

                var Memo = db.View_CalMemo.Where(c => queryInvoiceItem.Select(s => s.Memo_Index).Contains(c.Memo_Index)
                            && c.Memo_Date >= data.start_Date.toBetweenDate().start 
                            && c.Memo_Date <= data.end_Date.toBetweenDate().end)
                            .ToList();


                var fix = db.im_InvoiceItem.Where(c => c.Invoice_Index == data.invoice_Index 
                          && c.ServiceCharge_Index == new Guid("512CC5B8-7B0A-4CD4-87A9-E77C5D36DF70")).FirstOrDefault();


                if (fix != null)
                {
                    fixamount = fix.Amount.GetValueOrDefault();
                }

                var ResultJoin = (from Sto in queryStorage
                                  join memo in Memo on Sto.Doc_Date equals memo.Memo_Date
                                  select new ReportInvoiceStorageChargeViewModel
                                  {
                                      serviceCharge_Index = Sto.ServiceCharge_Index,
                                      serviceCharge_Id = Sto.ServiceCharge_Id,
                                      serviceCharge_Name = Sto.ServiceCharge_Name,
                                      Date = Sto.Doc_Date,
                                      qtyBal = Sto.QtyBal,
                                      volumeBal = Sto.VolumeBal,
                                      amount = Sto.Amount,
                                      amountMemo = memo.MemoAmount

                                  }).ToList();



                if (queryStorage.Count > 0)
                {

                    foreach (var item in queryStorage)
                    {
                        decimal memo = 0;


                        var ResultItemStorage = new ReportInvoiceStorageChargeViewModel();


                        ResultItemStorage.doc_Date = item.Doc_Date.ToString("dd/MM/yyyy");
                        ResultItemStorage.serviceCharge_Index = item.ServiceCharge_Index;
                        ResultItemStorage.serviceCharge_Id = item.ServiceCharge_Id;
                        ResultItemStorage.serviceCharge_Name = item.ServiceCharge_Name;
                        ResultItemStorage.binBalance_QtyBal = item.QtyBal;
                        ResultItemStorage.binBalance_VolumeBal = item.VolumeBal;
                        ResultItemStorage.amount = item.Amount;

                        ResultItemStorage.amountMemo = ResultJoin.Where(c => c.Date == item.Doc_Date).Select(s => s.amountMemo).FirstOrDefault();

                        if (ResultItemStorage.amountMemo != null)
                        {
                            memo = ResultItemStorage.amountMemo.GetValueOrDefault();
                        }

                        DateTime DateTime = (DateTime)data.end_Date.toDate();
                        String Date = DateTime.ToString("dd/MM/yyyy");
                        if (ResultItemStorage.doc_Date == Date)
                        {
                            ResultItemStorage.amountOther = (memo + fixamount);
                        }
                        else
                        {
                            ResultItemStorage.amountOther = memo;
                        }
                        ResultItemStorage.total = (ResultItemStorage.amount + ResultItemStorage.amountOther);
                        ResultItemStorage.invoice_No = header.Invoice_No;
                        ResultItemStorage.owner_Id = header.Owner_Id;
                        ResultItemStorage.owner_Name = header.Owner_Name;

                        DateTime start_Date = (DateTime)header.Start_Date;
                        DateTime end_Date = (DateTime)header.End_Date;
                        ResultItemStorage.invoice_Date = start_Date.ToString("dd") + " - " + end_Date.ToString("dd MMMM yyyy");

                        result.Add(ResultItemStorage);
                    }
                }




                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport";

                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);


                var saveLocation = rootPath + fullPath;
                //File.Delete(saveLocation);
                //ExcelRefresh(reportPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string saveReport(byte[] file, string name, string rootPath)
        {
            var saveLocation = PhysicalPath(name, rootPath);
            FileStream fs = new FileStream(saveLocation, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                try
                {
                    bw.Write(file);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return VirtualPath(name);
        }

        public string PhysicalPath(string name, string rootPath)
        {
            var filename = name;
            var vPath = ReportPath;
            var path = rootPath + vPath;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            var saveLocation = System.IO.Path.Combine(path, filename);
            return saveLocation;
        }
        public string VirtualPath(string name)
        {
            var filename = name;
            var vPath = ReportPath;
            vPath = vPath.Replace("~", "");
            return vPath + filename;
        }
        private string ReportPath
        {
            get
            {
                var url = "\\ReportGenerator\\";
                return url;
            }
        }

        public string ExportInvoice(ReportInvoiceViewModel data, string rootPath = "")
        {
            decimal fixamount = 0;

            try
            {
                var result = new List<ReportInvoiceViewModel>();
                rootPath = rootPath.Replace("\\BinbalanceAPI", "");
                //var reportPath = rootPath + "\\BinbalanceBusiness\\Reports\\Invoice\\Invoice.rdlc";
                var reportPath = rootPath + "\\Reports\\Invoice\\Invoice.rdlc";

                string pwhereFilter = "";
                string pwhereBeginFilter = "";

                var query = db.im_Invoice.Where(c => c.Invoice_Date >= data.invoice_Date.toBetweenDate().start && c.Invoice_Date <= data.invoice_DateTo.toBetweenDate().end).ToList();


                var ResultInvoice = new ReportInvoiceViewModel();

                DateTime start_Date = (DateTime)data.invoice_Date.toDate();


                DateTime end_Date = (DateTime)data.invoice_DateTo.toDate();


                ResultInvoice.invoice_Date = start_Date.ToString("dd") + " - " + end_Date.ToString("dd MMMM yyyy");

                ResultInvoice.TotalAmount = query.Sum(s => s.Amount);

                //result.Add(ResultInvoice);


                int addNumber = 0;

                foreach (var item in query)
                {
                    var resultItem = new ReportInvoiceViewModel();
                    addNumber++;
                    resultItem.row = addNumber;
                    resultItem.invoice_Index = item.Invoice_Index;
                    resultItem.invoice_No = item.Invoice_No; 
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.amountOther = item.AmountOther;
                    resultItem.amountStorage = item.AmountStorage;
                    resultItem.amount = item.Amount;
                    resultItem.TotalAmount = query.Sum(s => s.Amount);
                    resultItem.invoice_Date = start_Date.ToString("dd") + " - " + end_Date.ToString("dd MMMM yyyy");

                    result.Add(resultItem);

                }



                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport";

                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);


                var saveLocation = rootPath + fullPath;
                //File.Delete(saveLocation);
                //ExcelRefresh(reportPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public Boolean deleteInvoice(InvoiceViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var deleteInvoice = db.im_Invoice.Find(data.invoice_Index);

                if (deleteInvoice != null)
                {
                    deleteInvoice.IsActive = 0;
                    deleteInvoice.IsDelete = 1;
                    deleteInvoice.Cancel_By = data.cancel_By;
                    deleteInvoice.Cancel_Date = DateTime.Now;
                }
                else
                {
                    return false;

                }
                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                    return true;
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("deleteInvoice", msglog);
                    transactionx.Rollback();

                    throw exy;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        }
}

using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.Stock.ViewModels;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.Product;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BinbalanceBusiness.Stock.ViewModels.CheckStockMobileViewModel;

namespace BinbalanceBusiness.Stock
{
    public class CheckStockMobileService
    {
        private BinbalanceDbContext db;
        private MasterDbContext dbMaster;

        public CheckStockMobileService()
        {
            db = new BinbalanceDbContext();
            dbMaster = new MasterDbContext();
        }

        public CheckStockMobileService(BinbalanceDbContext db)
        {
            this.db = db;
        }

        public actionResultViewModel mobileFilterByProduct(CheckStockMobileViewModel model)
        {
            try
            {
                //var query = db.wm_BinBalance.AsQueryable();
                var query = db.wm_BinBalance.AsQueryable().ToList();
                if (!string.IsNullOrEmpty(model.product_Index.ToString()) && model.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    query = query.Where(c => c.Product_Index == (model.product_Index)).ToList();
                }

                if (!string.IsNullOrEmpty(model.owner_Index.ToString()) && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    query = query.Where(c => c.Owner_Index == (model.owner_Index)).ToList();
                }

                var resultquery = query;

                var queryGroup = (from res in resultquery
                                  join loc in dbMaster.View_Location.ToList() on res.Location_Index equals loc.Location_Index
                                  select new
                                  {
                                      Product_Index = res.Product_Index,
                                      Product_Name = res.Product_Name,
                                      Product_Id = res.Product_Id,
                                      Owner_Index = res.Owner_Index,
                                      Owner_Id = res.Owner_Id,
                                      Owner_Name = res.Owner_Name,
                                      LocationType_Name = loc.LocationType_Name,
                                      ProductConversion_Index = res.ProductConversion_Index,
                                      ProductConversion_Id = res.ProductConversion_Id,
                                      ProductConversion_Name = res.ProductConversion_Name,
                                      GoodsReceive_ProductConversion_Index = res.GoodsReceive_ProductConversion_Index,
                                      GoodsReceive_ProductConversion_Id = res.GoodsReceive_ProductConversion_Id,
                                      GoodsReceive_ProductConversion_Name = res.GoodsReceive_ProductConversion_Name,
                                      ItemStatus_Index = res.ItemStatus_Index,
                                      ItemStatus_Id = res.ItemStatus_Id,
                                      ItemStatus_Name = res.ItemStatus_Name,
                                      Product_Lot = res.Product_Lot,
                                      BinBalance_Ratio = res.BinBalance_Ratio,
                                      BinBalance_QtyBal = res.BinBalance_QtyBal,
                                      BinBalance_QtyReserve = res.BinBalance_QtyReserve,
                                      BinBalance_WeightBal = res.BinBalance_WeightBal,
                                      ERP_Location = res.ERP_Location,
                                  }).GroupBy(c => new
                                  {
                                      c.Product_Index,
                                      c.Product_Name,
                                      c.Product_Id,
                                      c.Owner_Index,
                                      c.Owner_Id,
                                      c.Owner_Name,
                                      c.LocationType_Name,
                                      c.ProductConversion_Index,
                                      c.ProductConversion_Id,
                                      c.ProductConversion_Name,
                                      c.GoodsReceive_ProductConversion_Index,
                                      c.GoodsReceive_ProductConversion_Id,
                                      c.GoodsReceive_ProductConversion_Name,
                                      c.ItemStatus_Index,
                                      c.ItemStatus_Id,
                                      c.ItemStatus_Name,
                                      c.Product_Lot,
                                      c.BinBalance_Ratio,
                                      c.ERP_Location
                                  })
                                .Select(c => new
                                {
                                    c.Key.Product_Index,
                                    c.Key.Product_Name,
                                    c.Key.Product_Id,
                                    c.Key.Owner_Index,
                                    c.Key.Owner_Id,
                                    c.Key.Owner_Name,
                                    c.Key.LocationType_Name,
                                    c.Key.ProductConversion_Index,
                                    c.Key.ProductConversion_Id,
                                    c.Key.ProductConversion_Name,
                                    c.Key.GoodsReceive_ProductConversion_Index,
                                    c.Key.GoodsReceive_ProductConversion_Id,
                                    c.Key.GoodsReceive_ProductConversion_Name,
                                    c.Key.ItemStatus_Index,
                                    c.Key.ItemStatus_Id,
                                    c.Key.ItemStatus_Name,
                                    c.Key.Product_Lot,
                                    c.Key.BinBalance_Ratio,
                                    c.Key.ERP_Location,

                                    SumQtyBal = c.Sum(s => s.BinBalance_QtyBal),
                                    SumQtyRe = c.Sum(s => s.BinBalance_QtyReserve),
                                    SumWeight = c.Sum(s => s.BinBalance_WeightBal)
                                });

                var model_con = new SearchProductConversionViewModel();
                var get_conversion = utils.SendDataApi<List<SearchProductConversionViewModel>>(new AppSettingConfig().GetUrl("conversion_sale_unit"), model_con.sJson());

                var join_sale = (from bin in queryGroup
                                 join conversion in get_conversion.ToList() on bin.Product_Index equals conversion.product_Index
                                 into conversion2
                                 from conversion in conversion2.DefaultIfEmpty()
                                 select new CheckStockMobileViewModel
                                 {
                                     product_Index = bin.Product_Index,
                                     product_Id = bin.Product_Id,
                                     product_Name = bin.Product_Name,
                                     productConversion_Index = bin.ProductConversion_Index,
                                     productConversion_Id = bin.ProductConversion_Id,
                                     productConversion_Name = bin.ProductConversion_Name,
                                     owner_Index = bin.Owner_Index,
                                     owner_Id = bin.Owner_Id,
                                     owner_Name = bin.Owner_Name,
                                     binBalance_QtyBal = bin.SumQtyBal,
                                     binBalance_QtyReserve = bin.SumQtyRe,
                                     goodsReceive_ProductConversion_Index = bin.GoodsReceive_ProductConversion_Index,
                                     goodsReceive_ProductConversion_Id = bin.GoodsReceive_ProductConversion_Id,
                                     goodsReceive_ProductConversion_Name = bin.GoodsReceive_ProductConversion_Name,
                                     sale_qty = bin.BinBalance_Ratio,
                                     sale_unit = bin.ProductConversion_Name,
                                     binBalance_WeightBal = bin.SumWeight,
                                     itemStatus_Index = bin.ItemStatus_Index,
                                     itemStatus_Id = bin.ItemStatus_Id,
                                     itemStatus_Name = bin.ItemStatus_Name,
                                     product_Lot = bin.Product_Lot,
                                     binBalance_Ratio = bin.BinBalance_Ratio,
                                     erp_location = bin.ERP_Location,
                                     SumQtyBal = bin.SumQtyBal, //c.Sum(s => s.BinBalance_QtyBal),
                                     SumQtyRe = bin.SumQtyRe, //c.Sum(s => s.BinBalance_QtyReserve),
                                     SumWeight = bin.SumWeight//c.Sum(s => s.BinBalance_WeightBal)
                                 });
                var TotalRow = join_sale.ToList();
                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    join_sale = join_sale.Skip(((model.CurrentPage - 1) * model.PerPage)).Take(model.PerPage).ToList();
                }
                
                var Item = queryGroup.ToList();

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    join_sale = join_sale.Skip(((model.CurrentPage - 1) * model.PerPage)).Take(model.PerPage).ToList();
                }

                var ITem = join_sale.ToList();
                var result = new List<CheckStockMobileViewModel>();
                int addNumber = 0;

                foreach (var item in ITem)
                {
                    var unit = get_conversion.Where(c => c.product_Index == item.product_Index).FirstOrDefault();
                    var resultItem = new CheckStockMobileViewModel();
                    addNumber++;
                    resultItem.row = addNumber;
                    resultItem.product_Index = item.product_Index;
                    resultItem.product_Id = item.product_Id;
                    resultItem.product_Name = item.product_Name;
                    resultItem.productConversion_Index = item.productConversion_Index;
                    resultItem.productConversion_Id = item.productConversion_Id;
                    resultItem.productConversion_Name = item.productConversion_Name;
                    resultItem.productConversion_Width = (unit == null) ? 0 : unit.productConversion_Width;
                    resultItem.productConversion_Length = (unit == null) ? 0 : unit.productConversion_Length;
                    resultItem.productConversion_Height = (unit == null) ? 0 : unit.productConversion_Height;
                    resultItem.productConversion_Volume = (unit == null) ? 0 : unit.productConversion_Volume;
                    resultItem.productConversion_ratio = (unit == null) ? 0 : unit.productConversion_Ratio;
                    resultItem.locationType_Name = item.locationType_Name;
                    resultItem.owner_Index = item.owner_Index;
                    resultItem.owner_Id = item.owner_Id;
                    resultItem.owner_Name = item.owner_Name;
                    resultItem.binBalance_QtyBal = item.SumQtyBal;
                    resultItem.binBalance_QtyReserve = item.SumQtyRe;
                    resultItem.amount = resultItem.binBalance_QtyBal - resultItem.binBalance_QtyReserve;
                    resultItem.total = resultItem.binBalance_QtyBal + resultItem.binBalance_QtyReserve;
                    resultItem.goodsReceive_ProductConversion_Index = item.goodsReceive_ProductConversion_Index;
                    resultItem.goodsReceive_ProductConversion_Id = item.goodsReceive_ProductConversion_Id;
                    resultItem.goodsReceive_ProductConversion_Name = item.goodsReceive_ProductConversion_Name;

                    if (unit != null)
                    {
                        if (unit.productConversion_Ratio != null)
                        {
                            resultItem.sale_qty = item.SumQtyBal / unit.productConversion_Ratio;
                            resultItem.sale_unit = unit.productConversion_Name;
                        }
                        else
                        {
                            resultItem.sale_qty = item.SumQtyBal / item.binBalance_Ratio;
                            resultItem.sale_unit = item.productConversion_Name;
                        }
                    }
                    else
                    {
                        resultItem.sale_qty = item.SumQtyBal / item.binBalance_Ratio;
                        resultItem.sale_unit = item.productConversion_Name;
                    }
                    resultItem.binBalance_WeightBal = item.SumWeight;
                    resultItem.itemStatus_Index = item.itemStatus_Index;
                    resultItem.itemStatus_Id = item.itemStatus_Id;
                    resultItem.itemStatus_Name = item.itemStatus_Name;
                    resultItem.product_Lot = item.product_Lot;
                    resultItem.erp_location = item.erp_location;
                    var cm3 = resultItem.sale_qty * (resultItem.productConversion_Width * resultItem.productConversion_Length * resultItem.productConversion_Height);
                    resultItem.cbm = (cm3 == 0) ? 0 : cm3 / 1000000;

                    result.Add(resultItem);

                }
                var count = TotalRow.Count;

                var actionResult = new actionResultViewModel();
                actionResult.itemsStock = result.ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };

                return actionResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultViewModel mobileFilterByLocation(CheckStockMobileViewModel model)
        {
            var actionResult = new actionResultViewModel();
            try
            {
                if (string.IsNullOrEmpty(model.product_Id) && string.IsNullOrEmpty(model.location_Name) && string.IsNullOrEmpty(model.tag_No))
                {
                    actionResult.resultIsUse = false;
                    actionResult.resultMsg = "กรุณาใส่ข้อมูลเพื่อทำการค้นหา";
                    return actionResult;
                }
                
                var query = db.wm_BinBalance.AsQueryable();

                query = query.Where(c => c.BinBalance_QtyBal > 0 && c.BinBalance_QtyReserve >= 0);

                #region Search

                if (!string.IsNullOrEmpty(model.product_Id) )
                {
                    query = query.Where(c => c.Product_Id == (model.product_Id));
                }
                

                if (!string.IsNullOrEmpty(model.location_Name) )
                {
                    query = query.Where(c => c.Location_Name == (model.location_Name));
                }

                if (!string.IsNullOrEmpty(model.tag_No ))
                {
                    query = query.Where(c => c.Tag_No == (model.tag_No));
                }
                #endregion

                var resultquery = query.OrderByDescending(c => c.Update_Date).ToList();

                var queryGroup = (from res in resultquery
                                  join loc in dbMaster.View_Location.ToList()
                                    on res.Location_Index equals loc.Location_Index
                                  select new
                                  {
                                      Product_Index = res.Product_Index,
                                      Product_Name = res.Product_Name,
                                      Product_Id = res.Product_Id,
                                      Tag_Index = res.Tag_Index,
                                      Tag_No = res.Tag_No,
                                      Location_Index = res.Location_Index,
                                      Location_Id = res.Location_Id,
                                      Location_Name = res.Location_Name,
                                      ItemStatus_Index = res.ItemStatus_Index,
                                      ItemStatus_Id = res.ItemStatus_Id,
                                      ItemStatus_Name = res.ItemStatus_Name,
                                      Product_Lot = res.Product_Lot,
                                      ProductConversion_Index = res.ProductConversion_Index,
                                      ProductConversion_Id = res.ProductConversion_Id,
                                      ProductConversion_Name = res.ProductConversion_Name,
                                      GoodsReceive_Index = res.GoodsReceive_Index,
                                      GoodsReceive_No = res.GoodsReceive_No,
                                      GoodsReceive_Date = res.GoodsReceive_Date,
                                      GoodsReceive_MFG_Date = res.GoodsReceive_MFG_Date,
                                      GoodsReceive_EXP_Date = res.GoodsReceive_EXP_Date,
                                      LocationType_Name = loc.LocationType_Name,
                                      BinBalance_Ratio = res.BinBalance_Ratio,
                                      BinBalance_QtyBal = res.BinBalance_QtyBal,
                                      BinBalance_QtyReserve = res.BinBalance_QtyReserve,
                                      BinBalance_WeightBal = res.BinBalance_WeightBal,
                                      ERP_Location = res.ERP_Location,
                                  }).GroupBy(c => new
                                  {
                                      c.Product_Index,
                                      c.Product_Name,
                                      c.Product_Id,
                                      c.Tag_Index,
                                      c.Tag_No,
                                      c.Location_Index,
                                      c.Location_Id,
                                      c.Location_Name,
                                      c.ItemStatus_Index,
                                      c.ItemStatus_Id,
                                      c.ItemStatus_Name,
                                      c.Product_Lot,
                                      c.ProductConversion_Index,
                                      c.ProductConversion_Id,
                                      c.ProductConversion_Name,
                                      c.GoodsReceive_Index,
                                      c.GoodsReceive_No,
                                      c.GoodsReceive_Date,
                                      c.GoodsReceive_MFG_Date,
                                      c.GoodsReceive_EXP_Date,
                                      c.LocationType_Name,
                                      c.BinBalance_Ratio,
                                      c.ERP_Location,
                                  }).Select(c => new
                                  {
                                      c.Key.Product_Index,
                                      c.Key.Product_Name,
                                      c.Key.Product_Id,
                                      c.Key.Tag_Index,
                                      c.Key.Tag_No,
                                      c.Key.Location_Index,
                                      c.Key.Location_Id,
                                      c.Key.Location_Name,
                                      c.Key.ItemStatus_Index,
                                      c.Key.ItemStatus_Id,
                                      c.Key.ItemStatus_Name,
                                      c.Key.Product_Lot,
                                      c.Key.ProductConversion_Index,
                                      c.Key.ProductConversion_Id,
                                      c.Key.ProductConversion_Name,
                                      c.Key.GoodsReceive_Index,
                                      c.Key.GoodsReceive_No,
                                      c.Key.GoodsReceive_Date,
                                      c.Key.GoodsReceive_MFG_Date,
                                      c.Key.GoodsReceive_EXP_Date,
                                      c.Key.LocationType_Name,
                                      c.Key.BinBalance_Ratio,
                                      c.Key.ERP_Location,
                                      SumQtyBal = c.Sum(s => s.BinBalance_QtyBal),
                                      SumQtyRe = c.Sum(s => s.BinBalance_QtyReserve),
                                      SumWeught = c.Sum(s => s.BinBalance_WeightBal),
                                  });
                var model_con = new SearchProductConversionViewModel();
                var get_conversion = utils.SendDataApi<List<SearchProductConversionViewModel>>(new AppSettingConfig().GetUrl("conversion_sale_unit"), model_con.sJson());

                var TotalRow = queryGroup.ToList();

                if (model.isExportByLocation == false)
                {
                    if (model.CurrentPage != 0 && model.PerPage != 0)
                    {
                        queryGroup = queryGroup.Skip(((model.CurrentPage - 1) * model.PerPage)).Take(model.PerPage).ToList();
                    }
                }

                var listDataZonelocation = utils.SendDataApi<List<ZoneLocationViewModel>>(new AppSettingConfig().GetUrl("getZoneLocationMaster"), new { }.sJson());

                var listDataZone = utils.SendDataApi<List<ZoneConfigViewModel>>(new AppSettingConfig().GetUrl("getZoneMaster"), new { }.sJson());

                var listDataProductType = utils.SendDataApi<List<View_BinbalanceProductTypeViewModel>>(new AppSettingConfig().GetUrl("configProductType"), new { }.sJson());

                var Item = queryGroup.ToList();

                var result = new List<CheckStockMobileViewModel>();
                int addNumber = 0;

                foreach (var item in Item)
                {
                    var unit = get_conversion.Where(c => c.product_Index == item.Product_Index).FirstOrDefault();

                    var resultItem = new CheckStockMobileViewModel();
                    addNumber++;
                    resultItem.row = addNumber;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_Width = (unit == null) ? 0 : unit.productConversion_Width;
                    resultItem.productConversion_Length = (unit == null) ? 0 : unit.productConversion_Length;
                    resultItem.productConversion_Height = (unit == null) ? 0 : unit.productConversion_Height;
                    resultItem.productConversion_Volume = (unit == null) ? 0 : unit.productConversion_Volume;
                    resultItem.productConversion_ratio = (unit == null) ? 0 : unit.productConversion_Ratio;
                    resultItem.tag_Index = item.Tag_Index;
                    resultItem.tag_No = item.Tag_No;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.locationType_Name = item.LocationType_Name;
                    resultItem.itemStatus_Index = item.ItemStatus_Index;
                    resultItem.itemStatus_Id = item.ItemStatus_Id;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;
                    resultItem.product_Lot = item.Product_Lot;
                    resultItem.binBalance_QtyBal = item.SumQtyBal;
                    resultItem.binBalance_QtyReserve = item.SumQtyRe;
                    resultItem.amount = resultItem.binBalance_QtyBal - (resultItem.binBalance_QtyReserve == null ? 0 : resultItem.binBalance_QtyReserve);
                    resultItem.total = resultItem.binBalance_QtyBal + (resultItem.binBalance_QtyReserve == null ? 0 : resultItem.binBalance_QtyReserve);
                    resultItem.goodsReceive_No = item.GoodsReceive_No;
                    resultItem.goodsReceive_Date = item.GoodsReceive_Date.toString();
                    resultItem.goodsReceive_MFG_Date = item.GoodsReceive_MFG_Date.toString();
                    resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date.toString();

                    var Zonelocation = listDataZonelocation.Where(c => c.location_Index == item.Location_Index).ToList();
                    if (Zonelocation.Count > 0)
                    {
                        var Zone = listDataZone.Where(c => c.zone_Index == Zonelocation.FirstOrDefault().zone_Index).FirstOrDefault();

                        resultItem.zone_Index = Zone.zone_Index;
                        resultItem.zone_Id = Zone.zone_Id;
                        resultItem.zone_Name = Zone.zone_Name;

                    }

                    var productType = listDataProductType.Where(c => c.product_Index == item.Product_Index).FirstOrDefault();
                    if (productType != null)
                    {
                        resultItem.productType_Name = productType.productType_Name;
                    }

                    if (unit != null)
                    {
                        if (unit.productConversion_Ratio != null)
                        {
                            resultItem.sale_qty = item.SumQtyBal / unit.productConversion_Ratio;
                            resultItem.sale_unit = unit.productConversion_Name;
                        }
                        else
                        {
                            resultItem.sale_qty = item.SumQtyBal / item.BinBalance_Ratio;
                            resultItem.sale_unit = item.ProductConversion_Name;
                        }
                    }
                    else
                    {
                        resultItem.sale_qty = item.SumQtyBal / item.BinBalance_Ratio;
                        resultItem.sale_unit = item.ProductConversion_Name;
                    }

                    resultItem.erp_location = item.ERP_Location;
                    var cm3 = resultItem.sale_qty * (resultItem.productConversion_Width * resultItem.productConversion_Length * resultItem.productConversion_Height);
                    resultItem.cbm = (cm3 == 0) ? 0 : cm3 / 1000000;

                    result.Add(resultItem);
                }
                var count = TotalRow.Count;

                actionResult.itemsStockLo = result.ToList();
                actionResult.resultIsUse = true;
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };

                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

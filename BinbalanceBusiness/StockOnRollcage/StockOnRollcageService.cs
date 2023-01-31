using BinbalanceBusiness.Binbalance.ViewModels;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.Product;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static BinbalanceBusiness.Binbalance.ViewModels.StockOnRollcageBinbalanceViewModel;

namespace BinbalanceBusiness.BinBalanceService
{
    public class StockOnRollcageService
    {
        private BinbalanceDbContext db;
        private MasterDbContext dbMaster;

        public StockOnRollcageService()
        {
            db = new BinbalanceDbContext();
            dbMaster = new MasterDbContext();
        }
        public StockOnRollcageService(BinbalanceDbContext db)
        {
            this.db = db;
        }


        public actionResultViewModel filterByProduct(StockOnRollcageBinbalanceViewModel model)
        {
            try
            {
                db.Database.SetCommandTimeout(360);
                dbMaster.Database.SetCommandTimeout(360);
                #region locationtype
                List<Guid> index_locationtype = new List<Guid>
                {
                    Guid.Parse("64341969-E596-4B8B-8836-395061777490"),
                    Guid.Parse("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
                    Guid.Parse("2E9338D3-0931-4E36-B240-782BF9508641"),
                };
                #endregion
                var olog = new logtxt();
                List<Guid> location = dbMaster.View_Location.Where(C => index_locationtype.Contains(C.LocationType_Index.GetValueOrDefault())).Select(c => c.Location_Index).ToList();

                var query = db.View_BinbalanceRollcage.Where(c=> !location.Contains(c.Location_Index));



                #region advanceSearch
                if (model.advanceSearch == true)
                {


                    if (!string.IsNullOrEmpty(model.owner_Name))
                    {
                        query = query.Where(c => c.Owner_Name.Contains(model.owner_Name));
                    }


                    if (!string.IsNullOrEmpty(model.goodsReceive_Date) && !string.IsNullOrEmpty(model.goodsReceive_Date_To))
                    {
                        var dateStart = model.goodsReceive_Date.toBetweenDate();
                        var dateEnd = model.goodsReceive_Date_To.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                    }

                    //if (!string.IsNullOrEmpty(model.goodsReceive_Date) && !string.IsNullOrEmpty(model.goodsReceive_Date_To))
                    //{
                    //    var dateStart = model.goodsReceive_Date.toBetweenDate();
                    //    var dateEnd = model.goodsReceive_Date_To.toBetweenDate();
                    //    query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                    //}

                    //if (!string.IsNullOrEmpty(model.goodsReceive_MFG_Date) && !string.IsNullOrEmpty(model.goodsReceive_MFG_Date_To))
                    //{
                    //    var dateStart = model.goodsReceive_MFG_Date.toBetweenDate();
                    //    var dateEnd = model.goodsReceive_MFG_Date_To.toBetweenDate();
                    //    query = query.Where(c => c.GoodsReceive_MFG_Date >= dateStart.start && c.GoodsReceive_MFG_Date <= dateEnd.end);
                    //}
                    //if (!string.IsNullOrEmpty(model.goodsReceive_EXP_Date) && !string.IsNullOrEmpty(model.goodsReceive_EXP_Date_To))
                    //{
                    //    var dateStart = model.goodsReceive_MFG_Date.toBetweenDate();
                    //    var dateEnd = model.goodsReceive_MFG_Date_To.toBetweenDate();
                    //    query = query.Where(c => c.GoodsReceive_EXP_Date >= dateStart.start && c.GoodsReceive_EXP_Date <= dateEnd.end);
                    //}

                    if (!string.IsNullOrEmpty(model.product_Index.ToString()) && model.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == (model.product_Index));
                    }
                    if (!string.IsNullOrEmpty(model.productType_Index.ToString()) && model.productType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        var resultPro = new List<ProductDetailViewModel>();
                        var filterModelPro = new ProductDetailViewModel();

                        filterModelPro.productType_Index = model.productType_Index;
                        resultPro = utils.SendDataApi<List<ProductDetailViewModel>>(new AppSettingConfig().GetUrl("ConfigViewProductDetail"), filterModelPro.sJson());

                        if (resultPro.Count > 0)
                        {
                            query = query.Where(c => resultPro.Select(s => s.product_Index).Contains(c.Product_Index));

                        }
                    }
                    if (!string.IsNullOrEmpty(model.itemStatus_Index.ToString()) && model.itemStatus_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.ItemStatus_Index == (model.itemStatus_Index));
                    }

                    if (!string.IsNullOrEmpty(model.zone_Index.ToString()) && model.zone_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {

                        var resultZone = new List<View_LocatinCyclecountViewModel>();
                        var filterModelZone = new View_LocatinCyclecountViewModel();

                        filterModelZone.zone_Index = model.zone_Index;
                        resultZone = utils.SendDataApi<List<View_LocatinCyclecountViewModel>>(new AppSettingConfig().GetUrl("ConfigViewCyclecount"), filterModelZone.sJson());

                        if (resultZone.Count > 0)
                        {
                            query = query.Where(c => resultZone.Select(s => s.location_Index).Contains(c.Location_Index));

                        }
                    }

                    if (!string.IsNullOrEmpty(model.location_Index.ToString()) && model.location_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Location_Index == (model.location_Index));
                    }
                    if (!string.IsNullOrEmpty(model.product_Lot))
                    {
                        query = query.Where(c => c.Product_Lot.Contains(model.product_Lot));
                    }
                    if (!string.IsNullOrEmpty(model.goodsReceive_No))
                    {
                        query = query.Where(c => c.GoodsReceive_No.Contains(model.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(model.goodsIssue_No))
                    {
                        query = query.Where(c => c.GoodsIssue_No.Contains(model.goodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(model.tag_No))
                    {
                        query = query.Where(c => c.Tag_No == (model.tag_No));
                    }


                }

                #endregion

                #region Basic
                else
                {
                    if (!string.IsNullOrEmpty(model.product_Index.ToString()) && model.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == (model.product_Index));
                    }

                    if (!string.IsNullOrEmpty(model.owner_Index.ToString()) && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Owner_Index == (model.owner_Index));
                    }


                    if (!string.IsNullOrEmpty(model.planGoodsIssue_Due_Date) && !string.IsNullOrEmpty(model.planGoodsIssue_Due_Date))
                    {
                        var dateStart = model.planGoodsIssue_Due_Date.toBetweenDate();
                        var dateEnd = model.planGoodsIssue_Due_Date.toBetweenDate();
                        query = query.Where(c => c.PlanGoodsIssue_Due_Date >= dateStart.start && c.PlanGoodsIssue_Due_Date <= dateEnd.end);
                    }

                    if (!string.IsNullOrEmpty(model.round_Name))
                    {
                        query = query.Where(c => c.Round_Name.Contains(model.round_Name));
                    }

                    //if (!string.IsNullOrEmpty(model.round_Name.ToString()) && model.round_Name.ToString() != "00000000-0000-0000-0000-000000000000")
                    //{
                    //    query = query.Where(c => c.Round_Name == (model.round_Name));
                    //}


                }

                #endregion



                var resultquery = query.ToList();
                olog.logging("filterByProduct", "resultquery " + resultquery.Count.ToString());
                //    var queryGroup = resultquery.GroupBy(c => new
                //    {
                //        c.Product_Index,
                //        c.Product_Name,
                //        c.Product_Id,
                //        c.Owner_Index,
                //        c.Owner_Id,
                //        c.Owner_Name,
                //        //c.Location_Index,
                //        c.ProductConversion_Index,
                //        c.ProductConversion_Id,
                //        c.ProductConversion_Name,
                //        c.GoodsReceive_ProductConversion_Index,
                //        c.GoodsReceive_ProductConversion_Id,
                //        c.GoodsReceive_ProductConversion_Name,
                //        c.ItemStatus_Index,
                //        c.ItemStatus_Id,
                //        c.ItemStatus_Name,
                //        c.Product_Lot,
                //        c.BinBalance_Ratio
                //    })
                //    .Select(c => new
                //    {
                //        c.Key.Product_Index,
                //        c.Key.Product_Name,
                //        c.Key.Product_Id,
                //        c.Key.Owner_Index,
                //        c.Key.Owner_Id,
                //        c.Key.Owner_Name,
                //            //c.Key.Location_Index,
                //            c.Key.ProductConversion_Index,
                //        c.Key.ProductConversion_Id,
                //        c.Key.ProductConversion_Name,
                //        c.Key.GoodsReceive_ProductConversion_Index,
                //        c.Key.GoodsReceive_ProductConversion_Id,
                //        c.Key.GoodsReceive_ProductConversion_Name,
                //        c.Key.ItemStatus_Index,
                //        c.Key.ItemStatus_Id,
                //        c.Key.ItemStatus_Name,
                //        c.Key.Product_Lot,
                //        c.Key.BinBalance_Ratio,

                //        SumQtyBal = c.Sum(s => s.BinBalance_QtyBal),
                //        SumQtyRe = c.Sum(s => s.BinBalance_QtyReserve),
                //        SumWeight = c.Sum(s => s.BinBalance_WeightBal)
                //    });

                var queryGroup = (from res in resultquery
                                  //join loc in dbMaster.View_Location.ToList() on res.Location_Index equals loc.Location_Index
                                   select new
                                   {
                                       Product_Index = res.Product_Index,
                                       Product_Name = res.Product_Name,
                                       Product_Id = res.Product_Id,
                                       Owner_Index = res.Owner_Index,
                                       Owner_Id = res.Owner_Id,
                                       Owner_Name = res.Owner_Name,
                                       LocationType_Name = "Location Rollcage",                                            //loc.LocationType_Name,
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
                var aa = queryGroup.ToList();

                var model_con = new SearchProductOnRollcageConversionViewModel();
                var get_conversion = utils.SendDataApi<List<SearchProductOnRollcageConversionViewModel>>(new AppSettingConfig().GetUrl("conversion_sale_unit"), model_con.sJson());


                var join_sale = (from bin in queryGroup
                                 join conversion in get_conversion.ToList() on bin.Product_Index equals conversion.product_Index 
                                 into conversion2
                                 from conversion in conversion2.DefaultIfEmpty()
                                 select new StockOnRollcageBinbalanceViewModel
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
                                     binBalance_Ratio = bin.BinBalance_Ratio
                                 });


                var TotalRow = join_sale.ToList();

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    join_sale = join_sale.Skip(((model.CurrentPage - 1) * model.PerPage)).Take(model.PerPage).ToList();
                }


                var Item = queryGroup.ToList();
                olog.logging("filterByProduct", "queryGroup " + Item.Count.ToString());
                var result = new List<StockOnRollcageBinbalanceViewModel>();
                int addNumber = 0;

                foreach (var item in Item)
                {
                    var unit = get_conversion.Where(c => c.product_Index == item.Product_Index).FirstOrDefault();
                    //var location = dbMaster.View_Location.Where(c => c.Location_Index == item.Location_Index).FirstOrDefault();

                    var resultItem = new StockOnRollcageBinbalanceViewModel();
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
                    resultItem.locationType_Name = item.LocationType_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.binBalance_QtyBal = item.SumQtyBal;
                    resultItem.binBalance_QtyReserve = item.SumQtyRe;
                    resultItem.amount = resultItem.binBalance_QtyBal - resultItem.binBalance_QtyReserve;
                    resultItem.total = resultItem.binBalance_QtyBal + resultItem.binBalance_QtyReserve;
                    resultItem.goodsReceive_ProductConversion_Index = item.GoodsReceive_ProductConversion_Index;
                    resultItem.goodsReceive_ProductConversion_Id = item.GoodsReceive_ProductConversion_Id;
                    resultItem.goodsReceive_ProductConversion_Name = item.GoodsReceive_ProductConversion_Name;

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
                    resultItem.binBalance_WeightBal = item.SumWeight;
                    resultItem.itemStatus_Index = item.ItemStatus_Index;
                    resultItem.itemStatus_Id = item.ItemStatus_Id;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;
                    resultItem.product_Lot = item.Product_Lot;
                    resultItem.erp_location = item.ERP_Location;
                    var cm3 = resultItem.sale_qty * (resultItem.productConversion_Width * resultItem.productConversion_Length * resultItem.productConversion_Height);
                    resultItem.cbm = (cm3 == 0) ? 0 : cm3 / 1000000;

                    result.Add(resultItem);
                }
                var count = TotalRow.Count;
                olog.logging("filterByProduct", "TotalRow " + Item.Count.ToString());
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

        public actionResultViewModel filterByLocatuion(StockOnRollcageBinbalanceViewModel model)
        {
            try
            {
                var actionResult = new actionResultViewModel();

                #region locationtype
                List<Guid> index_locationtype = new List<Guid>
                {
                    Guid.Parse("64341969-E596-4B8B-8836-395061777490"),
                    Guid.Parse("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
                    Guid.Parse("2E9338D3-0931-4E36-B240-782BF9508641"),
                };
                #endregion
                
                var olog = new logtxt();
                List<Guid> location = dbMaster.View_Location.Where(C => index_locationtype.Contains(C.LocationType_Index.GetValueOrDefault())).Select(c => c.Location_Index).ToList();

                var query = db.View_BinbalanceRollcage.Where(c => !location.Contains(c.Location_Index));
                //var query = db.wm_BinBalance.AsQueryable();
                query = query.Where(c => c.BinBalance_QtyBal > 0 && c.BinBalance_QtyReserve >= 0);
                #region advanceSearch
                if (model.advanceSearch == true)
                {


                    if (!string.IsNullOrEmpty(model.owner_Name))
                    {
                        query = query.Where(c => c.Owner_Name.Contains(model.owner_Name));
                    }


                    if (!string.IsNullOrEmpty(model.goodsReceive_Date) && !string.IsNullOrEmpty(model.goodsReceive_Date_To))
                    {
                        var dateStart = model.goodsReceive_Date.toBetweenDate();
                        var dateEnd = model.goodsReceive_Date_To.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                    }

                    if (!string.IsNullOrEmpty(model.goodsReceive_MFG_Date) && !string.IsNullOrEmpty(model.goodsReceive_MFG_Date_To))
                    {
                        var dateStart = model.goodsReceive_MFG_Date.toBetweenDate();
                        var dateEnd = model.goodsReceive_MFG_Date_To.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_MFG_Date >= dateStart.start && c.GoodsReceive_MFG_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(model.goodsReceive_EXP_Date) && !string.IsNullOrEmpty(model.goodsReceive_EXP_Date_To))
                    {
                        var dateStart = model.goodsReceive_MFG_Date.toBetweenDate();
                        var dateEnd = model.goodsReceive_MFG_Date_To.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_EXP_Date >= dateStart.start && c.GoodsReceive_EXP_Date <= dateEnd.end);
                    }

                    if (!string.IsNullOrEmpty(model.product_Index.ToString()) && model.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == (model.product_Index));
                    }
                    if (!string.IsNullOrEmpty(model.productType_Index.ToString()) && model.productType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        var resultPro = new List<ProductDetailViewModel>();
                        var filterModelPro = new ProductDetailViewModel();

                        filterModelPro.productType_Index = model.productType_Index;
                        resultPro = utils.SendDataApi<List<ProductDetailViewModel>>(new AppSettingConfig().GetUrl("ConfigViewProductDetail"), filterModelPro.sJson());

                        if (resultPro.Count > 0)
                        {
                            query = query.Where(c => resultPro.Select(s => s.product_Index).Contains(c.Product_Index));

                        }
                    }
                    if (!string.IsNullOrEmpty(model.itemStatus_Index.ToString()) && model.itemStatus_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.ItemStatus_Index == (model.itemStatus_Index));
                    }

                    if (!string.IsNullOrEmpty(model.zone_Index.ToString()) && model.zone_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {

                        var resultZone = new List<ZoneLocationViewModel>();
                        var filterModelZone = new ZoneLocationViewModel();

                        filterModelZone.zone_Index = model.zone_Index;
                        resultZone = utils.SendDataApi<List<ZoneLocationViewModel>>(new AppSettingConfig().GetUrl("getZoneLocationMaster"), filterModelZone.sJson());

                        if (resultZone.Count > 0)
                        {
                            query = query.Where(c => resultZone.Select(s => s.location_Index).Contains(c.Location_Index));

                        }
                    }

                    if (!string.IsNullOrEmpty(model.location_Index.ToString()) && model.location_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        //var resultLo = new List<View_LocatinCyclecountViewModel>();
                        //var filterModelLo = new View_LocatinCyclecountViewModel();

                        //filterModelLo.location_Index = model.location_Index;
                        //resultLo = utils.SendDataApi<List<View_LocatinCyclecountViewModel>>(new AppSettingConfig().GetUrl("ConfigViewCyclecount"), filterModelLo.sJson());

                        //if (resultLo.Count > 0)
                        //{
                        //    query = query.Where(c => resultLo.Select(s => s.location_Index).Contains(c.Location_Index));

                        //}
                        query = query.Where(c => c.Location_Index == (model.location_Index));


                    }
                    if (!string.IsNullOrEmpty(model.product_Lot))
                    {
                        query = query.Where(c => c.Product_Lot.Contains(model.product_Lot));
                    }
                    if (!string.IsNullOrEmpty(model.goodsReceive_No))
                    {
                        query = query.Where(c => c.GoodsReceive_No.Contains(model.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(model.goodsIssue_No))
                    {
                        query = query.Where(c => c.GoodsIssue_No.Contains(model.goodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(model.tag_No))
                    {
                        query = query.Where(c => c.Tag_No == (model.tag_No));
                    }


                }

                #endregion

                #region Basic
                else
                {
                    if (!string.IsNullOrEmpty(model.product_Index.ToString()) && model.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == (model.product_Index));
                    }

                    if (!string.IsNullOrEmpty(model.owner_Index.ToString()) && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Owner_Index == (model.owner_Index));
                    }

                    if (!string.IsNullOrEmpty(model.planGoodsIssue_Due_Date) && !string.IsNullOrEmpty(model.planGoodsIssue_Due_Date))
                    {
                        var dateStart = model.planGoodsIssue_Due_Date.toBetweenDate();
                        var dateEnd = model.planGoodsIssue_Due_Date.toBetweenDate();
                        query = query.Where(c => c.PlanGoodsIssue_Due_Date >= dateStart.start && c.PlanGoodsIssue_Due_Date <= dateEnd.end);
                    }

                    if (!string.IsNullOrEmpty(model.round_Name))
                    {
                        query = query.Where(c => c.Round_Name.Contains(model.round_Name));
                    }

                }

                #endregion


                var resultquery = query.OrderByDescending(c=> c.Update_Date).ToList();
                olog.logging("filterByLocatuion", "resultquery " + resultquery.Count.ToString());
                //var queryGroup = resultquery.GroupBy(c => new
                //{
                //    c.Product_Index,
                //    c.Product_Name,
                //    c.Product_Id,
                //    c.Tag_Index,
                //    c.Tag_No,
                //    c.Location_Index,
                //    c.Location_Id,
                //    c.Location_Name,
                //    c.ItemStatus_Index,
                //    c.ItemStatus_Id,
                //    c.ItemStatus_Name,
                //    c.Product_Lot,
                //    c.ProductConversion_Index,
                //    c.ProductConversion_Id,
                //    c.ProductConversion_Name,
                //    c.GoodsReceive_Index,
                //    c.GoodsReceive_No,
                //    c.GoodsReceive_Date,
                //    c.GoodsReceive_MFG_Date,
                //    c.GoodsReceive_EXP_Date,

                //}).Select(c => new
                //{
                //    c.Key.Product_Index,
                //    c.Key.Product_Name,
                //    c.Key.Product_Id,
                //    c.Key.Tag_Index,
                //    c.Key.Tag_No,
                //    c.Key.Location_Index,
                //    c.Key.Location_Id,
                //    c.Key.Location_Name,
                //    c.Key.ItemStatus_Index,
                //    c.Key.ItemStatus_Id,
                //    c.Key.ItemStatus_Name,
                //    c.Key.Product_Lot,
                //    c.Key.ProductConversion_Index,
                //    c.Key.ProductConversion_Id,
                //    c.Key.ProductConversion_Name,
                //    c.Key.GoodsReceive_Index,
                //    c.Key.GoodsReceive_No,
                //    c.Key.GoodsReceive_Date,
                //    c.Key.GoodsReceive_MFG_Date,
                //    c.Key.GoodsReceive_EXP_Date,

                //    SumQtyBal = c.Sum(s => s.BinBalance_QtyBal),
                //    SumQtyRe = c.Sum(s => s.BinBalance_QtyReserve),
                //    SumWeught = c.Sum(s => s.BinBalance_WeightBal),
                //});



                var queryGroup = (from res in resultquery
                                  //join loc in dbMaster.View_Location.ToList() on res.Location_Index equals loc.Location_Index
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
                                      LocationType_Name = "Location Rollcage",                                      //loc.LocationType_Name,
                                      BinBalance_Ratio = res.BinBalance_Ratio,
                                      BinBalance_QtyBal = res.BinBalance_QtyBal,
                                      BinBalance_QtyReserve = res.BinBalance_QtyReserve,
                                      BinBalance_WeightBal = res.BinBalance_WeightBal,
                                      ERP_Location = res.ERP_Location,
                                      GoodsIssue_No = res.GoodsIssue_No,
                                      Round_Name = res.Round_Name,
                                      PlanGoodsIssue_Due_Date = res.PlanGoodsIssue_Due_Date,
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
                                      c.GoodsIssue_No,
                                      c.Round_Name,
                                      c.PlanGoodsIssue_Due_Date,
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
                                      c.Key.GoodsIssue_No,
                                      c.Key.Round_Name,
                                      c.Key.PlanGoodsIssue_Due_Date,
                                      SumQtyBal = c.Sum(s => s.BinBalance_QtyBal),
                                      SumQtyRe = c.Sum(s => s.BinBalance_QtyReserve),
                                      SumWeught = c.Sum(s => s.BinBalance_WeightBal),
                                  });

                var model_con = new SearchProductOnRollcageConversionViewModel();
                var get_conversion = utils.SendDataApi<List<SearchProductOnRollcageConversionViewModel>>(new AppSettingConfig().GetUrl("conversion_sale_unit"), model_con.sJson());

                var TotalRow = queryGroup.ToList();

                olog.logging("filterByLocatuion", "queryGroup " + TotalRow.Count.ToString());

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

                var result = new List<StockOnRollcageBinbalanceViewModel>();
                int addNumber = 0;

                foreach (var item in Item)
                {
                    var unit = get_conversion.Where(c => c.product_Index == item.Product_Index).FirstOrDefault();

                    int? dateDiffGetdate = 0;
                    int? productShelfLife_D = 0;

                    if (!string.IsNullOrEmpty(item.GoodsReceive_EXP_Date.toString()))
                    {
                        dateDiffGetdate = (Convert.ToDateTime(item.GoodsReceive_EXP_Date) - DateTime.Now).Days;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.GoodsReceive_Date.toString()))
                        {
                            dateDiffGetdate = (Convert.ToDateTime(item.GoodsReceive_Date) - DateTime.Now).Days;
                        }
                    }

                    var resProduct = dbMaster.ms_Product.Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
                    if (resProduct != null)
                    {
                        productShelfLife_D = resProduct.ProductShelfLife_D;
                    }

                    var resultItem = new StockOnRollcageBinbalanceViewModel();
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
                    resultItem.goodsIssue_No = item.GoodsIssue_No;
                    resultItem.round_Name = item.Round_Name;
                    resultItem.planGoodsIssue_Due_Date = item.PlanGoodsIssue_Due_Date.toString();

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

                    resultItem.dateDiffGetdate = dateDiffGetdate;
                    resultItem.productShelfLife_D = productShelfLife_D;
                    resultItem.remainingShelfLife = (dateDiffGetdate > 0 && productShelfLife_D > 0) ? dateDiffGetdate - productShelfLife_D : 0;

                    var cm3 = resultItem.sale_qty * (resultItem.productConversion_Width * resultItem.productConversion_Length * resultItem.productConversion_Height);
                    resultItem.cbm = (cm3 == 0) ? 0 : cm3 / 1000000;

                    result.Add(resultItem);
                }
                var count = TotalRow.Count;

                actionResult.itemsStockLo = result.ToList();
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

using BinbalanceBusiness.Binbalance.ViewModels;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.Product;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static BinbalanceBusiness.Binbalance.ViewModels.actionResultSKUViewModel;
using static BinbalanceBusiness.Binbalance.ViewModels.actionResultSKUConversionViewModel;
using static BinbalanceBusiness.Binbalance.ViewModels.actionResultSKUAllocatedByViewModel;
using System.Data.SqlClient;
using System.Data;
using BinBalanceDataAccess.Models;
using GRBusiness.ConfigModel;

namespace BinbalanceBusiness.InventoryStock
{
    public class InventoryStockService
    {
        private BinbalanceDbContext db;

        public InventoryStockService()
        {
            db = new BinbalanceDbContext();
        }
        public InventoryStockService(BinbalanceDbContext db)
        {
            this.db = db;
        }

        public actionResultSKUViewModel search(SKUSearchViewModel data)
        {
            try
            {

                var queryInquirySKU = db.View_InquirySKU.AsQueryable();

                if (!string.IsNullOrEmpty(data.ProductId))
                {
                    queryInquirySKU = queryInquirySKU.Where(c => c.Product_Id.Contains(data.ProductId));
                }

                if (!string.IsNullOrEmpty(data.ProductName))
                {
                    queryInquirySKU = queryInquirySKU.Where(c => c.Product_Name.Contains(data.ProductName));
                }

                var query = queryInquirySKU.OrderByDescending(c => c.GoodsReceive_Date).GroupBy(c => new {
                    c.Product_Index
                    , c.Product_Id
                    , c.Product_Name
                    , c.Owner_Index
                    , c.Owner_Id
                    , c.Owner_Name
                    ,c.ProductConversion_Index
                    ,c.ProductConversion_Id
                    , c.ProductConversion_Name
                    , c.Tag_No
                    , c.Location_Name
                    , c.GoodsReceive_Date
                })
                    .Select(c => new {
                        c.Key.Product_Index,
                        c.Key.Product_Id,
                        c.Key.Product_Name,
                        c.Key.ProductConversion_Index,
                        c.Key.ProductConversion_Id,
                        c.Key.ProductConversion_Name,
                        c.Key.Owner_Index,
                        c.Key.Owner_Id,
                        c.Key.Owner_Name,
                        c.Key.Tag_No,
                        c.Key.Location_Name,
                        c.Key.GoodsReceive_Date,
                        //c.Key.Product_SecondName,
                        SumStockONHand = c.Sum(s => s.StockONHand),
                        SumStockAllocated = c.Sum(s => s.StockAllocated),
                        SumStockAvailable = c.Sum(s => s.StockAvailable)
                    }).OrderByDescending(o => o.GoodsReceive_Date).ToList();

                var perpages = data.PerPage == 0 ? query.ToList() : query.Skip((data.CurrentPage - 1) * data.PerPage).Take(data.PerPage).ToList();

                var resultProductConversion = utils.SendDataApi<List<ProductConversionViewModel>>(new AppSettingConfig().GetUrl("dropdownProductConversionV2"), new { }.sJson());

                var result = new List<SKUSearchViewModel>();
                foreach (var item in perpages)
                {
                    var resultItem = new SKUSearchViewModel();

                    resultItem.ProductIndex = item.Product_Index;
                    resultItem.ProductId = item.Product_Id;
                    resultItem.ProductName = item.Product_Name;
                    //resultItem.ProductSecondName = item.Product_SecondName;
                    resultItem.TagNo = item.Tag_No;
                    resultItem.LocationName = item.Location_Name;
                    resultItem.StockONHand = item.SumStockONHand;
                    resultItem.StockAllocated = item.SumStockAllocated;
                    resultItem.StockAvailable = item.SumStockAvailable;
                    resultItem.ProductConversionName = item.ProductConversion_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;

                    if (item.ProductConversion_Index != null)
                    {
                        if (resultProductConversion.Count > 0 && resultProductConversion != null)
                        {
                            var DataProductConversion = resultProductConversion.Find(c => c.productConversion_Index == item.ProductConversion_Index);
                            if (DataProductConversion != null)
                            {
                                resultItem.productConversion_Ref1 = DataProductConversion.ref_No1;
                                resultItem.productConversion_Ref2 = DataProductConversion.ref_No2;
                                resultItem.productConversion_Ref3 = DataProductConversion.ref_No3;
                            }
                        }
                    }

                    result.Add(resultItem);
                }

                var count = query.Count;
                var actionResultInquirySku = new actionResultSKUViewModel();
                actionResultInquirySku.items = result.ToList();
                actionResultInquirySku.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                return actionResultInquirySku;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultSKUViewModel GetStockDetails(SKUSearchViewModel data)
        {
            try
            {
                using (var context = new BinbalanceDbContext())
                {
                    string pwhereFilter = "";

                    if (!string.IsNullOrEmpty(data.ProductId) == true)
                    {
                        pwhereFilter += " And Product_Id LIKE N'%" + data.ProductId + "%'";
                    }

                    if (!string.IsNullOrEmpty(data.GoodsReceiveNo) == true)
                    {
                        pwhereFilter += " And GoodsReceive_No LIKE N'%" + data.GoodsReceiveNo + "%'";
                    }

                    if (!string.IsNullOrEmpty(data.LocationName) == true)
                    {
                        pwhereFilter += " And Location_Name LIKE N'%" + data.LocationName + "%'";
                    }

                    //data.Orderby = data.Orderby == "" || data.Orderby == null ? data.Orderby = "ASC" : data.Orderby;
                    //if ((data.ColumnName != "" && data.ColumnName != null) && (data.Orderby != "" && data.Orderby != null))
                    //{
                    //    pwhereFilter += " Order By " + data.ColumnName + " " + data.Orderby + "";
                    //}
                    //else
                    //{
                    //    pwhereFilter += " Order By Product_Id ASC ";
                    //}

                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                    var query = context.View_InquirySKU.FromSql("sp_GetInquirySKU @strwhere ", strwhere).Where(c => c.StockONHand > 0).ToList();

                    var perpages = data.PerPage == 0 ? query.ToList() : query.Skip((data.CurrentPage - 1) * data.PerPage).Take(data.PerPage).ToList();

                    var result = new List<SKUSearchViewModel>();
                    foreach (var item in perpages)
                    {
                        var resultItem = new SKUSearchViewModel();

                        resultItem.ProductId = item.Product_Id;
                        resultItem.ProductName = item.Product_Name;
                        resultItem.ProductSecondName = item.Product_SecondName;
                        resultItem.LocationName = item.Location_Name;
                        resultItem.GoodsReceiveNo = item.GoodsReceive_No;
                        resultItem.ReceivingRef = item.ReceivingRef;
                        resultItem.GoodsReceiveDate = item.GoodsReceive_Date.ToString();
                        resultItem.TagNo = item.Tag_No;
                        resultItem.ItemStatusName = item.ItemStatus_Name;
                        resultItem.MFGDate = item.MFG_Date.ToString();
                        resultItem.EXPDate = item.EXP_Date.ToString();
                        resultItem.ProductConversionName = item.ProductConversion_Name;
                        resultItem.StockONHand = item.StockONHand;
                        resultItem.StockAllocated = item.StockAllocated;
                        resultItem.StockAvailable = item.StockAvailable;
                        result.Add(resultItem);
                    }

                    var count = query.Count;
                    var actionResultInquirySku = new actionResultSKUViewModel();
                    actionResultInquirySku.items = result.ToList();
                    actionResultInquirySku.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                    return actionResultInquirySku;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultSKUConversionViewModel GetSKUConversion(SKUSearchViewModel data)
        {
            try
            {
                var productData = new
                {
                    product_Index = data.ProductIndex
                };
                var getProductConversionBarcode = utils.SendDataApi<actionResultProductConversionBarcodeViewModel>(new AppSettingConfig().GetUrl("getProductConversionBarcode"), productData.sJson());


                //MasterDbContext dbMaster = new MasterDbContext();
                //var queryInquirySKU_Conversion = dbMaster.View_InquirySKU_Conversion.AsQueryable();

                //if (!string.IsNullOrEmpty(data.ProductId))
                //{
                //    queryInquirySKU_Conversion = queryInquirySKU_Conversion.Where(c => c.Product_Id.Contains(data.ProductId));
                //}

                //var query = queryInquirySKU_Conversion.ToList();

                var query = getProductConversionBarcode.itemsProductConversionBarcode.ToList();

                var perpages = data.PerPage == 0 ? query.ToList() : query.Skip((data.CurrentPage - 1) * data.PerPage).Take(data.PerPage).ToList();

                var result = new List<SKUConversionViewModel>();
                foreach (var item in perpages)
                {
                    var resultItem = new SKUConversionViewModel();

                    resultItem.ProductId = item.product_Id;
                    resultItem.ProductName = item.product_Name;
                    resultItem.ProductSecondName = item.product_Name;
                    resultItem.ProductThirdName = item.product_Name;
                    resultItem.ProductConversionBarcode = item.productConversionBarcode;
                    resultItem.SKUConversionName = item.product_Name + " " + item.productConversion_Name;
                    resultItem.ProductConversionName = item.productConversion_Name;
                    resultItem.ProductConversionRatio = item.productConversion_Ratio;
                    result.Add(resultItem);
                }

                var count = query.Count;
                var actionResultInquirySkuConversion = new actionResultSKUConversionViewModel();
                actionResultInquirySkuConversion.items = result.ToList();
                actionResultInquirySkuConversion.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                return actionResultInquirySkuConversion;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultSKUAllocatedByViewModel GetSKUAllocatedBy(SKUAllocatedByViewModel data)
        {
            try
            {
                using (var context = new BinbalanceDbContext())
                {
                    string pwhereFilter = "";

                    if (!string.IsNullOrEmpty(data.ProductId) == true)
                    {
                        pwhereFilter += " And Product_Id LIKE N'%" + data.ProductId + "%'";
                    }

                    //data.Orderby = data.Orderby == "" || data.Orderby == null ? data.Orderby = "ASC" : data.Orderby;
                    //if ((data.ColumnName != "" && data.ColumnName != null) && (data.Orderby != "" && data.Orderby != null))
                    //{
                    //    pwhereFilter += " Order By " + data.ColumnName + " " + data.Orderby + "";
                    //}
                    //else
                    //{
                    //    pwhereFilter += " Order By Product_Id ASC ";
                    //}

                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                    var query = context.View_InquirySKU_AllocatedBy.FromSql("sp_GetInquirySKU_AllocatedBy @strwhere ", strwhere).ToList();

                    var perpages = data.PerPage == 0 ? query.ToList() : query.Skip((data.CurrentPage - 1) * data.PerPage).Take(data.PerPage).ToList();

                    var result = new List<SKUAllocatedByViewModel>();
                    foreach (var item in perpages)
                    {
                        var resultItem = new SKUAllocatedByViewModel();

                        resultItem.ProductId = item.Product_Id;
                        resultItem.GoodsIssueNo = item.GoodsIssue_No;
                        resultItem.RefDocumentNo = item.Ref_Document_No;
                        resultItem.PlanGoodsIssueDueDate = item.PlanGoodsIssue_Due_Date;
                        resultItem.RouteName = item.Route_Name;
                        resultItem.RoundName = item.Round_Name;
                        resultItem.BinCardReserveQtyBal = item.BinCardReserve_QtyBal;

                        result.Add(resultItem);
                    }

                    var count = query.Count;
                    var actionResultInquirySkuAllocatedBy = new actionResultSKUAllocatedByViewModel();
                    actionResultInquirySkuAllocatedBy.items = result.ToList();
                    actionResultInquirySkuAllocatedBy.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                    return actionResultInquirySkuAllocatedBy;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataTable ExportExcelSKU(ListInquirySKU vModel)
        //{
        //    DataTable result = new DataTable();

        //    try
        //    {
        //        var provider = new System.Globalization.CultureInfo("en-US");

        //        DataTable data = new DataTable("Inquiry");
        //        data.Columns.Add("No", typeof(string));
        //        data.Columns.Add("SKU ID", typeof(string));
        //        data.Columns.Add("SKU Name", typeof(string));
        //        data.Columns.Add("Stock on Hand", typeof(string));
        //        data.Columns.Add("Stock Allocated", typeof(string));
        //        data.Columns.Add("Stock Available", typeof(string));
        //        //var materialList = db.Materials.Where(c => c.IsActive && !c.IsDelete && c.MaterialCodeGC != null).OrderBy(o => o.MaterialCode).ToList();
        //        foreach (var item in vModel.listInquirySKU)
        //        {
        //            if (item != null)
        //            {
        //                data.Rows.Add(
        //                    item.RowIndex,
        //                    item.ProductId,
        //                    item.ProductName,
        //                    item.StockONHand,
        //                    item.StockAllocated,
        //                    item.StockAvailable
        //                 );
        //            }
        //        }
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

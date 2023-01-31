using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using BinBalanceBusiness;
using BinBalanceBusiness.ViewModels;
using BinBalanceDataAccess.Models;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;
using ProductViewModel = BinBalanceBusiness.ViewModels.ProductViewModel;

namespace BinbalanceBusiness.BinBalanceService
{
    public class MovementService
    {
        private BinbalanceDbContext db;

        public MovementService()
        {
            db = new BinbalanceDbContext();
        }
        public MovementService(BinbalanceDbContext db)
        {
            this.db = db;
        }

        public actionResultMovementViewModel filterOld(FilterSearchMovementViewModel model)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var query = db.wm_BinCard.AsQueryable();

                query = query.Where(c => c.BinCard_QtySign != 0);

                if (!string.IsNullOrEmpty(model.owner_Index))
                {
                    query = query.Where(c => c.Owner_Index == Guid.Parse(model.owner_Index));
                }
                else if (!string.IsNullOrEmpty(model.owner_Id))
                {
                    query = query.Where(c => c.Owner_Id == model.owner_Id);
                }
                else if (!string.IsNullOrEmpty(model.owner_Name))
                {
                    query = query.Where(c => c.Owner_Name == model.owner_Name);
                }

                if (!string.IsNullOrEmpty(model.product_Index))
                {
                    query = query.Where(c => c.Product_Index == Guid.Parse(model.product_Index));
                }
                else if (!string.IsNullOrEmpty(model.product_Id))
                {
                    query = query.Where(c => c.Product_Id == model.product_Id);
                }
                else if (!string.IsNullOrEmpty(model.product_Name))
                {
                    query = query.Where(c => c.Product_Name == model.product_Name);
                }

                if (model.AdvanceSearch)
                {
                    if (!string.IsNullOrEmpty(model.productType_Index))
                    {
                        object product_type = new { productType_Index = Guid.Parse(model.productType_Index) };
                        var itemsproduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProductOfType"), product_type.sJson());
                        query = query.Where(c => itemsproduct.Select(s => s.product_Index.sParse<Guid?>()).Contains(c.Product_Index));
                    }

                    if (!string.IsNullOrEmpty(model.product_Lot))
                    {
                        query = query.Where(c => c.Product_Lot == model.product_Lot);
                    }

                    if (!string.IsNullOrEmpty(model.ref_Document_Index) && model.ref_Document_Index != model.ref_Document_Name)
                    {
                        query = query.Where(c => c.Ref_Document_Index == Guid.Parse(model.ref_Document_Index));
                    }
                    else if (!string.IsNullOrEmpty(model.ref_Document_Name))
                    {
                        query = query.Where(c => c.Ref_Document_No == model.ref_Document_Name);
                    }

                    if (!string.IsNullOrEmpty(model.itemStatus_Index))
                    {
                        query = query.Where(c => c.ItemStatus_Index == Guid.Parse(model.itemStatus_Index));
                    }
                    else if (!string.IsNullOrEmpty(model.itemStatus_Id))
                    {
                        query = query.Where(c => c.ItemStatus_Id == model.itemStatus_Id);
                    }
                    else if (!string.IsNullOrEmpty(model.itemStatus_Name))
                    {
                        query = query.Where(c => c.ItemStatus_Name == model.itemStatus_Name);
                    }
                }

                var dataqueryBegin = query.ToList();

                if (model.AdvanceSearch)
                {
                    if (!string.IsNullOrEmpty(model.advanceSearch_Date_From) && !string.IsNullOrEmpty(model.advanceSearch_Date_To))
                    {
                        query = query.Where(c => c.Create_Date >= model.advanceSearch_Date_From.toBetweenDate().start && c.Create_Date <= model.advanceSearch_Date_To.toBetweenDate().end);
                    }
                }else if (!string.IsNullOrEmpty(model.date_From) && !string.IsNullOrEmpty(model.date_To))
                {
                    query = query.Where(c => c.Create_Date >= model.date_From.toBetweenDate().start && c.Create_Date <= model.date_To.toBetweenDate().end);
                }
             

                var dataBincard = query.ToList();

                var date = model.AdvanceSearch == true ? model.advanceSearch_Date_From : model.date_From;

                var dataUnion = (from bc in dataqueryBegin
                                 join bb in db.wm_BinBalance on bc.GoodsReceiveItemLocation_Index equals bb.GoodsReceiveItemLocation_Index
                                 where bc.Create_Date <= date.toDate()
                                 group bc by new
                                 {
                                     product_Id = bc.Product_Id,
                                     bb.BinBalance_Index
                                 } into g
                                 select new
                                 {
                                     tag_No = "",
                                     product_Id = "",
                                     product_Name = "",
                                     ref_Document_No = "",
                                     documentType_Name = "Begin",
                                     product_Lot = "",
                                     binCard_QtyIn = (decimal?)0.00,
                                     binCard_QtyOut = (decimal?)0.00,
                                     binCard_QtySign = g.Sum(s => s.BinCard_QtySign),
                                     productConversion_Name = "",
                                     create_Date = (DateTime?)null
                                 }
                 ).Union(from bc in dataBincard
                         join bb in db.wm_BinBalance on bc.GoodsReceiveItemLocation_Index equals bb.GoodsReceiveItemLocation_Index
                         group bc by new
                         {
                             tag_No = bc.Tag_No,
                             product_Id = bc.Product_Id,
                             product_Name = bc.Product_Name,
                             ref_Document_No = bc.Ref_Document_No,
                             documentType_Name = bc.DocumentType_Name,
                             product_Lot = bc.Product_Lot,
                             productConversion_Name = bc.ProductConversion_Name,
                             create_Date = bc.Create_Date,
                             bb.BinBalance_Index
                         } into g
                         where g.Sum(s => (decimal)s.BinCard_QtySign) != 0
                         select new
                         {
                             tag_No = g.Key.tag_No,
                             product_Id = g.Key.product_Id,
                             product_Name = g.Key.product_Name,
                             ref_Document_No = g.Key.ref_Document_No,
                             documentType_Name = g.Key.documentType_Name,
                             product_Lot = g.Key.product_Lot,
                             binCard_QtyIn = g.Sum(s => s.BinCard_QtyIn),
                             binCard_QtyOut = g.Sum(s => s.BinCard_QtyOut),
                             binCard_QtySign = g.Sum(s => s.BinCard_QtySign),
                             productConversion_Name = g.Key.productConversion_Name,
                             create_Date = g.Key.create_Date
                         }
                    ).OrderBy(o => o.create_Date).ToList();


                decimal? sumBinCard_QtySign = 0;

                var sum = model.CurrentPage <= 1 ? 0 : dataUnion.Skip((model.CurrentPage - 2) * model.PerPage).Take(model.PerPage).Sum(s => s.binCard_QtySign);

                var perpages = model.PerPage == 0 ? dataUnion.ToList() : dataUnion.Skip((model.CurrentPage - 1) * model.PerPage).Take(model.PerPage).ToList();

                var results = new List<MovementViewModel>();
                foreach (var d in perpages)
                {
                    sumBinCard_QtySign += d.binCard_QtySign + sum;
                    sum = sum - sum;
                    var r = new MovementViewModel
                    {
                        tag_No = d.tag_No,
                        product_Id = d.product_Id,
                        product_Name = d.product_Name,
                        ref_Document_No = d.ref_Document_No,
                        documentType_Name = d.documentType_Name,
                        product_Lot = d.product_Lot,
                        binCard_QtyIn = d.binCard_QtyIn,
                        binCard_QtyOut = d.binCard_QtyOut,
                        binCard_QtySign = sumBinCard_QtySign,
                        productConversion_Name = d.productConversion_Name,
                    };
                    results.Add(r);
                }

                var count = dataUnion.Count;
                var actionResult = new actionResultMovementViewModel();
                actionResult.items = results.ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage };

                return actionResult;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("UpdateStatusPGII", msglog);
                throw ex;
            }
        }

        public actionResultMovementViewModel filter(FilterSearchMovementViewModel model)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            try
            {

                var LocationIndex = utils.SendDataApi<string>(new AppSettingConfig().GetUrl("getConfigFromBase"), new { Config_Key = "Config_LocationType_Staging" }.sJson()).Split(',').Select(s => s == null || s == string.Empty ? (Guid?)null : Guid.Parse(s)).ToList();
                var location = utils.SendDataApi<List<locationViewModel>>(new AppSettingConfig().GetUrl("LocationConfig"), new { }.sJson()).Where(c => LocationIndex.Contains(c.locationType_Index)).ToList();


                var query = db.wm_BinCard.AsQueryable();

                query = query.Where(c => c.BinCard_QtySign != 0);

                query = query.Where(c => !location.Select(s => s.location_Index).Contains(c.Location_Index));

                if (!string.IsNullOrEmpty(model.owner_Index))
                {
                    try
                    {
                        var owner_index = Guid.Parse(model.owner_Index);
                        query = query.Where(c => c.Owner_Index == owner_index);
                    }
                    catch
                    {
                        query = query.Where(c => c.Owner_Name.Contains(model.owner_Name));
                    }
                }
                else if (!string.IsNullOrEmpty(model.owner_Id))
                {
                    query = query.Where(c => c.Owner_Id.Contains(model.owner_Id));
                }
                else if (!string.IsNullOrEmpty(model.owner_Name))
                {
                    query = query.Where(c => c.Owner_Name.Contains(model.owner_Name));
                }

                if (!string.IsNullOrEmpty(model.product_Index))
                {
                    try
                    {
                        var product_index = Guid.Parse(model.product_Index);
                        query = query.Where(c => c.Product_Index == product_index);
                    }
                    catch 
                    {
                        query = query.Where(c => c.Product_Name.Contains(model.product_Name));
                    }
                }
                else if (!string.IsNullOrEmpty(model.product_Id))
                {
                    query = query.Where(c => c.Product_Id.Contains(model.product_Id));
                }
                else if (!string.IsNullOrEmpty(model.product_Name))
                {
                    query = query.Where(c => c.Product_Name.Contains(model.product_Name));
                }

                if (model.AdvanceSearch)
                {
                    if (!string.IsNullOrEmpty(model.productType_Index))
                    {
                        object product_type = new { productType_Index = Guid.Parse(model.productType_Index) };
                        var itemsproduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProductOfType"), product_type.sJson());
                        query = query.Where(c => itemsproduct.Select(s => s.product_Index.sParse<Guid?>()).Contains(c.Product_Index));
                    }

                    if (!string.IsNullOrEmpty(model.product_Lot))
                    {
                        query = query.Where(c => c.Product_Lot == model.product_Lot);
                    }

                    if (!string.IsNullOrEmpty(model.ref_Document_Index) && model.ref_Document_Index != model.ref_Document_Name)
                    {
                        query = query.Where(c => c.Ref_Document_Index == Guid.Parse(model.ref_Document_Index));
                    }
                    else if (!string.IsNullOrEmpty(model.ref_Document_Name))
                    {
                        query = query.Where(c => c.Ref_Document_No == model.ref_Document_Name);
                    }

                    if (!string.IsNullOrEmpty(model.itemStatus_Index))
                    {
                        query = query.Where(c => c.ItemStatus_Index == Guid.Parse(model.itemStatus_Index));
                    }
                    else if (!string.IsNullOrEmpty(model.itemStatus_Id))
                    {
                        query = query.Where(c => c.ItemStatus_Id == model.itemStatus_Id);
                    }
                    else if (!string.IsNullOrEmpty(model.itemStatus_Name))
                    {
                        query = query.Where(c => c.ItemStatus_Name == model.itemStatus_Name);
                    }
                }

                var dataqueryBegin = query.ToList();

                if (model.AdvanceSearch)
                {
                    if (!string.IsNullOrEmpty(model.advanceSearch_Date_From) && !string.IsNullOrEmpty(model.advanceSearch_Date_To))
                    {
                        query = query.Where(c => c.Create_Date >= model.advanceSearch_Date_From.toBetweenDate().start && c.Create_Date <= model.advanceSearch_Date_To.toBetweenDate().end);
                    }
                }
                else if (!string.IsNullOrEmpty(model.date_From) && !string.IsNullOrEmpty(model.date_To))
                {
                    query = query.Where(c => c.Create_Date >= model.date_From.toBetweenDate().start && c.Create_Date <= model.date_To.toBetweenDate().end);
                }


                var dataBincard = query.ToList();

                var date = model.AdvanceSearch == true ? model.advanceSearch_Date_From : model.date_From;

                var dataUnion = (from bc in dataqueryBegin
                                 where bc.Create_Date <= date.toDate()
                                 group bc by new
                                 {
                                     product_Id = bc.Product_Id,
                                 } into g
                                 select new
                                 {
                                     tag_No = "",
                                     bincard_date = (DateTime?)null,
                                     product_Id = "",
                                     product_Name = "",
                                     ref_Document_No = "",
                                     documentType_Name = "Begin",
                                     product_Lot = "",
                                     binCard_QtyIn = (decimal?)0.00,
                                     binCard_QtyOut = (decimal?)0.00,
                                     binCard_QtySign = g.Sum(s => s.BinCard_QtySign),
                                     productConversion_Name = "",
                                     location_Name = "",
                                     location_Name_To = "",
                                     itemStatus_Name = "",
                                     itemStatus_Name_To = "",
                                     create_Date = (DateTime?)null
                                 }
                 ).Union(from bc in dataBincard
                         group bc by new
                         {
                             tag_No = bc.Tag_No,
                             bincard_date = bc.BinCard_Date,
                             product_Id = bc.Product_Id,
                             product_Name = bc.Product_Name,
                             ref_Document_No = bc.Ref_Document_No,
                             documentType_Name = bc.DocumentType_Name,
                             product_Lot = bc.Product_Lot,
                             productConversion_Name = bc.ProductConversion_Name,
                             location_Name = bc.Location_Name,
                             location_Name_To = bc.Location_Name_To,
                             itemStatus_Name = bc.ItemStatus_Name,
                             itemStatus_Name_To = bc.ItemStatus_Name_To,
                             create_Date = bc.Create_Date,
                         } into g
                         select new
                         {
                             tag_No = g.Key.tag_No,
                             bincard_date = g.Key.bincard_date,
                             product_Id = g.Key.product_Id,
                             product_Name = g.Key.product_Name,
                             ref_Document_No = g.Key.ref_Document_No,
                             documentType_Name = g.Key.documentType_Name,
                             product_Lot = g.Key.product_Lot,
                             binCard_QtyIn = g.Sum(s => s.BinCard_QtyIn),
                             binCard_QtyOut = g.Sum(s => s.BinCard_QtyOut),
                             binCard_QtySign = g.Sum(s => s.BinCard_QtySign),
                             productConversion_Name = g.Key.productConversion_Name,
                             location_Name = g.Key.location_Name,
                             location_Name_To = g.Key.location_Name_To,
                             itemStatus_Name = g.Key.itemStatus_Name,
                             itemStatus_Name_To = g.Key.itemStatus_Name_To,
                             create_Date = g.Key.create_Date
                         }
                    ).OrderBy(o => o.create_Date).ToList();


                decimal? sumBinCard_QtySign = 0;

                var sum = model.CurrentPage <= 1 ? 0 : dataUnion.Skip((model.CurrentPage - 2) * model.PerPage).Take(model.PerPage).Sum(s => s.binCard_QtySign);

                var perpages = model.PerPage == 0 ? dataUnion.ToList() : dataUnion.Skip((model.CurrentPage - 1) * model.PerPage).Take(model.PerPage).ToList();

                var results = new List<MovementViewModel>();
                foreach (var d in perpages)
                {
                    sumBinCard_QtySign += d.binCard_QtySign + sum;
                    sum = sum - sum;
                    var r = new MovementViewModel
                    {
                        tag_No = d.tag_No,
                        bincard_date = d.bincard_date.toString(),
                        product_Id = d.product_Id,
                        product_Name = d.product_Name,
                        ref_Document_No = d.ref_Document_No,
                        documentType_Name = d.documentType_Name,
                        product_Lot = d.product_Lot,
                        binCard_QtyIn = d.binCard_QtyIn,
                        binCard_QtyOut = d.binCard_QtyOut,
                        binCard_QtySign = sumBinCard_QtySign,
                        productConversion_Name = d.productConversion_Name,
                        location_Name = d.location_Name,
                        location_Name_To = d.location_Name_To,
                        itemStatus_Name = d.itemStatus_Name,
                        itemStatus_Name_To = d.itemStatus_Name_To,
                    };
                    results.Add(r);
                }

                var count = dataUnion.Count;
                var actionResult = new actionResultMovementViewModel();
                actionResult.items = results.ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage };

                return actionResult;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("UpdateStatusPGII", msglog);
                throw ex;
            }
        }
    }
}

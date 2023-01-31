using BinBalanceBusiness.ViewModels;
using BinBalanceDataAccess.Models;

using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BinbalanceBusiness.ReplenishmentBalance
{
    public class ReplenishmentBalanceService : Pagination
    {
        private BinbalanceDbContext db;

        public ReplenishmentBalanceService()
        {
            db = new BinbalanceDbContext();
        }
        public ReplenishmentBalanceService(BinbalanceDbContext db)
        {
            this.db = db;
        }

        private SearchReplenishmentBalanceModel GetSearchReplenishmentBalanceModel(string jsonData)
        {
            SearchReplenishmentBalanceModel model = JsonConvert.DeserializeObject<SearchReplenishmentBalanceModel>(jsonData ?? string.Empty);
            if (model is null)
            {
                throw new Exception("Invalid JSon : Can not Convert to Model");
            }

            if (model.Items is null || model.Items.Count == 0)
            {
                throw new Exception("Invalid JSon : Replenish Item not found");
            }

            if (model.ReplenishLocationIndexs is null || model.ReplenishLocationIndexs.Count == 0)
            {
                throw new Exception("Invalid JSon : Replenish Location not found");
            }

            if (model.ReplenishItemStatusIndexs is null || model.ReplenishItemStatusIndexs.Count == 0)
            {
                throw new Exception("Invalid JSon : Replenish ItemStatus not found");
            }

            return model;
        }

        public List<ReplenishmentBalanceModel> SearchReplenishmentBinBalance(string jsonData)
        {
            try
            {
                SearchReplenishmentBalanceModel data = GetSearchReplenishmentBalanceModel(jsonData);
                List<ReplenishmentBalanceModel> ReplenishmentBinBalance = new List<ReplenishmentBalanceModel>();

                List<wm_BinBalance> StorageBinBalances;
                decimal ReplenishQty, SumLocationBinBalanceQty, SumStorageBalanceQty, PendingReplenishQty, StorageQty;

                foreach (SearchReplenishmentBalanceItemModel Item in data.Items)
                {
                    SumLocationBinBalanceQty = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.Location_Index.Equals(Item.Location_Index)) &&
                             (s.BinBalance_QtyBal - s.BinBalance_QtyReserve > 0)
                    ).Sum(s => s.BinBalance_QtyBal) ?? 0; // (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;

                    ReplenishQty = (Item.Replenish_Qty > Item.Minimum_Qty ? Item.Replenish_Qty : Item.Minimum_Qty) - Item.Pending_Replenish_Qty - SumLocationBinBalanceQty;
                    if (ReplenishQty <= 0)
                    {
                        //BinBalance no need to Replenishment
                        continue;
                    }

                    StorageBinBalances = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.BinBalance_QtyBal - s.BinBalance_QtyReserve > 0) &&
                             (data.ReplenishLocationIndexs.Contains(s.Location_Index)) &&
                             (data.ReplenishItemStatusIndexs.Contains(s.ItemStatus_Index))
                    ).ToList();

                    if ((StorageBinBalances?.Count ?? 0) == 0)
                    {
                        //Storage BinBalance not found
                        continue;
                    }

                    SumStorageBalanceQty = StorageBinBalances.Sum(s => (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;
                    if (ReplenishQty > SumStorageBalanceQty)
                    {
                        //Not Enough Storage to Replenish
                        //TO DO Add All ?
                    }

                    PendingReplenishQty = ReplenishQty;
                    foreach (wm_BinBalance StorageBalance in StorageBinBalances)
                    {
                        if (PendingReplenishQty <= 0)
                        {
                            break;
                        }

                        StorageQty = StorageBalance.BinBalance_QtyBal.Value - StorageBalance.BinBalance_QtyReserve.Value;
                        if (StorageQty > PendingReplenishQty)
                        {
                            StorageQty = PendingReplenishQty;
                        }

                        ReplenishmentBinBalance.Add(
                            new ReplenishmentBalanceModel()
                            {
                                BinBalance = StorageBalance,
                                Owner_Index = StorageBalance.Owner_Index,
                                Location_Index = Item.Location_Index,
                                Location_Id = Item.Location_Id,
                                Location_Name = Item.Location_Name,
                                Replenish_Qty = StorageQty
                            }
                        );
                        PendingReplenishQty -= StorageQty;
                    }
                }
                return ReplenishmentBinBalance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<ReplenishmentBalanceModel> SearchReplenishmentBinBalanceASRS(string jsonData)
        {
            try
            {
                SearchReplenishmentBalanceModel data = GetSearchReplenishmentBalanceModel(jsonData);
                List<ReplenishmentBalanceModel> ReplenishmentBinBalance = new List<ReplenishmentBalanceModel>();


                
                List<wm_BinBalance> StorageBinBalances;
                decimal ReplenishQty, SumLocationBinBalanceQty, SumStorageBalanceQty, PendingReplenishQty, StorageQty ;

                foreach (SearchReplenishmentBalanceItemModel Item in data.Items)
                {
                    SumLocationBinBalanceQty = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.Location_Index.Equals(Item.Location_Index)) &&
                             (  s.BinBalance_QtyReserve >= 0) &&
                             (s.BinBalance_QtyBal > 0)
                    ).Sum(s => s.BinBalance_QtyBal) ?? 0; // (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;

                    if (SumLocationBinBalanceQty > 0)
                    {
                        ReplenishQty = 0;
                    }
                    else
                    {
                        ReplenishQty = (Item.Replenish_Qty > Item.Minimum_Qty ? Item.Replenish_Qty : Item.Minimum_Qty) - Item.Pending_Replenish_Qty - SumLocationBinBalanceQty;

                    }

                   
                    if (ReplenishQty <= 0)
                    {
                        //BinBalance no need to Replenishment
                        continue;
                    }

                    StorageBinBalances = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.BinBalance_QtyBal - s.BinBalance_QtyReserve > 0) &&
                             (data.ReplenishLocationIndexs.Contains(s.Location_Index)) &&
                             (s.BinBalance_QtyReserve == 0) &&
                             (data.ReplenishItemStatusIndexs.Contains(s.ItemStatus_Index))
                    ).OrderBy(c => c.GoodsReceive_EXP_Date).ThenBy(d => d.Location_Name).ToList();

                    if ((StorageBinBalances?.Count ?? 0) == 0)
                    {
                        //Storage BinBalance not found
                        continue;
                    }

                    SumStorageBalanceQty = StorageBinBalances.Sum(s => (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;
                    if (ReplenishQty > SumStorageBalanceQty)
                    {


                    }

                    PendingReplenishQty = ReplenishQty;
                    foreach (wm_BinBalance StorageBalance in StorageBinBalances.OrderBy(c => c.GoodsReceive_EXP_Date).ThenBy(d => d.Location_Name))
                    {
                        if (PendingReplenishQty <= 0)
                        {
                            break;
                        }

                        StorageQty = StorageBalance.BinBalance_QtyBal.Value - StorageBalance.BinBalance_QtyReserve.Value;
                        if (StorageQty > PendingReplenishQty)
                        {
                            StorageQty = PendingReplenishQty;
                        }

                        ReplenishmentBinBalance.Add(
                            new ReplenishmentBalanceModel()
                            {
                                BinBalance = StorageBalance,
                                Owner_Index = StorageBalance.Owner_Index,
                                Location_Index = Item.Location_Index,
                                Location_Id = Item.Location_Id,
                                Location_Name = Item.Location_Name,
                                Replenish_Qty = StorageQty
                            }
                        );
                        PendingReplenishQty -= StorageQty;
                    }
                }
                return ReplenishmentBinBalance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReplenishmentBalanceModel> SearchReplenishmentBinBalancePIECEPICK(string jsonData)
        {
            try
            {
                SearchReplenishmentBalanceModel data = GetSearchReplenishmentBalanceModel(jsonData);
                List<ReplenishmentBalanceModel> ReplenishmentBinBalance = new List<ReplenishmentBalanceModel>();

                List<wm_BinBalance> StorageBinBalances;
                decimal ReplenishQty, SumLocationBinBalanceQty, SumStorageBalanceQty, PendingReplenishQty, StorageQty, QtyBalLocation;

                foreach (SearchReplenishmentBalanceItemModel Item in data.Items)
                {
                    SumLocationBinBalanceQty = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.Location_Index.Equals(Item.Location_Index)) &&
                              (s.BinBalance_QtyReserve >= 0) &&
                             (s.BinBalance_QtyBal > 0)
                    ).Sum(s => s.BinBalance_QtyBal) ?? 0; // (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;
                    
  
          

                    ReplenishQty = (Item.Replenish_Qty > Item.Minimum_Qty ? Item.Replenish_Qty : Item.Minimum_Qty) - Item.Pending_Replenish_Qty - SumLocationBinBalanceQty;
                    if (ReplenishQty <= 0)
                    {
                        //BinBalance no need to Replenishment
                        continue;
                    }

                    StorageBinBalances = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.BinBalance_QtyBal - s.BinBalance_QtyReserve > 0) &&
                             (data.ReplenishLocationIndexs.Contains(s.Location_Index)) &&
                         //    (s.BinBalance_QtyReserve == 0) &&
                             (data.ReplenishItemStatusIndexs.Contains(s.ItemStatus_Index)) &&
                             (s.ERP_Location == "AB01")
                    ).OrderBy(c => c.GoodsReceive_EXP_Date).ThenBy(d => d.Location_Name).ToList();

                    if ((StorageBinBalances?.Count ?? 0) == 0)
                    {
                        //Storage BinBalance not found
                        continue;
                    }

                    SumStorageBalanceQty = StorageBinBalances.Sum(s => (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;
                    if (ReplenishQty > SumStorageBalanceQty)
                    {
                        //Not Enough Storage to Replenish
                        //TO DO Add All ?
                    }

                    PendingReplenishQty = ReplenishQty;
                    foreach (wm_BinBalance StorageBalance in StorageBinBalances.OrderBy(c => c.GoodsReceive_EXP_Date).ThenBy(d => d.Location_Name))
                    {
                        if (PendingReplenishQty <= 0)
                        {
                            break;
                        }

                        StorageQty = StorageBalance.BinBalance_QtyBal.Value - StorageBalance.BinBalance_QtyReserve.Value;
                        if (StorageQty > PendingReplenishQty)
                        {
                            StorageQty = PendingReplenishQty;
                        }

                        ReplenishmentBinBalance.Add(
                            new ReplenishmentBalanceModel()
                            {
                                BinBalance = StorageBalance,
                                Owner_Index = StorageBalance.Owner_Index,
                                Location_Index = Item.Location_Index,
                                Location_Id = Item.Location_Id,
                                Location_Name = Item.Location_Name,
                                Replenish_Qty = StorageQty
                            }
                        );
                        PendingReplenishQty -= StorageQty;
                    }
                }
                return ReplenishmentBinBalance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReplenishmentBalanceModel> SearchReplenishmentBinBalancePIECEPICK_V2(string jsonData)
        {
            try
            {
                SearchReplenishmentBalanceModel data = GetSearchReplenishmentBalanceModel(jsonData);
                List<ReplenishmentBalanceModel> ReplenishmentBinBalance = new List<ReplenishmentBalanceModel>();

                List<wm_BinBalance> StorageBinBalances;
                decimal ReplenishQty, SumLocationBinBalanceQty, SumStorageBalanceQty, PendingReplenishQty, StorageQty, QtyBalLocation;

                foreach (SearchReplenishmentBalanceItemModel Item in data.Items)
                {
                    SumLocationBinBalanceQty = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.Location_Index.Equals(Item.Location_Index)) &&
                              (s.BinBalance_QtyReserve >= 0) &&
                             (s.BinBalance_QtyBal > 0)
                    ).Sum(s => s.BinBalance_QtyBal - s.BinBalance_QtyReserve) ?? 0; // (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;




                    ReplenishQty = (Item.Replenish_Qty > Item.Minimum_Qty ? Item.Replenish_Qty : Item.Minimum_Qty) - Item.Pending_Replenish_Qty - SumLocationBinBalanceQty;
                    if (ReplenishQty <= 0)
                    {
                        //BinBalance no need to Replenishment
                        continue;
                    }

                    StorageBinBalances = db.wm_BinBalance.Where(
                        s => (data.Owner_Index.HasValue ? s.Owner_Index.Equals(data.Owner_Index) : true) &&
                             (s.Product_Index.Equals(Item.Product_Index)) &&
                             (s.BinBalance_QtyBal - s.BinBalance_QtyReserve > 0) &&
                             (data.ReplenishLocationIndexs.Contains(s.Location_Index)) &&
                             //    (s.BinBalance_QtyReserve == 0) &&
                             (data.ReplenishItemStatusIndexs.Contains(s.ItemStatus_Index)) &&
                             (s.ERP_Location == "AB01")
                    ).OrderBy(c => c.GoodsReceive_EXP_Date).ThenBy(d => d.Location_Name).ToList();

                    if ((StorageBinBalances?.Count ?? 0) == 0)
                    {
                        //Storage BinBalance not found
                        continue;
                    }

                    SumStorageBalanceQty = StorageBinBalances.Sum(s => (s.BinBalance_QtyBal - s.BinBalance_QtyReserve)) ?? 0;
                    if (ReplenishQty > SumStorageBalanceQty)
                    {
                        //Not Enough Storage to Replenish
                        //TO DO Add All ?
                    }

                    PendingReplenishQty = ReplenishQty;
                    foreach (wm_BinBalance StorageBalance in StorageBinBalances.OrderBy(c => c.GoodsReceive_EXP_Date).ThenBy(d => d.Location_Name))
                    {
                        if (PendingReplenishQty <= 0)
                        {
                            break;
                        }

                        StorageQty = StorageBalance.BinBalance_QtyBal.Value - StorageBalance.BinBalance_QtyReserve.Value;
                        if (StorageQty > PendingReplenishQty)
                        {
                            StorageQty = PendingReplenishQty;
                        }

                        ReplenishmentBinBalance.Add(
                            new ReplenishmentBalanceModel()
                            {
                                BinBalance = StorageBalance,
                                Owner_Index = StorageBalance.Owner_Index,
                                Location_Index = Item.Location_Index,
                                Location_Id = Item.Location_Id,
                                Location_Name = Item.Location_Name,
                                Replenish_Qty = StorageQty
                            }
                        );
                        PendingReplenishQty -= StorageQty;
                    }
                }
                return ReplenishmentBinBalance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


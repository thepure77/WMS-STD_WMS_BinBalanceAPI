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
using static BinbalanceBusiness.Binbalance.ViewModels.StockBinbalanceViewModel;
using static BinbalanceBusiness.Binbalance.ViewModels.View_agingViewModel;

namespace BinbalanceBusiness.BinBalanceService
{
    public class AgingService
    {
        private BinbalanceDbContext db;

        public AgingService()
        {
            db = new BinbalanceDbContext();
        }
        public AgingService(BinbalanceDbContext db)
        {
            this.db = db;
        }


        public actionResultAgingViewModel filter(View_agingViewModel model)
        {
            try
            {
                var query = db.View_aging.AsQueryable();

                if (!string.IsNullOrEmpty(model.product_Index.ToString()) && model.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    query = query.Where(c => c.Product_Index == (model.product_Index));
                }

                if (!string.IsNullOrEmpty(model.owner_Index.ToString()) && model.owner_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    query = query.Where(c => c.Owner_Index == (model.owner_Index));
                }
  
                var TotalRow = query.ToList();

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);

                }

                var Item = query.ToList();

                var result = new List<View_agingViewModel>();
                int addNumber = 0;

                foreach (var item in Item)
                {
                    var resultItem = new View_agingViewModel();
                    addNumber++;
                    resultItem.row = addNumber;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    //resultItem.age = item.average;
                    resultItem.age = string.Format(String.Format("{0:N3}", item.average));
                    resultItem.c1 = item.date_1;
                    resultItem.c2 = item.date_2;
                    resultItem.c3 = item.date_3;
                    resultItem.c4 = item.date_4;
                    resultItem.qty = item.sumQtyBalance;
                    if (resultItem.qty != 0)
                    {
                        resultItem.p1 = string.Format(String.Format("{0:N3}", (resultItem.c1 / resultItem.qty) * 100));
                        resultItem.p2 = string.Format(String.Format("{0:N3}", (resultItem.c2 / resultItem.qty) * 100));
                        resultItem.p3 = string.Format(String.Format("{0:N3}", (resultItem.c3 / resultItem.qty) * 100));
                        resultItem.p4 = string.Format(String.Format("{0:N3}", (resultItem.c4 / resultItem.qty) * 100));
                    }
                    else
                    {
                        resultItem.p1 = 0.ToString();
                        resultItem.p2 = 0.ToString();
                        resultItem.p3 = 0.ToString();
                        resultItem.p4 = 0.ToString();
                    }


                    result.Add(resultItem);
                }
                var count = TotalRow.Count;

                var actionResult = new actionResultAgingViewModel();
                actionResult.itemsAging = result.ToList();
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

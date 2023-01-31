using BinbalanceBusiness.Binbalance.ViewModels;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using static BinbalanceBusiness.Binbalance.ViewModels.MemoSearchViewModel;
using BinBalanceBusiness;
using BinBalanceDataAccess.Models;
using System.Data;

namespace BinbalanceBusiness.InventoryStock
{
    public class MemoService
    {
        #region BinbalanceDbContext
        private BinbalanceDbContext db;

        public MemoService()
        {
            db = new BinbalanceDbContext();
        }
        public MemoService(BinbalanceDbContext db)
        {
            this.db = db;
        }
        #endregion

        #region search
        public MemoSearchViewModel search(SearchMemoViewModel data)
        {
            try
            {
                var resultItem = new MemoSearchViewModel();
                var queryMemo = db.im_Memo.Where(c => c.Ref_Document_Index == data.Index).FirstOrDefault();
                if (queryMemo != null)
                {
                    resultItem.memo_Index = queryMemo.Memo_Index;
                    resultItem.memo_No = queryMemo.Memo_No;
                    resultItem.memo_Date = queryMemo.Memo_Date.toString();
                    resultItem.ref_Document_No = queryMemo.Ref_Document_No;
                    resultItem.documentType_Id = queryMemo.DocumentType_Id;
                    resultItem.documentType_Index = queryMemo.DocumentType_Index;
                    resultItem.documentType_Name = queryMemo.DocumentType_Name;
                    resultItem.Index = queryMemo.Ref_Document_Index;
                    resultItem.documentRef_No1 = queryMemo.DocumentRef_No1;
                    resultItem.documentRef_No2 = queryMemo.DocumentRef_No2;
                    resultItem.documentRef_No3 = queryMemo.DocumentRef_No3;
                    resultItem.documentRef_No4 = queryMemo.DocumentRef_No4;
                    resultItem.documentRef_No5 = queryMemo.DocumentRef_No5;
                    resultItem.remark = queryMemo.Document_Remark;
                    resultItem.owner_Name = queryMemo.Owner_Name;
                    resultItem.owner_Index = queryMemo.Owner_Index;
                    resultItem.owner_Id = queryMemo.Owner_Id;
                }
                else
                {
                    return resultItem;
                }
                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region searchitem
        public List<MemoItemSearchViewModel> searchitem(string id)
        {
            try
            {
                Guid MemoIndex = new Guid(id);
                var resultItem = new List<MemoItemSearchViewModel>();
                var queryMemo = db.im_MemoItem.Where(c => c.Memo_Index == MemoIndex && c.Document_Status == 0);

                if (queryMemo != null)
                {
                    foreach (var item in queryMemo) {
                        var result = new MemoItemSearchViewModel();
                        result.Memo_Item_Index = item.Memo_Item_Index.Value;
                        result.Memo_No = item.Memo_No;
                        result.Memo_Index = item.Memo_Index.Value;
                        result.ServiceCharge_Name = item.ServiceCharge_Name;
                        result.Qty = item.Qty;
                        result.UnitCharge = item.UnitCharge;
                        result.Weight = item.Weight;
                        result.Volume = item.Volume;
                        result.Rate = item.Rate;
                        result.Amount = item.Amount;
                        resultItem.Add(result);
                    }

                }
                else
                {
                    return resultItem;
                }
                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region CreateOrUpdate
        public string CreateOrUpdate(MemoSearchViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            Boolean IsNew = false;
            Boolean IsNewLine = false;
            String GoodsReceiveNo = "";
            var actionResult = new actionResultViewModel();
            var culture = new System.Globalization.CultureInfo("en-US");


            try
            {
                var itemDetail = new List<im_MemoItem>();
                var isHaveMemo = db.im_Memo.Find(data.memo_Index);
                im_Memo itemHeader = new im_Memo();
                #region Create

                if (isHaveMemo == null)
                {

                    itemHeader.Memo_Index = Guid.NewGuid();
                    itemHeader.Memo_No = "MEMO-"+data.ref_Document_No;
                    itemHeader.Memo_Date = data.memo_Date.toDate();
                    itemHeader.Owner_Index = data.owner_Index;
                    itemHeader.Owner_Id = data.owner_Id;
                    itemHeader.Owner_Name = data.owner_Name;
                    itemHeader.DocumentType_Index = data.documentType_Index;
                    itemHeader.DocumentType_Id = data.documentType_Id;
                    itemHeader.DocumentType_Name = data.documentType_Name;
                    itemHeader.DocumentRef_No1 = data.documentRef_No1;
                    itemHeader.DocumentRef_No2 = data.documentRef_No2;
                    itemHeader.DocumentRef_No3 = data.documentRef_No3;
                    itemHeader.DocumentRef_No4 = data.documentRef_No4;
                    itemHeader.DocumentRef_No5 = data.documentRef_No5;
                    itemHeader.Document_Remark = data.remark;
                    itemHeader.Document_Status = 0;
                    itemHeader.Ref_Document_No = data.ref_Document_No;
                    itemHeader.Ref_Process_Index = data.Process;
                    itemHeader.Ref_Document_Index = data.Index;
                    itemHeader.Total_Amount = data.Total_Amount;
                    itemHeader.Create_By = "User Admin";
                    itemHeader.Create_Date = DateTime.Now;
                    itemHeader.Update_By = null;
                    itemHeader.Update_Date = null;
                    itemHeader.Cancel_By = null;
                    itemHeader.Cancel_Date = null;
                    db.im_Memo.Add(itemHeader);

                    //----Set Detail-----

                    State = "itemDetail";

                    int addNumber = 0;
                    int refDocLineNum = 0;
                    if (data.listItemViewModels.Count > 0)
                    {
                        foreach (var item in data.listItemViewModels)
                        {
                            addNumber++;

                            im_MemoItem resultItem = new im_MemoItem();

                            // Gen Index for line item
                            resultItem.Memo_Item_Index = Guid.NewGuid();
                            resultItem.Memo_Index = itemHeader.Memo_Index;
                            resultItem.Memo_No = itemHeader.Memo_No;
                            resultItem.ServiceCharge_Index = item.ServiceCharge_Index;
                            resultItem.ServiceCharge_Id = item.ServiceCharge_Id;
                            resultItem.ServiceCharge_Name = item.ServiceCharge_Name;
                            resultItem.UnitCharge = item.UnitCharge;
                            resultItem.Rate = item.Rate;
                            resultItem.Minimumrate = item.Minimumrate;
                            resultItem.Qty = item.Qty;
                            resultItem.Weight = item.Weight;
                            resultItem.Volume = item.Volume;
                            resultItem.Pallet = item.Pallet;
                            resultItem.RT = item.RT;
                            resultItem.Document_Status = 0;
                            resultItem.Amount = item.Amount;
                            resultItem.Create_By = "User Admin";
                            resultItem.Create_Date = DateTime.Now;
                            resultItem.Update_By = null;
                            resultItem.Update_Date = null;
                            resultItem.Cancel_By = null;
                            resultItem.Cancel_Date = null;
                            db.im_MemoItem.Add(resultItem);

                            
                        }
                    }

                }

                #endregion

                #region Update
                else
                {

                    State = "itemHeader";

                    isHaveMemo.Memo_Index = data.memo_Index;
                    isHaveMemo.Memo_No = data.memo_No;
                    isHaveMemo.Memo_Date = data.memo_Date.toDate();
                    isHaveMemo.Owner_Index = data.owner_Index;
                    isHaveMemo.Owner_Id = data.owner_Id;
                    isHaveMemo.Owner_Name = data.owner_Name;
                    isHaveMemo.DocumentType_Index = data.documentType_Index;
                    isHaveMemo.DocumentType_Id = data.documentType_Id;
                    isHaveMemo.DocumentType_Name = data.documentType_Name;
                    isHaveMemo.DocumentRef_No1 = data.documentRef_No1;
                    isHaveMemo.DocumentRef_No2 = data.documentRef_No2;
                    isHaveMemo.DocumentRef_No3 = data.documentRef_No3;
                    isHaveMemo.DocumentRef_No4 = data.documentRef_No4;
                    isHaveMemo.DocumentRef_No5 = data.documentRef_No5;
                    isHaveMemo.Document_Remark = data.remark;
                    isHaveMemo.Document_Status = 0;
                    isHaveMemo.Ref_Document_No = data.ref_Document_No;
                    isHaveMemo.Ref_Process_Index = data.Process;
                    isHaveMemo.Ref_Document_Index = data.Index;
                    isHaveMemo.Total_Amount = data.Total_Amount;
                    isHaveMemo.Update_By = "User Admin";
                    isHaveMemo.Update_Date = DateTime.Now;
                    isHaveMemo.Cancel_By = null;
                    isHaveMemo.Cancel_Date = null;

                    foreach (var item in data.listItemViewModels)
                    {
                        var ItemOld = db.im_MemoItem.Find(item.Memo_Item_Index);

                        im_MemoItem resultItem = new im_MemoItem();

                        #region Update Line Item
                        //if (ItemOld != null)
                        //{
                        //    ItemOld.Memo_Item_Index = item.Memo_Item_Index;
                        //    ItemOld.Memo_Index = item.Memo_Index;
                        //    ItemOld.Memo_No = item.Memo_No;
                        //    ItemOld.ServiceCharge_Index = item.ServiceCharge_Index;
                        //    ItemOld.ServiceCharge_Id = item.ServiceCharge_Id;
                        //    ItemOld.ServiceCharge_Name = item.ServiceCharge_Name;
                        //    ItemOld.UnitCharge = item.UnitCharge;
                        //    ItemOld.Rate = item.Rate;
                        //    ItemOld.Qty = item.Qty;
                        //    ItemOld.Weight = item.Weight;
                        //    ItemOld.Volume = item.Volume;
                        //    ItemOld.Pallet = item.Pallet;
                        //    ItemOld.RT = item.RT;
                        //    resultItem.Document_Status = 0;
                        //    ItemOld.Amount = item.Amount;
                        //    ItemOld.Create_By = "User Admin";
                        //    ItemOld.Create_Date = DateTime.Now;
                        //    ItemOld.Update_By = null;
                        //    ItemOld.Update_Date = null;
                        //    ItemOld.Cancel_By = null;
                        //    ItemOld.Cancel_Date = null;
                        //    ItemOld.Update_By = "User Admin";
                        //    ItemOld.Update_Date = DateTime.Now;
                        //    ItemOld.Cancel_By = null;
                        //    ItemOld.Cancel_Date = null;

                        //}

                        #endregion

                        #region Create NewLine Item
                        if (ItemOld == null) {

                            State = "Update ms_ItemStatus";
                        
                            int refDocLineNum = 0;
                            int addNumber = 0;

                            addNumber++;
                            State = "Update resultItem";
                            
                            resultItem.Memo_Item_Index = Guid.NewGuid();
                            resultItem.Memo_Index = isHaveMemo.Memo_Index;
                            resultItem.Memo_No = isHaveMemo.Memo_No;
                            resultItem.ServiceCharge_Index = item.ServiceCharge_Index;
                            resultItem.ServiceCharge_Id = item.ServiceCharge_Id;
                            resultItem.ServiceCharge_Name = item.ServiceCharge_Name;
                            resultItem.UnitCharge = item.UnitCharge;
                            resultItem.Rate = item.Rate;
                            resultItem.Minimumrate = item.Minimumrate;
                            resultItem.Qty = item.Qty;
                            resultItem.Weight = item.Weight;
                            resultItem.Volume = item.Volume;
                            resultItem.Pallet = item.Pallet;
                            resultItem.RT = item.RT;
                            resultItem.Document_Status = 0;
                            resultItem.Amount = item.Amount;
                            resultItem.Create_By = "User Admin";
                            resultItem.Create_Date = DateTime.Now;
                            resultItem.Update_By = null;
                            resultItem.Update_Date = null;
                            resultItem.Cancel_By = null;
                            resultItem.Cancel_Date = null;

                            db.im_MemoItem.Add(resultItem);

                        }
                        #endregion
                    }

                    var deleteItem = db.im_MemoItem.Where(c => !data.listItemViewModels.Select(s => s.Memo_Item_Index).Contains(c.Memo_Item_Index)
                                    && c.Memo_Index == isHaveMemo.Memo_Index).ToList();

                    foreach (var c in deleteItem)   
                    {
                        var deleteGoodsReceiveItem = db.im_MemoItem.Find(c.Memo_Item_Index);

                        deleteGoodsReceiveItem.Document_Status = -1;
                        deleteGoodsReceiveItem.Update_By = "User Admin";
                        deleteGoodsReceiveItem.Update_Date = DateTime.Now;

                    }
                }
                #endregion

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    State = "SaveChanges";

                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " exy Rollback " + exy.Message.ToString();
                    transactionx.Rollback();

                    throw exy;

                }

                return "true";
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                throw ex;

            }
        }
        #endregion

        #region
        public actionResultViewModel searchView(SearchMemoViewModel data) {
            try {
                var query = db.im_Memo.AsQueryable();
                query = query.Where(c => c.Document_Status != -1);

                if (!string.IsNullOrEmpty(data.memo_Date_Default)) {
                    var date = data.memo_Date_Default.Split('-');
                    var dateS = date[0].Split('/');
                    var dateStart = dateS[2].Trim()+dateS[1].Trim() + dateS[0].Trim();
                    var dateStartuser = dateStart.toBetweenDate();
                    var dateE = date[1].Split('/');
                    var dateEnd = dateE[2].Trim()+ dateE[1].Trim() + dateE[0].Trim();
                    var dateEnduser = dateEnd.toBetweenDate();
                    query = query.Where(c => c.Memo_Date >= dateStartuser.start && c.Memo_Date <= dateEnduser.end);
                }
                if (!string.IsNullOrEmpty(data.Memo_No))
                {
                    query = query.Where(c => c.Memo_No.Contains(data.Memo_No));
                }
                if (!string.IsNullOrEmpty(data.Ref_Document_No))
                {
                    query = query.Where(c => c.Ref_Document_No.Contains(data.Ref_Document_No));
                }

                var Item = new List<im_Memo>();
                var TotalRow = new List<im_Memo>();
                TotalRow = query.ToList();
                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }

                var num = 1;
                if (data.PerPage == 100)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 100;
                    }
                }
                if (data.PerPage == 50)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 50;
                    }
                }
                int rowCount = num;

                Item = query.OrderBy(o => o.Create_Date).ToList();
                var result = new List<MemoSearchViewModel>();
                foreach (var List in Item) {
                    var resultItem = new MemoSearchViewModel();

                    resultItem.memo_Index = List.Memo_Index;
                    resultItem.memo_No = List.Memo_No;
                    resultItem.memo_Date = List.Memo_Date.toString();
                    resultItem.ref_Document_No = List.Ref_Document_No;
                    resultItem.documentType_Id = List.DocumentType_Id;
                    resultItem.documentType_Index = List.DocumentType_Index;
                    resultItem.documentType_Name = List.DocumentType_Name;
                    resultItem.Index = List.Ref_Document_Index;
                    resultItem.ref_Document_No = List.Ref_Document_No;
                    resultItem.documentRef_No1 = List.DocumentRef_No1;
                    resultItem.documentRef_No2 = List.DocumentRef_No2;
                    resultItem.documentRef_No3 = List.DocumentRef_No3;
                    resultItem.documentRef_No4 = List.DocumentRef_No4;
                    resultItem.documentRef_No5 = List.DocumentRef_No5;
                    resultItem.remark = List.Document_Remark;
                    resultItem.owner_Name = List.Owner_Name;
                    resultItem.owner_Index = List.Owner_Index;
                    resultItem.owner_Id = List.Owner_Id;
                    result.Add(resultItem);
                }
                var count = TotalRow.Count;

                var actionResultViewModel = new actionResultViewModel();
                actionResultViewModel.items = result.ToList();
                actionResultViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.Memo_No };
                return actionResultViewModel;

            } catch (Exception e) {
                throw e;
            }

           
        }

        #endregion
    }
}

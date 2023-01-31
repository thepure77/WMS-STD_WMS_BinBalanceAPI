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
using ProductViewModel = MasterDataBusiness.ViewModels.ProductViewModel;

namespace BinbalanceBusiness.PickBinbalance
{
    public class PickBinbalance
    {
        private BinbalanceDbContext db;

        public PickBinbalance()
        {
            db = new BinbalanceDbContext();
        }
        public PickBinbalance(BinbalanceDbContext db)
        {
            this.db = db;
        }
        #region autocomplete
        public List<ItemListViewModel> AutoCompleteProductId(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoSkufilter"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleteProductName(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoProductfilter"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleteGoodsReceive(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("GrFilter"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleteProductLot(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoProductfilter"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleteTag(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("TagFilter"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleterProdctLot(ItemListViewModel model)
        {
            try
            {
                var result = new List<ItemListViewModel>();
                var items = db.wm_BinBalance.Where(c => model.key != "-" ? model.key.Contains(c.Product_Lot) : !(c.Product_Lot == null || c.Product_Lot == string.Empty)).GroupBy(g => g.Product_Lot).ToList();
                foreach (var i in items)
                {
                    var r = new ItemListViewModel
                    {
                        name = i.Key,
                    };
                    result.Add(r);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Dropdown
        public List<WarehouseViewModel> dropdownWarehouse(WarehouseViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<WarehouseViewModel>>(new AppSettingConfig().GetUrl("dropdownWarehouse"), model.sJson());

                return result.OrderBy(o => o.warehouse_Id).ToList(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemStatusDocViewModel> dropdownItemStatus(ItemStatusDocViewModel data)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemStatusDocViewModel>>(new AppSettingConfig().GetUrl("dropdownItemStatus"), data.sJson());

                return result.OrderBy(o => o.itemStatus_Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ZoneViewModel> dropdownZone(ZoneViewModel data)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ZoneViewModel>>(new AppSettingConfig().GetUrl("dropdownZone"), data.sJson());

                return result.OrderBy(o => o.zone_Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LocationViewModel> dropdownLocation(LocationViewModel data)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("locationFilter"), data.sJson());

                return result.OrderBy(o => o.location_Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductTypeViewModel> dropdownProductType(ProductTypeViewModel data)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ProductTypeViewModel>>(new AppSettingConfig().GetUrl("productTypeFilter"), data.sJson());

                return result.OrderBy(o => o.productType_Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductConversionViewModelDoc> dropdownProductconversion(ProductConversionViewModelDoc data)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ProductConversionViewModelDoc>>(new AppSettingConfig().GetUrl("dropdownProductconversion"), data.sJson());

                return result.OrderBy(o => o.productConversion_Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> dropdownDocumentTypeMEMO(DocumentTypeViewModel data)
        {
            try
            {
                var result = new List<DocumentTypeViewModel>();

                var filterModel = new DocumentTypeViewModel();


                filterModel.process_Index = "6A877EA3-7FDD-43E8-A409-4B6BBE2BF199";


                //GetConfig
                result = utils.SendDataApi<List<DocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        



        #endregion

        public actionResultPickbinbalanceViewModel Filter(FilterPickbinbalanceViewModel model)
        {
            var resul = new actionResultPickbinbalanceViewModel();
            var items = new List<PickbinbalanceViewModel>();
            try
            {
                var query = db.wm_BinBalance.AsQueryable();

                var LocationIndex = utils.SendDataApi<string>(new AppSettingConfig().GetUrl("getConfigFromBase"), new { Config_Key = "Config_LocationType_Staging" }.sJson()).Split(',').Select(s => s == null || s == string.Empty ? (Guid?)null : Guid.Parse(s)).ToList();
                var location = utils.SendDataApi<List<locationViewModel>>(new AppSettingConfig().GetUrl("LocationConfig"), new { }.sJson()).Where(c => LocationIndex.Contains(c.locationType_Index)).ToList();

                query = query.Where(c => (c.BinBalance_QtyBal - c.BinBalance_QtyReserve) > 0);

                query = query.Where(c => !location.Select(s => s.location_Index).Contains(c.Location_Index));

                query = query.Where(c => c.Owner_Index == model.owner_Index);

                #region filter data

                #region filter tab 1
                if (!string.IsNullOrEmpty(model.dropdownProductType?.productType_Index.ToString()))
                {
                    object product_type = new { productType_Index = model.dropdownProductType.productType_Index };
                    var itemsproduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProductOfType"), product_type.sJson());
                    query = query.Where(c => itemsproduct.Select(s => s.product_Index).Contains(c.Product_Index));
                }

                if (!string.IsNullOrEmpty(model.product_Index))
                {
                    query = query.Where(c => model.product_Index.Contains(c.Product_Index.ToString()));
                }
                else if (!string.IsNullOrEmpty(model.product_Id))
                {
                    query = query.Where(c => model.product_Id.Contains(c.Product_Id));
                }
                else if (!string.IsNullOrEmpty(model.product_Name))
                {
                    query = query.Where(c => model.product_Name.Contains(c.Product_Name));
                }

                if (!string.IsNullOrEmpty(model.goodsReceive_No))
                {
                    query = query.Where(c => model.goodsReceive_No.Contains(c.GoodsReceive_No));
                }

                if (!string.IsNullOrEmpty(model.dropdownItemStatus?.itemStatus_Index.ToString()))
                {
                    query = query.Where(c => c.ItemStatus_Index == model.dropdownItemStatus.itemStatus_Index.sParse<Guid>());
                }

                if (!string.IsNullOrEmpty(model.product_Lot))
                {
                    //query = query.Where(c => model.product_Lot.Contains(c.Product_Lot));
                    query = query.Where(c => c.Product_Lot == model.product_Lot);
                }

                if (!string.IsNullOrEmpty(model.tag_Index))
                {
                    try
                    {
                        query = query.Where(c => model.tag_Index.Contains(c.Tag_Index.ToString()));
                    }
                    catch
                    {
                        query = query.Where(c => model.tag_No.Contains(c.Tag_No));
                    }
                }

                if (!string.IsNullOrEmpty(model.tag_No))
                {
                    query = query.Where(c => model.tag_No.Contains(c.Tag_No));
                }
                if (!string.IsNullOrEmpty(model.erp_Location))
                {
                    query = query.Where(c => model.erp_Location.Contains(c.ERP_Location));
                }
                #endregion

                #region filter tab 2
                if (!string.IsNullOrEmpty(model.goodsReceive_Date) && !string.IsNullOrEmpty(model.goodsReceive_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_Date >= model.goodsReceive_Date.toBetweenDate().start && c.GoodsReceive_Date <= model.goodsReceive_Date_To.toBetweenDate().end);
                }
                if (!string.IsNullOrEmpty(model.mfg_Date) && !string.IsNullOrEmpty(model.mfg_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_MFG_Date >= model.mfg_Date.toBetweenDate().start && c.GoodsReceive_MFG_Date <= model.mfg_Date_To.toBetweenDate().end);
                }
                if (!string.IsNullOrEmpty(model.exp_Date) && !string.IsNullOrEmpty(model.exp_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_EXP_Date >= model.exp_Date.toBetweenDate().start && c.GoodsReceive_EXP_Date <= model.exp_Date_To.toBetweenDate().end);
                }
                #endregion

                object warehouse_Index = new { };
                var warehouseLocation = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("locationFilter"), warehouse_Index.sJson());
                #region filter tab 3
                if (!string.IsNullOrEmpty(model.dropdownWarehouse?.warehouse_Index.ToString()))
                {
                    //object warehouse_Index = new { warehouse_Index = model.dropdownWarehouse?.warehouse_Index };
                    //var warehouse = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("locationFilter"), warehouse_Index.sJson());
                    var wahereWarehouse = warehouseLocation.Where(c => c.warehouse_Index == model.dropdownWarehouse?.warehouse_Index.ToString()).ToList();
                    query = query.Where(c => wahereWarehouse.Select(s => s.location_Index).Contains(c.Location_Index));
                }
                object zone_Index = new { };
                var zoneLocation = utils.SendDataApi<List<BinBalanceBusiness.ViewModels.ZoneLocationViewModel>>(new AppSettingConfig().GetUrl("zoneLocationFilter"), zone_Index.sJson());
                if (!string.IsNullOrEmpty(model.dropdownZone?.zone_Index.ToString()))
                {
                    //object zone_Index = new { zone_Index = model.dropdownZone?.zone_Index };
                    //var zone = utils.SendDataApi<List<BinBalanceBusiness.ViewModels.ZoneLocationViewModel>>(new AppSettingConfig().GetUrl("zoneLocationFilter"), zone_Index.sJson());
                    var whereZone = zoneLocation.Where(c => c.Zone_Index == model.dropdownZone?.zone_Index).ToList();
                    query = query.Where(c => whereZone.Select(s => s.Location_Index).Contains(c.Location_Index));
                }
                if (!string.IsNullOrEmpty(model.location_Index.ToString()))
                {
                    query = query.Where(c => c.Location_Index == model.location_Index);
                }

                #endregion

                #endregion


                int count = query.Count();

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);
                }

                var dataList = query.OrderByDescending(o => o.Create_Date).ThenByDescending(o => o.Create_Date).ToList();

                var warehouse = utils.SendDataApi<List<WarehouseViewModel>>(new AppSettingConfig().GetUrl("dropdownWarehouse"), model.sJson());
                var zone = utils.SendDataApi<List<ZoneViewModel>>(new AppSettingConfig().GetUrl("dropdownZone"), new { }.sJson());
                var productconversion = utils.SendDataApi<List<ProductConversionViewModel>>(new AppSettingConfig().GetUrl("dropdownProductConversionV2"), new { }.sJson());

                foreach (var d in dataList)
                {
                    var saleUnit = productconversion.Where(c => c.product_Index == d.Product_Index && c.sale_UNIT == 1 ).Select(c => c.productConversion_Name);
                   
                    var item = new PickbinbalanceViewModel
                    {
                        
                        binbalance_Index = d?.BinBalance_Index.ToString(),
                        goodsReceive_Index = d?.GoodsReceive_Index.ToString(),
                        goodsReceive_No = d?.GoodsReceive_No,
                        tag_Index = d?.Tag_Index.ToString(),
                        tag_No = d?.Tag_No,
                        product_Index = d?.Product_Index.ToString(),
                        product_Lot = d?.Product_Lot,
                        //documentType_Index = d.documentType_Index,
                        //documentType_Id    = d.documentType_Id   ,
                        //documentType_Name  = d.documentType_Name ,
                        product_Id = d?.Product_Id,
                        product_Name = d?.Product_Name,
                        qty = d?.BinBalance_QtyBal - d?.BinBalance_QtyReserve,
                        weight = d?.BinBalance_WeightBal - d?.BinBalance_WeightReserve,

                        binBalance_UnitWeightBal = d?.BinBalance_UnitWeightBal,
                        binBalance_UnitWeightBal_Index = d?.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = d?.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = d?.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = d?.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitNetWeightBal = d?.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = d?.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = d?.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = d?.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = d?.BinBalance_UnitNetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = d?.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = d?.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = d?.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = d?.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = d?.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_UnitWidthBal = d?.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = d?.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = d?.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = d?.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = d?.BinBalance_UnitWidthBalRatio,
                        binBalance_UnitLengthBal = d?.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = d?.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = d?.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = d?.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = d?.BinBalance_UnitLengthBalRatio,
                        binBalance_UnitHeightBal = d?.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = d?.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = d?.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = d?.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = d?.BinBalance_UnitHeightBalRatio,

                        productConversion_Index = d?.ProductConversion_Index.ToString(),
                        productConversion_Id = d?.ProductConversion_Id,
                        productConversion_Name = d?.ProductConversion_Name,
                        productConversion_Ratio = d?.BinBalance_Ratio,
                        status_Index = d?.ItemStatus_Index.ToString(),
                        status_Id = d?.ItemStatus_Id,
                        status_Name = d?.ItemStatus_Name,
                        location_Index = d?.Location_Index.ToString(),
                        location_Id = d?.Location_Id,
                        location_Name = d?.Location_Name,
                        goodsReceive_Date = d?.GoodsReceive_Date != null ? d?.GoodsReceive_Date.toString() : "",
                        goodsReceive_MFG_Date = d?.GoodsReceive_MFG_Date != null ? d?.GoodsReceive_MFG_Date.toString() : "",
                        goodsReceive_EXP_Date = d?.GoodsReceive_EXP_Date != null ? d?.GoodsReceive_EXP_Date.toString() : "",
                        //warehouse_Index = warehouse.FirstOrDefault(f => f.warehouse_Index == new Guid(warehouseLocation.FirstOrDefault(wf => wf.location_Index == d?.Location_Index)?.warehouse_Index))?.warehouse_Index.ToString(),
                        //warehouse_Id = warehouse.FirstOrDefault(f => f.warehouse_Index == new Guid(warehouseLocation.FirstOrDefault(wf => wf.location_Index == d?.Location_Index)?.warehouse_Index))?.warehouse_Id,
                        //warehouse_Name = warehouse.FirstOrDefault(f => f.warehouse_Index == new Guid(warehouseLocation.FirstOrDefault(wf => wf.location_Index == d?.Location_Index)?.warehouse_Index))?.warehouse_Name,
                        zone_Index = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Index.ToString(),
                        zone_Id = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Id,
                        zone_Name = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Name,
                        sale_unit = (saleUnit.FirstOrDefault() == null ) ? null : saleUnit.FirstOrDefault(),
                        Erp_location = d.ERP_Location,
                    };
                    items.Add(item);
                }

                resul.items = items;
                resul.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };

                return resul;
            }
            catch (Exception ex)
            {
                resul.resultMsg = ex.Message;
                return resul;
            }
        }

        #region Unpack fillter.

        public actionResultPickbinbalanceViewModel UnpackFilter(FilterPickbinbalanceViewModel model)
        {
            var resul = new actionResultPickbinbalanceViewModel();
            var items = new List<PickbinbalanceViewModel>();
            try
            {
                var query = db.wm_BinBalance.AsQueryable();

                List <Guid> LocationIndex = new List<Guid>
                {
                Guid.Parse("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
                Guid.Parse("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
                Guid.Parse("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
                };
                var location = utils.SendDataApi<List<locationViewModel>>(new AppSettingConfig().GetUrl("LocationConfig"), new { }.sJson()).Where(c => LocationIndex.Contains(c.locationType_Index.GetValueOrDefault())).ToList();

                query = query.Where(c => (c.BinBalance_QtyBal - c.BinBalance_QtyReserve) > 0);

                query = query.Where(c => location.Select(s => s.location_Index).Contains(c.Location_Index));

                query = query.Where(c => c.Owner_Index == model.owner_Index);

                #region filter data

                #region filter tab 1
                if (!string.IsNullOrEmpty(model.dropdownProductType?.productType_Index.ToString()))
                {
                    object product_type = new { productType_Index = model.dropdownProductType.productType_Index };
                    var itemsproduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProductOfType"), product_type.sJson());
                    query = query.Where(c => itemsproduct.Select(s => s.product_Index).Contains(c.Product_Index));
                }

                if (!string.IsNullOrEmpty(model.product_Index))
                {
                    query = query.Where(c => model.product_Index.Contains(c.Product_Index.ToString()));
                }
                else if (!string.IsNullOrEmpty(model.product_Id))
                {
                    query = query.Where(c => model.product_Id.Contains(c.Product_Id));
                }
                else if (!string.IsNullOrEmpty(model.product_Name))
                {
                    query = query.Where(c => model.product_Name.Contains(c.Product_Name));
                }

                if (!string.IsNullOrEmpty(model.goodsReceive_No))
                {
                    query = query.Where(c => model.goodsReceive_No.Contains(c.GoodsReceive_No));
                }

                if (!string.IsNullOrEmpty(model.dropdownItemStatus?.itemStatus_Index.ToString()))
                {
                    query = query.Where(c => c.ItemStatus_Index == model.dropdownItemStatus.itemStatus_Index.sParse<Guid>());
                }

                if (!string.IsNullOrEmpty(model.product_Lot))
                {
                    query = query.Where(c => c.Product_Lot == model.product_Lot);
                }

                if (!string.IsNullOrEmpty(model.tag_Index))
                {
                    try
                    {
                        query = query.Where(c => model.tag_Index.Contains(c.Tag_Index.ToString()));
                    }
                    catch
                    {
                        query = query.Where(c => model.tag_No.Contains(c.Tag_No));
                    }
                }

                if (!string.IsNullOrEmpty(model.tag_No))
                {
                    query = query.Where(c => model.tag_No.Contains(c.Tag_No));
                }
               
                    query = query.Where(c => c.ERP_Location == "AB01");
                
                #endregion

                #region filter tab 2
                if (!string.IsNullOrEmpty(model.goodsReceive_Date) && !string.IsNullOrEmpty(model.goodsReceive_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_Date >= model.goodsReceive_Date.toBetweenDate().start && c.GoodsReceive_Date <= model.goodsReceive_Date_To.toBetweenDate().end);
                }
                if (!string.IsNullOrEmpty(model.mfg_Date) && !string.IsNullOrEmpty(model.mfg_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_MFG_Date >= model.mfg_Date.toBetweenDate().start && c.GoodsReceive_MFG_Date <= model.mfg_Date_To.toBetweenDate().end);
                }
                if (!string.IsNullOrEmpty(model.exp_Date) && !string.IsNullOrEmpty(model.exp_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_EXP_Date >= model.exp_Date.toBetweenDate().start && c.GoodsReceive_EXP_Date <= model.exp_Date_To.toBetweenDate().end);
                }
                #endregion

                object warehouse_Index = new { };
                var warehouseLocation = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("locationFilter"), warehouse_Index.sJson());
                #region filter tab 3
                if (!string.IsNullOrEmpty(model.dropdownWarehouse?.warehouse_Index.ToString()))
                {
                    var wahereWarehouse = warehouseLocation.Where(c => c.warehouse_Index == model.dropdownWarehouse?.warehouse_Index.ToString()).ToList();
                    query = query.Where(c => wahereWarehouse.Select(s => s.location_Index).Contains(c.Location_Index));
                }
                object zone_Index = new { };
                var zoneLocation = utils.SendDataApi<List<BinBalanceBusiness.ViewModels.ZoneLocationViewModel>>(new AppSettingConfig().GetUrl("zoneLocationFilter"), zone_Index.sJson());
                if (!string.IsNullOrEmpty(model.dropdownZone?.zone_Index.ToString()))
                {
                    var whereZone = zoneLocation.Where(c => c.Zone_Index == model.dropdownZone?.zone_Index).ToList();
                    query = query.Where(c => whereZone.Select(s => s.Location_Index).Contains(c.Location_Index));
                }
                if (!string.IsNullOrEmpty(model.location_Index.ToString()))
                {
                    query = query.Where(c => c.Location_Index == model.location_Index);
                }

                #endregion

                #endregion


                int count = query.Count();

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);
                }

                var dataList = query.OrderByDescending(o => o.Create_Date).ThenByDescending(o => o.Create_Date).ToList();

                var warehouse = utils.SendDataApi<List<WarehouseViewModel>>(new AppSettingConfig().GetUrl("dropdownWarehouse"), model.sJson());
                var zone = utils.SendDataApi<List<ZoneViewModel>>(new AppSettingConfig().GetUrl("dropdownZone"), new { }.sJson());
                var productconversion = utils.SendDataApi<List<ProductConversionViewModel>>(new AppSettingConfig().GetUrl("dropdownProductConversionV2"), new { }.sJson());

                foreach (var d in dataList)
                {
                    var saleUnit = productconversion.Where(c => c.product_Index == d.Product_Index && c.sale_UNIT == 1).Select(c => c.productConversion_Name);

                    var item = new PickbinbalanceViewModel
                    {

                        binbalance_Index = d?.BinBalance_Index.ToString(),
                        goodsReceive_Index = d?.GoodsReceive_Index.ToString(),
                        goodsReceive_No = d?.GoodsReceive_No,
                        tag_Index = d?.Tag_Index.ToString(),
                        tag_No = d?.Tag_No,
                        product_Index = d?.Product_Index.ToString(),
                        product_Lot = d?.Product_Lot,
                        product_Id = d?.Product_Id,
                        product_Name = d?.Product_Name,
                        qty = d?.BinBalance_QtyBal - d?.BinBalance_QtyReserve,
                        weight = d?.BinBalance_WeightBal - d?.BinBalance_WeightReserve,

                        binBalance_UnitWeightBal = d?.BinBalance_UnitWeightBal,
                        binBalance_UnitWeightBal_Index = d?.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = d?.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = d?.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = d?.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitNetWeightBal = d?.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = d?.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = d?.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = d?.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = d?.BinBalance_UnitNetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = d?.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = d?.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = d?.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = d?.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = d?.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_UnitWidthBal = d?.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = d?.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = d?.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = d?.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = d?.BinBalance_UnitWidthBalRatio,
                        binBalance_UnitLengthBal = d?.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = d?.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = d?.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = d?.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = d?.BinBalance_UnitLengthBalRatio,
                        binBalance_UnitHeightBal = d?.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = d?.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = d?.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = d?.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = d?.BinBalance_UnitHeightBalRatio,

                        productConversion_Index = d?.ProductConversion_Index.ToString(),
                        productConversion_Id = d?.ProductConversion_Id,
                        productConversion_Name = d?.ProductConversion_Name,
                        productConversion_Ratio = d?.BinBalance_Ratio,
                        status_Index = d?.ItemStatus_Index.ToString(),
                        status_Id = d?.ItemStatus_Id,
                        status_Name = d?.ItemStatus_Name,
                        location_Index = d?.Location_Index.ToString(),
                        location_Id = d?.Location_Id,
                        location_Name = d?.Location_Name,
                        goodsReceive_Date = d?.GoodsReceive_Date != null ? d?.GoodsReceive_Date.toString() : "",
                        goodsReceive_MFG_Date = d?.GoodsReceive_MFG_Date != null ? d?.GoodsReceive_MFG_Date.toString() : "",
                        goodsReceive_EXP_Date = d?.GoodsReceive_EXP_Date != null ? d?.GoodsReceive_EXP_Date.toString() : "",
                        zone_Index = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Index.ToString(),
                        zone_Id = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Id,
                        zone_Name = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Name,
                        sale_unit = (saleUnit.FirstOrDefault() == null) ? null : saleUnit.FirstOrDefault(),
                        Erp_location = d.ERP_Location,
                    };
                    items.Add(item);
                }

                resul.items = items;
                resul.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };

                return resul;
            }
            catch (Exception ex)
            {
                resul.resultMsg = ex.Message;
                return resul;
            }
        }

        #endregion

        public actionResultPickbinbalanceViewModel filterbinbalance_unpack(PickbinbalanceViewModel model)
        {
            var resul = new actionResultPickbinbalanceViewModel();
            var items = new List<PickbinbalanceViewModel>();

            try
            {
                var query = db.wm_BinBalance.Where(c => c.BinBalance_Index == Guid.Parse(model.binbalance_Index));
                
                int count = query.Count();
                

                var dataList = query.OrderByDescending(o => o.Create_Date).ThenByDescending(o => o.Create_Date).ToList();
                foreach (var d in dataList)
                {
                    var item = new PickbinbalanceViewModel
                    {
                        binbalance_Index = d?.BinBalance_Index.ToString(),
                        goodsReceive_Index = d?.GoodsReceive_Index.ToString(),
                        goodsReceive_No = d?.GoodsReceive_No,
                        tag_Index = d?.Tag_Index.ToString(),
                        tag_No = d?.Tag_No,
                        product_Index = d?.Product_Index.ToString(),
                        product_Lot = d?.Product_Lot,
                        product_Id = d?.Product_Id,
                        product_Name = d?.Product_Name,
                        qty = d?.BinBalance_QtyBal - d?.BinBalance_QtyReserve,
                        weight = d?.BinBalance_WeightBal - d?.BinBalance_WeightReserve,

                        binBalance_UnitWeightBal = d?.BinBalance_UnitWeightBal,
                        binBalance_UnitWeightBal_Index = d?.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = d?.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = d?.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = d?.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitNetWeightBal = d?.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = d?.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = d?.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = d?.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = d?.BinBalance_UnitNetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = d?.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = d?.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = d?.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = d?.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = d?.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_UnitWidthBal = d?.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = d?.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = d?.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = d?.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = d?.BinBalance_UnitWidthBalRatio,
                        binBalance_UnitLengthBal = d?.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = d?.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = d?.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = d?.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = d?.BinBalance_UnitLengthBalRatio,
                        binBalance_UnitHeightBal = d?.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = d?.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = d?.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = d?.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = d?.BinBalance_UnitHeightBalRatio,

                        productConversion_Index = d?.ProductConversion_Index.ToString(),
                        productConversion_Id = d?.ProductConversion_Id,
                        productConversion_Name = d?.ProductConversion_Name,
                        productConversion_Ratio = d?.BinBalance_Ratio,
                        status_Index = d?.ItemStatus_Index.ToString(),
                        status_Id = d?.ItemStatus_Id,
                        status_Name = d?.ItemStatus_Name,
                        location_Index = d?.Location_Index.ToString(),
                        location_Id = d?.Location_Id,
                        location_Name = d?.Location_Name,
                        goodsReceive_Date = d?.GoodsReceive_Date != null ? d?.GoodsReceive_Date.toString() : "",
                        goodsReceive_MFG_Date = d?.GoodsReceive_MFG_Date != null ? d?.GoodsReceive_MFG_Date.toString() : "",
                        goodsReceive_EXP_Date = d?.GoodsReceive_EXP_Date != null ? d?.GoodsReceive_EXP_Date.toString() : "",
                    };
                    items.Add(item);
                }

                resul.items = items;
                resul.pagination = new Pagination() { TotalRow = count, CurrentPage = 0, PerPage = 1, };

                return resul;
            }
            catch (Exception ex)
            {
                resul.resultMsg = ex.Message;
                return resul;
            }
        }

        public actionResultPickbinbalanceViewModel filterbinbalance_pack(PickModel model)
        {
            var resul = new actionResultPickbinbalanceViewModel();
            var items = new List<PickbinbalanceViewModel>();

            try
            {
                var BInbalance_index = model.items.Select(c => c.binbalance_Index).ToList();
                var query = db.wm_BinBalance.Where(c => BInbalance_index.Contains(c.BinBalance_Index.ToString()));

                int count = query.Count();


                var dataList = query.OrderByDescending(o => o.Create_Date).ThenByDescending(o => o.Create_Date).ToList();
                foreach (var d in dataList)
                {
                    var item = new PickbinbalanceViewModel
                    {
                        binbalance_Index = d?.BinBalance_Index.ToString(),
                        goodsReceive_Index = d?.GoodsReceive_Index.ToString(),
                        goodsReceive_No = d?.GoodsReceive_No,
                        tag_Index = d?.Tag_Index.ToString(),
                        tag_No = d?.Tag_No,
                        product_Index = d?.Product_Index.ToString(),
                        product_Lot = d?.Product_Lot,
                        product_Id = d?.Product_Id,
                        product_Name = d?.Product_Name,
                        qty = d?.BinBalance_QtyBal - d?.BinBalance_QtyReserve,
                        weight = d?.BinBalance_WeightBal - d?.BinBalance_WeightReserve,

                        binBalance_UnitWeightBal = d?.BinBalance_UnitWeightBal,
                        binBalance_UnitWeightBal_Index = d?.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = d?.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = d?.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = d?.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitNetWeightBal = d?.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = d?.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = d?.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = d?.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = d?.BinBalance_UnitNetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = d?.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = d?.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = d?.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = d?.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = d?.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_UnitWidthBal = d?.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = d?.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = d?.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = d?.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = d?.BinBalance_UnitWidthBalRatio,
                        binBalance_UnitLengthBal = d?.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = d?.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = d?.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = d?.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = d?.BinBalance_UnitLengthBalRatio,
                        binBalance_UnitHeightBal = d?.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = d?.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = d?.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = d?.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = d?.BinBalance_UnitHeightBalRatio,

                        productConversion_Index = d?.ProductConversion_Index.ToString(),
                        productConversion_Id = d?.ProductConversion_Id,
                        productConversion_Name = d?.ProductConversion_Name,
                        productConversion_Ratio = d?.BinBalance_Ratio,
                        status_Index = d?.ItemStatus_Index.ToString(),
                        status_Id = d?.ItemStatus_Id,
                        status_Name = d?.ItemStatus_Name,
                        location_Index = d?.Location_Index.ToString(),
                        location_Id = d?.Location_Id,
                        location_Name = d?.Location_Name,
                        goodsReceive_Date = d?.GoodsReceive_Date != null ? d?.GoodsReceive_Date.toString() : "",
                        goodsReceive_MFG_Date = d?.GoodsReceive_MFG_Date != null ? d?.GoodsReceive_MFG_Date.toString() : "",
                        goodsReceive_EXP_Date = d?.GoodsReceive_EXP_Date != null ? d?.GoodsReceive_EXP_Date.toString() : "",
                    };
                    items.Add(item);
                }

                resul.items = items;
                resul.pagination = new Pagination() { TotalRow = count, CurrentPage = 0, PerPage = 1, };

                return resul;
            }
            catch (Exception ex)
            {
                resul.resultMsg = ex.Message;
                return resul;
            }
        }

        public actionResultPickbinbalanceViewModel filterAB03(FilterPickbinbalanceViewModel model)
        {
            var resul = new actionResultPickbinbalanceViewModel();
            var items = new List<PickbinbalanceViewModel>();
            var ount = 1;

            try
            {
                var query = db.wm_BinBalance.Where(c => c.ERP_Location == "AB03");
                //var query = db.wm_BinBalance.AsQueryable();

                var LocationIndex = utils.SendDataApi<string>(new AppSettingConfig().GetUrl("getConfigFromBase"), new { Config_Key = "Config_LocationType_Staging" }.sJson()).Split(',').Select(s => s == null || s == string.Empty ? (Guid?)null : Guid.Parse(s)).ToList();
                var location = utils.SendDataApi<List<locationViewModel>>(new AppSettingConfig().GetUrl("LocationConfig"), new { }.sJson()).Where(c => LocationIndex.Contains(c.locationType_Index)).ToList();

                query = query.Where(c => (c.BinBalance_QtyBal - c.BinBalance_QtyReserve) > 0);

                query = query.Where(c => !location.Select(s => s.location_Index).Contains(c.Location_Index));

                query = query.Where(c => c.Owner_Index == model.owner_Index);

                #region filter data

                #region filter tab 1
                if (!string.IsNullOrEmpty(model.dropdownProductType?.productType_Index.ToString()))
                {
                    object product_type = new { productType_Index = model.dropdownProductType.productType_Index };
                    var itemsproduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProductOfType"), product_type.sJson());
                    query = query.Where(c => itemsproduct.Select(s => s.product_Index).Contains(c.Product_Index));
                }

                if (!string.IsNullOrEmpty(model.product_Index))
                {
                    query = query.Where(c => model.product_Index.Contains(c.Product_Index.ToString()));
                }
                else if (!string.IsNullOrEmpty(model.product_Id))
                {
                    query = query.Where(c => model.product_Id.Contains(c.Product_Id));
                }
                else if (!string.IsNullOrEmpty(model.product_Name))
                {
                    query = query.Where(c => model.product_Name.Contains(c.Product_Name));
                }

                if (!string.IsNullOrEmpty(model.goodsReceive_No))
                {
                    query = query.Where(c => model.goodsReceive_No.Contains(c.GoodsReceive_No));
                }

                if (!string.IsNullOrEmpty(model.dropdownItemStatus?.itemStatus_Index.ToString()))
                {
                    query = query.Where(c => c.ItemStatus_Index == model.dropdownItemStatus.itemStatus_Index.sParse<Guid>());
                }

                if (!string.IsNullOrEmpty(model.product_Lot))
                {
                    //query = query.Where(c => model.product_Lot.Contains(c.Product_Lot));
                    query = query.Where(c => c.Product_Lot == model.product_Lot);
                }

                if (!string.IsNullOrEmpty(model.tag_Index))
                {
                    try
                    {
                        query = query.Where(c => model.tag_Index.Contains(c.Tag_Index.ToString()));
                    }
                    catch
                    {
                        query = query.Where(c => model.tag_No.Contains(c.Tag_No));
                    }
                }

                if (!string.IsNullOrEmpty(model.tag_No))
                {
                    query = query.Where(c => model.tag_No.Contains(c.Tag_No));
                }
                if (!string.IsNullOrEmpty(model.erp_Location))
                {
                    query = query.Where(c => model.erp_Location.Contains(c.ERP_Location));
                }
                #endregion

                #region filter tab 2
                if (!string.IsNullOrEmpty(model.goodsReceive_Date) && !string.IsNullOrEmpty(model.goodsReceive_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_Date >= model.goodsReceive_Date.toBetweenDate().start && c.GoodsReceive_Date <= model.goodsReceive_Date_To.toBetweenDate().end);
                }
                if (!string.IsNullOrEmpty(model.mfg_Date) && !string.IsNullOrEmpty(model.mfg_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_MFG_Date >= model.mfg_Date.toBetweenDate().start && c.GoodsReceive_MFG_Date <= model.mfg_Date_To.toBetweenDate().end);
                }
                if (!string.IsNullOrEmpty(model.exp_Date) && !string.IsNullOrEmpty(model.exp_Date_To))
                {
                    query = query.Where(c => c.GoodsReceive_EXP_Date >= model.exp_Date.toBetweenDate().start && c.GoodsReceive_EXP_Date <= model.exp_Date_To.toBetweenDate().end);
                }
                #endregion

                object warehouse_Index = new { };
                var warehouseLocation = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("locationFilter"), warehouse_Index.sJson());
                #region filter tab 3
                if (!string.IsNullOrEmpty(model.dropdownWarehouse?.warehouse_Index.ToString()))
                {
                    //object warehouse_Index = new { warehouse_Index = model.dropdownWarehouse?.warehouse_Index };
                    //var warehouse = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("locationFilter"), warehouse_Index.sJson());
                    var wahereWarehouse = warehouseLocation.Where(c => c.warehouse_Index == model.dropdownWarehouse?.warehouse_Index.ToString()).ToList();
                    query = query.Where(c => wahereWarehouse.Select(s => s.location_Index).Contains(c.Location_Index));
                }
                object zone_Index = new { };
                var zoneLocation = utils.SendDataApi<List<BinBalanceBusiness.ViewModels.ZoneLocationViewModel>>(new AppSettingConfig().GetUrl("zoneLocationFilter"), zone_Index.sJson());
                if (!string.IsNullOrEmpty(model.dropdownZone?.zone_Index.ToString()))
                {
                    //object zone_Index = new { zone_Index = model.dropdownZone?.zone_Index };
                    //var zone = utils.SendDataApi<List<BinBalanceBusiness.ViewModels.ZoneLocationViewModel>>(new AppSettingConfig().GetUrl("zoneLocationFilter"), zone_Index.sJson());
                    var whereZone = zoneLocation.Where(c => c.Zone_Index == model.dropdownZone?.zone_Index).ToList();
                    query = query.Where(c => whereZone.Select(s => s.Location_Index).Contains(c.Location_Index));
                }
                if (!string.IsNullOrEmpty(model.location_Index.ToString()))
                {
                    query = query.Where(c => c.Location_Index == model.location_Index);
                }

                #endregion

                #endregion


                int count = query.Count();

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);
                }

                var dataList = query.OrderByDescending(o => o.Create_Date).ThenByDescending(o => o.Create_Date).ToList();

                var warehouse = utils.SendDataApi<List<WarehouseViewModel>>(new AppSettingConfig().GetUrl("dropdownWarehouse"), model.sJson());
                var zone = utils.SendDataApi<List<ZoneViewModel>>(new AppSettingConfig().GetUrl("dropdownZone"), new { }.sJson());
                foreach (var d in dataList)
                {
                    var item = new PickbinbalanceViewModel
                    {
                        binbalance_Index = d?.BinBalance_Index.ToString(),
                        goodsReceive_Index = d?.GoodsReceive_Index.ToString(),
                        goodsReceive_No = d?.GoodsReceive_No,
                        tag_Index = d?.Tag_Index.ToString(),
                        tag_No = d?.Tag_No,
                        product_Index = d?.Product_Index.ToString(),
                        product_Lot = d?.Product_Lot,
                        //documentType_Index = d.documentType_Index,
                        //documentType_Id    = d.documentType_Id   ,
                        //documentType_Name  = d.documentType_Name ,
                        product_Id = d?.Product_Id,
                        product_Name = d?.Product_Name,
                        qty = d?.BinBalance_QtyBal - d?.BinBalance_QtyReserve,
                        weight = d?.BinBalance_WeightBal - d?.BinBalance_WeightReserve,

                        binBalance_UnitWeightBal = d?.BinBalance_UnitWeightBal,
                        binBalance_UnitWeightBal_Index = d?.BinBalance_UnitWeightBal_Index,
                        binBalance_UnitWeightBal_Id = d?.BinBalance_UnitWeightBal_Id,
                        binBalance_UnitWeightBal_Name = d?.BinBalance_UnitWeightBal_Name,
                        binBalance_UnitWeightBalRatio = d?.BinBalance_UnitWeightBalRatio,
                        binBalance_UnitNetWeightBal = d?.BinBalance_UnitNetWeightBal,
                        binBalance_UnitNetWeightBal_Index = d?.BinBalance_UnitNetWeightBal_Index,
                        binBalance_UnitNetWeightBal_Id = d?.BinBalance_UnitNetWeightBal_Id,
                        binBalance_UnitNetWeightBal_Name = d?.BinBalance_UnitNetWeightBal_Name,
                        binBalance_UnitNetWeightBalRatio = d?.BinBalance_UnitNetWeightBalRatio,
                        binBalance_UnitGrsWeightBal = d?.BinBalance_UnitGrsWeightBal,
                        binBalance_UnitGrsWeightBal_Index = d?.BinBalance_UnitGrsWeightBal_Index,
                        binBalance_UnitGrsWeightBal_Id = d?.BinBalance_UnitGrsWeightBal_Id,
                        binBalance_UnitGrsWeightBal_Name = d?.BinBalance_UnitGrsWeightBal_Name,
                        binBalance_UnitGrsWeightBalRatio = d?.BinBalance_UnitGrsWeightBalRatio,
                        binBalance_UnitWidthBal = d?.BinBalance_UnitWidthBal,
                        binBalance_UnitWidthBal_Index = d?.BinBalance_UnitWidthBal_Index,
                        binBalance_UnitWidthBal_Id = d?.BinBalance_UnitWidthBal_Id,
                        binBalance_UnitWidthBal_Name = d?.BinBalance_UnitWidthBal_Name,
                        binBalance_UnitWidthBalRatio = d?.BinBalance_UnitWidthBalRatio,
                        binBalance_UnitLengthBal = d?.BinBalance_UnitLengthBal,
                        binBalance_UnitLengthBal_Index = d?.BinBalance_UnitLengthBal_Index,
                        binBalance_UnitLengthBal_Id = d?.BinBalance_UnitLengthBal_Id,
                        binBalance_UnitLengthBal_Name = d?.BinBalance_UnitLengthBal_Name,
                        binBalance_UnitLengthBalRatio = d?.BinBalance_UnitLengthBalRatio,
                        binBalance_UnitHeightBal = d?.BinBalance_UnitHeightBal,
                        binBalance_UnitHeightBal_Index = d?.BinBalance_UnitHeightBal_Index,
                        binBalance_UnitHeightBal_Id = d?.BinBalance_UnitHeightBal_Id,
                        binBalance_UnitHeightBal_Name = d?.BinBalance_UnitHeightBal_Name,
                        binBalance_UnitHeightBalRatio = d?.BinBalance_UnitHeightBalRatio,

                        productConversion_Index = d?.ProductConversion_Index.ToString(),
                        productConversion_Id = d?.ProductConversion_Id,
                        productConversion_Name = d?.ProductConversion_Name,
                        productConversion_Ratio = d?.BinBalance_Ratio,
                        status_Index = d?.ItemStatus_Index.ToString(),
                        status_Id = d?.ItemStatus_Id,
                        status_Name = d?.ItemStatus_Name,
                        location_Index = d?.Location_Index.ToString(),
                        location_Id = d?.Location_Id,
                        location_Name = d?.Location_Name,
                        goodsReceive_Date = d?.GoodsReceive_Date != null ? d?.GoodsReceive_Date.toString() : "",
                        goodsReceive_MFG_Date = d?.GoodsReceive_MFG_Date != null ? d?.GoodsReceive_MFG_Date.toString() : "",
                        goodsReceive_EXP_Date = d?.GoodsReceive_EXP_Date != null ? d?.GoodsReceive_EXP_Date.toString() : "",
                        //warehouse_Index = warehouse.FirstOrDefault(f => f.warehouse_Index == new Guid(warehouseLocation.FirstOrDefault(wf => wf.location_Index == d?.Location_Index)?.warehouse_Index))?.warehouse_Index.ToString(),
                        //warehouse_Id = warehouse.FirstOrDefault(f => f.warehouse_Index == new Guid(warehouseLocation.FirstOrDefault(wf => wf.location_Index == d?.Location_Index)?.warehouse_Index))?.warehouse_Id,
                        //warehouse_Name = warehouse.FirstOrDefault(f => f.warehouse_Index == new Guid(warehouseLocation.FirstOrDefault(wf => wf.location_Index == d?.Location_Index)?.warehouse_Index))?.warehouse_Name,
                        zone_Index = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Index.ToString(),
                        zone_Id = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Id,
                        zone_Name = zone?.FirstOrDefault(f => f.zone_Index == zoneLocation?.FirstOrDefault(zf => zf.Location_Index == d?.Location_Index)?.Zone_Index)?.zone_Name,
                    };
                    items.Add(item);
                }

                resul.items = items;
                resul.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };

                return resul;
            }
            catch (Exception ex)
            {
                resul.resultMsg = ex.Message;
                return resul;
            }
        }

        public actionResultPickbinbalanceFromGIViewModel pickProduct_tranfer(PickbinbalanceFromGIViewModel model)
        {
            String State = "Start " + model.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var result = new actionResultPickbinbalanceFromGIViewModel();
                var BinCardReserve_Index = Guid.NewGuid();
                //var BinCard_Index = Guid.NewGuid();
                var itemBin = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));
                if ((itemBin.BinBalance_QtyBal - itemBin.BinBalance_QtyReserve) >= (model.pick * model.productConversion_Ratio))
                {
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
                    BinCardReserve.BinCardReserve_QtyBal = (model.pick * model.productConversion_Ratio);

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

                    if (!String.IsNullOrEmpty(model.goodsTransfer_Index) && !String.IsNullOrEmpty(model.goodsTransferItem_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsTransfer_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsTransfer_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsTransferItem_Index);
                    }
                    else if (!String.IsNullOrEmpty(model.goodsIssue_Index) && !String.IsNullOrEmpty(model.goodsIssueItemLocation_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsIssue_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                        BinCardReserve.Ref_Wave_Index = model.goodsIssue_Index;
                    }

                    BinCardReserve.Create_By = model.create_By;
                    BinCardReserve.Create_Date = DateTime.Now;
                    BinCardReserve.ERP_Location = itemBin.ERP_Location;

                    //var BinCard = new wm_BinCard();

                    //BinCard.BinCard_Index = BinCard_Index;
                    //BinCard.Process_Index = new Guid(model.process_Index);  
                    //BinCard.DocumentType_Index = new Guid(model.documentType_Index); //item.DocumentType_Index;
                    //BinCard.DocumentType_Id = model.documentType_Id;//item.DocumentType_Id;
                    //BinCard.DocumentType_Name = model.documentType_Name;//item.DocumentType_Name;
                    //BinCard.GoodsReceive_Index = itemBin.GoodsReceive_Index;

                    //BinCard.GoodsReceiveItem_Index = model.GIIL.GoodsReceiveItem_Index;
                    //BinCard.GoodsReceiveItemLocation_Index = new Guid(model?.goodsIssueItemLocation_Index);//item.GoodsReceiveItemLocation_Index;

                    //BinCard.BinCard_No = model.goodsIssue_No; //item.BinCard_No;.
                    //BinCard.BinCard_Date = model.goodsIssue_Date.toDate(); //item.BinCard_Date;
                    //BinCard.TagItem_Index = model.GIIL.TagItem_Index;
                    //BinCard.Tag_Index = model.GIIL.Tag_Index;
                    //BinCard.Tag_No = model.GIIL.Tag_No;
                    //BinCard.Tag_Index_To = model.GIIL.TagItem_Index; //item.Tag_Index_To;
                    //BinCard.Tag_No_To = model.GIIL.Tag_No; //item.Tag_No_To;
                    //BinCard.Product_Index = model.GIIL.Product_Index;
                    //BinCard.Product_Id = model.GIIL.Product_Id;
                    //BinCard.Product_Name = model.GIIL.Product_SecondName;
                    //BinCard.Product_SecondName = model.GIIL.Product_SecondName;
                    //BinCard.Product_ThirdName = model.GIIL.Product_ThirdName;
                    //BinCard.Product_Index_To = model.GIIL.Product_Index; //item.Product_Index_To;
                    //BinCard.Product_Id_To = model.GIIL.Product_Id;
                    //BinCard.Product_Name_To = model.GIIL.Product_Name;
                    //BinCard.Product_SecondName_To = model.GIIL.Product_SecondName;
                    //BinCard.Product_ThirdName_To = model.GIIL.Product_ThirdName;
                    //BinCard.Product_Lot = model.GIIL.Product_Lot;
                    //BinCard.Product_Lot_To = model.GIIL.Product_Lot;
                    //BinCard.ItemStatus_Index = model.GIIL.ItemStatus_Index;
                    //BinCard.ItemStatus_Id = model.GIIL.ItemStatus_Id;
                    //BinCard.ItemStatus_Name = model.GIIL.ItemStatus_Name;
                    //BinCard.ItemStatus_Index_To = model.GIIL.ItemStatus_Index;
                    //BinCard.ItemStatus_Id_To = model.GIIL.ItemStatus_Id;
                    //BinCard.ItemStatus_Name_To = model.GIIL.ItemStatus_Name;
                    //BinCard.ProductConversion_Index = model.GIIL.ProductConversion_Index;
                    //BinCard.ProductConversion_Id = model.GIIL.ProductConversion_Id;
                    //BinCard.ProductConversion_Name = model.GIIL.ProductConversion_Name;
                    //BinCard.Owner_Index = new Guid(model.owner_Index);//item.Owner_Index;
                    //BinCard.Owner_Id = model.owner_Id;//item.Owner_Id;
                    //BinCard.Owner_Name = model.owner_Name; // item.Owner_Name;
                    //BinCard.Owner_Index_To = new Guid(model.owner_Index);//item.Owner_Index;
                    //BinCard.Owner_Id_To = model.owner_Id;//item.Owner_Id;
                    //BinCard.Owner_Name_To = model.owner_Name; // item.Owner_Name;

                    //BinCard.Location_Index = model.GIIL.Location_Index;//item.Location_Index;
                    //BinCard.Location_Id = model.GIIL.Location_Id; //item.Location_Id;
                    //BinCard.Location_Name = model.GIIL.Location_Name;//item.Location_Name;

                    //BinCard.Location_Index_To = model.GIIL.Location_Index;
                    //BinCard.Location_Id_To = model.GIIL.Location_Id;
                    //BinCard.Location_Name_To = model.GIIL.Location_Name;

                    //BinCard.GoodsReceive_EXP_Date = model.GIIL.EXP_Date;
                    //BinCard.GoodsReceive_EXP_Date_To = model.GIIL.EXP_Date;
                    //BinCard.BinCard_QtyIn = 0;
                    //BinCard.BinCard_QtyOut = model.GIIL.TotalQty;
                    //BinCard.BinCard_QtySign = model.GIIL.TotalQty * -1;

                    //if (itemBin.BinBalance_WeightBegin != 0)
                    //{
                    //    var unitWeight = itemBin.BinBalance_WeightBegin / itemBin.BinBalance_QtyBegin;
                    //    BinCard.BinCard_WeightIn = 0;
                    //    BinCard.BinCard_WeightOut = model.GIIL.TotalQty * unitWeight;
                    //    BinCard.BinCard_WeightSign = (model.GIIL.TotalQty * unitWeight) * -1;
                    //}
                    //else
                    //{
                    //    BinCard.BinCard_WeightIn = 0;
                    //    BinCard.BinCard_WeightOut = 0;
                    //    BinCard.BinCard_WeightSign = 0;
                    //}

                    //if (itemBin.BinBalance_VolumeBegin != 0)
                    //{
                    //    var unitVol = itemBin.BinBalance_VolumeBegin / itemBin.BinBalance_QtyBegin;

                    //    BinCard.BinCard_VolumeIn = 0;
                    //    BinCard.BinCard_VolumeOut = (model.GIIL.TotalQty * unitVol);
                    //    BinCard.BinCard_VolumeSign = (model.GIIL.TotalQty * unitVol) * -1;
                    //}
                    //else
                    //{
                    //    BinCard.BinCard_VolumeIn = 0;
                    //    BinCard.BinCard_VolumeOut = 0;
                    //    BinCard.BinCard_VolumeSign = 0;

                    //}
                    //BinCard.UDF_1 = model.GIIL.UDF_1;
                    //BinCard.UDF_2 = model.GIIL.UDF_2;
                    //BinCard.UDF_3 = model.GIIL.UDF_3;
                    //BinCard.UDF_4 = model.GIIL.UDF_4;
                    //BinCard.UDF_5 = model.GIIL.UDF_5;
                    //BinCard.Ref_Document_No = model.goodsIssue_No;
                    //BinCard.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index); //tem.Ref_Document_Index;
                    //BinCard.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                    //BinCard.Create_Date = DateTime.Now;
                    //BinCard.Create_By = model.create_By;

                    //db.wm_BinCard.Add(BinCard);


                    db.wm_BinCardReserve.Add(BinCardReserve);

                    itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyReserve + (model.pick * model.productConversion_Ratio);

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

                    var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transactionx.Commit();
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("pickProductBinbalance", msglog);
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
                //model.binCard_Index = BinCard_Index.ToString();
                result.items = model;
                result.resultMsg = "รับสินค้าเรียบร้อยแล้ว";
                result.resultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("pickProductBinbalance", msglog);
                var result = new actionResultPickbinbalanceFromGIViewModel();
                result.resultIsUse = false;
                result.resultMsg = ex.Message;
                return result;
            }

        }

        public actionResultPickbinbalanceFromGIViewModel pickProduct(PickbinbalanceFromGIViewModel model)
        {
            String State = "Start " + model.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var result = new actionResultPickbinbalanceFromGIViewModel();
                var BinCardReserve_Index = Guid.NewGuid();
                //var BinCard_Index = Guid.NewGuid();
                var itemBin = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));
                if ((itemBin.BinBalance_QtyBal - itemBin.BinBalance_QtyReserve) >= model.pick)
                {
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
                    BinCardReserve.BinCardReserve_QtyBal = (model.pick * model.productConversion_Ratio);

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

                    if (!String.IsNullOrEmpty(model.goodsTransfer_Index) && !String.IsNullOrEmpty(model.goodsTransferItem_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsTransfer_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsTransfer_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsTransferItem_Index);
                    }
                    else if (!String.IsNullOrEmpty(model.goodsIssue_Index) && !String.IsNullOrEmpty(model.goodsIssueItemLocation_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsIssue_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                        BinCardReserve.Ref_Wave_Index = model.goodsIssue_Index;
                    }

                    BinCardReserve.Create_By = model.create_By;
                    BinCardReserve.Create_Date = DateTime.Now;
                    BinCardReserve.ERP_Location = itemBin.ERP_Location;

                    //var BinCard = new wm_BinCard();

                    //BinCard.BinCard_Index = BinCard_Index;
                    //BinCard.Process_Index = new Guid(model.process_Index);  
                    //BinCard.DocumentType_Index = new Guid(model.documentType_Index); //item.DocumentType_Index;
                    //BinCard.DocumentType_Id = model.documentType_Id;//item.DocumentType_Id;
                    //BinCard.DocumentType_Name = model.documentType_Name;//item.DocumentType_Name;
                    //BinCard.GoodsReceive_Index = itemBin.GoodsReceive_Index;

                    //BinCard.GoodsReceiveItem_Index = model.GIIL.GoodsReceiveItem_Index;
                    //BinCard.GoodsReceiveItemLocation_Index = new Guid(model?.goodsIssueItemLocation_Index);//item.GoodsReceiveItemLocation_Index;

                    //BinCard.BinCard_No = model.goodsIssue_No; //item.BinCard_No;.
                    //BinCard.BinCard_Date = model.goodsIssue_Date.toDate(); //item.BinCard_Date;
                    //BinCard.TagItem_Index = model.GIIL.TagItem_Index;
                    //BinCard.Tag_Index = model.GIIL.Tag_Index;
                    //BinCard.Tag_No = model.GIIL.Tag_No;
                    //BinCard.Tag_Index_To = model.GIIL.TagItem_Index; //item.Tag_Index_To;
                    //BinCard.Tag_No_To = model.GIIL.Tag_No; //item.Tag_No_To;
                    //BinCard.Product_Index = model.GIIL.Product_Index;
                    //BinCard.Product_Id = model.GIIL.Product_Id;
                    //BinCard.Product_Name = model.GIIL.Product_SecondName;
                    //BinCard.Product_SecondName = model.GIIL.Product_SecondName;
                    //BinCard.Product_ThirdName = model.GIIL.Product_ThirdName;
                    //BinCard.Product_Index_To = model.GIIL.Product_Index; //item.Product_Index_To;
                    //BinCard.Product_Id_To = model.GIIL.Product_Id;
                    //BinCard.Product_Name_To = model.GIIL.Product_Name;
                    //BinCard.Product_SecondName_To = model.GIIL.Product_SecondName;
                    //BinCard.Product_ThirdName_To = model.GIIL.Product_ThirdName;
                    //BinCard.Product_Lot = model.GIIL.Product_Lot;
                    //BinCard.Product_Lot_To = model.GIIL.Product_Lot;
                    //BinCard.ItemStatus_Index = model.GIIL.ItemStatus_Index;
                    //BinCard.ItemStatus_Id = model.GIIL.ItemStatus_Id;
                    //BinCard.ItemStatus_Name = model.GIIL.ItemStatus_Name;
                    //BinCard.ItemStatus_Index_To = model.GIIL.ItemStatus_Index;
                    //BinCard.ItemStatus_Id_To = model.GIIL.ItemStatus_Id;
                    //BinCard.ItemStatus_Name_To = model.GIIL.ItemStatus_Name;
                    //BinCard.ProductConversion_Index = model.GIIL.ProductConversion_Index;
                    //BinCard.ProductConversion_Id = model.GIIL.ProductConversion_Id;
                    //BinCard.ProductConversion_Name = model.GIIL.ProductConversion_Name;
                    //BinCard.Owner_Index = new Guid(model.owner_Index);//item.Owner_Index;
                    //BinCard.Owner_Id = model.owner_Id;//item.Owner_Id;
                    //BinCard.Owner_Name = model.owner_Name; // item.Owner_Name;
                    //BinCard.Owner_Index_To = new Guid(model.owner_Index);//item.Owner_Index;
                    //BinCard.Owner_Id_To = model.owner_Id;//item.Owner_Id;
                    //BinCard.Owner_Name_To = model.owner_Name; // item.Owner_Name;

                    //BinCard.Location_Index = model.GIIL.Location_Index;//item.Location_Index;
                    //BinCard.Location_Id = model.GIIL.Location_Id; //item.Location_Id;
                    //BinCard.Location_Name = model.GIIL.Location_Name;//item.Location_Name;

                    //BinCard.Location_Index_To = model.GIIL.Location_Index;
                    //BinCard.Location_Id_To = model.GIIL.Location_Id;
                    //BinCard.Location_Name_To = model.GIIL.Location_Name;

                    //BinCard.GoodsReceive_EXP_Date = model.GIIL.EXP_Date;
                    //BinCard.GoodsReceive_EXP_Date_To = model.GIIL.EXP_Date;
                    //BinCard.BinCard_QtyIn = 0;
                    //BinCard.BinCard_QtyOut = model.GIIL.TotalQty;
                    //BinCard.BinCard_QtySign = model.GIIL.TotalQty * -1;

                    //if (itemBin.BinBalance_WeightBegin != 0)
                    //{
                    //    var unitWeight = itemBin.BinBalance_WeightBegin / itemBin.BinBalance_QtyBegin;
                    //    BinCard.BinCard_WeightIn = 0;
                    //    BinCard.BinCard_WeightOut = model.GIIL.TotalQty * unitWeight;
                    //    BinCard.BinCard_WeightSign = (model.GIIL.TotalQty * unitWeight) * -1;
                    //}
                    //else
                    //{
                    //    BinCard.BinCard_WeightIn = 0;
                    //    BinCard.BinCard_WeightOut = 0;
                    //    BinCard.BinCard_WeightSign = 0;
                    //}

                    //if (itemBin.BinBalance_VolumeBegin != 0)
                    //{
                    //    var unitVol = itemBin.BinBalance_VolumeBegin / itemBin.BinBalance_QtyBegin;

                    //    BinCard.BinCard_VolumeIn = 0;
                    //    BinCard.BinCard_VolumeOut = (model.GIIL.TotalQty * unitVol);
                    //    BinCard.BinCard_VolumeSign = (model.GIIL.TotalQty * unitVol) * -1;
                    //}
                    //else
                    //{
                    //    BinCard.BinCard_VolumeIn = 0;
                    //    BinCard.BinCard_VolumeOut = 0;
                    //    BinCard.BinCard_VolumeSign = 0;

                    //}
                    //BinCard.UDF_1 = model.GIIL.UDF_1;
                    //BinCard.UDF_2 = model.GIIL.UDF_2;
                    //BinCard.UDF_3 = model.GIIL.UDF_3;
                    //BinCard.UDF_4 = model.GIIL.UDF_4;
                    //BinCard.UDF_5 = model.GIIL.UDF_5;
                    //BinCard.Ref_Document_No = model.goodsIssue_No;
                    //BinCard.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index); //tem.Ref_Document_Index;
                    //BinCard.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                    //BinCard.Create_Date = DateTime.Now;
                    //BinCard.Create_By = model.create_By;

                    //db.wm_BinCard.Add(BinCard);


                    db.wm_BinCardReserve.Add(BinCardReserve);

                    itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyReserve + (model.pick * model.productConversion_Ratio);

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

                    var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transactionx.Commit();
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("pickProductBinbalance", msglog);
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
                //model.binCard_Index = BinCard_Index.ToString();
                result.items = model;
                result.resultMsg = "รับสินค้าเรียบร้อยแล้ว";
                result.resultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("pickProductBinbalance", msglog);
                var result = new actionResultPickbinbalanceFromGIViewModel();
                result.resultIsUse = false;
                result.resultMsg = ex.Message;
                return result;
            }

        }

        public actionResultPickbinbalanceFromGIViewModel pickProductUnpack(PickbinbalanceFromGIViewModel model)
        {
            String State = "Start " + model.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var result = new actionResultPickbinbalanceFromGIViewModel();
                var BinCardReserve_Index = Guid.NewGuid();
                //var BinCard_Index = Guid.NewGuid();
                var itemBin = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));
                if ((itemBin.BinBalance_QtyBal - itemBin.BinBalance_QtyReserve) >= (model.pick* model.productConversion_Ratio))
                {
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
                    BinCardReserve.BinCardReserve_QtyBal = (model.pick * model.productConversion_Ratio);

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

                    if (!String.IsNullOrEmpty(model.goodsTransfer_Index) && !String.IsNullOrEmpty(model.goodsTransferItem_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsTransfer_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsTransfer_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsTransferItem_Index);
                    }
                    else if (!String.IsNullOrEmpty(model.goodsIssue_Index) && !String.IsNullOrEmpty(model.goodsIssueItemLocation_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsIssue_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                        BinCardReserve.Ref_Wave_Index = model.goodsIssue_Index;
                    }

                    BinCardReserve.Create_By = model.create_By;
                    BinCardReserve.Create_Date = DateTime.Now;
                    BinCardReserve.ERP_Location = itemBin.ERP_Location;

                    //var BinCard = new wm_BinCard();

                    //BinCard.BinCard_Index = BinCard_Index;
                    //BinCard.Process_Index = new Guid(model.process_Index);  
                    //BinCard.DocumentType_Index = new Guid(model.documentType_Index); //item.DocumentType_Index;
                    //BinCard.DocumentType_Id = model.documentType_Id;//item.DocumentType_Id;
                    //BinCard.DocumentType_Name = model.documentType_Name;//item.DocumentType_Name;
                    //BinCard.GoodsReceive_Index = itemBin.GoodsReceive_Index;

                    //BinCard.GoodsReceiveItem_Index = model.GIIL.GoodsReceiveItem_Index;
                    //BinCard.GoodsReceiveItemLocation_Index = new Guid(model?.goodsIssueItemLocation_Index);//item.GoodsReceiveItemLocation_Index;

                    //BinCard.BinCard_No = model.goodsIssue_No; //item.BinCard_No;.
                    //BinCard.BinCard_Date = model.goodsIssue_Date.toDate(); //item.BinCard_Date;
                    //BinCard.TagItem_Index = model.GIIL.TagItem_Index;
                    //BinCard.Tag_Index = model.GIIL.Tag_Index;
                    //BinCard.Tag_No = model.GIIL.Tag_No;
                    //BinCard.Tag_Index_To = model.GIIL.TagItem_Index; //item.Tag_Index_To;
                    //BinCard.Tag_No_To = model.GIIL.Tag_No; //item.Tag_No_To;
                    //BinCard.Product_Index = model.GIIL.Product_Index;
                    //BinCard.Product_Id = model.GIIL.Product_Id;
                    //BinCard.Product_Name = model.GIIL.Product_SecondName;
                    //BinCard.Product_SecondName = model.GIIL.Product_SecondName;
                    //BinCard.Product_ThirdName = model.GIIL.Product_ThirdName;
                    //BinCard.Product_Index_To = model.GIIL.Product_Index; //item.Product_Index_To;
                    //BinCard.Product_Id_To = model.GIIL.Product_Id;
                    //BinCard.Product_Name_To = model.GIIL.Product_Name;
                    //BinCard.Product_SecondName_To = model.GIIL.Product_SecondName;
                    //BinCard.Product_ThirdName_To = model.GIIL.Product_ThirdName;
                    //BinCard.Product_Lot = model.GIIL.Product_Lot;
                    //BinCard.Product_Lot_To = model.GIIL.Product_Lot;
                    //BinCard.ItemStatus_Index = model.GIIL.ItemStatus_Index;
                    //BinCard.ItemStatus_Id = model.GIIL.ItemStatus_Id;
                    //BinCard.ItemStatus_Name = model.GIIL.ItemStatus_Name;
                    //BinCard.ItemStatus_Index_To = model.GIIL.ItemStatus_Index;
                    //BinCard.ItemStatus_Id_To = model.GIIL.ItemStatus_Id;
                    //BinCard.ItemStatus_Name_To = model.GIIL.ItemStatus_Name;
                    //BinCard.ProductConversion_Index = model.GIIL.ProductConversion_Index;
                    //BinCard.ProductConversion_Id = model.GIIL.ProductConversion_Id;
                    //BinCard.ProductConversion_Name = model.GIIL.ProductConversion_Name;
                    //BinCard.Owner_Index = new Guid(model.owner_Index);//item.Owner_Index;
                    //BinCard.Owner_Id = model.owner_Id;//item.Owner_Id;
                    //BinCard.Owner_Name = model.owner_Name; // item.Owner_Name;
                    //BinCard.Owner_Index_To = new Guid(model.owner_Index);//item.Owner_Index;
                    //BinCard.Owner_Id_To = model.owner_Id;//item.Owner_Id;
                    //BinCard.Owner_Name_To = model.owner_Name; // item.Owner_Name;

                    //BinCard.Location_Index = model.GIIL.Location_Index;//item.Location_Index;
                    //BinCard.Location_Id = model.GIIL.Location_Id; //item.Location_Id;
                    //BinCard.Location_Name = model.GIIL.Location_Name;//item.Location_Name;

                    //BinCard.Location_Index_To = model.GIIL.Location_Index;
                    //BinCard.Location_Id_To = model.GIIL.Location_Id;
                    //BinCard.Location_Name_To = model.GIIL.Location_Name;

                    //BinCard.GoodsReceive_EXP_Date = model.GIIL.EXP_Date;
                    //BinCard.GoodsReceive_EXP_Date_To = model.GIIL.EXP_Date;
                    //BinCard.BinCard_QtyIn = 0;
                    //BinCard.BinCard_QtyOut = model.GIIL.TotalQty;
                    //BinCard.BinCard_QtySign = model.GIIL.TotalQty * -1;

                    //if (itemBin.BinBalance_WeightBegin != 0)
                    //{
                    //    var unitWeight = itemBin.BinBalance_WeightBegin / itemBin.BinBalance_QtyBegin;
                    //    BinCard.BinCard_WeightIn = 0;
                    //    BinCard.BinCard_WeightOut = model.GIIL.TotalQty * unitWeight;
                    //    BinCard.BinCard_WeightSign = (model.GIIL.TotalQty * unitWeight) * -1;
                    //}
                    //else
                    //{
                    //    BinCard.BinCard_WeightIn = 0;
                    //    BinCard.BinCard_WeightOut = 0;
                    //    BinCard.BinCard_WeightSign = 0;
                    //}

                    //if (itemBin.BinBalance_VolumeBegin != 0)
                    //{
                    //    var unitVol = itemBin.BinBalance_VolumeBegin / itemBin.BinBalance_QtyBegin;

                    //    BinCard.BinCard_VolumeIn = 0;
                    //    BinCard.BinCard_VolumeOut = (model.GIIL.TotalQty * unitVol);
                    //    BinCard.BinCard_VolumeSign = (model.GIIL.TotalQty * unitVol) * -1;
                    //}
                    //else
                    //{
                    //    BinCard.BinCard_VolumeIn = 0;
                    //    BinCard.BinCard_VolumeOut = 0;
                    //    BinCard.BinCard_VolumeSign = 0;

                    //}
                    //BinCard.UDF_1 = model.GIIL.UDF_1;
                    //BinCard.UDF_2 = model.GIIL.UDF_2;
                    //BinCard.UDF_3 = model.GIIL.UDF_3;
                    //BinCard.UDF_4 = model.GIIL.UDF_4;
                    //BinCard.UDF_5 = model.GIIL.UDF_5;
                    //BinCard.Ref_Document_No = model.goodsIssue_No;
                    //BinCard.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index); //tem.Ref_Document_Index;
                    //BinCard.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                    //BinCard.Create_Date = DateTime.Now;
                    //BinCard.Create_By = model.create_By;

                    //db.wm_BinCard.Add(BinCard);


                    db.wm_BinCardReserve.Add(BinCardReserve);

                    itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyReserve + (model.pick * model.productConversion_Ratio);

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

                    var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transactionx.Commit();
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("pickProductBinbalance", msglog);
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
                //model.binCard_Index = BinCard_Index.ToString();
                result.items = model;
                result.resultMsg = "รับสินค้าเรียบร้อยแล้ว";
                result.resultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("pickProductBinbalance", msglog);
                var result = new actionResultPickbinbalanceFromGIViewModel();
                result.resultIsUse = false;
                result.resultMsg = ex.Message;
                return result;
            }

        }

        public bool deletePickProduct(PickbinbalanceFromGIViewModel model)
        {
            String State = "Start " + model.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {
                //var itemReserve = db.wm_BinCardReserve.Where(c=> c.BinCardReserve_Index ==(Guid.Parse(model.binCardReserve_Index)) && c.BinCardReserve_Status != -1).FirstOrDefault();
                var itemReserve = db.wm_BinCardReserve.Where(c => c.Ref_DocumentItem_Index == (Guid.Parse(model.ref_DocumentItem_Index)) && c.BinCardReserve_Status != -1).ToList();

                if (itemReserve.Count() == null)
                {
                    return false;
                }
                foreach (var ir in itemReserve)
                {
                    ir.BinCardReserve_Status = -1;

                    //var itemBin = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));
                    var itemBin = db.wm_BinBalance.Find(ir.BinBalance_Index);

                    itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyReserve - ir.BinCardReserve_QtyBal;
                    itemBin.BinBalance_WeightReserve = itemBin.BinBalance_WeightReserve - ir.BinCardReserve_WeightBal;
                    itemBin.BinBalance_NetWeightReserve = itemBin.BinBalance_NetWeightReserve - ir.BinCardReserve_NetWeightBal;
                    itemBin.BinBalance_GrsWeightReserve = itemBin.BinBalance_GrsWeightReserve - ir.BinCardReserve_GrsWeightBal;
                    itemBin.BinBalance_WidthReserve = itemBin.BinBalance_WidthReserve - ir.BinCardReserve_WidthBal;
                    itemBin.BinBalance_LengthReserve = itemBin.BinBalance_LengthReserve - ir.BinCardReserve_LengthBal;
                    itemBin.BinBalance_HeightReserve = itemBin.BinBalance_HeightReserve - ir.BinCardReserve_HeightBal;
                    itemBin.BinBalance_VolumeReserve = itemBin.BinBalance_VolumeReserve - ir.BinCardReserve_VolumeBal;
                    itemBin.Price = itemBin.Price + ir.Price;
                }


                //var BinCard = new wm_BinCard();

                //BinCard.BinCard_Index = Guid.NewGuid();
                //BinCard.Process_Index = new Guid(model.process_Index);
                //BinCard.DocumentType_Index = new Guid(model.documentType_Index); //item.DocumentType_Index;
                //BinCard.DocumentType_Id = model.documentType_Id;//item.DocumentType_Id;
                //BinCard.DocumentType_Name = model.documentType_Name;//item.DocumentType_Name;
                //BinCard.GoodsReceive_Index = itemBin.GoodsReceive_Index;

                //BinCard.GoodsReceiveItem_Index = model.GIIL.GoodsReceiveItem_Index;
                //BinCard.GoodsReceiveItemLocation_Index = new Guid(model?.goodsIssueItemLocation_Index);//item.GoodsReceiveItemLocation_Index;

                //BinCard.BinCard_No = model.goodsIssue_No; //item.BinCard_No;.
                //BinCard.BinCard_Date = model.goodsIssue_Date.toDate(); //item.BinCard_Date;
                //BinCard.TagItem_Index = model.GIIL.TagItem_Index;
                //BinCard.Tag_Index = model.GIIL.Tag_Index;
                //BinCard.Tag_No = model.GIIL.Tag_No;
                //BinCard.Tag_Index_To = model.GIIL.TagItem_Index; //item.Tag_Index_To;
                //BinCard.Tag_No_To = model.GIIL.Tag_No; //item.Tag_No_To;
                //BinCard.Product_Index = model.GIIL.Product_Index;
                //BinCard.Product_Id = model.GIIL.Product_Id;
                //BinCard.Product_Name = model.GIIL.Product_SecondName;
                //BinCard.Product_SecondName = model.GIIL.Product_SecondName;
                //BinCard.Product_ThirdName = model.GIIL.Product_ThirdName;
                //BinCard.Product_Index_To = model.GIIL.Product_Index; //item.Product_Index_To;
                //BinCard.Product_Id_To = model.GIIL.Product_Id;
                //BinCard.Product_Name_To = model.GIIL.Product_Name;
                //BinCard.Product_SecondName_To = model.GIIL.Product_SecondName;
                //BinCard.Product_ThirdName_To = model.GIIL.Product_ThirdName;
                //BinCard.Product_Lot = model.GIIL.Product_Lot;
                //BinCard.Product_Lot_To = model.GIIL.Product_Lot;
                //BinCard.ItemStatus_Index = model.GIIL.ItemStatus_Index;
                //BinCard.ItemStatus_Id = model.GIIL.ItemStatus_Id;
                //BinCard.ItemStatus_Name = model.GIIL.ItemStatus_Name;
                //BinCard.ItemStatus_Index_To = model.GIIL.ItemStatus_Index;
                //BinCard.ItemStatus_Id_To = model.GIIL.ItemStatus_Id;
                //BinCard.ItemStatus_Name_To = model.GIIL.ItemStatus_Name;
                //BinCard.ProductConversion_Index = model.GIIL.ProductConversion_Index;
                //BinCard.ProductConversion_Id = model.GIIL.ProductConversion_Id;
                //BinCard.ProductConversion_Name = model.GIIL.ProductConversion_Name;
                //BinCard.Owner_Index = new Guid(model.owner_Index);//item.Owner_Index;
                //BinCard.Owner_Id = model.owner_Id;//item.Owner_Id;
                //BinCard.Owner_Name = model.owner_Name; // item.Owner_Name;
                //BinCard.Owner_Index_To = new Guid(model.owner_Index);//item.Owner_Index;
                //BinCard.Owner_Id_To = model.owner_Id;//item.Owner_Id;
                //BinCard.Owner_Name_To = model.owner_Name; // item.Owner_Name;

                //BinCard.Location_Index = model.GIIL.Location_Index;//item.Location_Index;
                //BinCard.Location_Id = model.GIIL.Location_Id; //item.Location_Id;
                //BinCard.Location_Name = model.GIIL.Location_Name;//item.Location_Name;

                //BinCard.Location_Index_To = model.GIIL.Location_Index;
                //BinCard.Location_Id_To = model.GIIL.Location_Id;
                //BinCard.Location_Name_To = model.GIIL.Location_Name;

                //BinCard.GoodsReceive_EXP_Date = model.GIIL.EXP_Date;
                //BinCard.GoodsReceive_EXP_Date_To = model.GIIL.EXP_Date;
                //BinCard.BinCard_QtyIn = model.GIIL.TotalQty;
                //BinCard.BinCard_QtyOut = 0;
                //BinCard.BinCard_QtySign = model.GIIL.TotalQty * -1;

                //if (itemBin.BinBalance_WeightBegin != 0)
                //{
                //    var unitWeight = itemBin.BinBalance_WeightBegin / itemBin.BinBalance_QtyBegin;
                //    BinCard.BinCard_WeightIn = model.GIIL.TotalQty * unitWeight;
                //    BinCard.BinCard_WeightOut = 0;
                //    BinCard.BinCard_WeightSign = (model.GIIL.TotalQty * unitWeight) * -1;
                //}
                //else
                //{
                //    BinCard.BinCard_WeightIn = 0;
                //    BinCard.BinCard_WeightOut = 0;
                //    BinCard.BinCard_WeightSign = 0;
                //}

                //if (itemBin.BinBalance_VolumeBegin != 0)
                //{
                //    var unitVol = itemBin.BinBalance_VolumeBegin / itemBin.BinBalance_QtyBegin;

                //    BinCard.BinCard_VolumeIn = (model.GIIL.TotalQty * unitVol);
                //    BinCard.BinCard_VolumeOut = 0;
                //    BinCard.BinCard_VolumeSign = (model.GIIL.TotalQty * unitVol) * -1;
                //}
                //else
                //{
                //    BinCard.BinCard_VolumeIn = 0;
                //    BinCard.BinCard_VolumeOut = 0;
                //    BinCard.BinCard_VolumeSign = 0;

                //}
                //BinCard.UDF_1 = model.GIIL.UDF_1;
                //BinCard.UDF_2 = model.GIIL.UDF_2;
                //BinCard.UDF_3 = model.GIIL.UDF_3;
                //BinCard.UDF_4 = model.GIIL.UDF_4;
                //BinCard.UDF_5 = model.GIIL.UDF_5;
                //BinCard.Ref_Document_No = model.goodsIssue_No;
                //BinCard.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index); //tem.Ref_Document_Index;
                //BinCard.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                //BinCard.Create_Date = DateTime.Now;
                //BinCard.Create_By = model.create_By;

                //db.wm_BinCard.Add(BinCard);

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("deletepickProductBinbalance", msglog);
                    transactionx.Rollback();

                    throw exy;

                }
                return true;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("pickProductBinbalance", msglog);
                return false;
            }
        }


        public List<ItemListViewModel> AutoCompleterOwnerId(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("OwnerIdFilter"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleterOwnerName(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoOwnerFilter"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleterGR(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoGr"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleterGRandGI(ItemListViewModel model)
        {
            try
            {
                var result = new List<ItemListViewModel>();

                //GetConfig
                var GR = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoGr"), model.sJson());
                var GI = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoGI"), model.sJson());
                if (GR.Count() > 0)
                {
                    //var GRskip = GR.Count() < 5 ? GR : GR.Skip(5);
                    foreach (var gr in GR)
                    {
                        var r = new ItemListViewModel();
                        r.index = gr.index;
                        r.name = gr.name;
                        result.Add(r);
                    }
                }
                if (GI.Count() > 0)
                {
                    //var GIskip = GI.Count() < 5 ? GI : GI.Skip(5);
                    foreach (var gi in GI)
                    {
                        var r = new ItemListViewModel();
                        r.index = gi.index;
                        r.name = gi.name;
                        result.Add(r);
                    }
                }

                return result.Take(10).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public actionResultPickbinbalanceFromGIViewModel ListPickProduct(ListPickbinbalanceFromGIViewModel models)
        {
            String State = "Start " + models.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var result = new actionResultPickbinbalanceFromGIViewModel();

                foreach (var model in models.items)
                {
                    var BinCardReserve_Index = Guid.NewGuid();
                    //var BinCard_Index = Guid.NewGuid();
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
                    BinCardReserve.BinCardReserve_QtyBal = (itemBin.BinBalance_QtyBal - itemBin.BinBalance_QtyReserve);
                    BinCardReserve.BinCardReserve_WeightBal = (itemBin.BinBalance_WeightBal - itemBin.BinBalance_WeightReserve);
                    BinCardReserve.BinCardReserve_VolumeBal = (itemBin.BinBalance_VolumeBal - itemBin.BinBalance_VolumeReserve);

                    if (!String.IsNullOrEmpty(model.goodsTransfer_Index) && !String.IsNullOrEmpty(model.goodsTransferItem_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsTransfer_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsTransfer_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsTransferItem_Index);
                    }
                    else if (!String.IsNullOrEmpty(model.goodsIssue_Index) && !String.IsNullOrEmpty(model.goodsIssueItemLocation_Index))
                    {
                        BinCardReserve.Ref_Document_No = model.goodsIssue_No;
                        BinCardReserve.Ref_Document_Index = Guid.Parse(model.goodsIssue_Index);
                        BinCardReserve.Ref_DocumentItem_Index = Guid.Parse(model.goodsIssueItemLocation_Index);
                        BinCardReserve.Ref_Wave_Index = model.goodsIssue_Index;
                    }

                    BinCardReserve.Create_By = model.create_By;
                    BinCardReserve.Create_Date = DateTime.Now;

                    db.wm_BinCardReserve.Add(BinCardReserve);

                    itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyBal;
                    model.binCardReserve_Index = BinCardReserve_Index.ToString();


                    if (itemBin.BinBalance_WeightBegin != 0)
                    {
                        itemBin.BinBalance_WeightReserve = itemBin.BinBalance_WeightBegin;
                    }

                    if (itemBin.BinBalance_VolumeBegin != 0)
                    {
                        itemBin.BinBalance_VolumeReserve = itemBin.BinBalance_VolumeBegin;
                    }


                }
                var transactionx = db.Database.BeginTransaction();
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("pickProductBinbalance", msglog);
                    transactionx.Rollback();

                    throw exy;

                }


                //model.binCard_Index = BinCard_Index.ToString();
                result.items = models.items.FirstOrDefault();
                result.resultMsg = "รับสินค้าเรียบร้อยแล้ว";
                result.resultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("pickProductBinbalance", msglog);
                var result = new actionResultPickbinbalanceFromGIViewModel();
                result.resultIsUse = false;
                result.resultMsg = ex.Message;
                return result;
            }

        }

        public bool deletePickProductQI(ListPickbinbalanceFromGIViewModel models)
        {
            String State = "Start " + models.sJson();
            String msglog = "";
            var olog = new logtxt();
            try
            {
                foreach (var model in models.items)
                {
                    //var itemReserve = db.wm_BinCardReserve.Where(c=> c.BinCardReserve_Index ==(Guid.Parse(model.binCardReserve_Index)) && c.BinCardReserve_Status != -1).FirstOrDefault();
                    var itemReserve = db.wm_BinCardReserve.Where(c => c.Ref_DocumentItem_Index == (Guid.Parse(model.ref_DocumentItem_Index)) && c.BinCardReserve_Status != -1).ToList();

                    if (itemReserve.Count() == null)
                    {
                        return false;
                    }
                    foreach (var ir in itemReserve)
                    {
                        ir.BinCardReserve_Status = -1;

                        //var itemBin = db.wm_BinBalance.Find(Guid.Parse(model.binbalance_Index));
                        var itemBin = db.wm_BinBalance.Find(ir.BinBalance_Index);

                        itemBin.BinBalance_QtyReserve = itemBin.BinBalance_QtyReserve - ir.BinCardReserve_QtyBal;
                        itemBin.BinBalance_WeightReserve = itemBin.BinBalance_WeightReserve - ir.BinCardReserve_WeightBal;
                        itemBin.BinBalance_VolumeReserve = itemBin.BinBalance_VolumeReserve - ir.BinCardReserve_VolumeBal;
                    }
                }

                var transactionx = db.Database.BeginTransaction();
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("deletepickProductBinbalance", msglog);
                    transactionx.Rollback();

                    throw exy;

                }
                return true;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("pickProductBinbalance", msglog);
                return false;
            }
        }


        public List<ItemListViewModel> AutoCompleterTag(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoTag"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoCompleteProductType(ItemListViewModel model)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("AutoCompleteProductType"), model.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoZone(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoZoneFilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> autoLocationFilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoLocationFilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> autoItemStatus(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoItemStatus"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> autoServiceCharge(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }
                filterModel.index = new Guid("6A877EA3-7FDD-43E8-A409-4B6BBE2BF199");
                filterModel.sub_index = new Guid(data.key2);

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoServiceCharge"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GenDocumentTypeViewModel> dropdownDocumentType(GenDocumentTypeViewModel data)
        {
            try
            {
                var result = new List<GenDocumentTypeViewModel>();

                var filterModel = new GenDocumentTypeViewModel();


                filterModel.process_Index = new Guid("C06E0E4D-393D-43BC-B640-448C20832433");


                //GetConfig
                result = utils.SendDataApi<List<GenDocumentTypeViewModel>>(new AppSettingConfig().GetUrl("dropDownDocumentType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<storageChargeModel> dropDownStorageCharge(storageChargeModel data)
        {
            try
            {
                //GetConfig
                var result = utils.SendDataApi<List<storageChargeModel>>(new AppSettingConfig().GetUrl("dropDownStorageCharge"), data.sJson());

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> autoItemInvoice(ItemListViewModel data)
        {
            try
            {

                var query = db.im_Invoice.AsQueryable();

                if (data.key == "-")
                {

                }

                else if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Invoice_No.Contains(data.key));
                }


                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.Invoice_Index, c.Invoice_No }).Distinct().Take(10).ToList();

                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        index = item.Invoice_Index,
                        id = item.Invoice_No,
                        name = item.Invoice_No,
                    };

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ServiceChargeViewModel> dropDownServiceCharge(ServiceChargeViewModel data)
        {
            try
            {
                var result = new List<ServiceChargeViewModel>();

                var filterModel = new ServiceChargeViewModel();

                //GetConfig
                result = utils.SendDataApi<List<ServiceChargeViewModel>>(new AppSettingConfig().GetUrl("dropDownServiceCharge"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region autoCompleteMemo
        public List<ItemListViewModel> autoCompleteMemo(ItemListViewModel data)
        {
            try
            {

                using (var context = new BinbalanceDbContext())
                {

                    var query = context.im_Memo.Where(c => c.Document_Status != -1);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Memo_No.Contains(data.key));

                    }
                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.Memo_Index, c.Memo_No }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Memo_Index,
                            id = item.Memo_No,
                            name = item.Memo_No
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public List<View_ServiceChargeFixViewModel> dropDownServiceChargeFix(View_ServiceChargeFixViewModel data)
        {
            try
            {
                var result = new List<View_ServiceChargeFixViewModel>();

                var filterModel = new View_ServiceChargeFixViewModel();
                filterModel.owner_Index = data.owner_Index;

                //GetConfig
                result = utils.SendDataApi<List<View_ServiceChargeFixViewModel>>(new AppSettingConfig().GetUrl("dropDownServiceChargeFix"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


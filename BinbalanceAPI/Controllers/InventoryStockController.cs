using System;
using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.BinBalanceService;
using BinbalanceBusiness.InventoryStock;
using BinbalanceBusiness.PickBinbalance;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using BinBalanceBusiness.ViewModels;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;

namespace BinbalanceAPI.Controllers
{
    [Route("api/InventoryStock")]
    [ApiController]
    public class InventoryStockController : ControllerBase
    {

        #region InquirySKU

        [HttpPost("searchSKU")]
        public IActionResult GetInquirySKU([FromBody]JObject body)
        {
            try
            {
                var service = new InventoryStockService();
                var Models = new SKUSearchViewModel();
                Models = JsonConvert.DeserializeObject<SKUSearchViewModel>(body.ToString());
                var result = service.search(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getSKU_StockDetails")]
        public IActionResult GetInquirySKUStockDetails([FromBody]JObject body)
        {
            try
            {
                var service = new InventoryStockService();
                var Models = new SKUSearchViewModel();
                Models = JsonConvert.DeserializeObject<SKUSearchViewModel>(body.ToString());
                var result = service.GetStockDetails(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getSKU_SKUConversion")]
        public IActionResult GetInquirySKUSKUConversion([FromBody]JObject body)
        {
            try
            {
                var service = new InventoryStockService();
                var Models = new SKUSearchViewModel();
                Models = JsonConvert.DeserializeObject<SKUSearchViewModel>(body.ToString());
                var result = service.GetSKUConversion(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getSKU_SKUAllocatedBy")]
        public IActionResult GetInquirySKUSKUAllocatedBy([FromBody]JObject body)
        {
            try
            {
                var service = new InventoryStockService();
                var Models = new SKUAllocatedByViewModel();
                Models = JsonConvert.DeserializeObject<SKUAllocatedByViewModel>(body.ToString());
                var result = service.GetSKUAllocatedBy(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost]
        //[Route("ExportExcelSKU")]
        ////[ResponseType(typeof(int))]
        //public IActionResult ExportExcelSKU(ListInquirySKU model)
        //{
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    try
        //    {
        //        InquirySKUService _appService = new InquirySKUService();
        //        var shippingAdviceData = _appService.ExportExcelSKU(model);
        //        //HttpResponse response = HttpContext.Current.Response;
        //        var wb = new ClosedXML.Excel.XLWorkbook();

        //        wb.Worksheets.Add(shippingAdviceData);
        //        using (var ms = new System.IO.MemoryStream())
        //        {
        //            wb.SaveAs(ms);
        //            ms.Position = 0;
        //            ms.Close();

        //            return File(ms.ToArray(), "application/octet-stream");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        #endregion




    }
}

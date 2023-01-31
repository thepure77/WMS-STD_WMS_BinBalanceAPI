using System;
using System.Net;
using System.Net.Http;
using binbalanceBusiness.Binbalance.ViewModels;
using binbalanceBusiness.BinServiceChargeViewModel;
using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.BinBalanceService;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;

namespace BinbalanceAPI.Controllers
{
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public InvoiceController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region loadtransction
        [HttpPost("loadtransction")]
        public IActionResult loadtransction([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new BinBalanceServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<BinBalanceServiceChargeViewModel>(body.ToString());
                var result = service.loadtransction(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region cal
        [HttpPost("cal")]
        public IActionResult cal([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new BinServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<BinServiceChargeViewModel>(body.ToString());
                var result = service.cal(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region groupStorage
        [HttpPost("groupStorage")]
        public IActionResult groupStorage([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new BinServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<BinServiceChargeViewModel>(body.ToString());
                var result = service.groupStorage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region groupInvoice
        [HttpPost("groupInvoice")]
        public IActionResult groupInvoice([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new BinServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<BinServiceChargeViewModel>(body.ToString());
                var result = service.groupInvoice(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region saveInvoice
        [HttpPost("saveInvoice")]
        public IActionResult saveInvoice([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new InvoiceViewModel();
                Models = JsonConvert.DeserializeObject<InvoiceViewModel>(body.ToString());
                var result = service.saveInvoice(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region filterInvoice
        [HttpPost("filterInvoice")]
        public IActionResult filterInvoice([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new InvoiceViewModel();
                Models = JsonConvert.DeserializeObject<InvoiceViewModel>(body.ToString());
                var result = service.filterInvoice(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region filterInvoiceItem
        [HttpPost("filterInvoiceItem")]
        public IActionResult filterInvoiceItem([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new InvoiceViewModel();
                Models = JsonConvert.DeserializeObject<InvoiceViewModel>(body.ToString());
                var result = service.filterInvoiceItem(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region deleteInvoiceItem
        [HttpPost("deleteInvoiceItem")]
        public IActionResult deleteInvoiceItem([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new InvoiceViewModel();
                Models = JsonConvert.DeserializeObject<InvoiceViewModel>(body.ToString());
                var result = service.deleteInvoiceItem(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region loadMemo
        [HttpPost("loadMemo")]
        public IActionResult loadMemo([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new BinBalanceServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<BinBalanceServiceChargeViewModel>(body.ToString());
                var result = service.loadMemo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        [HttpPost]
        [Route("ExportStorage")]
        //[ResponseType(typeof(int))]
        public IActionResult ExportStorage([FromBody]JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                InvoiceService _appService = new InvoiceService();
                var Models = new ReportInvoiceStorageChargeViewModel();
                Models = JsonConvert.DeserializeObject<ReportInvoiceStorageChargeViewModel>(body.ToString());
                StockMovementPath = _appService.ExportStorage(Models, _hostingEnvironment.ContentRootPath);

                if (!System.IO.File.Exists(StockMovementPath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(StockMovementPath), "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                System.IO.File.Delete(StockMovementPath);
            }
        }

        [HttpPost]
        [Route("ExportInvoice")]
        //[ResponseType(typeof(int))]
        public IActionResult ExportInvoice([FromBody]JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                InvoiceService _appService = new InvoiceService();
                var Models = new ReportInvoiceViewModel();
                Models = JsonConvert.DeserializeObject<ReportInvoiceViewModel>(body.ToString());
                StockMovementPath = _appService.ExportInvoice(Models, _hostingEnvironment.ContentRootPath);

                if (!System.IO.File.Exists(StockMovementPath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(StockMovementPath), "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                System.IO.File.Delete(StockMovementPath);
            }
        }

        #region deleteInvoice
        [HttpPost("deleteInvoice")]
        public IActionResult deleteInvoice([FromBody]JObject body)
        {
            try
            {
                var service = new InvoiceService();
                var Models = new InvoiceViewModel();
                Models = JsonConvert.DeserializeObject<InvoiceViewModel>(body.ToString());
                var result = service.deleteInvoice(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


    }
}

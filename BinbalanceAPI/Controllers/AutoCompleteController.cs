using System;
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
    [Route("api/PickBinbalance")]
    [ApiController]
    public class AutoCompleteController : ControllerBase
    {

        #region AutoCompleteProductId
        [HttpPost("AutoCompleteProductId")]
        public IActionResult AutoCompleteProductId([FromBody]JObject body)

        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleteProductId(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleteProductName
        [HttpPost("AutoCompleteProductName")]
        public IActionResult AutoCompleteProductName([FromBody]JObject body)

        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleteProductName(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleteGoodsReceive
        [HttpPost("AutoCompleteGoodsReceive")]
        public IActionResult AutoCompleteGoodsReceive([FromBody]JObject body)

        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleteGoodsReceive(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleteProductLot
        [HttpPost("AutoCompleteProductLot")]
        public IActionResult AutoCompleteProductLot([FromBody]JObject body)

        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleteProductLot(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleteTag
        [HttpPost("AutoCompleteTag")]
        public IActionResult AutoCompleteTag([FromBody]JObject body)

        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleteTag(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleterProdctLot
        [HttpPost("AutoCompleterProdctLot")]
        public IActionResult AutoCompleterProdctLot([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleterProdctLot(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleterOwnerId
        [HttpPost("AutoCompleterOwnerId")]
        public IActionResult AutoCompleterOwnerId([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleterOwnerId(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleterOwnerName
        [HttpPost("AutoCompleterOwnerName")]
        public IActionResult AutoCompleterOwnerName([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleterOwnerName(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleterGR
        [HttpPost("AutoCompleterGR")]
        public IActionResult AutoCompleterGR([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleterGR(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleterGRandGI
        [HttpPost("AutoCompleterGRandGI")]
        public IActionResult AutoCompleterGRandGI([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleterGRandGI(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleterTag
        [HttpPost("AutoCompleterTag")]
        public IActionResult AutoCompleterTag([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleterTag(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoCompleteProductType
        [HttpPost("AutoCompleteProductType")]
        public IActionResult AutoCompleteProductType([FromBody]JObject body)

        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoCompleteProductType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutoZone
        [HttpPost("autoZonefilter")]
        public IActionResult autoProdutfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoZone(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationFilter
        [HttpPost("autoLocationFilter")]
        public IActionResult autoLocationFilter([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoItemStatus
        [HttpPost("autoItemStatus")]
        public IActionResult autoItemStatus([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoItemStatus(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region ServiceCharge
        [HttpPost("ServiceCharge")]
        public IActionResult ServiceCharge([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoServiceCharge(Models);
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoItemInvoice
        [HttpPost("autoItemInvoice")]
        public IActionResult autoItemInvoice([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoItemInvoice(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region Memo
        [HttpPost("autoCompleteMemo")]
        public IActionResult autoCompleteMemo([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoCompleteMemo(Models);
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

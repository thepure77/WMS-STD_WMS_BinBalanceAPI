using System;
using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.BinBalanceService;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;

namespace BinbalanceAPI.Controllers
{
    [Route("api/BinCard")]
    [ApiController]
    public class BinCard : ControllerBase
    {
        [HttpPost("InsertBinCard")]
        public IActionResult InsertBinCard([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardService();
                var Models = JsonConvert.DeserializeObject<BinCardViewModel>(body.ToString());
                var result = service.InsertBinCard(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("InsertBinCardBy_Scanpick")]
        public IActionResult InsertBinCardBy_Scanpick([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardService();
                var Models = JsonConvert.DeserializeObject<BinCardViewModel>(body.ToString());
                var result = service.InsertBinCardBy_Scanpick(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("InsertBinCardV2")]
        public IActionResult InsertBinCardV2([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardService();
                var Models = JsonConvert.DeserializeObject<BinCardViewModel>(body.ToString());
                var result = service.InsertBinCardV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CancelBinCardGRToList")]
        public IActionResult CancelBinCardGRToList([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardService();
                var Models = JsonConvert.DeserializeObject<ListBinCardViewModel>(body.ToString());
                var result = service.CancelBinCardGRToList(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("InsertBinCardTransfer")]
        public IActionResult InsertBinCardTransfer([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardService();
                var Models = JsonConvert.DeserializeObject<BinCardViewModel>(body.ToString());
                var result = service.InsertBinCardTransfer(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("InsertBinCardTransferNotCreateTagItem")]
        public IActionResult InsertBinCardTransferNotCreateTagItem([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardService();
                var Models = JsonConvert.DeserializeObject<BinCardViewModel>(body.ToString());
                var result = service.InsertBinCardTransferNotCreateTagItem(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

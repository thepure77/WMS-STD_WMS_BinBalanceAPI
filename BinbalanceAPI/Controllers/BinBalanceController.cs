using System;
using System.Collections.Generic;
using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.BinBalanceService;
using BinbalanceBusiness.ReplenishmentBalance;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;
using MasterDataBusiness.ViewModels;

namespace BinbalanceAPI.Controllers
{
    [Route("api/Binbalance")]
    [ApiController]
    public class BinBalance : ControllerBase
    {
        [HttpPost("find")]
        public IActionResult find([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceViewModel>(body.ToString());
                var result = service.find(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("findLocationNow")]
        public IActionResult findLocationNow([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceViewModel>(body.ToString());
                var result = service.findLocationNow(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getBinbalance")]
        public IActionResult getBinbalance([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<FilterBinbalanceViewModel>(body.ToString());
                var result = service.getBinbalance(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getViewBinbalance")]
        public IActionResult getViewBinbalance([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<FilterBinbalanceViewModel>(body.ToString());
                var result = service.getViewBinbalance(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("updateIsuseViewBinbalance")]
        public IActionResult updateIsuseViewBinbalance([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<FilterBinbalanceViewModel>(body.ToString());
                var result = service.updateIsuseViewBinbalance(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("insertBinCardReserve")]
        public IActionResult insertBinCardReserve([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceFromGIViewModel>(body.ToString());
                var result = service.insertBinCardReserve(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost("CutSlotsBinBalance")]
        public IActionResult CutSlotsBinBalance([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<goodsIssueItemLocationFilterViewModel>(body.ToString());
                var result = service.CutSlotsBinBalance(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateBinbalanceQIareUU")]
        public IActionResult UpdateBinbalanceQIareUU([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<GoodsTransferItemViewModel>(body.ToString());
                var result = service.UpdateBinbalanceQIareUU(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SearchReplenishmentBinBalance")]
        public IActionResult SearchReplenishmentBinBalance([FromBody]JObject body)
        {
            try
            {
                ReplenishmentBalanceService service = new ReplenishmentBalanceService();
                var result = service.SearchReplenishmentBinBalance(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SearchReplenishmentBinBalanceASRS")]
        public IActionResult SearchReplenishmentBinBalanceASRS([FromBody]JObject body)
        {
            try
            {
                ReplenishmentBalanceService service = new ReplenishmentBalanceService();
                var result = service.SearchReplenishmentBinBalanceASRS(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SearchReplenishmentBinBalancePIECEPICK")]
        public IActionResult SearchReplenishmentBinBalancePIECEPICK([FromBody]JObject body)
        {
            try
            {
                ReplenishmentBalanceService service = new ReplenishmentBalanceService();
                var result = service.SearchReplenishmentBinBalancePIECEPICK(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SearchReplenishmentBinBalancePIECEPICK_V2")]
        public IActionResult SearchReplenishmentBinBalancePIECEPICK_V2([FromBody]JObject body)
        {
            try
            {
                ReplenishmentBalanceService service = new ReplenishmentBalanceService();
                var result = service.SearchReplenishmentBinBalancePIECEPICK_V2(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ReserveBinBalance")]
        public IActionResult ReserveBinBalance([FromBody]JObject body)
        {
            try
            {
                BinBalanceService service = new BinBalanceService();
                var result = service.ReserveBinBalance(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("checkProductLocation")]
        public IActionResult checkProductLocation([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<chekcProductLocationViewModel>(body.ToString());
                var result = service.checkProductLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("checkLocation")]
        public IActionResult checkLocation([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<chekcProductLocationViewModel>(body.ToString());
                var result = service.checkLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getBinbalanceGreaterThanZero")]
        public IActionResult getBinbalanceGreaterThanZero([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<FilterBinbalanceViewModel>(body.ToString());
                var result = service.getBinbalanceGreaterThanZero(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getBinbalanceGreaterThanZeroV2")]
        public IActionResult getBinbalanceGreaterThanZeroV2([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<FilterBinbalanceViewModel>(body.ToString());
                var result = service.getBinbalanceGreaterThanZeroV2(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getBinbalanceGreaterThanZeroV3")]
        public IActionResult getBinbalanceGreaterThanZeroV3([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<FilterBinbalanceViewModel>(body.ToString());
                var result = service.getBinbalanceGreaterThanZeroV3(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

        [HttpPost("checkTransferLocation")]
        public IActionResult checkTransferLocation([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<chekcProductLocationViewModel>(body.ToString());
                var result = service.checkTransferLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getCheckStock")]
        public IActionResult getCheckStock([FromBody]JObject body)
        {
            try
            {
                var service = new BinBalanceService();
                var Models = JsonConvert.DeserializeObject<CheckStockViewModel>(body.ToString());
                var result = service.getCheckStock(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

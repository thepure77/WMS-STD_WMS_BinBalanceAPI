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
    [Route("api/BinCardReserve")]
    [ApiController]
    public class BinCardReserve : ControllerBase
    {
        [HttpPost("chkBinCardReserve")]
        public IActionResult chkBinCardReserve([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardReserveService();
                var Models = JsonConvert.DeserializeObject<BinCardReserveViewModel>(body.ToString());
                var result = service.chkBinCardReserve(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getBinBalancearFromReserve")]
        public IActionResult getBinBalancearFromReserve([FromBody]JObject body)
        {
            try
            {
                var service = new BinCardReserveService();
                var Models = JsonConvert.DeserializeObject<BinCardReserveViewModel>(body.ToString());
                var result = service.getBinBalancearFromReserve(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

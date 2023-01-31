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
    [Route("api/Movement")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        [HttpPost("FilterOld")]
        public IActionResult FilterOld([FromBody]JObject body)
        {
            try
            {
                var service = new MovementService();
                var Models = JsonConvert.DeserializeObject<FilterSearchMovementViewModel>(body.ToString());
                var result = service.filterOld(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Filter")]
        public IActionResult Filter([FromBody]JObject body)
        {
            try
            {
                var service = new MovementService();
                var Models = JsonConvert.DeserializeObject<FilterSearchMovementViewModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

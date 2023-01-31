using System;
using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.BinBalanceService;
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
    [Route("api/Aging")]
    [ApiController]
    public class AgingController : ControllerBase
    {

        #region filter
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new AgingService();
                var Models = new View_agingViewModel();
                Models = JsonConvert.DeserializeObject<View_agingViewModel>(body.ToString());
                var result = service.filter(Models);
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

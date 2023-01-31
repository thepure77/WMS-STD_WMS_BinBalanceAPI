using System;
using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.BinBalanceService;
using BinbalanceBusiness.PickBinbalance;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using BinbalanceBusiness.Stock;
using BinbalanceBusiness.Stock.ViewModels;
using BinBalanceBusiness.ViewModels;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;

namespace BinbalanceAPI.Controllers
{
    [Route("api/StockOnRollcage")]
    [ApiController]
    public class StockOnRollcageController : ControllerBase
    {

        #region filterByProduct
        [HttpPost("filterByProduct")]
        public IActionResult filterByProduct([FromBody]JObject body)
        {
            try
            {
                var service = new StockOnRollcageService();
                var Models = new StockOnRollcageBinbalanceViewModel();
                Models = JsonConvert.DeserializeObject<StockOnRollcageBinbalanceViewModel>(body.ToString());
                var result = service.filterByProduct(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region filterByLocatuion
        [HttpPost("filterByLocatuion")]
        public IActionResult filterByLocatuion([FromBody]JObject body)
        {
            try
            {
                var service = new StockOnRollcageService();
                var Models = new StockOnRollcageBinbalanceViewModel();
                Models = JsonConvert.DeserializeObject<StockOnRollcageBinbalanceViewModel>(body.ToString());
                var result = service.filterByLocatuion(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region checkStockMobile
        [HttpPost("CheckStockMobile/mobileFilterByLocation")]
        public IActionResult mobileFilterByLocation([FromBody]JObject body)
        {
            try
            {
                var service = new CheckStockOnRollcageMobileService();
                var Models = new CheckStockOnRollcageMobileViewModel();
                Models = JsonConvert.DeserializeObject<CheckStockOnRollcageMobileViewModel>(body.ToString());
                var result = service.mobileFilterByLocation(Models);
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

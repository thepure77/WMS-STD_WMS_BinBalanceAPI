using System;
using BinbalanceBusiness.Binbalance.ViewModels;
using BinbalanceBusiness.InventoryStock;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BinbalanceAPI.Controllers
{
    [Route("api/Memo")]
    [ApiController]
    public class MemoController : ControllerBase
    {

        #region filter
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new MemoService();
                var Models = new SearchMemoViewModel();
                Models = JsonConvert.DeserializeObject<SearchMemoViewModel>(body.ToString());
                var result = service.search(Models);
                return Ok(result);
                //return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region filterView
        [HttpPost("filterView")]
        public IActionResult filterView([FromBody]JObject body)
        {
            try
            {
                var service = new MemoService();
                var Models = new SearchMemoViewModel();
                Models = JsonConvert.DeserializeObject<SearchMemoViewModel>(body.ToString());
                var result = service.searchView(Models);
                return Ok(result);
                //return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region filteritem
        [HttpGet("filteritem/{id}")]
        public IActionResult filteritem(string id)
        {
            try
            {
                var service = new MemoService();
                var result = service.searchitem(id);
                return Ok(result);
                //return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region CreateUpdate
        [HttpPost("CreateUpdate")]
        public IActionResult CreateUpdate([FromBody]JObject body)
        {
            try
            {
                var service = new MemoService();
                var Models = new MemoSearchViewModel();
                Models = JsonConvert.DeserializeObject<MemoSearchViewModel>(body.ToString());
                var result = service.CreateOrUpdate(Models);
                return Ok(result);
                //return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

       
    }
}

using System;
using System.Collections.Generic;
using BinbalanceBusiness.PickBinbalance;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;

namespace BinbalanceAPI.Controllers
{
    [Route("api/PickBinbalance")]
    [ApiController]
    public class PickBinbalanceController : ControllerBase
    {
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<FilterPickbinbalanceViewModel>(body.ToString());
                var result = service.Filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterUnpack")]
        public IActionResult filterUnpack([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<FilterPickbinbalanceViewModel>(body.ToString());
                var result = service.UnpackFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterAB03")]
        public IActionResult filterAB03([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<FilterPickbinbalanceViewModel>(body.ToString());
                var result = service.filterAB03(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterbinbalance_unpack")]
        public IActionResult filterbinbalance_unpack([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceViewModel>(body.ToString());
                var result = service.filterbinbalance_unpack(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("filterbinbalance_pack")]
        public IActionResult filterbinbalance_pack([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                PickModel Models = JsonConvert.DeserializeObject<PickModel>(body.ToString());
                var result = service.filterbinbalance_pack(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("pickProduct_tranfer")]
        public IActionResult pickProduct_tranfer([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceFromGIViewModel>(body.ToString());
                var result = service.pickProduct_tranfer(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("pickProduct")]
        public IActionResult pickProduct([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceFromGIViewModel>(body.ToString());
                var result = service.pickProduct(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("pickProductUnpack")]
        public IActionResult pickProductUnpack([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceFromGIViewModel>(body.ToString());
                var result = service.pickProductUnpack(Models); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("deletePickProduct")]
        public IActionResult deletePickProduct([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<PickbinbalanceFromGIViewModel>(body.ToString());
                var result = service.deletePickProduct(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ListPickProduct")]
        public IActionResult ListPickProduct([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ListPickbinbalanceFromGIViewModel>(body.ToString());
                var result = service.ListPickProduct(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("deletePickProductQI")]
        public IActionResult deletePickProductQI([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ListPickbinbalanceFromGIViewModel>(body.ToString());
                var result = service.deletePickProductQI(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

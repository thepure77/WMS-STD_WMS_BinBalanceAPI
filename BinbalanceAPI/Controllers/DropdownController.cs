using System;
using BinbalanceBusiness.PickBinbalance;
using BinbalanceBusiness.PickBinbalance.ViewModels;
using BinBalanceBusiness.ViewModels;
using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BinbalanceBusiness.PickBinbalance.ViewModels.FilterPickbinbalanceViewModel;

namespace BinbalanceAPI.Controllers
{
    [Route("api/PickBinbalance")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        #region DropdownWarehouse
        [HttpPost("dropdownWarehouse")]
        public IActionResult DropdownWarehouse([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<WarehouseViewModel>(body.ToString());
                var result = service.dropdownWarehouse(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownItemStatus
        [HttpPost("dropdownItemStatus")]
        public IActionResult dropdownItemStatus([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ItemStatusDocViewModel>(body.ToString());
                var result = service.dropdownItemStatus(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownZone
        [HttpPost("dropdownZone")]
        public IActionResult dropdownZone([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ZoneViewModel>(body.ToString());
                var result = service.dropdownZone(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownLocation
        [HttpPost("dropdownLocation")]
        public IActionResult dropdownLocation([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<LocationViewModel>(body.ToString());
                var result = service.dropdownLocation(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownProductType
        [HttpPost("dropdownProductType")]
        public IActionResult dropdownProductType([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = JsonConvert.DeserializeObject<ProductTypeViewModel>(body.ToString());
                var result = service.dropdownProductType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownProductconversion
        [HttpPost("dropdownProductconversion")]
        public IActionResult dropdownProductconversion([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ProductConversionViewModelDoc();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModelDoc>(body.ToString());
                var result = service.dropdownProductconversion(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownDocumentTypeMEMO
        [HttpPost("dropdownDocumentTypeMEMO")]
        public IActionResult dropdownDocumentTypeGR([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.dropdownDocumentTypeMEMO(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
        #region dropDownStorageCharge
        [HttpPost("dropDownStorageCharge")]
        public IActionResult dropDownStorageCharge([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new storageChargeModel();
                Models = JsonConvert.DeserializeObject<storageChargeModel>(body.ToString());
                var result = service.dropDownStorageCharge(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownDocumentType
        [HttpPost("dropdownDocumentType")]
        public IActionResult dropdownDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new GenDocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<GenDocumentTypeViewModel>(body.ToString());
                var result = service.dropdownDocumentType(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region dropDownServiceCharge
        [HttpPost("dropDownServiceCharge")]
        public IActionResult dropDownServiceCharge([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new ServiceChargeViewModel();
                Models = JsonConvert.DeserializeObject<ServiceChargeViewModel>(body.ToString());
                var result = service.dropDownServiceCharge(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region dropDownServiceChargeFix
        [HttpPost("dropDownServiceChargeFix")]
        public IActionResult dropDownServiceChargeFix([FromBody]JObject body)
        {
            try
            {
                var service = new PickBinbalance();
                var Models = new View_ServiceChargeFixViewModel();
                Models = JsonConvert.DeserializeObject<View_ServiceChargeFixViewModel>(body.ToString());
                var result = service.dropDownServiceChargeFix(Models);

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

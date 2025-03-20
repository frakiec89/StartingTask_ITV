using Microsoft.AspNetCore.Mvc;
using StartingTask_ITV.Core.Interfaces;
using StartingTask_ITV.Core.Model;

namespace StartingTask_ITV.API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController (IDeviceService service)
        {
            _deviceService = service;
        }

        [HttpGet]
        public async Task<ActionResult< IEnumerable<Device>>> GetDevices()
        {
            try
            {
                var resaul =   await _deviceService.GetDevicesAsync();
                return Ok(resaul);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{type}")]
        public async Task<IActionResult> AddDevice(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
               return  BadRequest("тип устройства не может быть пустым");

            try
            {
                await _deviceService.AddDeviceAsync(type);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(uint id)
        {
            try
            {
                await _deviceService.DeleteDeviceAsync((int)id);
                return  Ok();
            }
            catch (NotImplementedException ex)
            {
                return StatusCode(410, ex.Message);
            }
            catch( Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("type/{type}")]
        public async Task<IActionResult> DeleteDeviceType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
               return BadRequest("тип устройства не может быть пустым");

            try
            {
                await _deviceService.DeleteDevicesTypeAsync(type);
               return  Ok();
            }
            catch (NotImplementedException ex)
            {
                return StatusCode(410, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

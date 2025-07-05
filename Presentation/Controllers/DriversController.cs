using Microsoft.AspNetCore.Mvc;
using Flota365.API.Application.Services;
using Flota365.API.Application.DTOs.Driver;

namespace Flota365.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly DriverService _driverService;
        
        public DriversController(DriverService driverService)
        {
            _driverService = driverService;
        }
        
        /// <summary>
        /// Get all drivers
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers()
        {
            try
            {
                var drivers = await _driverService.GetAllDriversAsync();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving drivers",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Get driver by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> GetDriver(int id)
        {
            try
            {
                var driver = await _driverService.GetDriverByIdAsync(id);
                if (driver == null)
                    return NotFound("Driver not found");
                    
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving driver",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Create new driver
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<DriverDto>> CreateDriver([FromBody] CreateDriverDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var driver = await _driverService.CreateDriverAsync(createDto);
                return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { 
                    success = false, 
                    message = ex.Message 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error creating driver",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Update driver
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<DriverDto>> UpdateDriver(int id, [FromBody] UpdateDriverDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var driver = await _driverService.UpdateDriverAsync(id, updateDto);
                if (driver == null)
                    return NotFound("Driver not found");
                
                return Ok(driver);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { 
                    success = false, 
                    message = ex.Message 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error updating driver",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Delete driver (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver(int id)
        {
            try
            {
                var result = await _driverService.DeleteDriverAsync(id);
                if (!result)
                    return NotFound("Driver not found");
                
                return Ok(new {
                    success = true,
                    message = "Driver deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error deleting driver",
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Get driver statistics
        /// </summary>
        [HttpGet("stats")]
        public async Task<ActionResult<DriverStatsDto>> GetDriverStats()
        {
            try
            {
                var stats = await _driverService.GetDriverStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving driver statistics",
                    error = ex.Message 
                });
            }
        }
    }
}

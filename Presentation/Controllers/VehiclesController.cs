using Microsoft.AspNetCore.Mvc;
using Flota365.API.Application.Services;
using Flota365.API.Application.DTOs.Vehicle;

namespace Flota365.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleService _vehicleService;
        
        public VehiclesController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        
        /// <summary>
        /// Get all vehicles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehiclesAsync();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving vehicles",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Get vehicle by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null)
                    return NotFound("Vehicle not found");
                    
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving vehicle",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Create new vehicle
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<VehicleDto>> CreateVehicle([FromBody] CreateVehicleDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vehicle = await _vehicleService.CreateVehicleAsync(createDto);
                return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
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
                    message = "Error creating vehicle",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Update vehicle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleDto>> UpdateVehicle(int id, [FromBody] UpdateVehicleDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vehicle = await _vehicleService.UpdateVehicleAsync(id, updateDto);
                if (vehicle == null)
                    return NotFound("Vehicle not found");
                
                return Ok(vehicle);
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
                    message = "Error updating vehicle",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Delete vehicle (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            try
            {
                var result = await _vehicleService.DeleteVehicleAsync(id);
                if (!result)
                    return NotFound("Vehicle not found");
                
                return Ok(new {
                    success = true,
                    message = "Vehicle deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error deleting vehicle",
                    error = ex.Message 
                });
            }
        }
    }
}

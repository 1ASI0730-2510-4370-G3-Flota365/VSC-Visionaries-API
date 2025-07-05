using Microsoft.AspNetCore.Mvc;
using Flota365.API.Application.Services;
using Flota365.API.Application.DTOs.Fleet;

namespace Flota365.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetsController : ControllerBase
    {
        private readonly FleetService _fleetService;
        
        public FleetsController(FleetService fleetService)
        {
            _fleetService = fleetService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FleetDto>>> GetFleets()
        {
            try
            {
                var fleets = await _fleetService.GetAllFleetsAsync();
                return Ok(fleets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving fleets",
                    error = ex.Message 
                });
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<FleetDto>> GetFleet(int id)
        {
            try
            {
                var fleet = await _fleetService.GetFleetByIdAsync(id);
                if (fleet == null)
                    return NotFound("Fleet not found");
                    
                return Ok(fleet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving fleet",
                    error = ex.Message 
                });
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<FleetDto>> CreateFleet([FromBody] CreateFleetDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var fleet = await _fleetService.CreateFleetAsync(createDto);
                return CreatedAtAction(nameof(GetFleet), new { id = fleet.Id }, fleet);
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
                    message = "Error creating fleet",
                    error = ex.Message 
                });
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<FleetDto>> UpdateFleet(int id, [FromBody] UpdateFleetDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var fleet = await _fleetService.UpdateFleetAsync(id, updateDto);
                if (fleet == null)
                    return NotFound("Fleet not found");
                
                return Ok(fleet);
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
                    message = "Error updating fleet",
                    error = ex.Message 
                });
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFleet(int id)
        {
            try
            {
                var result = await _fleetService.DeleteFleetAsync(id);
                if (!result)
                    return NotFound("Fleet not found");
                
                return Ok(new {
                    success = true,
                    message = "Fleet deleted successfully"
                });
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
                    message = "Error deleting fleet",
                    error = ex.Message 
                });
            }
        }
    }
}

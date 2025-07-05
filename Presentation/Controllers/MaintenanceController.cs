using Microsoft.AspNetCore.Mvc;
using Flota365.API.Application.Services;
using Flota365.API.Application.DTOs.Maintenance;

namespace Flota365.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly MaintenanceService _maintenanceService;
        
        public MaintenanceController(MaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }
        
        [HttpGet("records")]
        public async Task<ActionResult<IEnumerable<MaintenanceDto>>> GetMaintenanceRecords()
        {
            try
            {
                var maintenance = await _maintenanceService.GetAllMaintenanceAsync();
                return Ok(maintenance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving maintenance records",
                    error = ex.Message 
                });
            }
        }
        
        [HttpGet("records/{id}")]
        public async Task<ActionResult<MaintenanceDto>> GetMaintenanceRecord(int id)
        {
            try
            {
                var maintenance = await _maintenanceService.GetMaintenanceByIdAsync(id);
                if (maintenance == null)
                    return NotFound("Maintenance record not found");
                    
                return Ok(maintenance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving maintenance record",
                    error = ex.Message 
                });
            }
        }

        [HttpGet("records/vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<MaintenanceDto>>> GetMaintenanceByVehicle(int vehicleId)
        {
            try
            {
                var maintenance = await _maintenanceService.GetMaintenanceByVehicleIdAsync(vehicleId);
                return Ok(maintenance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving maintenance records for vehicle",
                    error = ex.Message 
                });
            }
        }
        
        [HttpPost("records")]
        public async Task<ActionResult<MaintenanceDto>> CreateMaintenanceRecord([FromBody] CreateMaintenanceDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var maintenance = await _maintenanceService.CreateMaintenanceAsync(createDto);
                return CreatedAtAction(nameof(GetMaintenanceRecord), new { id = maintenance.Id }, maintenance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error creating maintenance record",
                    error = ex.Message 
                });
            }
        }
        
        [HttpPut("records/{id}")]
        public async Task<ActionResult<MaintenanceDto>> UpdateMaintenanceRecord(int id, [FromBody] UpdateMaintenanceDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var maintenance = await _maintenanceService.UpdateMaintenanceAsync(id, updateDto);
                if (maintenance == null)
                    return NotFound("Maintenance record not found");
                
                return Ok(maintenance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error updating maintenance record",
                    error = ex.Message 
                });
            }
        }
        
        [HttpDelete("records/{id}")]
        public async Task<ActionResult> DeleteMaintenanceRecord(int id)
        {
            try
            {
                var result = await _maintenanceService.DeleteMaintenanceAsync(id);
                if (!result)
                    return NotFound("Maintenance record not found");
                
                return Ok(new {
                    success = true,
                    message = "Maintenance record deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error deleting maintenance record",
                    error = ex.Message 
                });
            }
        }

        [HttpGet("records/overdue")]
        public async Task<ActionResult<IEnumerable<MaintenanceDto>>> GetOverdueMaintenance()
        {
            try
            {
                var overdue = await _maintenanceService.GetOverdueMaintenanceAsync();
                return Ok(overdue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving overdue maintenance",
                    error = ex.Message 
                });
            }
        }
        
        [HttpGet("services")]
        public async Task<ActionResult<IEnumerable<ServiceRecordDto>>> GetServiceRecords()
        {
            try
            {
                var services = await _maintenanceService.GetAllServiceRecordsAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving service records",
                    error = ex.Message 
                });
            }
        }
        
        [HttpGet("services/{id}")]
        public async Task<ActionResult<ServiceRecordDto>> GetServiceRecord(int id)
        {
            try
            {
                var service = await _maintenanceService.GetServiceRecordByIdAsync(id);
                if (service == null)
                    return NotFound("Service record not found");
                    
                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving service record",
                    error = ex.Message 
                });
            }
        }

        [HttpGet("services/vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<ServiceRecordDto>>> GetServicesByVehicle(int vehicleId)
        {
            try
            {
                var services = await _maintenanceService.GetServiceRecordsByVehicleIdAsync(vehicleId);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving service records for vehicle",
                    error = ex.Message 
                });
            }
        }
        
        [HttpPost("services")]
        public async Task<ActionResult<ServiceRecordDto>> CreateServiceRecord([FromBody] CreateServiceRecordDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var service = await _maintenanceService.CreateServiceRecordAsync(createDto);
                return CreatedAtAction(nameof(GetServiceRecord), new { id = service.Id }, service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error creating service record",
                    error = ex.Message 
                });
            }
        }
        
        [HttpDelete("services/{id}")]
        public async Task<ActionResult> DeleteServiceRecord(int id)
        {
            try
            {
                var result = await _maintenanceService.DeleteServiceRecordAsync(id);
                if (!result)
                    return NotFound("Service record not found");
                
                return Ok(new {
                    success = true,
                    message = "Service record deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error deleting service record",
                    error = ex.Message 
                });
            }
        }
    }
}

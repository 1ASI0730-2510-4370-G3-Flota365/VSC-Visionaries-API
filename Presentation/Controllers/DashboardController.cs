using Microsoft.AspNetCore.Mvc;
using Flota365.API.Application.Services;
using Flota365.API.Application.DTOs.Dashboard;

namespace Flota365.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;
        
        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        
        /// <summary>
        /// Get dashboard statistics
        /// </summary>
        [HttpGet("stats")]
        public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
        {
            try
            {
                var stats = await _dashboardService.GetDashboardStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving dashboard statistics",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Get active vehicles for dashboard
        /// </summary>
        [HttpGet("active-vehicles")]
        public async Task<ActionResult<List<ActiveVehicleDto>>> GetActiveVehicles()
        {
            try
            {
                var vehicles = await _dashboardService.GetActiveVehiclesAsync();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving active vehicles",
                    error = ex.Message 
                });
            }
        }
        
        /// <summary>
        /// Get fleet summary for dashboard
        /// </summary>
        [HttpGet("fleet-summary")]
        public async Task<ActionResult<FleetSummaryDto>> GetFleetSummary()
        {
            try
            {
                var summary = await _dashboardService.GetFleetSummaryAsync();
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving fleet summary",
                    error = ex.Message 
                });
            }
        }
    }
}

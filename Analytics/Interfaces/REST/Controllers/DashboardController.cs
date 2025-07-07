using Microsoft.AspNetCore.Mvc;
using MediatR;
using Flota365.Platform.API.Analytics.Domain.Model.Queries;
using Flota365.Platform.API.Analytics.Interfaces.REST.Resources;

namespace Flota365.Platform.API.Analytics.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get dashboard statistics
        /// </summary>
        [HttpGet("dashboard/stats")]
        public async Task<ActionResult<DashboardStatsResource>> GetDashboardStats()
        {
            try
            {
                var query = new GetDashboardStatsQuery();
                var stats = await _mediator.Send(query);
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
        /// Get fleet summary for dashboard
        /// </summary>
        [HttpGet("dashboard/fleet-summary")]
        public async Task<ActionResult<FleetSummaryResource>> GetFleetSummary()
        {
            try
            {
                var query = new GetFleetSummaryQuery();
                var summary = await _mediator.Send(query);
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

        /// <summary>
        /// Get active vehicles for dashboard
        /// </summary>
        [HttpGet("dashboard/active-vehicles")]
        public async Task<ActionResult<IEnumerable<ActiveVehicleResource>>> GetActiveVehicles()
        {
            try
            {
                var query = new GetActiveVehiclesQuery();
                var vehicles = await _mediator.Send(query);
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
    }
}
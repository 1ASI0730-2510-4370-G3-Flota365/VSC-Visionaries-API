using Microsoft.AspNetCore.Mvc;
using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Commands;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

// FleetsController.cs
namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/fleet-management/fleets")]
    public class FleetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FleetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FleetResource>>> GetFleets([FromQuery] bool activeOnly = false, [FromQuery] string? type = null)
        {
            try
            {
                object query = (activeOnly, type) switch
                {
                    (true, _) => new GetActiveFleetQuery(),
                    (_, not null) => new GetFleetByTypeQuery(type),
                    _ => new GetAllFleetsQuery()
                };

                var fleets = await _mediator.Send(query);
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
        public async Task<ActionResult<FleetResource>> GetFleet(int id)
        {
            try
            {
                var query = new GetFleetByIdQuery(id);
                var fleet = await _mediator.Send(query);
                
                if (fleet == null)
                    return NotFound(new { success = false, message = "Fleet not found" });
                    
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
        public async Task<ActionResult<FleetResource>> CreateFleet([FromBody] CreateFleetResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = CreateFleetCommandFromResourceAssembler.ToCommandFromResource(resource);
                var fleetId = await _mediator.Send(command);
                
                var query = new GetFleetByIdQuery(fleetId);
                var fleet = await _mediator.Send(query);
                
                return CreatedAtAction(nameof(GetFleet), new { id = fleetId }, fleet);
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
        public async Task<ActionResult<FleetResource>> UpdateFleet(int id, [FromBody] UpdateFleetResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = UpdateFleetCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "Fleet not found" });
                
                var query = new GetFleetByIdQuery(id);
                var fleet = await _mediator.Send(query);
                
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
                var command = new DeleteFleetCommand(id);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "Fleet not found" });
                
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

// VehiclesController.cs
namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/fleet-management/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleResource>>> GetVehicles([FromQuery] int? fleetId = null, [FromQuery] string? status = null)
        {
            try
            {
                object query = (fleetId, status) switch
                {
                    (not null, _) => new GetVehiclesByFleetIdQuery(fleetId.Value),
                    (_, "maintenance") => new GetVehiclesInMaintenanceQuery(),
                    (_, "service-due") => new GetVehiclesDueForServiceQuery(),
                    (_, not null) => new GetVehiclesByStatusQuery(status),
                    _ => new GetAllVehiclesQuery()
                };

                var vehicles = await _mediator.Send(query);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleResource>> GetVehicle(int id)
        {
            try
            {
                var query = new GetVehicleByIdQuery(id);
                var vehicle = await _mediator.Send(query);
                
                if (vehicle == null)
                    return NotFound(new { success = false, message = "Vehicle not found" });
                    
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

        [HttpPost]
        public async Task<ActionResult<VehicleResource>> CreateVehicle([FromBody] CreateVehicleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(resource);
                var vehicleId = await _mediator.Send(command);
                
                var query = new GetVehicleByIdQuery(vehicleId);
                var vehicle = await _mediator.Send(query);
                
                return CreatedAtAction(nameof(GetVehicle), new { id = vehicleId }, vehicle);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleResource>> UpdateVehicle(int id, [FromBody] UpdateVehicleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = UpdateVehicleCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "Vehicle not found" });
                
                var query = new GetVehicleByIdQuery(id);
                var vehicle = await _mediator.Send(query);
                
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            try
            {
                var command = new DeleteVehicleCommand(id);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "Vehicle not found" });
                
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

        [HttpPost("{id}/assign-fleet")]
        public async Task<ActionResult> AssignToFleet(int id, [FromBody] AssignVehicleToFleetResource resource)
        {
            try
            {
                var command = AssignVehicleCommandFromResourceAssembler.ToFleetCommandFromResource(id, resource);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "Vehicle not found" });
                
                return Ok(new { success = true, message = "Vehicle assigned to fleet successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("{id}/update-mileage")]
        public async Task<ActionResult> UpdateMileage(int id, [FromBody] UpdateMileageResource resource)
        {
            try
            {
                var command = UpdateMileageCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "Vehicle not found" });
                
                return Ok(new { success = true, message = "Mileage updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
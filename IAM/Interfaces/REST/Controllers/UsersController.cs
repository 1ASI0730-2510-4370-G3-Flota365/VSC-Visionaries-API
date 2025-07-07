using Microsoft.AspNetCore.Mvc;
using MediatR;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Domain.Model.Queries;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;
using Flota365.Platform.API.IAM.Interfaces.REST.Transform;

namespace Flota365.Platform.API.IAM.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/iam/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<UserResource>> SignUp([FromBody] SignUpResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
                var userId = await _mediator.Send(command);
                
                var query = new GetUserByIdQuery(userId);
                var user = await _mediator.Send(query);
                
                return CreatedAtAction(nameof(GetUserById), new { id = userId }, new
                {
                    success = true,
                    message = "User registered successfully",
                    user = user
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
                    message = "An error occurred during registration",
                    error = ex.Message 
                });
            }
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<UserResource>> SignIn([FromBody] SignInResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
                var userId = await _mediator.Send(command);
                
                var query = new GetUserByIdQuery(userId);
                var user = await _mediator.Send(query);

                return Ok(new { 
                    success = true, 
                    message = "Login successful", 
                    user = user 
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { 
                    success = false, 
                    message = "Invalid email or password" 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred during login",
                    error = ex.Message 
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResource>> GetUserById(int id)
        {
            try
            {
                var query = new GetUserByIdQuery(id);
                var user = await _mediator.Send(query);
                
                if (user == null)
                    return NotFound(new { success = false, message = "User not found" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving user",
                    error = ex.Message 
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetAllUsers([FromQuery] bool activeOnly = false)
        {
            try
            {
                var query = activeOnly ? new GetActiveUsersQuery() : new GetAllUsersQuery();
                var users = await _mediator.Send(query);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving users",
                    error = ex.Message 
                });
            }
        }

        [HttpGet("by-email")]
        public async Task<ActionResult<UserResource>> GetUserByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { success = false, message = "Email is required" });

            try
            {
                var query = new GetUserByEmailQuery(email);
                var user = await _mediator.Send(query);
                
                if (user == null)
                    return NotFound(new { success = false, message = "User not found" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving user",
                    error = ex.Message 
                });
            }
        }

        [HttpPut("{id}/profile")]
        public async Task<ActionResult<UserResource>> UpdateProfile(int id, [FromBody] UpdateProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "User not found" });

                var query = new GetUserByIdQuery(id);
                var user = await _mediator.Send(query);

                return Ok(new
                {
                    success = true,
                    message = "Profile updated successfully",
                    user = user
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
                    message = "An error occurred while updating profile",
                    error = ex.Message 
                });
            }
        }

        [HttpPost("{id}/change-password")]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] ChangePasswordResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (resource.NewPassword != resource.ConfirmPassword)
                return BadRequest(new { 
                    success = false, 
                    message = "New password and confirmation password do not match" 
                });

            try
            {
                var command = ChangePasswordCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return BadRequest(new { 
                        success = false, 
                        message = "Current password is incorrect or user not found" 
                    });

                return Ok(new {
                    success = true,
                    message = "Password changed successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while changing password",
                    error = ex.Message 
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeactivateUser(int id)
        {
            try
            {
                var command = new DeactivateUserCommand(id);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { success = false, message = "User not found" });

                return Ok(new {
                    success = true,
                    message = "User deactivated successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while deactivating user",
                    error = ex.Message 
                });
            }
        }

        [HttpGet("health")]
        public ActionResult HealthCheck()
        {
            return Ok(new {
                success = true,
                message = "IAM service is running",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
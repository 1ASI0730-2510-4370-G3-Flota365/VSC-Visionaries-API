using Microsoft.AspNetCore.Mvc;
using Flota365.API.Application.Services;
using Flota365.API.Application.DTOs.Auth;

namespace Flota365.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="loginDto">Login credentials</param>
        /// <returns>User information if login successful</returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _authService.LoginAsync(loginDto);
                if (user == null)
                    return Unauthorized("Invalid email or password");

                return Ok(new { 
                    success = true, 
                    message = "Login successful", 
                    user = user 
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

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="registerDto">Registration information</param>
        /// <returns>Created user information</returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _authService.RegisterAsync(registerDto);
                return CreatedAtAction(nameof(GetProfile), new { id = user.Id }, new {
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

        /// <summary>
        /// Get user profile by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User profile information</returns>
        [HttpGet("profile/{id}")]
        public async Task<ActionResult<UserDto>> GetProfile(int id)
        {
            try
            {
                var user = await _authService.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound("User not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving profile",
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Get user profile by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User profile information</returns>
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetProfileByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            try
            {
                var user = await _authService.GetUserByEmailAsync(email);
                if (user == null)
                    return NotFound("User not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving profile",
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="updateDto">Updated profile information</param>
        /// <returns>Updated user information</returns>
        [HttpPut("profile/{id}")]
        public async Task<ActionResult<UserDto>> UpdateProfile(int id, [FromBody] UpdateProfileDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _authService.UpdateProfileAsync(id, updateDto);
                if (user == null)
                    return NotFound("User not found");

                return Ok(new {
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

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="changePasswordDto">Password change information</param>
        /// <returns>Success status</returns>
        [HttpPost("change-password/{id}")]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                return BadRequest("New password and confirmation password do not match");

            try
            {
                var result = await _authService.ChangePasswordAsync(id, changePasswordDto);
                if (!result)
                    return BadRequest("Current password is incorrect or user not found");

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

        /// <summary>
        /// Get all users (admin only)
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _authService.GetAllUsersAsync();
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

        /// <summary>
        /// Deactivate user (admin only)
        /// </summary>
        /// <param name="id">User ID to deactivate</param>
        /// <returns>Success status</returns>
        [HttpDelete("users/{id}")]
        public async Task<ActionResult> DeactivateUser(int id)
        {
            try
            {
                var result = await _authService.DeactivateUserAsync(id);
                if (!result)
                    return NotFound("User not found");

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

        /// <summary>
        /// Check if service is running
        /// </summary>
        /// <returns>Service status</returns>
        [HttpGet("health")]
        public ActionResult HealthCheck()
        {
            return Ok(new {
                success = true,
                message = "Auth service is running",
                timestamp = DateTime.UtcNow
            });
        }
    }
}

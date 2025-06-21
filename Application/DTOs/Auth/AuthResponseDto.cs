namespace Flota365.API.Application.DTOs.Auth
{
    /// <summary>
    /// Standard response DTO for auth operations
    /// </summary>
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserDto? User { get; set; }
        public string? Token { get; set; } // Para JWT en el futuro
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
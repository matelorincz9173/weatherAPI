using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Data;

public record RegistrationRequest(
    [Required]string Email, 
    [Required]string Username, 
    [Required]string Password);
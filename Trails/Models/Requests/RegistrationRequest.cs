using System.ComponentModel.DataAnnotations;

public class RegistrationRequest {
  [Required]
  public string Email { get; set;}

  [Required]
  public string ConfirmEmail { get; set;}

  [Required]
  public string Username { get; set; }

  [Required]
  public string Password { get; set; }

  [Required]
  public string ConfirmPassword { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace JWTApp.Front.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username gereklidir.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password gereklidir.")]
        public string? Password { get; set; }
    }
}

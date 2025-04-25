using heseninTaski_Ecommerce.Enums;

namespace heseninTaski_Ecommerce.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}

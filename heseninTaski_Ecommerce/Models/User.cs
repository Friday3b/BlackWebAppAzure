﻿using heseninTaski_Ecommerce.Enums;

namespace heseninTaski_Ecommerce.Models;

public class User
{
    public int Id { get; set; }
    public string ?Username { get; set; }
    public string ?Password { get; set; }
    public UserRole ?Role { get; set; }
}

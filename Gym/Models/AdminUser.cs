using System;
using System.Collections.Generic;

namespace Gym.Models;

public partial class AdminUser
{
    public long Adminid { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int Accessibility { get; set; }
}

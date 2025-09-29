using System;
using System.Collections.Generic;

namespace Gym.Models;

public partial class UserLog
{
    public long Logid { get; set; }

    public long Userid { get; set; }

    public string? Enterytime { get; set; }

    public string? Exittime { get; set; }

    public DateTime? Enterydate { get; set; }

    public DateTime? Exitdate { get; set; }
}

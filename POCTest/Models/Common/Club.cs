using System;
using System.Collections.Generic;

namespace POCTest.Models.Common;

public partial class Club
{
    public int ClubId { get; set; }

    public string CanonicalName { get; set; } = null!;
}

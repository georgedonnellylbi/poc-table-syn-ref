using System;
using System.Collections.Generic;

namespace POCTest.Data.Footage;

public partial class Film
{
    public int FilmId { get; set; }

    public string Title { get; set; } = null!;

    public int ClubId { get; set; }

    public bool Active { get; set; }
}

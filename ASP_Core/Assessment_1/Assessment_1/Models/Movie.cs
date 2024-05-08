using System;
using System.Collections.Generic;

namespace Assessment_1.Models;

public partial class Movie
{
    public int MId { get; set; }

    public string MovieName { get; set; } = null!;

    public DateOnly DateOfRelease { get; set; }
}

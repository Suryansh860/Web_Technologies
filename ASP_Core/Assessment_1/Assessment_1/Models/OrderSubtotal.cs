using System;
using System.Collections.Generic;

namespace Assessment_1.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}

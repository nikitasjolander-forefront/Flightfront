using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFront.Core.Models;

public class Airport
{
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Continent { get; set; } = string.Empty;
    public string Municipality { get; set; } = string.Empty;
    public string IcaoCode { get; set; } = string.Empty;
}

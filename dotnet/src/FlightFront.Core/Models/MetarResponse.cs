using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FlightFront.Core.Models;

public record MetarResponse(
    int Results,
    string[] Data
);

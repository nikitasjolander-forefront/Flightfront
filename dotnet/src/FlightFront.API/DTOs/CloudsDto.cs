using FlightFront.Core.Models;
using System.Text.Json.Serialization;

namespace FlightFront.API.DTOs;

public record CloudsDto
{
    public CloudType? CloudCover { get; set; }
    public string CloudCoverDescription => CloudCover switch
    {
        CloudType.SKC => "Sky Clear",
        CloudType.CLR => "Clear (automated)",
        CloudType.NSC => "No Significant Cloud",
        CloudType.NCD => "No Cloud Detected",
        CloudType.FEW => "Few",
        CloudType.SCT => "Scattered",
        CloudType.BKN => "Broken",
        CloudType.OVC => "Overcast",
        CloudType.VV => "Vertical Visibility",
        _ => "-"
    };
    public int CloudHeight { get; set; }

    public CloudModifier? Modifier { get; set; }

}

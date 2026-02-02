using System.Text.Json.Serialization;

namespace FlightFront.Core.Models;

public class Clouds
{
    [JsonIgnore]
    public CloudType? CloudCover { get; set; }
    
    public string? CloudCoverDescription => CloudCover?.ToDescription();
    
    public int CloudHeight { get; set; }
    
    [JsonIgnore]
    public CloudModifier? Modifier { get; set; }
    
    public string? ModifierDescription => Modifier?.ToDescription();
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CloudType
{
    SKC,  // Sky Clear
    CLR,  // Clear (automated)
    NSC,  // No Significant Cloud
    NCD,  // No Cloud Detected
    FEW,  // Few (1-2 oktas)
    SCT,  // Scattered (3-4 oktas)
    BKN,  // Broken (5-7 oktas)
    OVC,  // Overcast (8 oktas)
    VV    // Vertical Visibility (obscured)
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CloudModifier
{
    CB,   // Cumulonimbus
    TCU   // Towering Cumulus
}

public static class CloudExtensions
{
    public static string ToDescription(this CloudType cloudType)
    {
        return cloudType switch
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
    }

    public static string ToDescription(this CloudModifier modifier)
    {
        return modifier switch
        {
            CloudModifier.CB => "Cumulonimbus",
            CloudModifier.TCU => "Towering Cumulus",
            _ => "-"
        };
    }
}

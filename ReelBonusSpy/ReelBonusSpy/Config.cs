using System.Text.Json.Serialization;

namespace ReelBonusSpy;

public class Config {
    [JsonInclude] public bool ShowSuccesses = true;
    [JsonInclude] public bool ShowFailures = true;
}

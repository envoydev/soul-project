using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoulProject.Api.Extensions;

public static class JsonExtensions
{
    public static void SetApplicationJsonSerializeRules(this JsonSerializerOptions jsonSerializerOptions)
    {
        jsonSerializerOptions.WriteIndented = true;
        jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        jsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}
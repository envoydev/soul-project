using SoulProject.Api.Constants;

namespace SoulProject.Api.Extensions;

public static class StringExtensions
{
    public static string ClearEndpointName(this string source)
    {
        if (!source.Contains(PresentationConstants.EndpointNameEnding))
        {
            return source;
        }
        
        var span = source.AsSpan();
        var end = source.Length - PresentationConstants.EndpointNameEnding.Length;
        var slicedSpan = span[..end];

        return new string(slicedSpan);
    } 
}
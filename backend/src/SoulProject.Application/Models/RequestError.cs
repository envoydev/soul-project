namespace SoulProject.Application.Models;

public record RequestError(int StatusCode, string? Details = null, IDictionary<string, string[]>? Errors = null);
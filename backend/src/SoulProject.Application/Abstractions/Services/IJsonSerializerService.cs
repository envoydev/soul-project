namespace SoulProject.Application.Abstractions.Services;

public interface IJsonSerializerService
{
    string SerializeObject(object? value);
    TObject? DeserializeObject<TObject>(string json);
}
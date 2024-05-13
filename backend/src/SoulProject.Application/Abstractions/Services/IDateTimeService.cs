namespace SoulProject.Application.Abstractions.Services;

public interface IDateTimeService
{
    DateTime GetUtc();
    int FromDateTimeToUnixTimeStamp(DateTime dateTime);
    DateTime FromUnixTimeStampToDateTime(int unixTimeStamp);
}
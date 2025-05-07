namespace Marketplace.Tests.Utils;

public static class DateTimeUtils
{
    public static DateTimeOffset Truncate(this DateTimeOffset date)
    {
        return new DateTimeOffset(date.Ticks - date.Ticks % TimeSpan.TicksPerSecond, date.Offset);
    }
}
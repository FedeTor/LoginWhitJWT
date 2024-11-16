using TimeZoneConverter;

namespace Application.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo ArgentinaTimeZone = TZConvert.GetTimeZoneInfo("America/Argentina/Buenos_Aires");

        public static DateTime GetArgentinaTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ArgentinaTimeZone);
        }

        public static DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}

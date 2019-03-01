namespace Alpha.Messages.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        private const string ShortDateWithSlash = "MM/dd/yyyy";
        private const string LongTime = "hh:mm:ss tt";

        private const string ShortFormatDate = "yyyy-MM-dd";
        private const string LongTime24h = "HH:mm:ss";

        public static string ToShortDateWithSlash(this DateTime dateTime)
            => dateTime.ToString(ShortDateWithSlash);

        public static string ToLongTime(this DateTime dateTime)
            => dateTime.ToString(LongTime);

        public static string ToShortFormatDate(this DateTime dateTime)
            => dateTime.ToString(ShortFormatDate);

        public static string ToLongTime24h(this DateTime dateTime)
            => dateTime.ToString(LongTime24h);
    }
}
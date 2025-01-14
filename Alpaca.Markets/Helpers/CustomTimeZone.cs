﻿#if !(NETFRAMEWORK || NET6_0_OR_GREATER)
using System.Runtime.InteropServices;
#endif

namespace Alpaca.Markets;

internal static class CustomTimeZone
{
#if NETFRAMEWORK || NET6_0_OR_GREATER
    private static TimeZoneInfo Est { get; } =
        TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
#else
    private static TimeZoneInfo Est { get; } =
        TimeZoneInfo.FindSystemTimeZoneById(
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Eastern Standard Time"
                : "America/New_York");
#endif

    public static DateTime ConvertFromEstToUtc(
        DateTime estDateTime) =>
        TimeZoneInfo.ConvertTimeToUtc(estDateTime, Est);
}

﻿using System.Runtime.CompilerServices;

namespace Alpaca.Markets.Extensions;

internal static class NullableHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureNotNull<T>(this T value, String name) where T : class => value ?? throw new ArgumentNullException(name);
}

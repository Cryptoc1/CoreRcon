﻿using System.Text.RegularExpressions;

namespace CoreRCON.Internal;

internal static class NewLineSanitizer
{
    private static readonly Regex pattern = new(@"\r\n|\n\r|\n|\r", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Multiline);

    public static string Sanitize(string value) => pattern.Replace(value, Environment.NewLine);
}

using System.Globalization;

namespace Vehicles.Common;
public static class StringManipulation
{
    public static string ToTitleCase(this string value)
    {
        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower().Trim());
    }
}

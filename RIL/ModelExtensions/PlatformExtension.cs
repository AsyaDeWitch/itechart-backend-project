using RIL.Models;
using System.ComponentModel;

namespace RIL.ModelExtensions
{
    public static class PlatformExtension
    {
        public static string ToDescriptionString(this Platform value)
        {
            var attributes = (DescriptionAttribute[])value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}

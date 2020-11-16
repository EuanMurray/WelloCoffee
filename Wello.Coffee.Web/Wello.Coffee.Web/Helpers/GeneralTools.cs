using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;

namespace Wello.Coffee.Web.Helpers
{
    public static class GeneralTools
    {
        public static String ToDescription(this Enum value)
        {
            if (value == null) return String.Empty;

            var enumType = value.GetType();
            var fieldName = value.ToString();

            DescriptionAttribute[] attributes = (DescriptionAttribute[])enumType
                ?.GetField(fieldName)
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?.Length > 0 ? attributes[0].Description : fieldName;
        }
    }
}

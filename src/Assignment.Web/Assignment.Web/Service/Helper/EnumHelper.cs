using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment.Web.Service.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// ex EnumHelper.GetDescription(UserColours.BrightPink);
        /// </summary>
        /// <param name="value">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetEnumDescription(this Enum value)

        {

            var enumType = value.GetType();

            var field = enumType.GetField(value.ToString());

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;

        }

        /// <summary>
        /// return list of enum of SelectListItem
        /// </summary>
        /// <returns>A string representing the friendly name</returns>
        public static List<SelectListItem> GetSelectListItemsWithoutDescription<T>()
        {
            var list = Enum.GetNames(typeof(T)).Select(name => new SelectListItem()
            {
                Text = name,
                Value = name
            });


            return list.ToList();
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}

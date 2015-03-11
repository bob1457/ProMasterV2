using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace ProMaster.DAL.Reporting
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToPropertyList(this IEnumerable<Core.Reporting.Property> properties, int id)
        {
            return properties.OrderBy(property => property.PropertyName)
                .Select(property => new SelectListItem
                {
                    Selected = (property.PropertyTypeId == id),
                    Text = property.PropertyName,
                    Value = property.PropertyId.ToString(CultureInfo.InvariantCulture)
                });

        }
    }
}

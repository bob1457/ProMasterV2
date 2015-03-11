using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace ProMaster.DAL.Tenant
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToStatusList(this IEnumerable<Core.Tenant.ScreeningCheckStatu> statuses, int id)
        {
            return statuses.OrderBy(status => status.ScreeningCheckStatusName)
                .Select(status => new SelectListItem
                {
                    Selected = (status.ScreeningCheckStatusId == id),
                    Text = status.ScreeningCheckStatusName,
                    Value = status.ScreeningCheckStatusId.ToString(CultureInfo.InvariantCulture)
                });

        }

        public static IEnumerable<SelectListItem> ToListedPropertyList(this IEnumerable<Core.Tenant.Property> properties, int id)
        {
            return properties.OrderBy(property => property.PropertyName)
                .Select(property => new SelectListItem
                {
                    Selected = (property.PropertyId == id),
                    Text = property.PropertyName,
                    Value = property.PropertyId.ToString(CultureInfo.InvariantCulture)
                });
        }

        public static IEnumerable<SelectListItem> ToAllPropertyList(this IEnumerable<Core.Tenant.Property> properties, int id)
        {
            return properties.OrderBy(property => property.PropertyName)
                .Select(property => new SelectListItem
                {
                    Selected = (property.PropertyId == id),
                    Text = property.PropertyName,
                    Value = property.PropertyId.ToString(CultureInfo.InvariantCulture)
                });
        }
    }
}

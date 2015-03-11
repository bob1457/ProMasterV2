using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProMaster.Core.Property;


namespace ProMaster.DAL.Property
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToLandlordList(this IEnumerable<PropertyOwner> owners, int id)
        {
            return owners.OrderBy(owner => owner.LastName + ", " +owner.FirstName)
                .Select(owner => new SelectListItem { 
                    Selected = (owner.PropertyOwnerId ==id),
                    Text = owner.LastName + ", " + owner.FirstName,
                    Value = owner.PropertyOwnerId.ToString(CultureInfo.InvariantCulture)
                });

        }

        public static IEnumerable<SelectListItem> ToPropertyTypeList(this IEnumerable<PropertyType> types, int id)
        {
            return types.OrderBy(type => type.PropertyType1)
                .Select(type => new SelectListItem
                {
                    Selected = (type.PropertyTypeId == id),
                    Text = type.PropertyType1,
                    Value = type.PropertyTypeId.ToString(CultureInfo.InvariantCulture)
                });

        }

        public static IEnumerable<SelectListItem> ToPropertyStatusList(this IEnumerable<RentalStatu> statuses, int id)
        {
            return statuses.OrderBy(status => status.Status)
                .Select(status => new SelectListItem
                {
                    Selected = (status.RentalStatusId == id),
                    Text = status.Status,
                    Value = status.RentalStatusId.ToString(CultureInfo.InvariantCulture)
                });

        }
        
    }
}

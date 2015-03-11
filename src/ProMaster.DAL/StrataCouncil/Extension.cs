﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace ProMaster.DAL.StrataCouncil
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToCondoList(this IEnumerable<Core.StrataCouncil.Property> properties, int id)
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

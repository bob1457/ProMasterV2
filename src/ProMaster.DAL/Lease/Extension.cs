using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using ProMaster.Core.Lease;


namespace ProMaster.DAL.Lease
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToLeaseTermList(this IEnumerable<LeaseTerm> terms, int id)
        {
            return terms.OrderBy(term => term.LeaaseTerm)
                .Select(term => new SelectListItem
                {
                    Selected = (term.LeaseTermId == id),
                    Text = term.LeaaseTerm,
                    Value = term.LeaseTermId.ToString(CultureInfo.InvariantCulture)
                });

        }



        public static IEnumerable<SelectListItem> ToTenantList(this IEnumerable<Core.Lease.Tenant> tenants, int id)
        {
            return tenants.OrderBy(tenant => tenant.LastName + ", " + tenant.FirstName)
                .Select(tenant => new SelectListItem
                {
                    Selected = (tenant.TenantId == id),
                    Text = tenant.LastName + ", " + tenant.FirstName,
                    Value = tenant.TenantId.ToString(CultureInfo.InvariantCulture)
                });

        }

        public static IEnumerable<SelectListItem> ToPropertyList(this IEnumerable<Core.Lease.Property> properties, int id)
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

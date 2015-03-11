using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using ProMaster.Infrastructure.UserProfile;

namespace ProMaster.DAL.UserProfile
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToRoleList(this IEnumerable<webpages_Roles> roles, int id)
        {
            return
                roles.OrderBy(role => role.RoleName)
                .Select(role => new SelectListItem
                {
                    Selected = (role.RoleId == id),
                    Text = role.RoleName,
                    Value = role.RoleId.ToString(CultureInfo.InvariantCulture)
                });

        }
    }
}

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using ProMaster.Core.Documents;

namespace ProMaster.DAL.Document
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToDocTypeList(this IEnumerable<DocumentType> types, int id)
        {
            return types.OrderBy(type => type.DocumentType1)
                .Select(type => new SelectListItem
                {
                    Selected = (type.DocumentTypeId == id),
                    Text = type.DocumentType1,
                    Value = type.DocumentTypeId.ToString(CultureInfo.InvariantCulture)
                });

        }

        public static IEnumerable<SelectListItem> ToPrincipalTypeList(this IEnumerable<DocumentPrincipalType> types, int id)
        {
            return types
                .Select(type => new SelectListItem
                {
                    Selected = (type.DocumentPrincipalTypeId == id),
                    Text = type.DocumentPrincipalType1,
                    Value = type.DocumentPrincipalTypeId.ToString(CultureInfo.InvariantCulture)
                });

        }
    }
}

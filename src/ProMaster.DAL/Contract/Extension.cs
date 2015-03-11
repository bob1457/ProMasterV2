using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using ProMaster.Core.Contract;


namespace ProMaster.DAL.Contract
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToFeeFrequencyList(this IEnumerable<ManagementFeeFrequency> frequencies, int id)
        {
            return frequencies.OrderBy(frequency => frequency.ManagementFeeFrequency1)
                .Select(frequency => new SelectListItem
                {
                    Selected = (frequency.ManagementFeeFrequencyId == id),
                    Text = frequency.ManagementFeeFrequency1,
                    Value = frequency.ManagementFeeFrequencyId.ToString(CultureInfo.InvariantCulture)
                });

        }

        public static IEnumerable<SelectListItem> ToPropertyList(this IEnumerable<Core.Contract.Property> properties, int id)
        {
            return properties.OrderBy(property => property.PropertyId)
                .Select(property => new SelectListItem
                {
                    Selected = (property.PropertyId == id),
                    Text = property.PropertyName,
                    Value = property.PropertyId.ToString(CultureInfo.InvariantCulture)
                });

        }


        public static IEnumerable<SelectListItem> ToContractList(this IEnumerable<ManagementContract> properties, int id)
        {
            foreach (ManagementContract contract1 in properties.OrderBy(contract => contract.ManagementContractId))
                yield return new SelectListItem
                {
                    Selected = (contract1.ManagementContractId == id), Text = contract1.ManagementContractTitile, Value = contract1.ManagementContractId.ToString(CultureInfo.InvariantCulture)
                };
        }
    }
}

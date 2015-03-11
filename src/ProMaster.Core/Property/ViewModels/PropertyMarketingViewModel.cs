using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProMaster.Core.Property.ViewModels
{
    public class PropertyMarketingViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime PropertyCreationDate { get; set; }
        public DateTime PropertyUpdateDate { get; set; }

        public string PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public string pType { get; set; }
        public int BuildYear { get; set; }

        public RentalStatu Status { get; set; }
        public int RentalStatusId { get; set; }
        public string RentalStatus { get; set; }

        public PropertyList ListingInfo { get; set; }
        public decimal MonthlyRent { get; set; }
        public DateTime ListingDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<PropertyMarketingViewModel> PendingProperty { get; set; }
        public IEnumerable<PropertyMarketingViewModel> PropertiesForRent { get; set; }

        public IEnumerable<Core.PropertyOwner.Property> PropertyInfo { get; set; }

        public IEnumerable<PropertyMarketingViewModel> RentedPropertyList { get; set; }

        public IEnumerable<PropertyMarketingViewModel> PropertyPictures { get; set; }
    }
}

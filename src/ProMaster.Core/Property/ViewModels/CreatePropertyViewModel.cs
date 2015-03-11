using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Property.ViewModels
{
    public class CreatePropertyViewModel
    {
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //public PropertyType PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        //public string PropertyTypeName { get; set; }
        public IEnumerable<SelectListItem> PropertyType { get; set; }

        public int LandlordId { get; set; }
        
        public IEnumerable<SelectListItem> LandlordList { get; set; }

        public PropertyAddress Address { get; set; }
        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }

        public PropertyFeature Features { get; set; }
        public int PropertyFeatureId { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfFloors { get; set; }
        public int NumberOfParkings { get; set; }
        public int TotalLivingArea { get; set; }
        public bool BasementAvailable { get; set; }
        public bool IsShared { get; set; }        
        public string Notes1 { get; set; }

        public PropertyFacility Facility { get; set; }
        public int PropertyFacilityId { get; set; }
        public bool Stove { get; set; }
        public bool Refrigerator { get; set; }
        public bool Dishwasher { get; set; }
        public bool AireConditioner { get; set; }
        public bool Blinds { get; set; }
        public bool Laundry { get; set; }
        public bool CommonFacility { get; set; }
        public bool Security { get; set; }
        public bool FireAlarm { get; set; }
        public bool TVInternet { get; set; }
        public string Others { get; set; }
        public string Notes2 { get; set; }
        public bool Furniture { get; set; }

        //public RentalStatu Status { get; set; }
        public int RentalStatusId { get; set; }
        //public string RentalStatus { get; set; }
        public IEnumerable<SelectListItem> PropertyStatus { get; set; }

        //public decimal MonthlyRent { get; set; }
        //public int PropertyListingId { get; set; }
        //public DateTime ListedDate { get; set; }

    }
}

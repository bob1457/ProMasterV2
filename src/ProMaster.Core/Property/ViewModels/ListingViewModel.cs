using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProMaster.Core.Property.ViewModels
{
    public class ListingViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }        
        public string PropertyListDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }

        public int PropertyListPictureId { get; set; }
        public string ListPictureImgUrl { get; set; }
        public string ListPictureImgUrlThumb { get; set; }
        public string ListPictureImgUrlMed { get; set; }

        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }
        public IEnumerable<PropertyAddress> Address { get; set; }

        public int PropertyFeatureId { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfFloors { get; set; }
        public int NumberOfParkings { get; set; }
        public int TotalLivingArea { get; set; }
        public bool BasementAvailable { get; set; }
        public bool IsShared { get; set; }
        public string Notes1 { get; set; }
        public IEnumerable<PropertyFeature> Feature { get; set; }

        //public PropertyFacility Facility { get; set; }
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
        public IEnumerable<PropertyFacility> Facility { get; set; }

        
        public DateTime PictureCreateDate { get; set; }
        public DateTime PictureUpdateDate { get; set; }
        public string ListedPropertyPictureCaption { get; set; }
        public string PropertyPictureTitle { get; set; }

        public IEnumerable<DisplayPropertyViewModel> ListedProperty { get; set; }

        public IEnumerable<ListingViewModel> PropertyList { get; set; }

        public IEnumerable<ListingViewModel> PropertyPictures { get; set; }


        public string PropertyType { get; set; }
        public int PropertyTypeId { get; set; }

        public IEnumerable<SelectListItem> PropertyTypeList { get; set; } //For edit/update purposet;
        public string RentStatus { get; set; }

        public int RentalStatusId { get; set; }
        public IEnumerable<SelectListItem> PropertyStatus { get; set; }

        public int PropertyListingId { get; set; }
        public string PorpertyListingTitle { get; set; }
        public bool? IsListedExternally { get; set; }
        public decimal ListedRentAmount { get; set; }
        public string Notes { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public DateTime RecordUpdateDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ListedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ListingUpdateDate { get; set; }


        
        
       

    }
}

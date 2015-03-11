using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProMaster.Core.Property.ViewModels
{
    public class DisplayPropertyViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyListDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<DisplayPropertyViewModel> AllProperty { get; set; }

        public string PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        
        public IEnumerable<SelectListItem> PropertyTypeList { get; set; } //For edit/update purposet;
        public string RentStatus { get; set; }

        public int LandlordId { get; set; }
        public string LandlordFirstName { get; set; }
        public string LandlordLastName { get; set; }
        public string LandlordContactEmail { get; set; }
        public string LandlordContactTelephone { get; set; }
        public IEnumerable<PropertyOwner> Landlords { get; set; }

        //public IEnumerable<SelectListItem> LandlordList { get; set; }

        //public PropertyAddress Address { get; set; }
        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }
        public IEnumerable<PropertyAddress> Address { get; set; }

        //public PropertyFeature Features { get; set; }
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
        public int BuildYear { get; set; }
        public IEnumerable<PropertyFacility> Facility { get; set; }

        public decimal MonthlyRent { get; set; }
        public DateTime ListingDate { get; set; }
        public string Status { get; set; }
        public int RentalStatusId { get; set; }
        //public string RentalStatus { get; set; }
        public IEnumerable<SelectListItem> PropertyStatus { get; set; }
        public IEnumerable<PropertyList> ListInfo { get; set; }





        //public int TenantId { set; get; }

        //-- Start of Test Code --

        //Lease Information
        //
        public int LeaseId { get; set; }
        public string LeaseTitle { get; set; }
        public string LeaseDesc { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }
        public string RentFrequency { get; set; }
        public decimal RentAmount { get; set; }
        public decimal DamageDepositAmount { get; set; }
        public decimal? PetDepositAmount { get; set; }
        public string RentDueOn { get; set; }
        public DateTime LeaseSignDate { get; set; }
        public bool Addendum { get; set; }
        public int LeaseTermId { get; set; }
        public string LeaseTerm { get; set; }
        public DateTime LeaseAddedDate { get; set; }
        public DateTime LeaseUpdateDate { get; set; }
        public bool IsLeaseActive { get; set; }
        public IEnumerable<Core.Lease.Lease> LeaseInfo { get; set; }

        //Tenant Information
        //
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }
        public bool OnLineAccessEnabled { get; set; }
        public bool IsTenantActive { get; set; }
        public string ManagedBy { get; set; }
        public IEnumerable<Core.Tenant.Tenant> TenantInfo { get; set; }


        //-- End of Test Code --

        //Document information
        // 
        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        public IEnumerable<Document> DocumentInfo { get; set; }
        public IEnumerable<SelectListItem> DocumentTypeList { get; set; }
        public IEnumerable<SelectListItem> DocumentTyPrincipalpeList { get; set; }

        //Property management event log
        //
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EventDate { get; set; }

        public int EventTypeId { get; set; }
        public string EventType { get; set; }
        public decimal MileageIncurred { get; set; }
        public DateTime EventCreationDate { get; set; }        
        public decimal TimeSpent { get; set; }

        public IEnumerable<DisplayPropertyViewModel> EventList { get; set; }

        //Work Order log
        //
        public int WorkOrderId { get; set; }
        public string WorkOrderName { get; set; }
        public string WorkOrderDetails { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public string WorkOrderCategory { get; set; }

        public int VendorId { get; set; }
        public string VendorName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }

        public decimal InvoiceAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime InvoiceDate { get; set; }

        public string InvoiceDocUrl { get; set; }
        public int WorkOrderStatusId { get; set; }
        public string WorkOrderStatus { get; set; }
        public int WorkOrderTypeId { get; set; }
        public string WorkOrderTypeName { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }

        public IEnumerable<DisplayPropertyViewModel> WorkOrderList { get; set; }
        public IEnumerable<DisplayPropertyViewModel> OtherCostList { get; set; }

        public int OtherCostId { get; set; }
        public string CostName { get; set; }
        public string CostDetails { get; set; }
        public bool IsCostPaid { get; set; }
        public DateTime CostAddedDate { get; set; }
        public decimal CostAmount { get; set; }

        //Rental listing
        //
        

        public int PropertyListPictureId { get; set; }
        public string ListPictureImgUrl { get; set; }
        public string ListPictureImgUrlThumb { get; set; }
        public string ListPictureImgUrlMed { get; set; }
        public IEnumerable<DisplayPropertyViewModel> PropertyPictures { get; set; }
        public int PropertyListingId { get; set; }
        public string PorpertyListingTitle { get; set; }
        
        public decimal ListedRentAmount { get; set; }
        public string Notes { get; set; }
        
    }
}

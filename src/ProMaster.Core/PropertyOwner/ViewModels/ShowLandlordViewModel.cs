using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using ProMaster.Core.Property;

namespace ProMaster.Core.PropertyOwner.ViewModels
{
    public class ShowLandlordViewModel
    {
        //User information
        //
        public int LandlordId { get; set; }
        public string UserName { get; set; }
        public string LandlordFirstName { get; set; }
        public string LandlordLastName { get; set; }
        public string LandlordContactEmail { get; set; }
        public string LandlordContactTelephone1 { get; set; }
        public string LandlordContactTelephone2 { get; set; }
        public string LandlordAvartaImgUrl { get; set; }
        public bool LandlordOnLineAccessEnabled { get; set; }
        public DateTime LandlordCreationDate { get; set; }
        public DateTime LandlordUpdateDate { get; set; }

        public IEnumerable<ShowLandlordViewModel> OwnerList { get; set; }
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
        public IEnumerable<Tenant> TenantInfo { get; set; }

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

        //Lease Information
        //
        public int LeaseId { get; set; }
        public string LeaseTitle { get; set; }
        public string LeaseDesc { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime LeaseStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime LeaseEndDate { get; set; }
        public string RentFrequency { get; set; }
        public decimal RentAmount { get; set; }
        public decimal DamageDepositAmount { get; set; }
        public decimal? PetDepositAmount { get; set; }
        public string RentDueOn { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime LeaseSignDate { get; set; }
        public bool Addendum { get; set; }
        public int LeaseTermId { get; set; }
        public string LeaseTerm { get; set; }
        public DateTime LeaseAddedDate { get; set; }
        public DateTime LeaseUpdateDate { get; set; }
        public bool IsLeaseActive { get; set; }
        public IEnumerable<Lease> LeaseInfo { get; set; }


        //Property information
        //
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime PropertyCreationDate { get; set; }
        public DateTime PropertyUpdateDate { get; set; }

        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }
        public IEnumerable<PropertyAddress> Address { get; set; }

        public IEnumerable<Core.PropertyOwner.Property> PropertyList { get; set; }
        //public IEnumerable<Core.PropertyOwner.Property> PropertyList { get; set; }
        //Management contract
        //
        public int ContractId { get; set; }
        public string ContractTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }

        public string FeeScale { get; set; } //management fee scale, i.e. 5% or 10% monthly
        public string ListingFeeScale { get; set; } //one time fee, i.e. 50% of monthly rent
        public int FeeFrequencyId { get; set; }
        public string ManagementFeeFrequency { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ContractSignDate { get; set; }
        public DateTime ContractCreateDate { get; set; }
        public DateTime ContractUpdateDate { get; set; }

        public IEnumerable<ManagementContract> ContractList { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ProMaster.Core.Reporting.ViewModels
{
    public class ReportingViewModel
    {
        //Property Reporting
        //
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public int PropertyTypeId {get; set;}
        public string PropertyType {get; set;}
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

        
        public PropertyAddress PropertyAddress {get; set;}
        public IEnumerable<ReportingViewModel> PropertyList { get; set; }


        //Tenants Reporting
        //
        public int TenantId { get; set; }
        public string TenantFistName { get; set; }
        public string TenantLastName { get; set; }
        public string TenantContactEmail { get; set; }
        public string TenantContactTelephone1 { get; set; }
        public string TenantContactTelephone2 { get; set; }
        public string TenantAvartaImgUrl { get; set; }
        public bool TenantOnLineAccessEnabled { get; set; }
        public DateTime TenantCreationDate { get; set; }
        public DateTime TenantUpdateDate { get; set; }

        public IEnumerable<ReportingViewModel> TenantList { get; set; }

        //Landlord Reporting
        //
        public int UserId { get; set; }
        public string LandlordFistName { get; set; }
        public string LandlordLastName { get; set; }
        public string LandlordContactEmail { get; set; }
        public string LandlordContactTelephone1 { get; set; }
        public string LandlordContactTelephone2 { get; set; }
        public string LandlordAvartaImgUrl { get; set; }
        public bool LandlordOnLineAccessEnabled { get; set; }
        public DateTime LandlordCreationDate { get; set; }
        public DateTime LandlordUpdateDate { get; set; }

        public IEnumerable<ReportingViewModel> LandlordList { get; set; }

        //Lease Contract Reporting
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
        public IEnumerable<Lease> LeaseInfo { get; set; }

        public IEnumerable<ReportingViewModel> LeaseAgreementList { get; set; }
    }
}

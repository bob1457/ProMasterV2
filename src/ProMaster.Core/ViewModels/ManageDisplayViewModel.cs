using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using ProMaster.Core.Property;

namespace ProMaster.Core.ViewModels
{
    public class ManageDisplayViewModel
    {
        #region Landlords

        public int UserId { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }
        public bool OnLineAccessEnabled { get; set; }
        public DateTime LandlordCreationDate { get; set; }
        public DateTime LandlordUpdateDate { get; set; }
        public IEnumerable<ManageDisplayViewModel> LandlordList { get; set; }

        #endregion

        #region Properties

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime PropertyCreationDate { get; set; }
        public DateTime PropertyUpdateDate { get; set; }

        public PropertyType PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public string pType { get; set; }
        public int BuildYear { get; set; }
       
        public RentalStatu Status { get; set; }
        public int RentalStatusId { get; set; }
        public string RentalStatus { get; set; }

        public PropertyList ListingInfo { get; set; }
        public decimal MonthlyRent { get; set; }
        public DateTime ListingDate { get; set; }

        public IEnumerable<ManageDisplayViewModel> MyProperty { get; set; }
        public IEnumerable<ManageDisplayViewModel> PropertiesForRent { get; set; }

        public IEnumerable<PropertyOwner.Property> PropertyInfo { get; set; }

        public IEnumerable<ManageDisplayViewModel> MyPropertyList { get; set; }


        #endregion

        #region Management Contracts

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
        public DateTime SignDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //public int PropertyId { get; set; }
        //public string PropertyName { get; set; }

        public int LandlordId { get; set; }
        public string LandlordName { get; set; }

        public DateTime ContractCreateDate { get; set; }
        public DateTime ContractUpdate { get; set; }


        public IEnumerable<ManageDisplayViewModel> MyContractList { get; set; }
        #endregion

        #region Lease

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
        public decimal PetDepositAmount { get; set; }
        public string RentDueOn { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime LeaseSignDate { get; set; }
        public bool Addendum { get; set; }
        public int TenantId { get; set; }
        public IEnumerable<Lease.Tenant> TenantInfo { get; set; }
        //public int PropertyId { get; set; }
        public IEnumerable<PropertyOwner.Lease> LeaseInfo { get; set; }
        public int LeaseTermId { get; set; }
        public IEnumerable<SelectListItem> LeaseTermList { get; set; }
        public IEnumerable<SelectListItem> TenantList { get; set; }
        public IEnumerable<SelectListItem> PropertyList { get; set; }
        public IEnumerable<ManageDisplayViewModel> LeaseList { get; set; }
        public DateTime LeaseAddedDate { get; set; }
        public DateTime LeaseUpdateDate { get; set; }

        #endregion

        #region Document

        public int DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime DocumentCreateDate { get; set; }
        public DateTime DocumentUpdateDate { get; set; }

        public IEnumerable<PropertyOwner.Document> DocumentList { get; set; }

        #endregion

        #region Tenants

        //public int TenantId { get; set; }
        public string TenantFirstName { get; set; }
        public string TenantLastName { get; set; }
        public string TenantContactEmail { get; set; }
        public string TenantContactTelephone1 { get; set; }
        public string TenantContactTelephone2 { get; set; }
        public string TenantAvartaImgUrl { get; set; }
        public bool TenantOnLineAccessEnabled { get; set; }
        public bool IstenantActive { get; set; }
        public string TenantManagedBy { get; set; }

        public DateTime TenantCreationDate { get; set; }
        public DateTime TenantUpdateDate { get; set; }

        public int ApplicaitonId {get; set;}
        public IEnumerable<ManageDisplayViewModel> ApplicatioinList {get; set;}
        public bool IsApproved { get; set; }

        public DateTime TenantAddedDate { get; set; }

        public IEnumerable<ManageDisplayViewModel> Tenants { get; set; }
       

        #endregion
        
        #region Vendor

        public int VendorId { get; set; }
        public string VendorBusinessName { get; set; }
        public string VendorFirstName { get; set; }
        public string VendorLastName { get; set; }
        public string VendorDesc { get; set; }
        public string VendorContactEmail { get; set; }
        public string VendorContactTelephone1 { get; set; }
        public string VendorContactTelephone2 { get; set; }
        public string VendorAvartaImgUrl { get; set; }
        public bool VendorOnLineAccessEnabled { get; set; }        
        public int VendorSpecialtyId { get; set; }
        public string VendorSepcialtyName { get; set; }
        public IEnumerable<ManageDisplayViewModel> VendorList { get; set; }

        public DateTime AddDate { get; set; }
        public DateTime ModifyDate { get; set; }

        #endregion

        #region Strata Council

        public int StrataCouncilId { get; set; }
        public string StrataCouncilName { get; set; }
        public string StrataCounvilMailingAddress { get; set; }
        public string StrataManageFirstName { get; set; }
        public string StrataManagerLastName { get; set; }
        public string StrataManagerContactEmail { get; set; }
        public string StrataManagerContactTel { get; set; }
        public string StrataNotes { get; set; }

        public DateTime StrataCreationDate { get; set; }
        public DateTime StrataUpdateDate { get; set; }

        public IEnumerable<ManageDisplayViewModel> StrataCouncilList { get; set; }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Lease.ViewModels
{
    public class DisplayLeaseViewModel
    {
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
        public bool IsActive { get; set; }
        public string ManagedBy { get; set; }
        public IEnumerable<Tenant> TenantInfo { get; set; }
        public IEnumerable<Core.Tenant.Tenant> TenantList { get; set; }

        public IEnumerable<Core.Tenant.Tenant> NewTenantList { get; set; }
        public IEnumerable<DisplayLeaseViewModel> UpdatedTenantList { get; set; }
        //Rent payment history
        //
        public int RentPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMoth { get; set; }
        public string PaymentYear { get; set; }
        public decimal PaymenAmount { get; set; }
        public int NumberOfLatePayment { get; set; }
        public decimal Balance { get; set; }
        public string RentPaymentMethod { get; set; }
        public int RentPaymentMethodId { get; set; }
        public bool IsCollectedInPerson { get; set; }
        public bool IsDepositForOwner { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public sbyte BankBranch { get; set; }
        public bool RentOnTime { get; set; }
        public string Notes { get; set; }
        public IEnumerable<RentPayment> PaymentInfo { get; set; }
        public IEnumerable<DisplayLeaseViewModel> PaymentHistoryList { get; set; }

        public string RentReciptMonth { get; set; }
        public string RentReciptAmount { get; set; }
        public DateTime RentPaymentDate { get; set; }
        public DateTime ReciptDate { get; set; }
        public string ReciptFrom { get; set; }
        
        //Property Information
        //
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        //public bool IsActive { get; set; }
        public IEnumerable<Property> PropertyInfo { get; set; }

        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }
        public IEnumerable<PropertyAddress> Address { get; set; }


        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public IEnumerable<Document> DocumentInfo { get; set; }

        public IEnumerable<SelectListItem> DocumentTypeList { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        public IEnumerable<SelectListItem> DocumentTyPrincipalpeList { get; set; }
        
    }
}

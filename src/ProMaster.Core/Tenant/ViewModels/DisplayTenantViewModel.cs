using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProMaster.Core.Tenant.ViewModels
{
    public class DisplayTenantViewModel
    {
        //Tenant Information
        //
        public int TenantId { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }
        public bool OnLineAccessEnabled { get; set; }        
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserName { get; set; }

        //Property information
        //
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Core.Tenant.Property> PropertyInfo { get; set; }


        //Lease information
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
        public IEnumerable<Lease> LeaseInfo { get; set; }

        //Document information
        // 
        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public IEnumerable<Core.Tenant.Document> DocumentInfo { get; set; }

        public IEnumerable<SelectListItem> DocumentTypeList { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        public IEnumerable<SelectListItem> DocumentTyPrincipalpeList { get; set; }

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
        public string RentReciptMonth { get; set; }
        public IEnumerable<DisplayTenantViewModel> PaymentInfo { get; set; }
        //public IEnumerable<DisplayTenantViewModel> PaymentHistoryList { get; set; }

    }
}

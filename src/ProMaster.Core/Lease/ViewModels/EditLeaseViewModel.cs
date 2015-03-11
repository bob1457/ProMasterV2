using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Lease.ViewModels
{
    public class EditLeaseViewModel
    {
        public int LeaseId { get; set; }
        public string LeaseTitle { get; set; }
        public string LeaseDesc { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }
        public int PropertyId { get; set; }
        public string RentFrequency { get; set; }
        public decimal RentAmount { get; set; }
        public decimal DamageDepositAmount { get; set; }
        public decimal? PetDepositAmount { get; set; }
        public string RentDueOn { get; set; }
        public DateTime LeaseUpdateDate { get; set; }
        public bool Addendum { get; set; }
        public int TenantId { get; set; }
        public IEnumerable<Tenant> TenantInfo { get; set; }
        public string Notes { get; set; }
        public Lease LeaseInfo { get; set; }
        public int LeaseTermId { get; set; }
        public string LeaseTerm { get; set; }
        //public string Lease { get; set; }
        public IEnumerable<SelectListItem> LeaseTermList { get; set; }
        public IEnumerable<SelectListItem> TenantList { get; set; }
        public IEnumerable<int> SelectedTenant { get; set; }
        
        //public DateTime LeaseUpdateDate { get; set; }



        //public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }
        public bool OnLineAccessEnabled { get; set; }
        public bool IsActive { get; set; }
        public string ManagedBy { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

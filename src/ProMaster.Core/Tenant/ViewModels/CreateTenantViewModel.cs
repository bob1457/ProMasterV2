using System;

namespace ProMaster.Core.Tenant.ViewModels
{
    public class CreateTenantViewModel
    {
        public int TenantId { get; set; }
        public string FistName { get; set; }
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

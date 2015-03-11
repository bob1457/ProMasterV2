using System;

namespace ProMaster.Core.PropertyOwner.ViewModels
{
    public class CreateLandlordViewModel
    {
        public int UserId { get; set; }        
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }

        //public int RoleId { get; set; }
        public bool OnLineAccessEnabled { get; set; }
        public bool AddProperty { get; set; } 
        
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

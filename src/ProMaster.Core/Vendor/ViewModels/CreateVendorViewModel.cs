using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Vendor.ViewModels
{
    public class CreateVendorViewModel
    {
        public int VendorId {get; set;}
        public string VendorBusinessName {get; set;}
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string VendorDesc { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }        
        public bool OnLineAccessEnabled { get; set; }
        public bool IsActive { get; set; }
        public int VendorSpecialtyId { get; set; }
        public string VendorSepcialtyName { get; set; }
        public IEnumerable<SelectListItem> VendorSpecialtyList { get; set; }

        public IEnumerable<SelectListItem> VendorList { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}

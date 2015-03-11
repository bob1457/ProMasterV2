using System;
using System.Collections.Generic;

namespace ProMaster.Core.ViewModels
{
    public class AdminViewModel
    {
        //Property Manager
        public int PropertyManagerId { get; set; }
        public string UserName { get; set; }
        public string PMFirstName { get; set; }
        public string PMLastName { get; set; }
        public string PMEmail { get; set; }
        public string PMTelephone1 { get; set; }
        public string PMTelephone2 { get; set; }
        public string PMAvatarImgUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime PMCreateDate { get; set; }
        public DateTime PMUpdateDate { get; set; }

        //Managed properties
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyListDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }        
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<AdminViewModel> ManagedPropertyList { get; set; }

        public string PropertyType { get; set; }
        public int PropertyTypeId { get; set; }        
        public string RentStatus { get; set; }

        //Property address informaton
        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }



    }
}

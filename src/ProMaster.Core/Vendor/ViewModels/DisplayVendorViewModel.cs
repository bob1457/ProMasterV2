using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Vendor.ViewModels
{
    public class DisplayVendorViewModel
    {
        public int VendorId { get; set; }
        public string VendorBusinessName { get; set; }
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

        public int WorkOrderId { get; set; }
        public string WorkOrderName { get; set; }
        public string WorkOrderDetails { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public string WorkOrderCategory { get; set; }

        //public int VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDocUrl { get; set; }
        public int WorkOrderStatusId { get; set; }
        public string WorkOrderStatus { get; set; }
        public int WorkOrderTypeId { get; set; }
        public string WorkOrderTypeName { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }

        public IEnumerable<DisplayVendorViewModel> WorkOrderList { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProMaster.Core.Vendor.ViewModels
{
    public class CreateWorkOrderViewModel
    {
        public int WorkOrderId { get; set; }
        public string WorkOrderName { get; set; }
        public string WorkOrderDetails { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public string WorkOrderCategory { get; set; }
        public int PropertyId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }

        public decimal InvoiceAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime InvoiceDate { get; set; }

        public string InvoiceDocUrl { get; set; }
        public int WorkOrderStatusId { get; set; }
        public string WorkOrderStatus { get; set; }
        public int WorkOrderTypeId { get; set; }
        public string WorkOrderTypeName { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsPaid { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PaidDate { get; set; }

        public decimal PaymentAmount { get; set; }
        
        public string PaymentMethod { get; set; }
        public DateTime RecordCreationDate { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> VendorList { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }


        
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

    }
}

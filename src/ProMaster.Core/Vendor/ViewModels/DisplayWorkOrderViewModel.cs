using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProMaster.Core.Vendor.ViewModels
{
    public class DisplayWorkOrderViewModel
    {
        public int WorkOrderId { get; set; }
        public string WorkOrderName { get; set; }
        public string WorkOrderDetails { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public string WorkOrderCategory { get; set; }
        public int PropertyId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }

        public string VendorFirstName { get; set; }
        public string VendorLastName { get; set; }

        public IEnumerable<SelectListItem> VendorList { get; set; }
 
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
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
        public decimal? PaymentAmount { get; set; }
        public DateTime? RecordUpdateDate { get; set; }

        public string PaymentMethod { get; set; }
        public DateTime? RecordCreationDate { get; set; }

        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }

        public string PropertyAddressSuoteNumber { get; set; }
        public string PropertyAddressNumber { get; set; }
        public string PropertyAddressStreet { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCodet { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ProMaster.Core.Lease.ViewModels
{
    public class RentPaymentViewModel
    {
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }

        public int RentPaymentId { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PaymentDate { get; set; }
        public string PaymentMonth { get; set; }
        public string PaymentYear { get; set; }
        public decimal PaymenAmount { get; set; }
        public int NumberOfLatePayment { get; set; }
        public decimal Balance { get; set; }
        public string RentPaymentMethod { get; set; }
        public int RentPaymentMethodId { get; set; }

        public bool IsRentTransferred  { get; set; }
        public decimal TranferAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TransferDate { get; set; }
        public string Bank { get; set; }

        public string TransferMethod { get; set; }

    }
}

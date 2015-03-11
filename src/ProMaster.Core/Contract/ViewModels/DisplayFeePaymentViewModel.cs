using System;

namespace ProMaster.Core.Contract.ViewModels
{
    public class DisplayFeePaymentViewModel
    {
        public DateTime FeePaidDate { get; set; }
        public string FeeMonth { get; set; }
        public string FeeYear { get; set; }
        public decimal FeePaidAmount { get; set; }
        public DateTime FeeReceivedDate { get; set; }
        public int FeeTypeId { get; set; }
        public string FeeType { get; set; }
        public string FeePayMethod { get; set; }
        public string FeeNotes { get; set; }
        public int FeePaymentId { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }

        public int LandlordId { get; set; }

        public string LandlordFisrtName { get; set; }
        public string LandlordLastName { get; set; }
        public string LandlordContactEmail { get; set; }

        public int ContractId { get; set; }
        public string ContractTitle { get; set; }
        public string ContractDesc { get; set; }


    }
}

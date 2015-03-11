using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ProMaster.Core.Contract.ViewModels
{
    public class DisplayContractViewModel
    {
        public int ContractId { get; set; }
        public string ContractTitle { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }
        public string FeeScale { get; set; } //management fee scale, i.e. 5% or 10% monthly
        public string ListingFeeScale { get; set; } //one time fee, i.e. 50% of monthly rent
        public int FeeFrequencyId { get; set; }
        public string ManagementFeeFrequency { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime SignDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public IEnumerable<DisplayContractViewModel> PropertyList { get; set; } 

        public int LandlordId { get; set; }
        public string LandlordFirstName { get; set; }
        public string LandlordLastName { get; set; }
        public string LandlordEmail { get; set; }
        public string LandlordTelepone { get; set; }

        public IEnumerable<DisplayContractViewModel> FeeHistoryList { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FeePaidDate { get; set; }
        public string FeeMonth { get; set; }
        public string FeeYear { get; set; }
        public decimal FeePaidAmount { get; set; }
        public int FeeTypeId { get; set; }
        public string FeeType { get; set; }
        public string FeePayMethod { get; set; }
        public string FeeNotes { get; set; }
        public int FeePaymentId { get; set; }


        //Document information
        // 
        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public IEnumerable<Document> DocumentInfo { get; set; }

        public IEnumerable<SelectListItem> DocumentTypeList { get; set; }
        public IEnumerable<SelectListItem> DocumentTyPrincipalList { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        

    }
}

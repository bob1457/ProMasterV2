using System;
using System.Collections.Generic;


namespace ProMaster.Core.Documents.ViewModels
{
    public class DisplayDcoumentViewModel
    {
        public int DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentCategory { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<DisplayDcoumentViewModel> PropertyDocuments { get; set; }
        public IEnumerable<DisplayDcoumentViewModel> TenantDocuments { get; set; }
        public IEnumerable<DisplayDcoumentViewModel> LeaseDocuments { get; set; }
        public IEnumerable<DisplayDcoumentViewModel> ContractDocuments { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        

        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantContactEmail { get; set; }
        public string TenantContactTel { get; set; }
       

        public int LeaseId { get; set; }
        public string LeaseTitle { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        

        public int ContractId { get; set; }
        public string ContractTitle { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        

        public int LandlordId { get; set; }
        public string LandlordName { get; set; }
        public string LandlordContactEmail { get; set; }
        public string LandlordContactTel { get; set; }

    }
}

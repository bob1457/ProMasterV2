using System;
using System.Collections.Generic;

namespace ProMaster.Core.SiteSearch.ViewModels
{
    public class DocSearchViewModel
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

        public IEnumerable<DocSearchViewModel> Documents { get; set; }
        //public IEnumerable<DisplayDcoumentViewModel> TenantDocuments { get; set; }
        //public IEnumerable<DisplayDcoumentViewModel> LeaseDocuments { get; set; }
        //public IEnumerable<DisplayDcoumentViewModel> ContractDocuments { get; set; }
    }
}

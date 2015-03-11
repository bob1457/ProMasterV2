using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Documents.ViewModels
{
    public class CreateDcoumentViewModel
    {
        public int DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public int TenantId { get; set; }
        public string DocumentPrincipal { get; set; } //to ewhat entity, e.g. property, or tenant, etc.
        public int DocumentPrincipalId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<SelectListItem> DocumentTypeList { get; set; }
        public IEnumerable<SelectListItem> DocumentTyPrincipalpeList { get; set; }
    }
}

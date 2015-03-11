using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.StrataCouncil.ViewModels
{
    public class DisplayStrataCouncilViewModel
    {
        public int StrataCouncilId { get; set; }
        public string StrataCouncilName { get; set; }
        public string StrataCounvilMailingAddress { get; set; }
        public string StrataManageFirstName { get; set; }
        public string StrataManagerLastName { get; set; }
        public string StrataManagerContactEmail { get; set; }
        public string StrataManagerContactTel { get; set; }
        public string Notes { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime PropertyCreationDate { get; set; }
        public DateTime PropertyUpdateDate { get; set; }  
        public RentalStatu Status { get; set; }
        public int RentalStatusId { get; set; }
        public string RentalStatus { get; set; }

        

        public IEnumerable<DisplayStrataCouncilViewModel> PropertyList { get; set; }

        public IEnumerable<SelectListItem> CondoList { get; set; }
    }
}

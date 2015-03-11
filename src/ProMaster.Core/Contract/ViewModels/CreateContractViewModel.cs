using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Contract.ViewModels
{
    public class CreateContractViewModel
    {
        public int ContractId {get; set;}
        public string ContractTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FeeScale { get; set; } //management fee scale, i.e. 5% or 10% monthly
        public string ListingFeeScale { get; set; } //one time fee, i.e. 50% of monthly rent
        public int FeeFrequencyId { get; set; }
        public IEnumerable<SelectListItem> FeeFrequency {get; set;}        
        public int PropertyId {get; set;}
        public IEnumerable<SelectListItem> Property { get; set; }
        public DateTime SignDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDAte { get; set; }
    }
}

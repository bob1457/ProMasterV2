using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Vendor.ViewModels
{
    public class OtherCostViewModel
    {
        public int OtherCostId { get; set; }
        public string OtherCostName { get; set; }
        public string CostDetails { get; set; }
        public string CostCategory { get; set; }
        public int CostCategoryId { get; set; }
        public int PropertyId { get; set; }
        public DateTime CostAddDate { get; set; }
        public decimal CostAmount { get; set; }
        public bool IsPaid { get; set; }
        public string PropertyName { get; set; }

        public IEnumerable<SelectListItem> CostCategoryList { get; set; }
    }
}

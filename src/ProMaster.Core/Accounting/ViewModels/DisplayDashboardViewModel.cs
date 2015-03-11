using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProMaster.Core.Property;

namespace ProMaster.Core.Accounting.ViewModels
{
    public class DisplayDashboardViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public PropertyType PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public string pType { get; set; }

        public RentalStatu Status { get; set; }
        public int RentalStatusId { get; set; }
        public string RentalStatus { get; set; }

        public IEnumerable<DisplayDashboardViewModel> MyProperty { get; set; }
        public IEnumerable<DisplayDashboardViewModel> RentedProperty { get; set; }

        public int NumberOfPromperties { get; set; }



    }
}

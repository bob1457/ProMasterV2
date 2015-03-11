using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Reporting.ViewModels
{
    public class DisplayDashboardViewModel
    {
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }

        //Properties
        //
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
        public string PType { get; set; }

        public RentalStatu Status { get; set; }
        public int RentalStatusId { get; set; }
        public string RentalStatus { get; set; }

        public IEnumerable<DisplayDashboardViewModel> MyProperty { get; set; }
        public IEnumerable<DisplayDashboardViewModel> RentedProperty { get; set; }
        public IEnumerable<DisplayDashboardViewModel> HoueseList { get; set; }
        public IEnumerable<DisplayDashboardViewModel> TownHoueseList { get; set; }
        public IEnumerable<DisplayDashboardViewModel> CondoList { get; set; }

        public IEnumerable<SelectListItem> PropertyList { get; set; }


        public int NumberOfPromperties { get; set; }



        //*************************************************//
        //************* New Implementation ****************//

        //Rental Applications
        //
        public int ApplicationId { get; set; }    
        public DateTime ApplyDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsApplicationActive { get; set; }

        public IEnumerable<DisplayDashboardViewModel> TenantApplicationList { get; set; }

        //Management Fee Revenue
        //
        public int ManagementFeeId { get; set; }
        public int ManagementFeeTypeId { get; set; }
        public string ManagementFeeType { get; set; }
        public int ManagementFeeFrequencyId { get; set; }
        public string ManagementFeeFrequency { get; set; }
        public decimal ManagementFeeAmount { get; set; }
        public bool IsManagementFeeReceived { get; set; }
        public DateTime ManagementFeeReceivedDate { get; set; }
        public decimal? TotalFee { get; set; }

        public IEnumerable<DisplayDashboardViewModel> FeeTransactionList { get; set; }


        //Management Cost
        //
        public string TotalMileage { get; set; }
        public string TotalWorkingHours { get; set; }
        public string TotalOtherMangementCost { get; set; }

        //Management Contract
        //
        public int ContractId { get; set; }
        public string ContractTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public DateTime SignDate { get; set; }

        public IEnumerable<DisplayDashboardViewModel> ContractList { get; set; }


    }
}

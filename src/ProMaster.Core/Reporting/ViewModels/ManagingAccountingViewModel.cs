using System;
using System.Collections.Generic;

namespace ProMaster.Core.Reporting.ViewModels
{
    public class ManagingAccountingViewModel
    {
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        //Property
        //
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public PropertyAddress PropertyAddress { get; set; }

        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }

        //Management Fee
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

        public IEnumerable<ManagingAccountingViewModel> FeeList { get; set; }

        //Visit times and mileage driven
        //
        //Property management event log
        //
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventDetails { get; set; }
        public DateTime EventDate { get; set; }
        public int EventTypeId { get; set; }
        public string EventType { get; set; }
        public decimal MileageIncurred { get; set; }
        public DateTime EventCreationDate { get; set; }
        public decimal VisitTimeSpent { get; set; }

        public decimal? TotalHours { get; set; }
        public decimal? TotalMiles { get; set; }

        public IEnumerable<ManagingAccountingViewModel> EventList { get; set; }



    }
}

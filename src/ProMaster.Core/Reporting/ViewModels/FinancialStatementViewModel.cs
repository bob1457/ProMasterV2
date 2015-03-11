using System;

namespace ProMaster.Core.Reporting.ViewModels
{
    public class FinancialStatementViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddresscity { get; set; }
        public string PropertyAddressProvinceState { get; set; }
        public string PropertyAddressCountry { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyImageUrl { get; set; }

        public decimal TotalRepairCosts { get; set; }
        public decimal TotalOtherCosts { get; set; }
        public decimal TotalFees { get; set; }
        public decimal TotalRentRevenue { get; set; }
        public decimal TotalNetIncome { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

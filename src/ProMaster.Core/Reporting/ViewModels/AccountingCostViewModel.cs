using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProMaster.Core.Reporting.ViewModels
{
    public class AccountingCostViewModel
    {
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartPeriod { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndPeriod { get; set; }

        //Repair and maintenance cost
        //
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }

        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }

        public IEnumerable<SelectListItem> PropertyList { get; set; }


        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public PropertyAddress PropertyAddress {get; set;}

        public int WorkOrderId { get; set; }
        public string WorkOrderName { get; set; }
        public string WorkOrderDetails { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public string WorkOrderCategory { get; set; }
        
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDocUrl { get; set; }
        public int WorkOrderStatusId { get; set; }
        public string WorkOrderStatus { get; set; }
        public int WorkOrderTypeId { get; set; }
        public string WorkOrderTypeName { get; set; }
        //public bool IsAuthorized { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        //public DateTime? RecordUpdateDate { get; set; }

        public string PaymentMethod { get; set; }
        //public DateTime? RecordCreationDate { get; set; }
        public IEnumerable<AccountingCostViewModel> WorkOrderList { get; set; }

        public decimal? TotalRepairCost { get; set; }




        //Other costs
        //
        public int OtherCostId { get; set; }
        public string OtherCostName { get; set; }
        public string CostDetails { get; set; }
        public string CostCategory { get; set; }
        public int CostCategoryId { get; set; }
        //public int PropertyId { get; set; }
        public DateTime CostAddDate { get; set; }
        public DateTime ? CostUpdateDate { get; set; }
        public decimal CostAmount { get; set; }
        public bool IsCostPaid { get; set; }
        //public string PropertyName { get; set; }

        public decimal? TotalOtherCost { get; set; }

        public IEnumerable<SelectListItem> CostCategoryList { get; set; }

        public IEnumerable<AccountingCostViewModel> CostList { get; set; }


        //Rent Revenue
        //
        public int RentPaymentId { get; set; }
        public decimal RentPaymentAmount { get; set; }
        public string RentPaymentMonth { get; set; }
        public string RentPamentYear { get; set; }
        public int LeaseId { get; set; }

        public decimal? TotalRentRevenue { get; set; }

        public IEnumerable<AccountingCostViewModel> RentRevenueList { get; set; }

        
        //Management Fee
        //
        public int ManagementFeeId { get; set; }
        public int ManagementFeeTypeId { get; set; }
        public string ManagementFeeType { get; set; }
        public int ManagementFeeFrequencyId { get; set; }
        public string ManagementFeeFrequency { get; set; }
        public decimal ManagementFeeAmount { get; set; }
        public bool IsManagementFeeReceived { get; set; }

        public decimal? TotalFee { get; set; }

        public IEnumerable<AccountingCostViewModel> FeeList { get; set; }

        public int reportType { get; set; }

        //For Fanancail Statement
        //
        public decimal? totalExpenses { get; set; }
        public decimal? totalRepairCosts { get; set; }
        public decimal? totalOtherCosts { get; set; }
        public decimal? TotalFees { get; set; }
        public decimal totalGrossRevenue { get; set; }
        public decimal totalNetIncome { get; set; }
        public decimal? totalMaintenanceCosts { get; set; }

    }

}

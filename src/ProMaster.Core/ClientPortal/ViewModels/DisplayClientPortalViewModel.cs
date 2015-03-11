using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using ProMaster.Core.Property;

namespace ProMaster.Core.ClientPortal.ViewModels
{
    public class DisplayClientPortalViewModel
    {
        //Basic client info
        //
        public int ClientId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientContactTel1 { get; set; }
        public string ClientContactTel2 { get; set; }
        public string ClientContactEmail { get; set; }
        public string ClientAvatarImgUrl { get; set; }
        public string ClientCreateDate { get; set; }

        //Lease info
        //
        public int LeaseId { get; set; }
        public string LeaseTitle { get; set; }
        public string LeaseDesc { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }
        public string RentFrequency { get; set; }
        public decimal RentAmount { get; set; }
        public decimal DamageDepositAmount { get; set; }
        public decimal? PetDepositAmount { get; set; }
        public string RentDueOn { get; set; }
        public DateTime LeaseSignDate { get; set; }
        public bool Addendum { get; set; }
        public int LeaseTermId { get; set; }
        public string LeaseTerm { get; set; }
        public DateTime LeaseAddedDate { get; set; }
        public DateTime LeaseUpdateDate { get; set; }
        public bool IsLeaseActive { get; set; }


        //Rent payment
        //
        public int RentPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMoth { get; set; }
        public string PaymentYear { get; set; }
        public decimal PaymenAmount { get; set; }
        public int NumberOfLatePayment { get; set; }
        public decimal Balance { get; set; }
        public string RentPaymentMethod { get; set; }
        public int RentPaymentMethodId { get; set; }
        public bool IsCollectedInPerson { get; set; }
        public bool IsDepositForOwner { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public sbyte BankBranch { get; set; }
        public bool RentOnTime { get; set; }
        public string Notes { get; set; }
        public string RentReciptMonth { get; set; }
        public IEnumerable<DisplayClientPortalViewModel> PaymentHisotry { get; set; }

        public IEnumerable<DisplayClientPortalViewModel> RentRevenueHisotry { get; set; }
        //Property info
        //
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyListDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime PropertyCreationDate { get; set; }
        public DateTime PropertyUpdateDate { get; set; }
        

        //public IEnumerable<DisplayClientPortalViewModel> PropertyList { get; set; }
        public IEnumerable<PropertyOwner.Property> PropertyList { get; set; }

        public IEnumerable<SelectListItem> OwnerPropertyList { get; set; }

        //Property address
        //
        public int PropertyAddressId { get; set; }
        public string PropertyAddressStreetNumber { get; set; }
        public string PropertyAddressSuiteNumber { get; set; }
        public string PropertyAddressStreetName { get; set; }
        public string PropertyAddressCity { get; set; }
        public string PropertyAddressProvState { get; set; }
        public string PropertyAddressPostZipCode { get; set; }
        public string PropertyAddressCountry { get; set; }
        public IEnumerable<PropertyAddress> Address { get; set; }

        //Management contract
        //
        public int ContractId { get; set; }
        public string ContractTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }

        public string FeeScale { get; set; } //management fee scale, i.e. 5% or 10% monthly
        public string ListingFeeScale { get; set; } //one time fee, i.e. 50% of monthly rent
        public int FeeFrequencyId { get; set; }
        public string ManagementFeeFrequency { get; set; }
        public DateTime ContractSignDate { get; set; }
        public DateTime ContractCreateDate { get; set; }
        public DateTime ContractUpdateDate { get; set; }

        public IEnumerable<PropertyOwner.ManagementContract> ContractList { get; set; }

        //Management fee payment history
        //
        public DateTime FeePaidDate { get; set; }
        public string FeeMonth { get; set; }
        public string FeeYear { get; set; }
        public decimal FeePaidAmount { get; set; }
        public DateTime FeeReceivedDate { get; set; }
        public int FeeTypeId { get; set; }
        public string FeeType { get; set; }
        public string FeePayMethod { get; set; }
        public string FeeNotes { get; set; }
        public int FeePaymentId { get; set; }

        public IEnumerable<SelectListItem> OwnerContractList { get; set; }

        public IEnumerable<DisplayClientPortalViewModel> FeePaymentHisotry { get; set; }

        //Repair costs and other costs
        //
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
        public IEnumerable<DisplayClientPortalViewModel> WorkOrderList { get; set; }








        //Documents
        //
        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public IEnumerable<Tenant.Document> DocumentList { get; set; }

    }
}

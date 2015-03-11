using System;
using System.Collections.Generic;

namespace ProMaster.Core.SiteSearch.ViewModels
{
    public class SiteSearchViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime PropertyAddDate { get; set; }
        

        public int LandlordId { get; set; }
        public string LandlordFirstName { get; set; }
        public string LandlordLastName { get; set; }
        public string LandlordContactEmail { get; set; }
        public string LandlordContactTelephone { get; set; }
        public string UserAvatarImgUrl { get; set; }

        public int ContractId { get; set; }
        public string ContractTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ContractAddDate { get; set; }

        public int LeaseId { get; set; }
        public string LeaseTitle { get; set; }
        public string LeaseDesc { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }        
        public DateTime LeaseSignDate { get; set; }
        public bool Addendum { get; set; }        
        public string LeaseTerm { get; set; }        
        public bool IsLeaseActive { get; set; }
        public DateTime LeaseAddDate { get; set; }

        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }
        public bool OnLineAccessEnabled { get; set; }
        public bool IsTenantActive { get; set; }
        public DateTime TenantAddDate { get; set; }

        public int WorkOrderId { get; set; }
        public string WorkOrderName { get; set; }
        public string WorkOrderDetails { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public string WorkOrderCategory { get; set; }
        public DateTime WorkOrderAddDate { get; set; }

        public int VendorId { get; set; }
        public string VendorName { get; set; }

        public int StrataCouncilId { get; set; }
        public string StrataCouncilName { get; set; }
        public string StrataCounvilMailingAddress { get; set; }
        public string StrataManageFirstName { get; set; }
        public string StrataManagerLastName { get; set; }
        public string StrataManagerContactEmail { get; set; }
        public string StrataManagerContactTel { get; set; }

        public int DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentPrincipalTypeId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDetails { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentCategory { get; set; }
        public DateTime DocAddDate { get; set; }
        public DateTime DocUpdateDate { get; set; }

        public IEnumerable<SiteSearchViewModel> ProopertyResults { get; set; }
        public IEnumerable<SiteSearchViewModel> TenantResults { get; set; }
        public IEnumerable<SiteSearchViewModel> ContractResults { get; set; }
        public IEnumerable<SiteSearchViewModel> VendorResults { get; set; }
        public IEnumerable<SiteSearchViewModel> CouncilResults { get; set; }
        public IEnumerable<SiteSearchViewModel> LeaseResults { get; set; }
        public IEnumerable<SiteSearchViewModel> LandlordResults { get; set; }


    }
}

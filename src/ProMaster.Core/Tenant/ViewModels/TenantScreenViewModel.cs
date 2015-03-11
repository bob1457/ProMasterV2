using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProMaster.Core.Tenant.ViewModels
{
    public class TenantScreenViewModel
    {
        //Applicant information
        //
        public int ApplicationId { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public string ApplicantSIN { get; set; }
        public string ApplicantCurrentAddress { get; set; }
        public string ApplicantPreviousAddress { get; set; }
        public string ApplicantContactEmail { get; set; }
        public string ApplicantContactTel { get; set; }
        public string ApplicantCurrentLandlorContact { get; set; }
        public string ApplicantPreviousLandlorContact { get; set; }
        public string ApplicantPreviousEmploerContact { get; set; }
        public string ApplicantCurrentEmploerContact { get; set; }
        public decimal ApplicantEmploymentIncome { get; set; }
        public decimal ApplicantOtherIncome { get; set; }
        public int ApplicantCreditReportId { get; set; }
        public string ApplicantCreditReport { get; set; }
        public string ApplicantCreditScore { get; set; }
        public int NumberOfOccupantse { get; set; }
        public int? NumberOfChildren { get; set; }
        public bool IsApplicationActive { get; set; }
        public DateTime ApplicaitonDate { get; set; } 
        public bool IsAuthorizedToCheckReference { get; set; }
        public bool IsApplicationApproved { get; set; }
        public string ScreeningNotes { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public int PropertyBuildYear { get; set; }

        public IEnumerable<TenantScreenViewModel> ApplicationList { get; set; }
        public IEnumerable<TenantScreenViewModel> CompletedApplications { get; set; }
        public IEnumerable<SelectListItem> ListedProperty { get; set; }
        //public TenancyApplication app { get; set; }
        public IEnumerable<SelectListItem> AllProperty { get; set; }

        public IEnumerable<SelectListItem> ScreenCheckStatusList { get; set; }

        public IEnumerable<TenantScreenViewModel> ScreenCheckTypeList { get; set; }
        //public IEnumerable<TenantScreenViewModel> ScreenCheckStatusList2 { get; set; }

        public CreditReport CReport { get; set; }

        //Screening process
        //
        public bool EmployerReferenceDocAvailable { get; set; }
        public bool LandlordReferenceDocAvailable { get; set; }
        public bool IdentificationDocAvailable { get; set; }
        public bool IncomeDocAvailable { get; set; }
        public bool IsAllDocumentsReady { get; set; }
        public bool IsCreditReportAvailable { get; set; }

        

        public string CurrentLandLordReferenceCheckStatus { get; set; }
        public string PreviousLandlordReferenceCheckStatus { get; set; }
        public string CurrentEmployerReferenceCheckStatus { get; set; }
        public string PreviousEmployerReferenceCheckStatus { get; set; } 
        public string CreditReportCheckStatus { get; set; }
        public string IncomeCheckStatus { get; set; }
        public string EmploymentHistoryReferenceCheckStatus { get; set; }
        public DateTime DocumentCheckLiatCompletionDate { get; set; }

        public string ScreenCheckType { get; set; }
        public int ScreenCheckTypeId { get; set; }
        public string ScreenCheckStatus { get; set; }
        public int ScreenCheckStatusId { get; set; }

        public string ScreenNotes { get; set; }

        public bool IsScreenProcessCompleted { get; set; }

        //public bool IsApplicationActive { get; set; }

        //public bool IsApplicationApproved { get; set; }

        public string FinalScreeningStatus { get; set; }


        //Screen criteria
        //
        //public bool CreditCheckResult { get; set; }
        //public bool RentalHistoryReferenceCheckResult { get; set; }

        public IEnumerable<TenantScreenViewModel> Screening { get; set; }
        public IEnumerable<TenantScreenViewModel> DocCheckList { get; set; }
    }

    public class CreditReport
    { 

    }
}

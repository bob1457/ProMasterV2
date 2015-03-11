using System;

namespace ProMaster.Core.Property.ViewModels
{
    public class CreateApplicationViewModel
    {
        public int ApplicationId { get; set; }
        public string SIN { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public string CurrentAddress { get; set; }
        public string PreviousAddress { get; set; }
        public string ApplicantContactEmail { get; set; }
        public string ApplicatnContactTel { get; set; }
        public int NumberOfOccupants { get; set; }
        public int NumberOfChildren { get; set; }
        public string CurrentEmployerContact { get; set; }
        public string PreviousEmployerContact { get; set; }
        public string CurrentLandlordContact { get; set; }
        public string PrevioustLandlordContact { get; set; }
        public bool AuthorizedRefCheck { get; set; }
        public bool IdenticationAvailable { get; set; }
        public bool ReferenceMaterialsAvailable { get; set; }
        public string CreditScore { get; set; }
        public DateTime ApplyDate { get; set; }
        public bool IsApproved { get; set; }
        public bool ScreenResult { get; set; }
        public int PropertyId { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string Notes { get; set; }
    }
}

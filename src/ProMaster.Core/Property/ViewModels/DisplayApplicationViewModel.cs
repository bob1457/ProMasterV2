using System;

namespace ProMaster.Core.Property.ViewModels
{
    public class DisplayApplicationViewModel
    {
        public int ApplicationId { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public string CurrentAddress { get; set; }
        public string ApplicantContactEmail { get; set; }
        public string ApplicatnContactTel { get; set; }
        public int NumberOfOccupants { get; set; }
        public int? NumberOfChildren { get; set; }
        public string CurrentEmployerContact { get; set; }
        public string CurrentLandlordContact { get; set; }
        public bool AuthorizedRefCheck { get; set; }
        public bool IdenticationAvailable { get; set; }
        public string CreditScore { get; set; }
        public DateTime ApplyDate { get; set; }
        public bool IsApproved { get; set; }
        public int PropertyId { get; set; }
        public decimal MonthlyIncome { get; set; }

        //public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyImageUrl { get; set; }
        public int PropertyBuildYear { get; set; }
        //public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PropertyType { get; set; }
        public int PropertyTypeId { get; set; }       
        public string RentStatus { get; set; }
        public int RentalStatusId { get; set; }

    }
}

using System;

namespace ProMaster.Core.StrataCouncil.ViewModels
{
    public class CreateStrataCouncilViewModel
    {
        public int StrataCouncilId { get; set; }
        public string StrataCouncilName { get; set; }
        public string StrataCounvilMailingAddress { get; set; }
        public string StrataManageFirstName { get; set; }
        public string StrataManagerLastName { get; set; }
        public string StrataManagerContactEmail { get; set; }
        public string StrataManagerContactTel { get; set; }
        public string Notes { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}

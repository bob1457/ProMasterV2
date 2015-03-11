using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProMaster.Infrastructure.UserProfile.ViewModels
{
    public class MyProfileViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone1 { get; set; }
        public string ContactTelephone2 { get; set; }
        public string AvartaImgUrl { get; set; }
               
        public string RoleName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

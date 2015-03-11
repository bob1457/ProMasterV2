using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ProMaster.Core.Documents
{
    public class Validation
    {
        [Required]
        public string DocumentTitle { get; set; }

        [Required]
        public string DocumentName { get; set; }

        //[Required]
        //public HttpPostedFileBase DocFile{ get; set; }
    }
}

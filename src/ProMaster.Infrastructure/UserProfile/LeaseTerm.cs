//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProMaster.Infrastructure.UserProfile
{
    using System;
    using System.Collections.Generic;
    
    public partial class LeaseTerm
    {
        public LeaseTerm()
        {
            this.Leases = new HashSet<Lease>();
        }
    
        public int LeaseTermId { get; set; }
        public string LeaaseTerm { get; set; }
    
        public virtual ICollection<Lease> Leases { get; set; }
    }
}
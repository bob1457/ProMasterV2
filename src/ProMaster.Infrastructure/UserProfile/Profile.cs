
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
    
public partial class Profile
{

    public int ProfileId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ContactEmail { get; set; }

    public string ContactTelephone1 { get; set; }

    public string ContactTelephone2 { get; set; }

    public int RoleId { get; set; }

    public System.DateTime CreationDate { get; set; }

    public System.DateTime UpdateDate { get; set; }

    public string UserName { get; set; }

    public string UserAvartaImgUrl { get; set; }

    public bool OnlineAccessEnbaled { get; set; }

    public Nullable<int> UserPrincipalId { get; set; }

}

}

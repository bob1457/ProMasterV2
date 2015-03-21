
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
    
public partial class Property
{

    public Property()
    {

        this.Leases = new HashSet<Lease>();

    }


    public int PropertyId { get; set; }

    public string PropertyName { get; set; }

    public string PropertyDesc { get; set; }

    public string PropertyListDesc { get; set; }

    public int PropertyTypeId { get; set; }

    public Nullable<int> StrataCouncilId { get; set; }

    public int PropertyAddressId { get; set; }

    public int PropertyFeatureId { get; set; }

    public int PropertyFacilityId { get; set; }

    public int PropertyOwnerId { get; set; }

    public int PropertyManagerId { get; set; }

    public string PropertyImgUrl { get; set; }

    public string PropertyVideoUrl { get; set; }

    public int PropertyBuildYear { get; set; }

    public int PropertyMgmntlStatusId { get; set; }

    public bool IsActive { get; set; }

    public bool IsShared { get; set; }

    public int RentalStatusId { get; set; }

    public System.DateTime CreatedDate { get; set; }

    public System.DateTime UpdateDate { get; set; }



    public virtual ICollection<Lease> Leases { get; set; }

    public virtual PropertyManager PropertyManager { get; set; }

    public virtual PropertyOwner PropertyOwner { get; set; }

    public virtual PropertyType PropertyType { get; set; }

}

}

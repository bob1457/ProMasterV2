
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
    
public partial class PropertyType
{

    public PropertyType()
    {

        this.Properties = new HashSet<Property>();

    }


    public int PropertyTypeId { get; set; }

    public string PropertyType1 { get; set; }



    public virtual ICollection<Property> Properties { get; set; }

}

}

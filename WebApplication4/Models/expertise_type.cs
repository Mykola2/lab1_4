//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class expertise_type
    {
        public expertise_type()
        {
            this.expertise_type1 = new HashSet<expertise_type>();
            this.qualifications = new HashSet<qualifications>();
        }
    
        public int idExpertise_type { get; set; }
        public string Expertise_type_name { get; set; }
        public Nullable<int> Expertise_parent_type_id { get; set; }
    
        public virtual ICollection<expertise_type> expertise_type1 { get; set; }
        public virtual expertise_type expertise_type2 { get; set; }
        public virtual ICollection<qualifications> qualifications { get; set; }
    }
}

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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class commissionorders
    {
        public commissionorders()
        {
            this.qualifications = new HashSet<qualifications>();
        }
    
        public int idCommissionOrders { get; set; }
        public string commissionName { get; set; }
        public Nullable<int> commissionOrderNumber { get; set; }
        public Nullable<System.DateTime> CommissionOrderDate { get; set; }
        public int Experts_idExperts { get; set; }


        [JsonIgnore]
        public virtual experts experts { get; set; }
        [JsonIgnore]
        public virtual ICollection<qualifications> qualifications { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AquatroHRIMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCompletion
    {
        public tblCompletion()
        {
            this.tblProjectDetails = new HashSet<tblProjectDetail>();
        }
    
        public int intCompletionId { get; set; }
        public string varComplition { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
        public Nullable<int> intCreatedby { get; set; }
        public Nullable<System.DateTime> dtUpdatedOn { get; set; }
        public Nullable<int> intUpdatedBy { get; set; }
    
        public virtual ICollection<tblProjectDetail> tblProjectDetails { get; set; }
    }
}

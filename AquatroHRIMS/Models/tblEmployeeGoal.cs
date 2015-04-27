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
    using System.ComponentModel.DataAnnotations;
    
    public partial class tblEmployeeGoal
    {
        public int intGoalID { get; set; }
        [Display(Name="Goal Description")]
        public string varGoalDescription { get; set; }
        public Nullable<System.DateTime> dtStartdate { get; set; }
        public Nullable<System.DateTime> dtEndDate { get; set; }
        [Display(Name="Completion")]
        public string varCompletion { get; set; }
        [Display(Name="Manager Comment")]
        public string varManagerComment { get; set; }
        [Display(Name="Employee Name")]
        public Nullable<int> intEmployeeID { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
        public Nullable<System.DateTime> dtUpdatedOn { get; set; }
        public Nullable<int> intCreatedby { get; set; }
        public Nullable<int> intUpdatedby { get; set; }
        [Display(Name="Goal Title")]
        public Nullable<int> intTitle { get; set; }
        [Display(Name="Goal Status")]
        public Nullable<int> intStatus { get; set; }
    
        public virtual tblEmployee tblEmployee { get; set; }
        public virtual tblGoalTitle tblGoalTitle { get; set; }
        public virtual tblStatus tblStatu { get; set; }
    }
}

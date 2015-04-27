using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AquatroHRIMS.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.ViewModel
{
    public class ProjectViewModel:tblProjectDetail
    {
        public tblProjectDetail objProjectDetails { get; set; }

        public SelectList EmployeeModel { get; set; }
        public SelectList ProjectStatusList { get; set; }
        public SelectList ClientModel { get; set; }
        public SelectList CompletionModel { get; set; }
        public string strExtClientHead { get; set; }

        public MultiSelectList InternalTeam { get; set; }

        public int[] EmployeeID { get; set; }
        [Required(ErrorMessage="External Project Head Email Id is Compulsory!!")]
        [Display(Name = "External Project Head Email Id")]
        public string strExtClientEmailID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AquatroHRIMS.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.ViewModel
{
    public class LoginViewModel:tblLogin
    {

        public tblLogin objtblLogin { get; set; }
        public IList<SelectListItem> Access { get; set; }

        public SelectList DepartmentModel { get; set; }
        public SelectList DesignationModel { get; set; }
        public SelectList EmployeeModel { get; set; }


        [Required(ErrorMessage = "Confirm Password is compulsory!!")]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers in Confirm Password!!")]
        [System.Web.Mvc.Compare("varPassword")]
        [Display(Name = "Confirm Password")]
        public string varConfirmPassword { get; set; }

        
        public int[] AccessId { get; set; }
        public MultiSelectList AccessList { get; set; }
    }
}
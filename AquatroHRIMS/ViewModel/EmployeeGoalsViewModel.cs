using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using AquatroHRIMS.Models;
using GridMvc;


namespace AquatroHRIMS.ViewModel
{
    public class EmployeeGoalsViewModel:tblEmployeeGoal
    {
        public tblEmployeeGoal objEmpGoals { get; set; }
        public SelectList StatusModel { get; set; }
        public SelectList GoalTitleModel { get; set; }
        public SelectList EmployeeModel { get; set; }
        public string completion { get; set; }
        public string ID { get; set; }

    }
}
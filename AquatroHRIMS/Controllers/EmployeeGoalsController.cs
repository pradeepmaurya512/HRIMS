using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.Models;
using AquatroHRIMS.ViewModel;

namespace AquatroHRIMS.Controllers
{
    public class EmployeeGoalsController : Controller
    {
        //
        // GET: /EmployeeGoals/

        HRIMSdbEntities db = new HRIMSdbEntities();
        EmployeeGoalsViewModel objEmpViewModel = new EmployeeGoalsViewModel();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddGoals()
        {
            //================================== Employee Goal Title Binding =======================================//

            List<tblGoalTitle> tgt = (from data in db.tblGoalTitles
                                      orderby data.varTitleName ascending
                                      select data).ToList();

            tblGoalTitle gt = new tblGoalTitle();
            gt.intTilteID = 0;
            gt.varTitleName = "-- Select --";
            tgt.Insert(0, gt);

            SelectList objGoalList = new SelectList (tgt, "intTilteID", "varTitleName", 0);
           
            objEmpViewModel.GoalTitleModel = objGoalList;

            //=================================== End Employee Goal title binding =================================//

            //=================================== Status Binding ==================================================//

            List<tblStatus> tc = (from data in db.tblStatus
                                  orderby data.varStatus ascending
                                  select data).ToList();

            tblStatus tc1 = new tblStatus();
            tc1.intStatusID = 0;
            tc1.varStatus = "-- Select --";
            tc.Insert(0, tc1);

            SelectList objStatusList = new SelectList(tc, "intStatusID", "varStatus", 0);

            objEmpViewModel.StatusModel = objStatusList;
            
            //=================================== End Status Binding ==============================================//
            
            return View(objEmpViewModel);
        }
        [HttpPost]
        public ActionResult AddGoals(EmployeeGoalsViewModel objGoals)
        {
            try
            {
                objGoals.objEmpGoals.dtStartdate = DateTime.Now;
                objGoals.objEmpGoals.varCompletion = objGoals.completion;
                objGoals.objEmpGoals.dtCreatedOn = DateTime.Now;
                db.tblEmployeeGoals.Add(objGoals.objEmpGoals);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return RedirectToAction("AddGoals");
        }
        public ActionResult ReviewGoals()
        {
            int empID = 0;
            int titleID = 0;

            if (Request.Form.Count > 0)
            {
                empID = Convert.ToInt32(Request.Form["objEmpGoals.intEmployeeID"]);
                titleID = Convert.ToInt32(Request.Form["objEmpGoals.intTitle"]);
            }

            //=================================== Employee Binding ==================================================//

            List<tblEmployee> empList = (from data in db.tblEmployees
                                         orderby data.varFirstName ascending
                                         select data).ToList();
            tblEmployee empList1 = new tblEmployee();
            empList1.intEmployeeID = 0;
            empList1.varFirstName = "-- Select --";
            empList.Insert(0, empList1);
            SelectList objEmployeeList = new SelectList(empList, "intEmployeeID", "varFirstName", empID);
            objEmpViewModel.EmployeeModel = objEmployeeList;

            //=================================== End Employee Binding ==============================================//
            
            if (empID == 0)
            {
                //================================== Employee Goal Title Binding =======================================//

                List<tblGoalTitle> tgt = new List<tblGoalTitle>();
                tblGoalTitle gt = new tblGoalTitle();
                gt.intTilteID = 0;
                gt.varTitleName = "-- Select --";
                tgt.Insert(0, gt);
                SelectList objGoalList = new SelectList(tgt, "intTilteID", "varTitleName", 0);
                objEmpViewModel.GoalTitleModel = objGoalList;

                //================================== End Employee Goal Title Binding =======================================//

                //=================================== Status Binding ==================================================//
                
                List<tblStatus> tc = new List<tblStatus>();
                tblStatus tc1 = new tblStatus();
                tc1.intStatusID = 0;
                tc1.varStatus = "-- Select --";
                tc.Insert(0, tc1);
                SelectList objStatusList = new SelectList(tc, "intStatusID", "varStatus", 0);
                objEmpViewModel.StatusModel = objStatusList;

                //=================================== End Status Binding ==============================================//         
            }
            else
            {
                   List<tblGoalTitle> gt = (from data in db.tblGoalTitles
                                             join data1 in db.tblEmployeeGoals on data.intTilteID equals data1.intTitle
                                             where (data1.intEmployeeID == empID)
                                             select data).ToList();

                    tblGoalTitle gt1 = new tblGoalTitle();
                    gt1.intTilteID = 0;
                    gt1.varTitleName = "-- Select --";
                    gt.Insert(0, gt1);
                    SelectList objGoalList = new SelectList(gt, "intTilteID", "varTitleName", titleID);
                    objEmpViewModel.GoalTitleModel = objGoalList;

                    List<tblStatus> tc = (from data in db.tblStatus
                                          orderby data.varStatus ascending
                                          select data).ToList();

                    tblStatus tc1 = new tblStatus();
                    tc1.intStatusID = 0;
                    tc1.varStatus = "-- Select --";
                    tc.Insert(0, tc1);
                    SelectList objStatusList = new SelectList(tc, "intStatusID", "varStatus", 0);
                    objEmpViewModel.StatusModel = objStatusList;
                                
                     var query = (from data in db.tblEmployeeGoals
                                  where (data.intTitle == titleID && data.intEmployeeID == empID)
                                  select new { data.intStatus, data.varGoalDescription, data.varCompletion,data.varManagerComment }).Distinct();

                     foreach (var i in query)
                     {                        
                         objEmpViewModel.varGoalDescription = i.varGoalDescription;
                         objEmpViewModel.varCompletion = i.varCompletion + "%";
                         objEmpViewModel.intStatus = i.intStatus;
                         objEmpViewModel.varManagerComment = i.varManagerComment;
                     }            
            }
            //=================================== End Employee Goal title binding =================================//
                                            
            return View(objEmpViewModel);           
        }        
        [HttpPost]
        public ActionResult saveGoalTitle(string goalTitle, string goalDesc)
        {            
            tblGoalTitle gt = new tblGoalTitle();
            gt.varTitleName = goalTitle;
            gt.varTitleDesc = goalDesc;
            gt.dtCreatedOn = DateTime.Now;

            db.tblGoalTitles.Add(gt);
            db.SaveChanges();

            return View();           
        }               
        [HttpPost]
        public ActionResult updateGoal(EmployeeGoalsViewModel objReviewGoals)
        {
            string strMgrComment = objReviewGoals.varManagerComment;
            int empId = Convert.ToInt32(objReviewGoals.objEmpGoals.intEmployeeID);
            int titleID = Convert.ToInt32(objReviewGoals.objEmpGoals.intTitle);
            db.Database.ExecuteSqlCommand("update tblemployeeGoals set varManagerComment='" + strMgrComment + "' where intEmployeeID=" + empId + " and intTitle=" + titleID);
            return RedirectToAction("ReviewGoals");
        }
	}
}
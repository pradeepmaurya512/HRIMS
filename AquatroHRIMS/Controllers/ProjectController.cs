using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AquatroHRIMS.Models;
using AquatroHRIMS.ViewModel;
using AquatroHRIMS.App_Code;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;

namespace AquatroHRIMS.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        HRIMSdbEntities db = new HRIMSdbEntities();
        public ActionResult AddProject()
        {
            //============================= Employee binding entity =======================================//


            List<tblEmployee> empList = (from data in db.tblEmployees
                                         orderby data.varFirstName ascending
                                         select data).ToList();
            tblEmployee tm = new tblEmployee();
            tm.intEmployeeID = 0;
            tm.varFirstName = "-- Select --";
            empList.Insert(0, tm);
            SelectList objModelEmp = new SelectList(empList, "intEmployeeID", "varFirstName", 0);

            ProjectViewModel objPrjModel = new ProjectViewModel();
            objPrjModel.EmployeeModel = objModelEmp;

            //============================= End Employee Binding Entity ===================================//

            //============================= Project Status entity =========================================//

            List<tblStatus> stList = (from data in db.tblStatus
                                      orderby data.varStatus ascending
                                      select data).ToList();

            tblStatus ts = new tblStatus();
            ts.intStatusID = 0;
            ts.varStatus = "-- Select --";
            stList.Insert(0, ts);

            SelectList objModelStatus = new SelectList(stList, "intStatusID", "varStatus", 0);

            objPrjModel.ProjectStatusList = objModelStatus;
            
            //============================= End Project Status entity ======================================//

            //============================= Client entity =================================================//

            List<tblClientDetail> clientList = (from data in db.tblClientDetails
                                                orderby data.varClientName ascending
                                                select data).ToList();
            tblClientDetail tcd = new tblClientDetail();
            tcd.intClientID = 0;
            tcd.varClientName = "-- Select--";
            clientList.Insert(0, tcd);

            SelectList objClientList = new SelectList(clientList, "intClientID", "varClientName", 0);
            objPrjModel.ClientModel = objClientList;
                            
            //============================= End Client entity =============================================//

            //============================= Internal Team entity ==========================================//

            List<tblEmployee> internTeamList = (from data in db.tblEmployees
                                         orderby data.varFirstName ascending
                                         select data).ToList();

            SelectList objInternList = new SelectList(internTeamList, "intEmployeeID", "varFirstName", 0);

            objPrjModel.InternalTeam = objInternList;


            //============================= End Internal Team entity ======================================//

            //============================ Completion entity =============================================//

            List<tblCompletion> complList = (from data in db.tblCompletions
                                             orderby data.varComplition ascending
                                             select data).ToList();

            tblCompletion tcm = new tblCompletion();
            tcm.intCompletionId = 0;
            tcm.varComplition = "-- Select --";
            complList.Insert(0,tcm);
            SelectList objCompletionlist = new SelectList(complList, "intCompletionId", "varComplition", 0);

            objPrjModel.CompletionModel = objCompletionlist;

            //=========================== End Completion entity ========================================//

            return View(objPrjModel);
        }
        [HttpPost]
        public ActionResult AddProject(ProjectViewModel objProjectViewModel)
        {
            try
            {
                int clientID = 0;
                var ClientTeamId = from data in db.tblClientTeams
                                   where data.varClientTeamMemName == objProjectViewModel.strExtClientHead
                                   select new { ID = data.intClientTeamID };

                foreach (var i in ClientTeamId)
                {
                    clientID = i.ID;
                }

                int[] empId = objProjectViewModel.EmployeeID;

                string selEmpId = String.Join(",",empId);

                //if (clientID == 0)
                //{
                //    tblClientTeam tc = new tblClientTeam();
                //    tc.varClientTeamMemName = objProjectViewModel.strExtClientHead;
                //    tc.intActive = true;
                //    tc.dtCreatedOn = DateTime.Now;
                //    tc.varClientTeamMemEmail = objProjectViewModel.strExtClientEmailID;
                //    db.tblClientTeams.Add(tc);
                //    db.SaveChanges();
                //    clientID = tc.intClientTeamID;
                //}

                objProjectViewModel.objProjectDetails.varTeamMemDetails = selEmpId;
                objProjectViewModel.objProjectDetails.dtCreatedOn = DateTime.Now;
                objProjectViewModel.objProjectDetails.varClientProjectHead = clientID.ToString();
                db.tblProjectDetails.Add(objProjectViewModel.objProjectDetails);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
            return RedirectToAction("AddProject");
        }
        [HttpPost]
        public JsonResult getClientTeamList(string criteria, int clientId)
        {
            var query = (from c in db.tblClientTeams
                         where (c.varClientTeamMemName.StartsWith(criteria) && c.intClientId == clientId )
                         orderby c.varClientTeamMemName ascending
                         select new { c.varClientTeamMemName }).Distinct();

            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getClientEmailAddress(string criteria, int clientId)
        {
            var query = (from c in db.tblClientTeams
                         where (c.varClientTeamMemName == criteria && c.intClientId == clientId)
                         select new { c.varClientTeamMemEmail }).Distinct();
            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult checkValidEmailID(string criteria, int clientId)
        {
            var query = (from c in db.tblClientTeams
                         where (c.varClientTeamMemEmail == criteria && c.intClientId == clientId)
                         select new { c.varClientTeamMemName}).Distinct();
            return Json(query.ToList(),JsonRequestBehavior.AllowGet);
        }
	}
}
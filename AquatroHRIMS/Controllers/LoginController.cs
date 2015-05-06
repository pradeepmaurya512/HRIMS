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
using System.Text;
namespace AquatroHRIMS.Controllers
{
    public class LoginController : Controller
    {
        HRIMSdbEntities db = new HRIMSdbEntities();        
        public ActionResult CreateEmployee()

        {
            int empID = 0;
            //===================================== Department Binding entity ============================================= //
            List<tblDepartment> deptList = (from data in db.tblDepartments
                                            orderby data.varDepartmentName ascending
                                            select data).ToList();
            tblDepartment objDept = new tblDepartment();
            objDept.varDepartmentName = "-- Select --";
            objDept.intDepartmentID = 0;
            deptList.Insert(0, objDept);
            SelectList objModelDept = new SelectList(deptList, "intDepartmentID", "varDepartmentName", 0);
            LoginViewModel objLogin = new LoginViewModel();
            objLogin.DepartmentModel = objModelDept;
            //===================================== End Department Binding ==================================================//


            //==================================== Start Designation Binding entity =======================================//
            List<tblDesignation> desigList = (from data in db.tblDesignations
                                              orderby data.varDesignationName ascending
                                              select data).ToList();
            tblDesignation objDesignation = new tblDesignation();
            objDesignation.varDesignationName = "-- Select --";
            objDesignation.intDesignationID = 0;
            desigList.Insert(0, objDesignation);
            SelectList objModelDesignation = new SelectList(desigList, "intDesignationID", "varDesignationName", 0);
            objLogin.DesignationModel = objModelDesignation;
            //=================================== End Designation Binding ================================================//


            //=================================== Employee Binding entity ================================================//
            List<tblEmployee> empList = (from data in db.tblEmployees 
                                         orderby data.varFirstName ascending
                                         select data).ToList();
            var empIndex = empList.Find(item => item.varFirstName == "Azam");

            if (empIndex != null)
            {
                empID = empIndex.intEmployeeID;
            }
            else
            {
                empID = 0;
            }

            tblEmployee objemp = new tblEmployee();
            objemp.varFirstName = "-- Select --";
            objemp.intEmployeeID = 0;
            empList.Insert(0, objemp);
            SelectList objEmployeeModel = new SelectList(empList, "intEmployeeID", "varFirstName", empID);
            objLogin.EmployeeModel = objEmployeeModel;
            //=================================== End Employee Binding =====================================================//

            //======================Access Binding entity ==============================================================//
            List<tblAccess> accessList = (from data in db.tblAccesses
                                          orderby data.varAccessName ascending
                             select data).ToList();
            SelectList objAccessList = new SelectList(accessList, "intAccessID", "varAccessName", 0);
            objLogin.AccessList = objAccessList;
            //=====================End Access Binding =======================================================================//


            return View(objLogin); 
        }

        [HttpPost]
        public ActionResult CreateEmployee(LoginViewModel objtlogin, int[] AccessId)
        {
            try
            {
                string accesslevels = string.Join(",", AccessId);

                string strOfficialEmailId = objtlogin.objtblLogin.tblEmployee.varOfficeEmailAdd;
                string strUserName = objtlogin.objtblLogin.varLoginName;
                string strPassword = objtlogin.objtblLogin.varPassword;

                objtlogin.objtblLogin.tblEmployee.varAccessLevel = accesslevels;
                objtlogin.objtblLogin.varLoginName = strOfficialEmailId;
                objtlogin.objtblLogin.IsActive = true;

                db.tblLogins.Add(objtlogin.objtblLogin);
                db.SaveChanges();

                sendMail(strOfficialEmailId, strPassword);

                ViewBag.Message = "Data has been submitted successfully!!.";

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;  // You can also choose to handle the exception here...
            }

          

            return RedirectToAction("CreateEmployee");
        }


        [HttpGet]
        public ActionResult AccountLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AccountLogin(tblLogin objlogin)
        {
            if (IsValid(objlogin.varLoginName, objlogin.varPassword))
            {
                FormsAuthentication.SetAuthCookie(objlogin.varLoginName, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("AccountLogin", "Login");
                //ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(objlogin);
        }

        private bool IsValid(string username, string password)
        {
            
            bool IsValid = false;
            using (HRIMSdbEntities db = new HRIMSdbEntities())
            {
                var user = db.tblLogins.Where(x => x.varLoginName == username).FirstOrDefault();
                if (user != null)
                {
                    if (user.varPassword == password)
                    {
                        IsValid = true;
                    }
                }
            }
            return IsValid;
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult Email()
        {
            return View();
        }

        public void sendMail(string email, string password)
        {
            try
            {
                string strBody = "";
                StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "HTML/EmpCredential.html"));
                strBody = sr.ReadToEnd();
                sr.Close();
                strBody = strBody.Replace("#emailId#", email);
                strBody = strBody.Replace("#password#", password);

                string str = Mail.SendEmail(email, "Employee Credentials", strBody, true);
            }
            catch (Exception ex)
            {                
                throw ex;
            }           
        }
	}
}

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
    public class ClientController : Controller
    {
        HRIMSdbEntities db = new HRIMSdbEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Client()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Client(tblClientDetail objClientDetails)
        {           
            objClientDetails.dtCreatedOn = DateTime.Now;
            db.tblClientDetails.Add(objClientDetails);
            db.SaveChanges();

            return RedirectToAction("Client");
        }
	}
}
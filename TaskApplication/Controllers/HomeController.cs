using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskApplication.Models;

namespace TaskApplication.Controllers
{
    public class HomeController : Controller
    {
        private TaskContex Repository = new TaskContex();

        public ActionResult Index()
        {
            //ViewBag.Message = Repository.Statuses.FirstOrDefault().StatusName;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
    }
}

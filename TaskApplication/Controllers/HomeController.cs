using System.Web.Mvc;

namespace TaskApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/About
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
    }
}

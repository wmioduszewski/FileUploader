using System.Web.Mvc;

namespace FileUploader.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "What this application is designed for?";

            return View();
        }
    }
}
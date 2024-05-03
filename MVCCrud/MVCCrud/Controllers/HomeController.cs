using MVCCrud.Authorization;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    [CustomActionFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
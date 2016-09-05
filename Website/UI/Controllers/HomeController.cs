using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : BaseController
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
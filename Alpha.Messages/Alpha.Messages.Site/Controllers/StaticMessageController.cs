namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using CN88.Web.Controllers;

    public class StaticMessageController : CN88BaseController
    {
        public ActionResult NewSportType() => View();

        public ActionResult Index(string id) => View(id);
    }
}
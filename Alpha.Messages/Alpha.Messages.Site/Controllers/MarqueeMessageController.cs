namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.MarqueeMessage;
    using Sunplus.Agent.Model.Entities;

    public class MarqueeMessageController : SiteBaseController
    {
        private readonly IUserSession userSession;

        public MarqueeMessageController(IMediator mediator, IUserSession userSession)
            : base(mediator)
        {
            this.userSession = userSession;
        }

        [HttpPost]
        public ActionResult Index()
        {
            var query = new MarqueeMessageQuery(userSession, false);

            return Json(mediator.Send(query));
        }

    }
}
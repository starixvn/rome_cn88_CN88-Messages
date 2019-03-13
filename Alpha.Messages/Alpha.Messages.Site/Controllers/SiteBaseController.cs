namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using CN88.Web.Controllers;
    using MediatR;

    public abstract class SiteBaseController : CN88BaseController
    {
        protected readonly IMediator mediator;

        protected SiteBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected ActionResult View<TResponse>(IRequest<TResponse> message)
            => View(mediator.Send(message));

        protected ActionResult Json<TResponse>(IRequest<TResponse> message)
            => new JsonResult { Data = mediator.Send(message) };
    }
}
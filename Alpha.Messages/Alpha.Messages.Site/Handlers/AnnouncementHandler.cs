namespace Alpha.Messages.Site.Handlers
{
    using CN88.Web.Helpers;
    using Constants;
    using MediatR;
    using Params.Announcement;
    using Requests.Announcement;
    using Services;
    using ViewModels;

    public class AnnouncementHandler :
        IRequestHandler<AnnouncementsHistoryQuery, AnnouncementHistoryViewModel>
    {
        private readonly AnnouncementService announcementService;
        private readonly AnnouncementFormatService formatAnnouncementService;
        private readonly NewMessageService newMessageService;
        private readonly BrowserHelper browserHelper;

        public AnnouncementHandler(
            AnnouncementService announcementService,
            AnnouncementFormatService formatService,
            NewMessageService newMessageService,
            BrowserHelper browserHelper)
        {
            this.announcementService = announcementService;
            this.formatAnnouncementService = formatService;
            this.newMessageService = newMessageService;
            this.browserHelper = browserHelper;
        }

        public AnnouncementHistoryViewModel Handle(AnnouncementsHistoryQuery message)
        {
            var parameters = (AnnouncementsHistoryParams)message.ConvertToParameters();

            var announcements = announcementService.GetAnnouncementsHistory(parameters);
            var formatedAnnouncements = formatAnnouncementService.Format(announcements, parameters);

            var unreadMessage = newMessageService.GetNumberOfUnreadMessage(parameters);

            return new AnnouncementHistoryViewModel(
                formatedAnnouncements,
                parameters.FromDate,
                parameters.ToDate,
                MessageType.System,
                message.RoleId,
                unreadMessage,
                browserHelper);
        }
    }
}
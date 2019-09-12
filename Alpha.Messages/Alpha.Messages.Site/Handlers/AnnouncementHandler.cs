namespace Alpha.Messages.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using CN88.Web.Helpers;
    using Constants;
    using MediatR;
    using Models.Announcement;
    using Params.Announcement;
    using Requests.Announcement;
    using Services;
    using ViewModels;

    public class AnnouncementHandler :
        IRequestHandler<AnnouncementsHistoryQuery, AnnouncementHistoryViewModel>,
        IRequestHandler<PopupAnnouncementQuery, AlphaAnnouncement>
    {
        private readonly AnnouncementService announcementService;
        private readonly NewMessageService newMessageService;
        private readonly BrowserHelper browserHelper;

        public AnnouncementHandler(
            AnnouncementService announcementService,
            NewMessageService newMessageService,
            BrowserHelper browserHelper)
        {
            this.announcementService = announcementService;
            this.newMessageService = newMessageService;
            this.browserHelper = browserHelper;
        }

        public AnnouncementHistoryViewModel Handle(AnnouncementsHistoryQuery message)
        {
            var parameters = (AnnouncementsHistoryParams)message.ConvertToParameters();

            var announcements = announcementService.GetAnnouncementsHistory(parameters);
            var formatedAnnouncements = Format(announcements, parameters);

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

        public AlphaAnnouncement Handle(PopupAnnouncementQuery message)
        {
            var parameters = (PopupAnnouncementParams)message.ConvertToParameters();
            var announcement = announcementService.GetPopUpAnnouncements(parameters);
            UpdatePopupIsShowedOnSession(message, announcement != null);

            return Format(announcement, parameters);
        }

        private static void UpdatePopupIsShowedOnSession(PopupAnnouncementQuery message, bool hasAnnouncement)
        {
            message.IsShowedPopup = hasAnnouncement ? hasAnnouncement : message.IsShowedPopup;
        }

        private static IEnumerable<AlphaAnnouncement> Format(IEnumerable<AlphaAnnouncement> announcements, BaseAnnouncementsParams message)
            => announcements.Select(i => Format(i, message));

        private static AlphaAnnouncement Format(AlphaAnnouncement announcement, BaseAnnouncementsParams message)
        {
            if (announcement != null)
            {
                announcement.Content = ReplaceMessageSiteTag(announcement.Content, message.IsMobileDevice);
            }

            return announcement;
        }

        private static string ReplaceMessageSiteTag(string content, bool isMoblie)
            => content.Replace(MessageConstants.SiteMessageTag, isMoblie ? MicroSites.MessageUrl("~") : MicroSites.MessageUrl());
    }
}
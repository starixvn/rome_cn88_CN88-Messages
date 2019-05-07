namespace Alpha.Messages.Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Alpha.Messages.Models.Announcement;
    using Helpers;
    using Models.Message;

    public class AnnouncementHistoryViewModel : MessageBase
    {
        private readonly BrowserHelper browserHelper;

        public AnnouncementHistoryViewModel(
            IEnumerable<AlphaAnnouncement> announcements,
            DateTime fromDate,
            DateTime toDate,
            int messageType,
            byte roleId,
            NumberOfUnreadMessage unreadMessage,
            BrowserHelper browserHelper) : base(messageType, roleId, fromDate, toDate, unreadMessage)
        {
            Announcements = announcements;
            MessageType = Constants.MessageType.System;
            this.browserHelper = browserHelper;
        }

        public IEnumerable<AlphaAnnouncement> Announcements { get; }

        public DateTime MinDate => DateTime.MinValue;

        public DateTime MaxDate => DateTime.Now;

        public DateTime CurrentDate => DateTime.Now;

        public bool IsDevice => browserHelper.IsMobile(isOnlyTablet: false);
    }
}
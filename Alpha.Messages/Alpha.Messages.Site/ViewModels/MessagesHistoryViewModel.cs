namespace Alpha.Messages.Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Helpers;
    using Models.Message;

    public class MessagesHistoryViewModel : MessageBase
    {
        private readonly BrowserHelper browserHelper;

        public MessagesHistoryViewModel(
            IEnumerable<MessageHistory> messages,
            DateTime lastSeenAnnouncement,
            string languageKey,
            DateTime startDate,
            DateTime endDate,
            int messageType,
            byte roleId,
            NumberOfUnreadMessage unreadMessage,
            BrowserHelper browserHelper) : base(messageType, roleId, startDate, endDate, unreadMessage)
        {
            Messages = messages;
            LastSeenAnnouncement = lastSeenAnnouncement;
            LanguageKey = languageKey;
            this.browserHelper = browserHelper;
        }

        public IEnumerable<MessageHistory> Messages { get; }

        public DateTime LastSeenAnnouncement { get; }

        public string LanguageKey { get; }

        public bool IsNewMessage(DateTime postTime) => postTime > LastSeenAnnouncement;

        public bool IsDevice => browserHelper.IsMobile(isOnlyTablet: false);
    }
}

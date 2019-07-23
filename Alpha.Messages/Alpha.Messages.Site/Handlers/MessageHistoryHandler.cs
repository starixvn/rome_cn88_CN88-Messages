namespace Alpha.Messages.Site.Handlers
{
    using System;
    using Helpers;
    using MediatR;
    using Models.Message;
    using Params;
    using Requests.MessageHistory;
    using Services;
    using ViewModels;
    using Constants;

    public class MessageHistoryHandler : IRequestHandler<MessageHistoryQuery, MessagesHistoryViewModel>
    {
        private readonly MessageHistoryService messageService;
        private readonly NewMessageService newMessageService;
        private readonly AnnouncementService announcementService;
        private readonly BrowserHelper browserHelper;

        public MessageHistoryHandler(
            MessageHistoryService messageService,
            NewMessageService newMessageService,
            AnnouncementService announcementService,
            BrowserHelper browserHelper)
        {
            this.messageService = messageService;
            this.newMessageService = newMessageService;
            this.announcementService = announcementService;
            this.browserHelper = browserHelper;
        }

        public MessagesHistoryViewModel Handle(MessageHistoryQuery message)
        {
            var lastSeenDate = GetLastSeenDate(message.CustId, message.SubAccId, message.MessageType);
            var messageParams = (MessageHistoryParams)message.ConvertToParameters();
            var messages = message.MessageType == MessageType.Personal ? messageService.GetPrivateMessagesHistory(messageParams) : 
                message.MessageType == MessageType.Special ? messageService.GetSpecialMessagesHistory(messageParams) : 
                messageService.GetPublicMessagesHistory(messageParams);

            var numberOfUnreadMessage = newMessageService.GetNumberOfUnreadMessage(messageParams);
            UpdateUnreadMessageOnSession(message, numberOfUnreadMessage);

            return new MessagesHistoryViewModel(
                messages,
                lastSeenDate,
                message.Language,
                messageParams.StartDate,
                message.EndDate,
                message.MessageType,
                message.RoleId,
                numberOfUnreadMessage,
                browserHelper);
        }

        private static void UpdateUnreadMessageOnSession(MessageHistoryQuery message, NumberOfUnreadMessage unreadMessage)
        {
            message.NumberOfUnreadMessage = unreadMessage;
        }

        private DateTime GetLastSeenDate(int custId, int subAccId, int messageType)
        {
            var lastSeenMessage = announcementService.GetLastSeenAnnouncement(custId, subAccId);

            return lastSeenMessage.GetLastSeenByType(messageType);
        }

    }
}
namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Criteria.Message;
    using Fanex.Data.Repository;
    using Models.Message;
    using Params;

    public interface MessageHistoryService
    {
        IEnumerable<MessageHistory> GetPublicMessagesHistory(MessageHistoryParams messageParams);

        IEnumerable<MessageHistory> GetPrivateMessagesHistory(MessageHistoryParams messageParams);

        IEnumerable<MessageHistory> GetSpecialMessagesHistory(MessageHistoryParams messageParams);
    }

    public class MessageHistoryServiceImpl : MessageHistoryService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly AnnouncementService announcementService;
        public MessageHistoryServiceImpl(
            AnnouncementService announcementService,
            IDynamicRepository dynamicRepository)
        {
            this.announcementService = announcementService;
            this.dynamicRepository = dynamicRepository;
        }

        public IEnumerable<MessageHistory> GetPrivateMessagesHistory(MessageHistoryParams messageParams)
            => ProcessPrivateMessagesHistory(messageParams);

        public IEnumerable<MessageHistory> GetPublicMessagesHistory(MessageHistoryParams messageParams)
            => ProcessPublicMessagesHistory(messageParams);

        public IEnumerable<MessageHistory> GetSpecialMessagesHistory(MessageHistoryParams messageParams)
            => ProcessSpecialMessagesHistory(messageParams);

        private IEnumerable<MessageHistory> ProcessPublicMessagesHistory(MessageHistoryParams messageParams)
        {
            UpdateLastSeenMessage(messageParams);

            var criteria = new GetPublicMessagesHistoryCriteria(
                messageParams.CustId,
                messageParams.ParentSites,
                messageParams.SpecialType,
                GetLimitedDate(messageParams.StartDate, messageParams.LimitedDate, messageParams.MessageType),
                messageParams.EndDate,
                messageParams.IsTest,
                messageParams.MessageType);

            return dynamicRepository.Fetch<MessageHistory>(criteria);
        }

        private IEnumerable<MessageHistory> ProcessSpecialMessagesHistory(MessageHistoryParams messageParams)
        {
            UpdateLastSeenMessage(messageParams);

            var criteria = new GetSpecialMessagesHistoryCriteria(
                messageParams.CustId,
                messageParams.ParentSites,
                messageParams.SpecialType,
                GetLimitedDate(messageParams.StartDate, messageParams.LimitedDate, messageParams.MessageType),
                messageParams.EndDate,
                messageParams.IsTest,
                messageParams.MessageType);

            return dynamicRepository.Fetch<MessageHistory>(criteria);
        }

        private IEnumerable<MessageHistory> ProcessPrivateMessagesHistory(MessageHistoryParams messageParams)
        {
            UpdateLastSeenMessage(messageParams);

            var criteria = new GetPrivateMessagesHistoryCriteria(
                messageParams.CustId,
                messageParams.SubAccId,
                messageParams.SpecialType,
                MessageConstants.DefaultTotalMessageHistory,
                GetLimitedDate(messageParams.StartDate, messageParams.LimitedDate, messageParams.MessageType),
                messageParams.EndDate,
                messageParams.IsTest);

            return dynamicRepository.Fetch<MessageHistory>(criteria);
        }

        private void UpdateLastSeenMessage(MessageHistoryParams messageParams)
        {
            messageParams.StartDate = GetAvailableLastSeenDate(messageParams.StartDate, messageParams.MessageType, messageParams.CustId, messageParams.SubAccId);
            announcementService.UpdateLastSeenAnnouncement(messageParams.CustId, messageParams.MessageType, messageParams.IsAdminUser, messageParams.SubAccId);
        }

        private DateTime GetAvailableLastSeenDate(DateTime fromDate, int messageType, int custId, int subAccId)
        {
            var lastSeenAnnouncement = announcementService.GetLastSeenAnnouncement(custId, subAccId);
            var lastSeenDate = lastSeenAnnouncement.GetLastSeenByType(messageType);
            if (fromDate == DateTime.MinValue)
            {
                var threeDayAgo = DateTime.Today.AddDays(-3);
                var sixMonthAgo = DateTime.Today.AddMonths(-6);
                if (threeDayAgo < lastSeenDate)
                {
                    return threeDayAgo;
                }
                else if (lastSeenDate < threeDayAgo && lastSeenDate > sixMonthAgo)
                {
                    return lastSeenDate;
                }
                else
                {
                    return sixMonthAgo;
                }
            }

            return fromDate;
        }

        private static DateTime GetLimitedDate(DateTime fromDate, DateTime limitedDate, int messageType)
            => messageType == MessageType.Personal && fromDate < limitedDate
                ? limitedDate
                : fromDate;
    }
}
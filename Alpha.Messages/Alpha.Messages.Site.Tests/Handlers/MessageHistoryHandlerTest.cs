namespace Alpha.Messages.Site.Tests.Handlers
{
    using System;
    using System.Linq;
    using CN88.Web.Helpers;
    using Helpers;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Constants;
    using Models.Announcement;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Params.Announcement;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.Requests.MessageHistory;
    using Site.ViewModels;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class MessageHanlderTests
    {
        private MessageHistoryService messageService;
        private NewMessageService newMessageService;
        private AnnouncementService announcementService;
        private MessageHistoryHandler messageHandler;
        private BrowserHelper browserHelper;
#pragma warning disable CS0618 // Type or member is obsolete
        private IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<MessageHistoryService>();
            newMessageService = Substitute.For<NewMessageService>();
            announcementService = Substitute.For<AnnouncementService>();
            browserHelper = Substitute.For<BrowserHelper>();
            messageHandler = new MessageHistoryHandler(messageService, newMessageService, announcementService, browserHelper);

#pragma warning disable CS0618 // Type or member is obsolete
            userSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // Type or member is obsolete
            userSession.Language.Returns(Language.English);
            userSession.CustId.Returns(263);
            userSession.RoleId.Returns(RoleId.Agent);
            userSession.GetProperty<DateTime>(SessionKey.CustomerDatelimit).Returns(DateTime.MinValue);
            fixture = new Fixture();
        }

        [Test]
        public void Handle_NotNullQuery_CallGetMessagesHistory()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            messageService.GetPrivateMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>())
                .Returns(FakeDataHelper.Create<LastSeenAnnouncement>());

            var query = new MessageHistoryQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false);

            // Act
            messageHandler.Handle(query);

            // Assert
            messageService
                .Received()
                .GetPrivateMessagesHistory(Arg.Is<MessageHistoryParams>(i => i.CustId == query.CustId
                && i.ParentSites == query.ParentSites
                && i.SpecialType == query.SpecialType
                && i.StartDate == query.StartDate
                && i.EndDate == query.EndDate
                && i.LimitedDate == query.LimitedDateToShowInfo
                && i.IsTest == query.IsTest
                && i.MessageType == query.MessageType));
        }

        [Test]
        public void HandleMessageQuery_NotNullQuery_ReturnMessagesViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            UrlHelperTestSetUp.SetUp();
            var now = DateTime.Now;

            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var lastSeenAnnouncement = new LastSeenAnnouncement(263, now, now, now, now);
            var numberOfUnReadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            newMessageService.GetNumberOfUnreadMessage(Arg.Any<BaseAnnouncementsParams>()).Returns(numberOfUnReadMessage);
            messageService.GetPrivateMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);

            var query = new MessageHistoryQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false);

            var expectedViewModel = new MessagesHistoryViewModel(
                messages,
                now,
                Language.Default,
                query.StartDate,
                query.EndDate,
                query.MessageType,
                query.RoleId,
                numberOfUnReadMessage,
                browserHelper);

            // Act
            var viewModel = messageHandler.Handle(query);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedViewModel, viewModel).AreEqual);
        }

        [Test]
        public void HandlePublic_NotNullQuery_CallGetMessagesHistory()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            messageService.GetPublicMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>())
                .Returns(FakeDataHelper.Create<LastSeenAnnouncement>());

            var query = new MessageHistoryQuery(
                userSession,
                0,
                DateTime.Today,
                DateTime.Today,
                false);

            // Act
            messageHandler.Handle(query);

            // Assert
            messageService
                .Received()
                .GetPublicMessagesHistory(Arg.Is<MessageHistoryParams>(i => i.CustId == query.CustId
                && i.ParentSites == query.ParentSites
                && i.SpecialType == query.SpecialType
                && i.StartDate == query.StartDate
                && i.EndDate == query.EndDate
                && i.LimitedDate == query.LimitedDateToShowInfo
                && i.IsTest == query.IsTest
                && i.MessageType == query.MessageType));
        }

        [Test]
        public void HandleMessageQueryPublic_NotNullQuery_ReturnMessagesViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            UrlHelperTestSetUp.SetUp();
            var now = DateTime.Now;

            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var lastSeenAnnouncement = new LastSeenAnnouncement(263, now, now, now, now);
            var numberOfUnReadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            newMessageService.GetNumberOfUnreadMessage(Arg.Any<BaseAnnouncementsParams>()).Returns(numberOfUnReadMessage);
            messageService.GetPublicMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);

            var query = new MessageHistoryQuery(
                userSession,
                0,
                DateTime.Today,
                DateTime.Today,
                false);

            var expectedViewModel = new MessagesHistoryViewModel(
                messages,
                now,
                Language.Default,
                query.StartDate,
                query.EndDate,
                query.MessageType,
                query.RoleId,
                numberOfUnReadMessage,
                browserHelper);

            // Act
            var viewModel = messageHandler.Handle(query);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedViewModel, viewModel).AreEqual);
        }

        [Test]
        public void HandleSpecial_NotNullQuery_CallGetMessagesHistory()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            messageService.GetSpecialMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>())
                .Returns(FakeDataHelper.Create<LastSeenAnnouncement>());

            var query = new MessageHistoryQuery(
                userSession,
                2,
                DateTime.Today,
                DateTime.Today,
                false);

            // Act
            messageHandler.Handle(query);

            // Assert
            messageService
                .Received()
                .GetSpecialMessagesHistory(Arg.Is<MessageHistoryParams>(i => i.CustId == query.CustId
                && i.ParentSites == query.ParentSites
                && i.SpecialType == query.SpecialType
                && i.StartDate == query.StartDate
                && i.EndDate == query.EndDate
                && i.LimitedDate == query.LimitedDateToShowInfo
                && i.IsTest == query.IsTest
                && i.MessageType == query.MessageType));
        }

        [Test]
        public void HandleMessageQuerySpecial_NotNullQuery_ReturnMessagesViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            UrlHelperTestSetUp.SetUp();
            var now = DateTime.Now;

            var messages = FakeDataHelper.CreateMany<MessageHistory>().ToList();
            var lastSeenAnnouncement = new LastSeenAnnouncement(263, now, now, now, now);
            var numberOfUnReadMessage = FakeDataHelper.Create<NumberOfUnreadMessage>();
            newMessageService.GetNumberOfUnreadMessage(Arg.Any<BaseAnnouncementsParams>()).Returns(numberOfUnReadMessage);
            messageService.GetSpecialMessagesHistory(Arg.Any<MessageHistoryParams>()).Returns(messages);
            announcementService.GetLastSeenAnnouncement(Arg.Any<int>(), Arg.Any<int>()).Returns(lastSeenAnnouncement);

            var query = new MessageHistoryQuery(
                userSession,
                2,
                DateTime.Today,
                DateTime.Today,
                false);

            var expectedViewModel = new MessagesHistoryViewModel(
                messages,
                now,
                Language.Default,
                query.StartDate,
                query.EndDate,
                query.MessageType,
                query.RoleId,
                numberOfUnReadMessage,
                browserHelper);

            // Act
            var viewModel = messageHandler.Handle(query);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedViewModel, viewModel).AreEqual);
        }
    }
}
namespace Alpha.Messages.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using Fixtures;
    using Messages.Constants;
    using Models.Announcement;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Announcement;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.Requests.Announcement;
    using Site.ViewModels;

    public class AnnouncementHandlerTest
    {
        private AnnouncementService announcementService;
        private NewMessageService messageService;
        private AnnouncementHandler handler;
        private UserSessionFixture userSessionFixture;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            announcementService = Substitute.For<AnnouncementService>();
            messageService = Substitute.For<NewMessageService>();
            handler = new AnnouncementHandler(announcementService, messageService, null);
            userSessionFixture = new UserSessionFixture();
            userSessionFixture.UserSession.RoleId.Returns(RoleId.Agent);
            fixture = new Fixture();
        }

        [Test]
        public void HandleAnnouncementsHistoryQuery_Alway_CallServices()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>();
            var query = new AnnouncementsHistoryQuery(userSessionFixture.UserSession, false);

            //// Act
            handler.Handle(query);

            //// Assert
            messageService.Received().GetNumberOfUnreadMessage(Arg.Any<BaseAnnouncementsParams>());
            announcementService.Received().GetAnnouncementsHistory(Arg.Any<AnnouncementsHistoryParams>());
        }

        [Test]
        public void HandleAnnouncementsHistoryQuery_Always_ReturnCorrectViewModel()
        {
            //// Arrange
            var announcements = new List<AlphaAnnouncement>();
            var query = new AnnouncementsHistoryQuery(userSessionFixture.UserSession, false);

            //// Act
            var act = handler.Handle(query);

            //// Assert
            Assert.IsInstanceOf(typeof(AnnouncementHistoryViewModel), act);
        }

        [Test]
        public void HandlePopupAnnouncementQuery_Alway_CallServices()
        {
            //// Arrange
            var announcement = fixture.Create<AlphaAnnouncement>();
            var query = new PopupAnnouncementQuery(userSessionFixture.UserSession, false);
            announcementService.GetPopUpAnnouncements(Arg.Any<PopupAnnouncementParams>())
                .Returns(announcement);

            //// Act
            handler.Handle(query);

            //// Assert
            announcementService.Received().GetPopUpAnnouncements(Arg.Any<PopupAnnouncementParams>());
        }

        [Test]
        public void HandlePopupAnnouncementQuery_Always_ReturnCorrectViewModel()
        {
            //// Arrange
            var announcement = fixture.Create<AlphaAnnouncement>();
            var query = new PopupAnnouncementQuery(userSessionFixture.UserSession, false);
            announcementService.GetPopUpAnnouncements(Arg.Any<PopupAnnouncementParams>())
                .Returns(announcement);

            //// Act
            var act = handler.Handle(query);

            //// Assert
            Assert.IsInstanceOf(typeof(AlphaAnnouncement), act);
        }
    }
}
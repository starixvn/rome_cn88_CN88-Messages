namespace Alpha.Messages.Site.Requests
{
    using Alpha.Announcements.Models;
    using Alpha.Messages.Constants;
    using Params.Announcement;
    using Sunplus.Agent.Model.Base;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.BL.Definitions;

    public class MessageHistoryBaseRequest
    {
        protected readonly IUserSession userSession;

        public MessageHistoryBaseRequest(IUserSession userSession, bool isTest)
        {
            this.userSession = userSession ?? new UserSessionExt();
            IsTest = isTest;
        }

        public IParam Specification { get; set; }

        public int UserId => userSession.CustId;

        public string ParentSites => MessageConstants.ParentSitesByLevel[userSession.RoleId];

        public int SubAccId => userSession.SubAccId;

        public byte RoleId => userSession.RoleId;

        public int AdminId => userSession.AdminId;

        public bool IsAdminUser => AdminId != 0;

        public int CustId => userSession.CustId;

        public string Language => userSession.Language;

        public string UserName => userSession.UserName;

        public string Site => userSession.Site;

        public bool IsTest { get; set; }

        virtual public BaseAnnouncementsParams ConvertToParameters()
            => new BaseAnnouncementsParams
            {
                AdminId = AdminId,
                CustId = CustId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                SubAccId = SubAccId,
                UserName = UserName,
                SpecificationParamter = Specification,
                ParentSites = ParentSites,
                IsTest = IsTest
            };
    }
}
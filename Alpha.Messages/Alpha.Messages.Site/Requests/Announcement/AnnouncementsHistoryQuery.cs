namespace Alpha.Messages.Site.Requests.Announcement
{
    using System;
    using MediatR;
    using Params.Announcement;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;
    using Configurations;

    public class AnnouncementsHistoryQuery : MessageHistoryBaseRequest, IRequest<AnnouncementHistoryViewModel>
    {
        public AnnouncementsHistoryQuery() : this(null, null) {  }

        public AnnouncementsHistoryQuery(IUserSession userSession, bool? isTest) : base(userSession, isTest.HasValue && isTest.Value)
        {
            IsTest = isTest ?? AppSettings.LocalTest;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; } = DateTime.Now;

        override public BaseAnnouncementsParams ConvertToParameters()
        {
            return new AnnouncementsHistoryParams
            {
                AdminId = AdminId,
                CustId = CustId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                ParentSites = ParentSites,
                SubAccId = SubAccId,
                UserName = UserName,
                FromDate = FromDate,
                ToDate = ToDate.AddDays(1).AddSeconds(-1),
                SpecificationParamter = Specification,
                IsTest = IsTest
            };
        }
    }
}

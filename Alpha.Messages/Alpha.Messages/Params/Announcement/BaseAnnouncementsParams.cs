namespace Alpha.Messages.Params.Announcement
{
    using Announcements.Models;
    using Configurations;

    public class BaseAnnouncementsParams
    {
        public BaseAnnouncementsParams() : this(null) { }

        public BaseAnnouncementsParams(bool? isTest)
        {
            IsTest = isTest ?? AppSettings.LocalTest;
        }

        public int SubAccId { get; set; }

        public byte RoleId { get; set; }

        public int AdminId { private get; set; }

        public bool IsAdminUser => AdminId != 0;

        public int CustId { get; set; }
        
        public string Language { get; set; }

        public string UserName { get; set; }

        public string Site { get; set; }        

        public bool IsMobileDevice { get; set; }

        public string ParentSites { get; set; }

        public IParam SpecificationParamter { get; set; }

        public bool IsTest { get; set; }
    }
}

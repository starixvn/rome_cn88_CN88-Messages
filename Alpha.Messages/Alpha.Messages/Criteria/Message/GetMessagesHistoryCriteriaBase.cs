namespace Alpha.Messages.Criteria.Message
{
    using Constants;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System;

    public abstract class GetMessagesHistoryCriteriaBase : CriteriaBase
    {
        protected GetMessagesHistoryCriteriaBase(
            int custId,
            string parentSites,
            int specialType,
            DateTime startDate,
            DateTime endDate,
            bool isTest,
            int messageType)
        {
            CustId = custId;
            ParentSites = parentSites;
            Special = specialType;
            TotalMessasge = MessageConstants.DefaultTotalMessageHistory;
            StartDate = startDate;
            EndDate = endDate;
            IsTest = isTest;
        }

        [SpParam(SqlParamName = "targetId")]
        public int CustId { get; }

        [SpParam(SqlParamName = "targetId2")]
        public string ParentSites { get; }

        public int Special { get; }

        [SpParam(SqlParamName = "record")]
        public int TotalMessasge { get; }

        [SpParam(SqlParamName = "sdate")]
        public DateTime StartDate { get; }

        [SpParam(SqlParamName = "edate")]
        public DateTime EndDate { get; }

        public bool IsTest { get; }
    }
}
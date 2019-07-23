namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetPortalMessagesCriteria : CriteriaBase
    {
        public GetPortalMessagesCriteria(
            int custId,
            int subAccId,
            string parentSites,
            int totalMessage,
            bool isTest)
        {
            CustId = custId;
            SubAccId = subAccId;
            ParentSites = parentSites;
            TotalMessage = totalMessage;
            IsTest = isTest;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        [SpParam(SqlParamName = "targetids")]
        public string ParentSites { get; }

        public int TotalMessage { get; }

        public bool IsTest { get; }

        public override string GetSettingKey() => "CS_CustomerSelMessages";

        public override bool IsValid() => CustId > 0 && !string.IsNullOrEmpty(ParentSites);
    }
}
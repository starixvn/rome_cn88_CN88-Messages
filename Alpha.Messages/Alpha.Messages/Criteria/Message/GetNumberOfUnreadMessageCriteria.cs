namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetNumberOfUnreadMessageCriteria : CriteriaBase
    {
        public GetNumberOfUnreadMessageCriteria(int custId, int subAccId, string parentSites, bool isTest)
        {
            CustId = custId;
            SubAccId = subAccId;
            ParentSites = parentSites;
            IsTest = isTest;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsTest { get; }

        [SpParam(SqlParamName = "targetId")]
        public string ParentSites { get; }

        public override string GetSettingKey() => "CS_MsgCustReadStatus_SelUnread";

        public override bool IsValid() => CustId > 0 && SubAccId >= 0;
    }
}

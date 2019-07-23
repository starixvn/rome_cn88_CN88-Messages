namespace Alpha.Messages.Criteria.Message
{
    using System;

    public class GetPublicMessagesHistoryCriteria : GetMessagesHistoryCriteriaBase
    {
        public GetPublicMessagesHistoryCriteria(int custId,
            string parentSites,
            int specialType,
            DateTime startDate,
            DateTime endDate,
            bool isTest,
            int messageType)
            : base(custId, parentSites, specialType, startDate, endDate, isTest, messageType)
        {
        }

        public override string GetSettingKey() => "Dot_Msg_GetPublicMessage";

        public override bool IsValid() => CustId > 0 && !string.IsNullOrWhiteSpace(ParentSites);
    }
}
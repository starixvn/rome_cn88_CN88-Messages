namespace Alpha.Messages.Criteria.Message
{
    using System;

    public class GetSpecialMessagesHistoryCriteria : GetMessagesHistoryCriteriaBase
    {
        public GetSpecialMessagesHistoryCriteria(int custId,
            string parentSites,
            int specialType,
            DateTime startDate,
            DateTime endDate,
            bool isTest,
            int messageType)
            : base(custId, parentSites, specialType, startDate, endDate, isTest, messageType)
        {
        }

        public override string GetSettingKey() => "Dot_Msg_GetSpecialMessage";

        public override bool IsValid() => CustId > 0 && !string.IsNullOrWhiteSpace(ParentSites);
    }
}
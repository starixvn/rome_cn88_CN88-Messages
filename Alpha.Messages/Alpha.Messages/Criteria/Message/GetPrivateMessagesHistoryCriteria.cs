namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data.Repository;
    using System;

    public class GetPrivateMessagesHistoryCriteria : CriteriaBase
    {
        public GetPrivateMessagesHistoryCriteria(int custId,
            int subAccId,
            int special,
            int totalMessage,
            DateTime fromDate,
            DateTime toDate,
            bool isTest)
        {
            CustId = custId;
            SubAccId = subAccId;
            Special = special;
            TotalMessage = totalMessage;
            FromDate = fromDate;
            ToDate = toDate;
            IsTest = isTest;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public int Special { get; }

        public int TotalMessage { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public bool IsTest { get; }

        public override string GetSettingKey() => "CS_Msg_GetPrivateMessage";

        public override bool IsValid() => CustId > 0 && SubAccId >= 0;
    }
}
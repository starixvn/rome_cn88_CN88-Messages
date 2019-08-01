namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data.Repository;

    public class GetMoveBetInformationCriteria : CriteriaBase
    {
        public int TimeZone { get; } = 8;

        public override string GetSettingKey() => "Age_MoveBetsProcessNotification";

        public override bool IsValid() => true;
    }
}

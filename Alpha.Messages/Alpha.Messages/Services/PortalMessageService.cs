namespace Alpha.Messages.Services
{
    using System.Collections.Generic;
    using Criteria.Message;
    using Fanex.Data.Repository;
    using Models.Message;

    public interface PortalMessageService
    {
        IEnumerable<PortalMessage> GetPortalMessages(
            int custId,
            string parentSites,
            int totalMessage,
            bool isTest);
        bool MarkMessageAsRead(int custId, int messageId, bool isInternal);
    }

    public class PortalMessageServiceImpl : PortalMessageService
    {
        private readonly IDynamicRepository dynamicRepository;

        public PortalMessageServiceImpl(
            IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public IEnumerable<PortalMessage> GetPortalMessages(
            int custId,
            string parentSites,
            int totalMessage,
            bool isTest)
        {
            var criteria = new GetPortalMessagesCriteria(custId, parentSites, totalMessage, isTest);

            //var message = dynamicRepository.Fetch<PortalMessage>(criteria);

            var message = Stub();

            return message;
        }

        public bool MarkMessageAsRead(int custId, int messageId, bool isInternal)
        {
            var command = new MarkMessageAsReadCommand(custId, messageId.ToString());

            if (!command.IsValid() || isInternal)
            {
                return false;
            }

            dynamicRepository.Execute(command);

            return true;
        }

        private static IEnumerable<PortalMessage> Stub()
        {
            var result = new List<PortalMessage>
            {
                new Models.Message.PortalMessage(1, "M1", "M1", "M1", "M1", "M1", "M1", "M1", new System.DateTime(2017, 4, 22, 19, 24, 50), 1, false, "1", false, 20),
                new Models.Message.PortalMessage(2, "M2", "M2", "M2", "M2", "M2", "M2", "M2", new System.DateTime(2017, 4, 23, 19, 24, 50), 1, false, "1", false, 20),
                new Models.Message.PortalMessage(3, "M3", "M3", "M3", "M3", "M3", "M3", "M3", new System.DateTime(2017, 4, 22, 19, 24, 50), 1, false, "1", false, 20),
                new Models.Message.PortalMessage(4, "M4", "M4", "M4", "M4", "M4", "M4", "M4", new System.DateTime(2017, 4, 26, 19, 24, 50), 1, false, "1", false, 21),
                new Models.Message.PortalMessage(5, "M5", "M5", "M5", "M5", "M5", "M5", "M5", new System.DateTime(2017, 4, 25, 22, 24, 53), 1, false, "1", false, 21),
                new Models.Message.PortalMessage(6, "M6", "M6", "M6", "M6", "M6", "M6", "M6", new System.DateTime(2017, 4, 24, 16, 41, 51), 1, false, "1", false, 21),
                new Models.Message.PortalMessage(7, "M7", "M7", "M7", "M7", "M7", "M7", "M7", new System.DateTime(2017, 4, 24, 22, 55, 30), 1, false, "1", false, 21),
                new Models.Message.PortalMessage(8, "M8", "M8", "M8", "M8", "M8", "M8", "M8", new System.DateTime(2017, 4, 24, 22, 02, 53), 1, false, "1", false, 21)
            };

            return result;
        }
    }
}
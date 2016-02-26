using Microsoft.Practices.Prism.PubSubEvents;

namespace LMS.UI.Utility
{
    public class EventAggregator
    {
        public static IEventAggregator GetInstance()
        {
            if (eventAggregator == null)
            {
                eventAggregator = new Microsoft.Practices.Prism.PubSubEvents.EventAggregator();
            }
            return eventAggregator;
        }

        private static IEventAggregator eventAggregator;
    }
}
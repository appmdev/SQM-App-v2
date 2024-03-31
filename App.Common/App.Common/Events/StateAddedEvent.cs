
using CQRS.Core.Events;

namespace App.Common.Events
{
    public class StateAddedEvent : BaseEvent
    {
        public StateAddedEvent() : base(nameof(StateAddedEvent))
        {
        }

        public Guid StateId { get; set; }
        public string RobotName { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }
        public string AdditionalData { get; set; }
        public DateTime StateDate { get; set; }
    }
}

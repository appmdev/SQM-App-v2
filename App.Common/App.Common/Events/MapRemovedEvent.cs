using CQRS.Core.Events;

namespace App.Common.Events
{
    public class MapRemovedEvent : BaseEvent
    {
        public MapRemovedEvent() : base(nameof(MapRemovedEvent))
        {
        }
    }
}

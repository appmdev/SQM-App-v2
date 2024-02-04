using CQRS.Core.Events;

namespace App.Common.Events
{
    public class MapCreatedEvent : BaseEvent
    {
        public MapCreatedEvent() : base(nameof(MapCreatedEvent))
        {
        }

        public string RobotName { get; set; }
        public string MapName { get; set; }
        public string Pointcloud {get; set; }
        public DateTime DatePosted { get; set; }
    }
}
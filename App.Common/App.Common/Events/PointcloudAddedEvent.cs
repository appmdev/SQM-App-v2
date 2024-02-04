using CQRS.Core.Events;
namespace App.Common.Events
{
    public class PointcloudAddedEvent : BaseEvent
    {
        public PointcloudAddedEvent() : base(nameof(PointcloudAddedEvent))
        {
        }

        public Guid PointcloudId { get; set; }
        public string Pointcloud { get; set; }
        public string RobotName { get; set; }
        public DateTime PointcloudDate { get; set; }
    }
}

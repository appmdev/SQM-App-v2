using App.Common.Events;
using CQRS.Core.Domain;

namespace App.Cmd.Domain.Aggregates
{
    public class MapAggregate : AggregateRoot
    {
        private bool _active;
        private string _author;
        private readonly Dictionary<Guid, Tuple<string, string>> _pointclouds = new();

        private record StateDetails(
                string Category,
                string Action,
                string RobotName,
                string AdditionalData,
                DateTime StateDate);

        private readonly Dictionary<Guid, StateDetails> _state = new();

        public bool Active
        {
            get => _active; set => _active = value;
        }
        public MapAggregate()
        {

        }
        public MapAggregate(Guid id, string author, string map)
        {
            RaiseEvent(new MapCreatedEvent
            {
                Id = id,
                RobotName = author,
                MapName = map,
                DatePosted = DateTime.UtcNow

            });
        }
        public void Apply(MapCreatedEvent @event)
        {
            _id = @event.Id;
            _active = true;
            _author = @event.RobotName;
        }
        public void AddPointcloud(string pointcloud, string robotName)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot add a pointcloud to inactive map!");
            }
            if (string.IsNullOrWhiteSpace(pointcloud))
            {
                throw new InvalidOperationException($"The value of {nameof(pointcloud)} cannot be null or empty. Plese provide a valid{nameof(pointcloud)}!");
            }

            RaiseEvent(new PointcloudAddedEvent
            {
                Id = _id,
                PointcloudId = Guid.NewGuid(),
                Pointcloud = pointcloud,
                RobotName = robotName,
                PointcloudDate = DateTime.UtcNow
            });
        }
        public void AddState(string robotName, string category, string action, string aditionalData)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot add a state to inactive Task!");
            }
            if (string.IsNullOrWhiteSpace(action))
            {
                throw new InvalidOperationException($"The value of {nameof(action)} cannot be null or empty. Plese provide a valid{nameof(action)}!");
            }
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new InvalidOperationException($"The value of {nameof(category)} cannot be null or empty. Plese provide a valid{nameof(category)}!");
            }

            RaiseEvent(new StateAddedEvent
            {
                Id = _id,
                StateId = Guid.NewGuid(),
                Category = category,
                Action = action,
                RobotName = robotName,
                AdditionalData = aditionalData,
                StateDate = DateTime.UtcNow
            });
        }
        public void Apply(StateAddedEvent @event)
        {
            _id = @event.Id;
            StateDetails state = new(@event.Category, @event.Action, @event.RobotName, @event.AdditionalData, @event.StateDate);
            _state.Add(@event.StateId, state);
        }


        public void Apply(PointcloudAddedEvent @event)
        {
            _id = @event.Id;
            _pointclouds.Add(@event.PointcloudId, new Tuple<string, string>(@event.Pointcloud, @event.RobotName));
        }
        public void DeleteMap(string robotName)
        {
            if (!_active)
            {
                throw new InvalidOperationException("The map has already been removed!");
            }

            RaiseEvent(new MapRemovedEvent
            {
                Id = _id
            });
        }
        public void Apply(MapRemovedEvent @event)
        {
            _id = @event.Id;
            _active = false;
        }
    }
}
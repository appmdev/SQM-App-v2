using App.Common.Events;
using App.Query.Domain.Entities;
using App.Query.Domain.Repositories;

namespace App.Query.Infrastructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly IMapRepository _mapRepository;
        private readonly IPointcloudRepository _pointcloudRepository;

        public EventHandler(IMapRepository mapRepository, IPointcloudRepository pointcloudRepository)
        {
            _mapRepository = mapRepository;
            _pointcloudRepository = pointcloudRepository;
        }

        public async Task On(MapCreatedEvent @event)
        {
            var map = new MapEntity
            {
                MapId = @event.Id,
                Author = @event.RobotName,
                DateCreated = @event.DatePosted,
                MapName = @event.MapName
            };
            await _mapRepository.CreateAsync(map);
        }

        public async Task On(MapRemovedEvent @event)
        {
            await _mapRepository.DeleteAsync(@event.Id);
        }

        public async Task On(PointcloudAddedEvent @event)
        {
            var pointcloud = new PointcloudEntity
            {
                MapId = @event.Id,
                PointcloudId = @event.PointcloudId,
                PointcloudDate = @event.PointcloudDate,
                Pointcloud = @event.Pointcloud,
                RobotName = @event.RobotName
            };

            await _pointcloudRepository.CreateAsync(pointcloud);
        }
    }

}
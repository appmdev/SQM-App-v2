using App.Common.Events;

namespace App.Query.Infrastructure.Handlers
{
    public interface IEventHandler
    {
        Task On(MapCreatedEvent @event);
        Task On(MapRemovedEvent @event);
        Task On(PointcloudAddedEvent @event);
    }
}
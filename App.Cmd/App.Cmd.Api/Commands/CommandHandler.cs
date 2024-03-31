using App.Cmd.Domain.Aggregates;
using CQRS.Core.Handlers;

namespace App.Cmd.Api.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHandler<MapAggregate> _eventSourcingHandler;

        public CommandHandler(IEventSourcingHandler<MapAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task HandleAsync(NewMapCommand command)
        {
            var aggregate = new MapAggregate(command.Id, command.Author, command.Map);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(AddPointcloudCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.AddPointcloud(command.Pointcloud, command.RobotName);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(DeleteMapCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.DeleteMap(command.RobotName);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(RestoreReadDbCommand command)
        {
            await _eventSourcingHandler.RepublishEventsAsync();
        }

        public async Task HandleAsync(AddStateCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.AddState(command.RobotName, command.Category, command.Action, command.AditionalData);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}
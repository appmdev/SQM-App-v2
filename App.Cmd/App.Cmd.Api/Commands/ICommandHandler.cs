namespace App.Cmd.Api.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewMapCommand command);
        Task HandleAsync(AddPointcloudCommand command);
        Task HandleAsync(DeleteMapCommand command);
        Task HandleAsync(RestoreReadDbCommand command);
    }
}

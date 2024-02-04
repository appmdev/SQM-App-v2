using CQRS.Core.Commands;

namespace App.Cmd.Api.Commands
{
    public class DeleteMapCommand : BaseCommand
    {
        public string RobotName { get; set; }
    }
}

using CQRS.Core.Commands;

namespace App.Cmd.Api.Commands
{
    public class AddStateCommand : BaseCommand
    {
        public string RobotName { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }
        public string AditionalData { get; set; }
    }
}

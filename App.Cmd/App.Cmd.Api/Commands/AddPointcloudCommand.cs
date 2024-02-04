using CQRS.Core.Commands;

namespace App.Cmd.Api.Commands
{
    public class AddPointcloudCommand : BaseCommand
    {
        public string Pointcloud { get; set; }
        public string RobotName { get; set; }
    }
}
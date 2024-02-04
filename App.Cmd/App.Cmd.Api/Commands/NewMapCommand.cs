using CQRS.Core.Commands;

namespace App.Cmd.Api.Commands
{
    public class NewMapCommand : BaseCommand
    {
        public string Author { get; set; }
        public string Map { get; set; }
    }
}

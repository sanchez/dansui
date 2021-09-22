using System.Threading.Tasks;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface ICommanderService
    {
        Task Init();

        IEnumerable<Command> SearchCommands(string filter);

        IDisposable RegisterCommand(Command command);
        IDisposable RegisterCommands(IEnumerable<Command> commands);
    }
}

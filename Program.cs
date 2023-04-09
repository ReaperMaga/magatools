using MagaTools.Command;
using MagaTools.Command.Implemented;

namespace MagaTools;

public class Program {

    public static void Main(string[] args) {
        CommandManager manager = new CommandManager();
        manager.AddCommand(new HelpCommand(manager));
        manager.AddCommand(new PasswordCommand());
        manager.Start(args);
    }

}

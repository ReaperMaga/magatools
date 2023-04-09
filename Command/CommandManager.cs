using System.Text;

namespace MagaTools.Command;

public class CommandManager {

    private List<Command> commands;

    public CommandManager() {
        this.commands = new List<Command>();
    }

    public void SendHelp() {
        StringBuilder builder = new StringBuilder();

        foreach (Command command in commands) {
            BuildCommandHelpLine(command, 2, builder);
        }
        Console.WriteLine("> MagaTools - Help");
        Console.WriteLine(builder.ToString());
    }

    private void BuildCommandHelpLine(Command command, int spaces, StringBuilder builder) {
        StringBuilder spacesBuilder = new StringBuilder();
        for (int i = 0; i < spaces; i++) {
            spacesBuilder.Append(' ');
        }
        builder.Append(spacesBuilder.ToString()).Append("- ").Append(command.name);

        if (command.arguments != null) {
            StringBuilder argumentsBuilder = new StringBuilder();

            foreach (string argument in command.arguments) {
                argumentsBuilder.Append('<').Append(argument).Append('>').Append(' ');
            }
            builder.Append(' ').Append(argumentsBuilder.ToString().TrimEnd());
        }
        
        if (command.description != null) {
            builder.Append(" | ").Append(command.description);
        }

        builder.Append('\n');
        foreach (Command child in command.children) {
            BuildCommandHelpLine(child, spaces+2, builder);
        }
    }

    public void AddCommand(Command command) {
        this.commands.Add(command);
    }

    public void Start(string[] args) {
        foreach (Command command in commands) {
            if (Check(command, 0, args)) {
                return;
            }
        }

        SendHelp();
    }

    private bool Check(Command command, int index, string[] args) {
        if (index <= args.Length - 1) {
            foreach (Command child in command.children) {
                if (Check(child, index + 1, args)) {
                    return true;
                }
            }

            string name = args[index];
            if (command.name.ToLower().Equals(name.ToLower()) || CheckAliases(command, name)) {
                ExecuteCommand(command, index, args);
                return true;
            }
        }

        return false;
    }

    private bool CheckAliases(Command command, string name) {
        foreach (string alias in command.aliases) {
            if (alias.ToLower().Equals(name.ToLower())) {
                return true;
            }
        }

        return false;
    }

    private void ExecuteCommand(Command command, int indexFrom, string[] args) {
        List<string> newArgs = new List<string>();
        for (int i = indexFrom+1; i < args.Length; i++) {
            newArgs.Add(args[i]);
        }

        if (!command.OnExecute(new CommandArgs(newArgs.ToArray()))) {
            SendHelp();
        }
    }

}
using System.Diagnostics.CodeAnalysis;

namespace MagaTools.Command; 

public abstract class Command {

    public string name;
    [MaybeNull]
    public string description;
    [MaybeNull]
    public List<string> arguments;
    
    public List<string> aliases;
    public List<Command> children;

    protected Command(string name) {
        this.name = name;
        this.aliases = new List<string>();
        this.children = new List<Command>();
    }

    protected Command AddArgument(string argument) {
        if (this.arguments == null) {
            this.arguments = new List<string>();
        }
        this.arguments.Add(argument);
        return this;
    }

    protected Command SetDescription(string description) {
        this.description = description;
        return this;
    }

    protected Command AddAlias(string alias) {
        this.aliases.Add(alias);
        return this;
    }

    protected Command AddChild(Command command) {
        this.children.Add(command);
        return this;
    }
    

    public abstract bool OnExecute(CommandArgs args);
    
}
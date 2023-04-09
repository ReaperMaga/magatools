namespace MagaTools.Command.Implemented; 

public class HelpCommand : Command {

    private readonly CommandManager _manager;
    
    public HelpCommand(CommandManager manager) : base("help") {
        this._manager = manager;
        SetDescription("List all available commands");
    }

    public override bool OnExecute(CommandArgs args) {
        _manager.SendHelp();
        return true;
    }

}
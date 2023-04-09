using System.Text;

namespace MagaTools.Command.Implemented; 

public class PasswordCommand : Command {

    private const string ALPHABETIC_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUFWXYZ";
    private const string NUMERIC_CHARACTERS = "1234567890";
    private const string SYMBOL_CHARACTERS = "{};'.,<>`~%&*()$#@!?[]-_=+:";

    private Random _random;
    
    public PasswordCommand() : base("password") {
        SetDescription("Generate a password");
        AddArgument("length");
        AddArgument("numbers?");
        AddArgument("symbols?");

        this._random = new Random();
    }

    public override bool OnExecute(CommandArgs args) {
        if (args.GetLength() >= 1) {
            int length = args.GetInt(0);
            bool numbers = args.GetLength() >= 2 && args.GetBool(1);
            bool symbols = args.GetLength() >= 3 && args.GetBool(2);

            StringBuilder charactersBuilder = new StringBuilder(ALPHABETIC_CHARACTERS).Append(ALPHABETIC_CHARACTERS.ToLower());
            if (numbers) {
                charactersBuilder.Append(NUMERIC_CHARACTERS);
            }
            if (symbols) {
                charactersBuilder.Append(SYMBOL_CHARACTERS);
            }

            string characters = charactersBuilder.ToString();
            
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < length; i++) {
                result.Append(characters[_random.Next(characters.Length)]);
            }
            Console.WriteLine(result.ToString());
            
            return true;
        }

        return false;
    }

}
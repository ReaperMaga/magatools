namespace MagaTools.Command; 

public class CommandArgs {

    public string[] raw;

    public CommandArgs(string[] raw) {
        this.raw = raw;
    }

    public bool GetBool(int index) {
        string value = GetString(index);
        if (value.ToLower().Equals("true")) {
            return true;
        }
        return false;
    }

    public int GetInt(int index) {
        string rawValue = GetString(index);
        int value;
        bool success = int.TryParse(rawValue, out value);
        return success ? value : -1;
    }

    public string GetString(int index) {
        return raw[index];
    }

    public int GetLength() {
        return raw.Length;
    }

}
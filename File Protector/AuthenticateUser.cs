using System.IO;

public static class AuthenticateUser
{
    public static string? _currentLoggedInUser;
    public static bool UserExists(string userName)
    {
        var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var _rootFolder = Path.Combine(_appData, "User Data");
        var path = Path.Combine(_appData, _rootFolder, "User Data", "UserInfo.txt");
        if (!File.Exists(path))
            throw new IOException("File does not exist.");

        string[] lines = File.ReadAllLines(path);
        for (int i = 0; i < lines.Length; i++)
            if (lines[i] == userName)
            {
                _currentLoggedInUser = userName;
                return true;
            }
        return false;
    }
    public static void GetUserInfo(string userName)
    {
        try
        {
            var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var _rootFolder = Path.Combine(_appData, "User Data");
            var path = Path.Combine(_appData, _rootFolder, "User Data", "UserInfo.txt");
            if (!File.Exists(path))
                throw new IOException("File does not exist.");

            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == userName)
                {
                    _currentLoggedInUser = userName;
                    Crypto.Salt = lines[i + 2];
                    Crypto.Hash = lines[i + 4];
                    break;
                }
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorLogging.ErrorLog(ex);
        }
    }
}

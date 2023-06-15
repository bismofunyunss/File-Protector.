using System.IO;

public static class AuthenticateUser
{
    private static string _currentLoggedInUser = string.Empty;
    public static string CurrentLoggedInUser
    {
        get { return _currentLoggedInUser; }
        set { _currentLoggedInUser = value; }
    }

    public static bool UserExists(string userName)
    {
        string path = GetUserInfoFilePath();

        try
        {
            if (!File.Exists(path))
                throw new IOException("File does not exist.");

            string[] lines = File.ReadAllLines(path);
            if (Array.IndexOf(lines, userName) != -1)
            {
                _currentLoggedInUser = userName;
                return true;
            }
        }
        catch (IOException ex)
        {
            ErrorLogging.ErrorLog(ex);
            return false;
        }
        return false;
    }

    public static void GetUserInfo(string userName)
    {
        try
        {
            string path = GetUserInfoFilePath();

            if (!File.Exists(path))
                throw new IOException("File does not exist.");

            string[] lines = File.ReadAllLines(path);
            int index = Array.IndexOf(lines, userName);
            if (index != -1)
            {
                Crypto.Salt = lines[index + 2];
                Crypto.Hash = lines[index + 4];
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorLogging.ErrorLog(ex);
        }
    }

    private static string GetUserInfoFilePath()
    {
        var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var _rootFolder = Path.Combine(_appData, "User Data");
        var path = Path.Combine(_appData, _rootFolder, "User Data", "UserInfo.txt");
        return path;
    }
}

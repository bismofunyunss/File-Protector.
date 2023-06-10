using Microsoft.VisualBasic;
using System.IO;
using System.Net.Http;

#pragma warning disable
public static class UserLog
{
    private static readonly HttpClient _httpClient = new HttpClient();

    struct InitializeVars
    {
        public static Uri? _externalIP = new("https://api.ipify.org");
        public static string? externalIP = _httpClient.GetStringAsync(_externalIP).Result;
        public static string? _userName = AuthenticateUser.CurrentLoggedInUser;
        public static string? _dateAndTime = DateAndTime.Now.ToString();
    }


    public static void LogUser(string userName)
    {
        if (!File.Exists("UserLog.txt"))
            File.Create("UserLog.txt").Dispose();

        try
        {
            using StreamWriter sw = File.AppendText("UserLog.txt");
            sw.AutoFlush = true;
            sw.WriteLine("Username: " + userName + " " + "logged in using IP: " + InitializeVars.externalIP + " " + InitializeVars._dateAndTime + "\n");
            sw.Flush();
        }
        catch (ArgumentException e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorLogging.ErrorLog(e);
        }
    }
}
#pragma warning restore

using ABI.System;
using Microsoft.VisualBasic;
using System.IO;
using System.Net.Http;

public static class UserLog
{
    private static readonly HttpClient _httpClient = new HttpClient();
    struct VariableInitializer
    {
        public static readonly System.Uri? _externalIP = new("https://api.ipify.org");
        public static readonly string? externalIP = _httpClient.GetStringAsync(_externalIP).Result;
        public static readonly string? _userName = AuthenticateUser._currentLoggedInUser;
        public static readonly string? _dateAndTime = DateAndTime.Now.ToString();
    }

   public static void LogUser()
    {
        if (!File.Exists("UserLog.txt"))
            File.Create("UserLog.txt").Dispose();

        try
        {
            using StreamWriter sw = File.AppendText("UserLog.txt");
            sw.AutoFlush = true;
            sw.WriteLine(VariableInitializer._userName + " " + "Logged in using IP: " + VariableInitializer.externalIP + " " + VariableInitializer._dateAndTime + "\n");
            sw.Flush();
        }
        catch (ArgumentException e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorLogging.ErrorLog(e);
        }
    }
}

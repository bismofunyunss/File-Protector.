using Microsoft.VisualBasic;
using System.IO;
using System.Net.Http;

#pragma warning disable
public static class UserLog
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static readonly Uri _externalIP = new Uri("https://api.ipify.org");

    public static void LogUser(string userName)
    {
        try
        {
            if (!File.Exists("UserLog.txt"))
                File.Create("UserLog.txt").Dispose();

            string externalIP = _httpClient.GetStringAsync(_externalIP).Result;
            string dateAndTime = DateTime.Now.ToString();

            using StreamWriter sw = File.AppendText("UserLog.txt");
            sw.AutoFlush = true;
            sw.WriteLine($"Username: {userName} logged in using IP: {externalIP} {dateAndTime}\n");
            sw.Flush();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorLogging.ErrorLog(e);
        }
    }
}
#pragma warning restore

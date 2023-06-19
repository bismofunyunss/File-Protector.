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
            File.AppendAllText("UserLog.txt", $"Username: {userName} logged in using IP: {_httpClient.GetStringAsync(_externalIP).Result} {DateTime.Now}\n");
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorLogging.ErrorLog(e);
        }
    }
}
#pragma warning restore

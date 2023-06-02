using System.IO;

public static class ErrorLogging
{
    public static void ErrorLog(System.Exception ex)
    {
        if (!File.Exists("ErrorLog.txt"))
            File.Create("ErrorLog.txt").Dispose();

        if (ex != null)
        {
            try
            {
                using StreamWriter Writer = File.AppendText("ErrorLog.txt");
                Writer.AutoFlush = true;
                Writer.Write(DateTime.Now + " ");
                Writer.Write(ex.Message);
                Writer.Write(ex.InnerException);
                Writer.Write(ex.StackTrace);
                Writer.WriteLine();
                Writer.WriteLine();
            }
            catch (ArgumentException e)
            {
                if (ex.InnerException == null || ex.StackTrace == null || ex.Message == null)
                    ErrorLog(e);
            }
        }
    }
}

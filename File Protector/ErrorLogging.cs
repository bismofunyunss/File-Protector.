﻿using System.IO;

public static class ErrorLogging
{
    public static void ErrorLog(Exception ex)
    {
        if (!File.Exists("ErrorLog.txt"))
            File.Create("ErrorLog.txt").Dispose();

        if (ex != null)
        {
            try
            {
                using StreamWriter writer = File.AppendText("ErrorLog.txt");
                writer.AutoFlush = true;
                writer.WriteLine($"{DateTime.Now} {ex.Message} {ex.InnerException} {ex.StackTrace}");
                writer.WriteLine();
            }
            catch (ArgumentException)
            {
                if (ex.InnerException == null || ex.StackTrace == null || ex.Message == null)
                    ErrorLog(new ArgumentException());
            }
        }
    }
}

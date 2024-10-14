namespace SyncTwoFolders
{
    internal static class FileLogger
    {
        internal static void LogChangesToFile(string action, string type, string name, string source, string destination, string logFilePath)
        {
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                if (action == "copy")
                {
                    sw.WriteLine($"{type} '{name}' copied from '{source}' to '{destination}' at '{DateTime.Now}'");
                } else if (action == "delete")
                {
                    sw.WriteLine($"{type} '{name}' deleted from '{source}' to '{destination}' at '{DateTime.Now}'");
                }
                else
                {
                    Console.WriteLine("Wrong action!");
                }
            }
        }
    }
}

namespace SyncTwoFolders
{
    internal static class ConsoleLogger
    {
        internal static void PrintMessage(string message) 
        {
            Console.WriteLine(message);
        }

        internal static void PrintCleanMessage(string message)
        {
            Console.Clear();
            Console.Write(message);
        }
    }
}

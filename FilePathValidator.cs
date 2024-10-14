namespace SyncTwoFolders
{
    internal static class FilePathValidator
    {
        internal static string CheckIfEmptyPath(string name)
        {
            string filePath = string.Empty;

            while (string.IsNullOrEmpty(filePath))
            {
                ConsoleLogger.PrintCleanMessage($"Please enter a valid file path for {name}: ");
                filePath = Console.ReadLine();

                if (string.IsNullOrEmpty(filePath))
                {
                    ConsoleLogger.PrintCleanMessage($"File path is empty! Please input a valid {name} path again!");
                    Console.ReadKey();
                }
            }
            return filePath;
        }
    }
}

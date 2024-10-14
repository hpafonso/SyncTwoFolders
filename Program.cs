namespace SyncTwoFolders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger.PrintMessage("Welcome! Please introduce source folder path, replica folder path and log file path");
            ConsoleLogger.PrintMessage("Please, press enter to continue...");
            Console.ReadKey();

            var source = FilePathValidator.CheckIfEmptyPath("source folder");
            var replica = FilePathValidator.CheckIfEmptyPath("replica folder");
            var logFilePath = FilePathValidator.CheckIfEmptyPath("log file");

            Console.ReadKey();

            var syncCommand = new SyncCommand(source, replica, logFilePath);
            var fileManager = new FileManager(syncCommand);

            fileManager.ExecuteCommand();

            Console.ReadKey();
        }
    }
}

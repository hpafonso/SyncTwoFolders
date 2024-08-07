namespace SyncTwoFolders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var source = @"C:\projects\SyncTwoFolders\folders\source";
            var replica = @"C:\projects\SyncTwoFolders\folders\replica";

            var syncCommand = new SyncCommand(source, replica);
            var fileManager = new FileManager(syncCommand);

            fileManager.ExecuteCommand();

            Console.ReadKey();
        }
    }
}

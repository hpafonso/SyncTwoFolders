namespace SyncTwoFolders
{
    internal class FileManager
    {
        ICommand _command;

        public FileManager(ICommand command)
        {
            _command = command;
        }

        public void ExecuteCommand()
        {
            _command.Execute();
        }
    }
}

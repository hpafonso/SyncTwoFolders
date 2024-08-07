namespace SyncTwoFolders
{
    internal class SyncCommand : ICommand
    {
        private readonly string _source;
        private readonly string _destination;

        public SyncCommand(string source, string destination)
        {
            _source = source;
            _destination = destination;
        }

        public void Execute()
        {
            SyncDirectories(_source, _destination);
        }

        private void SyncDirectories(string sourcePath, string destinationPath)
        {
            // source lists
            var sourceFileList = Directory.GetFiles(sourcePath);
            var sourceDirectoryList = Directory.GetDirectories(sourcePath);

            // destination lists
            var destinationFileList = Directory.GetFiles(destinationPath);
            var destinationDirectoryList = Directory.GetDirectories(destinationPath);

            // copies all files from source to replica
            foreach (var file in sourceFileList)
            {
                File.Copy(file, _destination, true);
            }

            // copies all directories from source to replica
            foreach (var directory in sourceDirectoryList)
            {
                File.Copy(directory, _destination, true);
            }

            // deletes all files from replica that do not exist on source anymore
            foreach(var file in destinationFileList)
            {
                if (!File.Exists(sourcePath + Path.GetFileName(file)))
                    File.Delete(file);
            }

            // deletes all directories from replica that do not exist on source anymore
            foreach (var file in destinationDirectoryList)
            {
                if (!Directory.Exists(sourcePath + Path.GetFileName(file)))
                    File.Delete(file);
            }
        }
    }
}

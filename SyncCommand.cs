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

            try
            {
                // copies all files from source to replica
                foreach (var file in sourceFileList)
                {
                    File.Copy(file, file.Replace(sourcePath, destinationPath), true);
                }

                // copies all directories from source to replica
                foreach (var directory in sourceDirectoryList)
                {
                    if (!Directory.Exists(directory.Replace(sourcePath, destinationPath)))
                    {
                        CopyDirectory(sourcePath, destinationPath);
                    }
                }

                // deletes all files from replica that do not exist on source anymore
                foreach (var file in destinationFileList)
                {
                    if (!File.Exists(sourcePath + Path.GetFileName(file)))
                        File.Delete(file);
                }

                // deletes all directories from replica that do not exist on source anymore
                foreach (var directory in destinationDirectoryList)
                {
                    if (!Directory.Exists(sourcePath + Path.GetDirectoryName(directory)))
                        Directory.Delete(directory);
                }

                Console.WriteLine("Sync Completed!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void CopyDirectory(string sourceDirectory, string destinationDirectory)
        {
            Directory.CreateDirectory(destinationDirectory);

            // Copy all the files
            foreach (string file in Directory.GetFiles(sourceDirectory))
            {
                string destFile = Path.Combine(destinationDirectory, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            // Copy all the subdirectories
            foreach (string subdir in Directory.GetDirectories(sourceDirectory))
            {
                string destSubdir = Path.Combine(destinationDirectory, Path.GetFileName(subdir));
                CopyDirectory(subdir, destSubdir);
            }
        }
    }
}

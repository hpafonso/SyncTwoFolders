namespace SyncTwoFolders
{
    internal class SyncCommand : ICommand
    {
        private readonly string _source;
        private readonly string _destination;
        private readonly string _logFilePath;

        public SyncCommand(string source, string destination, string logFilePath)
        {
            _source = source;
            _destination = destination;
            _logFilePath = logFilePath;
        }

        public void Execute()
        {
            SyncDirectories(_source, _destination, _logFilePath);
        }

        private void SyncDirectories(string sourcePath, string destinationPath, string logFilePath)
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
                    FileLogger.LogChangesToFile("copy", "File", Path.GetFileName(file), sourcePath, destinationPath, logFilePath);
                }

                // copies all directories from source to replica
                foreach (var directory in sourceDirectoryList)
                {
                    CopyDirectory(sourcePath, destinationPath);
                    FileLogger.LogChangesToFile("copy", "Directory", Path.GetDirectoryName(directory), sourcePath, destinationPath, logFilePath);
                }

                // deletes all files from replica that do not exist on source anymore
                foreach (var file in destinationFileList)
                {
                    if (!File.Exists(sourcePath + @"\" + Path.GetFileName(file)))
                    {
                        File.Delete(file);
                        FileLogger.LogChangesToFile("delete", "File", Path.GetFileName(file), sourcePath, destinationPath, logFilePath);
                    }
                }

                // deletes all directories from replica that do not exist on source anymore
                foreach (var directory in destinationDirectoryList)
                {
                    if (!Directory.Exists(sourcePath + @"\" + Path.GetFileName(directory)))
                    {
                        Directory.Delete(directory, true);
                        FileLogger.LogChangesToFile("delete", "Directory", Path.GetDirectoryName(directory), sourcePath, destinationPath, logFilePath);
                    }
                }
                ConsoleLogger.PrintMessage("Sync Completed!");
            }
            catch (Exception e)
            {
                ConsoleLogger.PrintMessage(e.ToString());
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

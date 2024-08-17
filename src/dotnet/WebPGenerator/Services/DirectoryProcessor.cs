using WebPGenerator.Utilities;

namespace WebPGenerator.Services
{
    public class DirectoryProcessor : IDirectoryProcessor
    {
        private readonly ImageConverter _imageConverter;
        private readonly ProgressReporter _progressReporter;
        private readonly string _assetsDirectory;

        public DirectoryProcessor(ImageConverter imageConverter, ProgressReporter progressReporter)
        {
            _imageConverter = imageConverter;
            _progressReporter = progressReporter;
            _assetsDirectory = @"todo";
        }

        public async Task ProcessDirectoriesAsync()
        {
            if (!Directory.Exists(_assetsDirectory))
            {
                Console.WriteLine($"ERROR: Path not found - {_assetsDirectory}");
                return;
            }

            var directories = Directory.EnumerateDirectories(_assetsDirectory);

            var processingTasks = directories.Select(directory => ProcessDirectoryAsync(directory));

            var progressTask = _progressReporter.ShowProgressAsync();

            await Task.WhenAll(processingTasks);

            _progressReporter.Stop();
        }

        private async Task ProcessDirectoryAsync(string directory)
        {
            var gifFiles = Directory.EnumerateFiles(directory, "*.gif");
            var pngFiles = Directory.EnumerateFiles(directory, "*.png");

            var allFiles = gifFiles.Concat(pngFiles);

            var conversionTasks = allFiles.Select(file => _imageConverter.ConvertToWebPAsync(file));

            await Task.WhenAll(conversionTasks);
        }
    }
}

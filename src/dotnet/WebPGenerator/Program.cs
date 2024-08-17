using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPGenerator.Converters;
using WebPGenerator.Services;
using WebPGenerator.Utilities;

public class Program
{    
    public static async Task Main(string[] args)
    {        
        var host = CreateHostBuilder(args).Build();

        var directoryProcessor = host.Services.GetRequiredService<IDirectoryProcessor>();
        await directoryProcessor.ProcessDirectoriesAsync();

        Console.WriteLine("All tasks completed.");
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IImageConverterStrategy, PngToWebPConverter>();
                services.AddSingleton<IImageConverterStrategy, GifToWebPConverter>();
                services.AddSingleton<ImageConverter>();
                services.AddSingleton<IDirectoryProcessor, DirectoryProcessor>();
                services.AddSingleton<ProgressReporter>();
            });
}
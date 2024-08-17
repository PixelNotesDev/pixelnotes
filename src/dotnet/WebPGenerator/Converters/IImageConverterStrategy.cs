namespace WebPGenerator.Converters
{
    public interface IImageConverterStrategy
    {
        string FileExtension { get; }
        Task ConvertAsync(string inputFilePath);
    }
}

using WebPGenerator.Converters;

public class ImageConverter
{
    private readonly Dictionary<string, IImageConverterStrategy> _strategies;

    public ImageConverter(IEnumerable<IImageConverterStrategy> strategies)
    {        
        _strategies = strategies.ToDictionary(
            strategy => strategy.FileExtension.ToLower(),
            strategy => strategy);
    }
     
    public async Task ConvertToWebPAsync(string inputFilePath)
    {
        string fileExtension = Path.GetExtension(inputFilePath).ToLower().TrimStart('.');

        if (_strategies.TryGetValue(fileExtension, out var strategy))
        {            
            await strategy.ConvertAsync(inputFilePath);
        }
        else
        {
            throw new NotSupportedException($"File extension is not supported: {fileExtension}");
        }
    }
}

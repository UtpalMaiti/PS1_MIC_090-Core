using Microsoft.AspNetCore.RequestDecompression;

namespace PS1_MIC_090_Core.Middlewares
{
    public class CustomDecompressionProvider : IDecompressionProvider
    {
        public Stream GetDecompressionStream(Stream stream)
        {
            // Perform custom decompression logic here
            return stream;
        }
    }
}

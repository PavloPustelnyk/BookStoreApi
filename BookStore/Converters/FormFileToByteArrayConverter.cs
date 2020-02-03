using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BookStore.WebAPI.Converters
{
    public class FormFileToByteArrayConverter : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
        {
            if (source == null || source.Length == 0)
            {
                return new byte[0];
            }

            using (var ms = new MemoryStream())
            {
                source.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.Converters
{
    public class ByteArrayToFormFileConverter : ITypeConverter<byte[], IFormFile>
    {
        public IFormFile Convert(byte[] source, IFormFile destination, ResolutionContext context)
        {
            if (source == null || source.Length == 0)
            {
                return null;
            }

            using (var ms = new MemoryStream(source))
            {
                return new FormFile(ms, 0, source.Length, "name", "fileName");
            }
        }
    }
}

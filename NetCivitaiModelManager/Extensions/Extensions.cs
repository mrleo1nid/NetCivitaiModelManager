using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extension
{
    public static class Extensions
    {
        public static async Task<string> GetHashAsync(this Stream stream, HashAlgorithm algorithm)
        {
            StringBuilder sb;

            using (var algo = algorithm)
            {
                var buffer = new byte[8192];
                int bytesRead;

                // compute the hash on 8KiB blocks
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    algo.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                algo.TransformFinalBlock(buffer, 0, bytesRead);

                // build the hash string
                sb = new StringBuilder(algo.HashSize / 4);
                foreach (var b in algo.Hash)
                    sb.AppendFormat("{0:x2}", b);
            }
            return sb?.ToString();
        }
        public static string? GetRowToRequest(this List<TypeToSelect> typeToSelects)
        {
            var types = typeToSelects.Select(x => x.Name.Replace(" ", "")).ToList();
            var type = String.Join("&types=", types);
            if (string.IsNullOrEmpty(type)) type = null;
            return type;
        }
    }
}

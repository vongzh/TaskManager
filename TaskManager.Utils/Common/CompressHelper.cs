using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.Common
{
    public class CompressHelper
    {
        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        /// <summary>
        /// 通用解压 支持rar,zip
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        public static void UnCompress(string compressfilepath, string uncompressdir)
        {
            string ext = Path.GetExtension(compressfilepath).ToLower();
            if (ext == ".rar")
                UnRar(compressfilepath, uncompressdir);
            else if (ext == ".zip")
                UnZip(compressfilepath, uncompressdir);
        }

        /// <summary>
        /// 解压rar
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        private static void UnRar(string compressfilepath, string uncompressdir)
        {
            using (Stream stream = File.OpenRead(compressfilepath))
            {
                using (var reader = ReaderFactory.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                            reader.WriteEntryToDirectory(uncompressdir, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                    }
                }
            }
        }

        /// <summary>
        /// 解压zip
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        private static void UnZip(string compressfilepath, string uncompressdir)
        {
            using (var archive = ArchiveFactory.Open(compressfilepath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                        entry.WriteToDirectory(uncompressdir, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                }
            }
        }
    }
}

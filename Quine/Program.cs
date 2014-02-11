using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Quine
{
    class Program
    {
        private const string Repo = @"../../../.git/";

        private static void Main(string[] args)
        {
            var headFilePath = Repo + File.ReadAllText(Repo + "HEAD").Split(' ')[1].Trim();
            var commitHash = File.ReadAllText(headFilePath);
            var commitFile = ReadObject(commitHash);
            Console.WriteLine(commitFile);
            var treeHash = commitFile.Split('\n')[0].Split(' ')[2].Trim();
            var rootTreeFile = ReadObject(treeHash);
            Console.WriteLine(rootTreeFile);
        }

        public static string ReadObject(string hash)
        {
            var objectPath = Repo + "objects/" + hash.Substring(0, 2) + "/" + hash.Substring(2).Trim();
            Console.WriteLine(objectPath);
            return Decompress(File.ReadAllBytes(objectPath));
        }

        public static string Decompress(byte[] input)
        {
            using (var inputStream = new MemoryStream(input))
            using (var zipStream = new InflaterInputStream(inputStream))
            using (var reader = new StreamReader(zipStream, Encoding.UTF8))
                return reader.ReadToEnd();
        }
    }
}
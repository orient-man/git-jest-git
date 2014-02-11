using System;
using System.IO;
using System.Linq;
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
            Log("Last commit", commitHash);
            var commitFile = Encoding.UTF8.GetString(ReadObject(commitHash));
            Log("Commit file", commitFile);
            var treeHash = commitFile.Split('\n')[0].Split(' ')[2].Trim();
            var treeHashFile = ReadObject(treeHash);
            Log("Commit root directory tree", Encoding.UTF8.GetString(treeHashFile));
            var quineDirHash = GetHashFromDirectoryTree(treeHashFile, "Quine");
            Log("Quine directory hash", quineDirHash);
            var quineDirTreeFile = ReadObject(quineDirHash);
            Log("Quine directory tree", Encoding.UTF8.GetString(quineDirTreeFile) + "\n");
            var programHash = GetHashFromDirectoryTree(quineDirTreeFile, "Program.cs");
            Log("Program hash", quineDirHash);
            Log("Program blob", Encoding.UTF8.GetString(ReadObject(programHash)));
        }

        private static byte[] ReadObject(string hash)
        {
            var objectPath = Repo + "objects/" + hash.Substring(0, 2) + "/" + hash.Substring(2).Trim();
            return Decompress(File.ReadAllBytes(objectPath));
        }

        private static string GetHashFromDirectoryTree(byte[] tree, string name)
        {
            var position = IndexOf(tree, Encoding.UTF8.GetBytes(name)) + name.Length + 1;
            return BitConverter.ToString(tree, position, 20).Replace("-", "").ToLower();
        }

        public static int IndexOf(byte[] input, byte[] pattern, int startIndex = 0)
        {
            var i = Array.IndexOf(input, pattern[0], startIndex);
            while (i >= 0 && i <= input.Length - pattern.Length)
            {
                var segment = new byte[pattern.Length];
                Buffer.BlockCopy(input, i, segment, 0, pattern.Length);
                if (segment.SequenceEqual(pattern))
                    return i;
                i = Array.IndexOf(input, pattern[0], i + pattern.Length);
            }
            return -1;
        }

        private static byte[] Decompress(byte[] input)
        {
            using (var inputStream = new MemoryStream(input))
            using (var zipStream = new InflaterInputStream(inputStream))
            using (var ms = new MemoryStream())
            {
                zipStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private static void Log(string header, string message)
        {
            Console.WriteLine(header);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(message.Trim());
            Console.WriteLine();
        }
    }
}
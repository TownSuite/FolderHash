using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FolderHash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(GetSha512FolderHash(args[0]));
        }

        public static string GetSha512FolderHash(string path)
        {         
            var files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            Array.Sort(files);
            using (var sha = new SHA512Managed())
            {
                int i = 0;
                while (i < files.Length)
                {
                    var file = files[i];
                    // hash path
                    var relativePath = file.Substring(path.Length + 1);
                    var pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                    sha.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                    // hash contents
                    var contentBytes = System.IO.File.ReadAllBytes(file);
                    if (i == files.Length - 1)
                    {
                        sha.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                    }
                    else
                    {
                        sha.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
                    }
                    i += 1;
                }
                return BitConverter.ToString(sha.Hash).Replace("-", "").ToLower();
            }
        }
    }
}

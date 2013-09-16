using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace NeopleDigger
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];

            FileStream stream = File.OpenRead(path);
            BinaryReader reader = new BinaryReader(stream);

            string fileName = new string(reader.ReadChars(24));
            reader.ReadInt32();
            int indexCount = reader.ReadInt32();
            Console.WriteLine("filename: {1}; Index count: {0}", indexCount, fileName);

            List<string> lines = new List<string>();

            for (int i = 0; i < indexCount; i++)
            {
                reader.ReadUInt64();
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                int size = reader.ReadInt32();

                int keyX = reader.ReadInt32();
                int keyY = reader.ReadInt32();

                int maxWidth = reader.ReadInt32();
                int maxHeight = reader.ReadInt32();

                byte[] data = reader.ReadBytes(size);

                lines.Add(string.Format("{0}|{1}|{2}|{3}", width, height, keyX, keyY));
            }

            string outputFileName = Path.GetFileNameWithoutExtension(path);
            File.WriteAllLines(outputFileName+".txt", lines.ToArray());
        }
    }
}

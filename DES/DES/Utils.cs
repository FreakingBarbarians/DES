using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace MyUtils
{
    public partial class Utils
    {
        public static Type[] GetTypes(params System.Object[] args)
        {
            Type[] types = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                types[i] = args[i].GetType();
            }
            return types;
        }

        public static string AddFileExtension(string file, string extension) {
            if (extension.Length < 1) {
                throw new Exception("Empty string found");
            }

            if (extension[0] == '.')
            {
                extension = extension.Substring(1);
            }

            if (extension.Length < 1)
            {
                throw new Exception("Empty string found");
            }

            string[] s = file.Split('.');
            return s[0] + "." + extension;
        }

        public static byte[] ByteSerialize(object obj) {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            bf.Serialize(stream, obj);
            return stream.ToArray();
        }

        public static byte[] ByteSerialize(int num)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            bf.Serialize(stream, num);
            return stream.ToArray();
        }

        public static int RollDice(int x, int y) {
            Random random = new Random();

            int sum = 0;

            for (int i = 0; i < x; i++)
            {
                sum += random.Next(1, y);
            }

            return sum;
        }


    }
}
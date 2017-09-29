using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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

        public static string AddFileExtension(string file, string extension)
        {
            if (extension.Length < 1)
            {
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

        public static byte[] ByteSerialize(object obj)
        {
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

        public static int RollDice(int x, int y)
        {
            Random random = new Random();

            int sum = 0;

            for (int i = 0; i < x; i++)
            {
                sum += random.Next(1, y);
            }

            return sum;
        }

        public static byte[] ByteParse(string toParse)
        {
            byte[] ret = new byte[0];
            if (toParse.Length > 0)
            {

                string[] dat = toParse.Split('%');

                if (dat[0][0] == 's' || dat[0][0] == 'S')
                {
                    ret = System.Text.Encoding.UTF8.GetBytes(dat[1]);
                }
                else if (dat[0][0] == 'd' || dat[0][0] == 'D')
                {
                    ret = BitConverter.GetBytes(Double.Parse(dat[1]));
                }
                else if (dat[0][0] == 'f' || dat[0][0] == 'F')
                {
                    ret = BitConverter.GetBytes(float.Parse(dat[1]));
                }
                else if (dat[0][0] == 'l' || dat[0][0] == 'L')
                {
                    ret = BitConverter.GetBytes(long.Parse(dat[1]));
                }
                else if (dat[0][0] == 'b' || dat[0][0] == 'B')
                {
                    ret = BitConverter.GetBytes(bool.Parse(dat[1]));
                }
                else if (dat[0][0] == 'i' || dat[0][0] == 'I')
                {
                    ret = BitConverter.GetBytes(int.Parse(dat[1]));
                }
                else
                {
                    throw new Exception("Unrecognizable type: " + toParse);
                }
            }
            return ret;
        }
    }
}
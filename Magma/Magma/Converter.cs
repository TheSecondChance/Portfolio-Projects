using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magma
{
    static class Converter
    {
        public static string ToBinaryString(string input)
        {
            StringBuilder builder = new StringBuilder();
            byte[] utf8Bytes = Encoding.Default.GetBytes(input);

            foreach (byte b in utf8Bytes)
            {
                string binaryByte = Convert.ToString(b, 2);

                while (binaryByte.Length < 8)
                    binaryByte = "0" + binaryByte;
                builder.Append(binaryByte);
                builder.Append(' ');
            }
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
        public static string ToString(string binaryString)
        {
            string[] stringBytes = binaryString.Split(' ');
            byte[] bytes = new byte[stringBytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(stringBytes[i], 2);
            }
            return Encoding.Default.GetString(bytes);
        }
        public static string FromHexToBinary(string hexString)
        {
            string binaryString = string.Empty;

            for (int i = 0; i < hexString.Length; i++)
            {
                string halfByte = Convert.ToString(Convert.ToInt32(hexString[i].ToString(), 16), 2);
                while (halfByte.Length < 4)
                {
                    halfByte = halfByte.Insert(0, "0");
                }
                binaryString += halfByte;
                if (i % 2 != 0 && i != hexString.Length - 1) binaryString += " ";
            }

            return binaryString;
        }
    }
}

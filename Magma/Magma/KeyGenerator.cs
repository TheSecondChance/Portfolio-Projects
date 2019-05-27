using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Magma
{
    class KeyGenerator
    {
        static private int keyLength = 256;

        static public string Generate()
        {
            Random randomizer = new Random();
            string key = "";
            for (int i = 0; i < keyLength / 4; i++)
            {
                key += Convert.ToString(randomizer.Next(15), 16);
            }
            return key;
        }
    }
}

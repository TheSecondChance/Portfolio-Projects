using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Optimal_encoding__Huffman_
{
    public partial class MainForm : Form
    {
        Dictionary<char, string> codeWords;
        string inputFileName;
        string outputFileName;
        int paddedBits;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button_LoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Текстовые файлы|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFileName = openFileDialog.FileName;
                label_fileName.Text = "Файл с исходным текстом: " + inputFileName;

                string fileText = File.ReadAllText(inputFileName);
                Dictionary<char, double> probs = FrequencyAnalysis(fileText);
                
                HuffmanEncoding encoding = new HuffmanEncoding();
                
                codeWords = encoding.getCodeWords(probs);

                encodingTable.Rows.Clear();
                for (int i = 0; i < codeWords.Count; i++)
                {
                    string[] row = new string[5];
                    row[0] = (i+1).ToString();

                    char symbol = probs.ElementAt(i).Key;

                    if (Char.IsWhiteSpace(symbol))
                    {
                        string descr = String.Empty;
                        switch(symbol)
                        {
                            case ' ':
                                descr = "Пробел";
                                break;
                            case '\n':
                                descr = "Перенос строки";
                                break;
                            case '\r':
                                descr = "Возврат каретки";
                                break;
                        }

                        row[1] = descr;
                    }
                    else
                        row[1] = probs.ElementAt(i).Key.ToString();

                    row[2] = probs.ElementAt(i).Value.ToString();
                    row[3] = codeWords.ElementAt(i).Value.ToString();
                    row[4] = row[3].Length.ToString();
                    encodingTable.Rows.Add(row);
                }
            }
        }

        private void button_EncodeFile_Click(object sender, EventArgs e)
        {
            string fileText = File.ReadAllText(inputFileName);
            string binaryFileString = "";

            for (int i = 0; i < fileText.Length; i++)
            {
                char symbol = fileText[i];
                binaryFileString += codeWords[symbol];
            }

            byte[] byteArray = StringToBytesArray(binaryFileString);
            outputFileName = Path.GetDirectoryName(inputFileName) + "\\encodedFile.bin";

            Stream stream = new FileStream(outputFileName, FileMode.Create);

            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(byteArray);
            }

            label_EncodedFile.Text = "Кодированный файл: " + outputFileName;
        }

        private void button_decodeFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Бинарные файлы|*.bin";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFileName = openFileDialog.FileName;
                label_fileName.Text = "Файл с кодированным текстом: " + inputFileName;

                byte[] byteArray = File.ReadAllBytes(inputFileName);
                string encodedString = "";

                bool isFirstByte = true;
                foreach (byte b in byteArray)
                {
                    if (isFirstByte)
                        encodedString += Convert.ToString(b, 2).PadLeft(8,'0');
                    else
                        encodedString += Convert.ToString(b, 2);
                }
                encodedString = encodedString.Substring(paddedBits, encodedString.Length - paddedBits);

                string decodedString = "";

                string code = "";
                for (int i = 0; i < encodedString.Length; i++)
                {
                    code += encodedString[i];
                    if (codeWords.ContainsValue(code))
                    {
                        var symbol = from entry in codeWords where entry.Value == code select entry.Key;
                        decodedString += symbol.FirstOrDefault();
                        code = "";
                    }
                }

                outputFileName = Path.GetDirectoryName(inputFileName) + "\\decodedFile.txt";
                File.WriteAllText(outputFileName, decodedString);
                label_EncodedFile.Text = "Декодированный файл: " + outputFileName;
            }
        }

        private Dictionary<char, double> FrequencyAnalysis(string text)
        {
            Dictionary<char, int> frequensies = new Dictionary<char, int>();
            
            for (int i = 0; i < text.Length; i++)
            {
                char symbol = text[i];

                if (!frequensies.ContainsKey(symbol))
                    frequensies.Add(symbol, 1);
                else
                    frequensies[symbol]++;
            }

            Dictionary<char, double> probabilities = new Dictionary<char, double>();
            foreach (KeyValuePair<char, int> item in frequensies)
                probabilities.Add(item.Key, Math.Round(item.Value / (double)text.Length, 4));

            var sort = from entry in probabilities orderby entry.Value descending select entry;

            Dictionary<char, double> sortedProbabilities = new Dictionary<char, double>();

            foreach (KeyValuePair<char, double> item in sort)
                sortedProbabilities.Add(item.Key, item.Value);

            return sortedProbabilities;
        }

        private byte[] StringToBytesArray(string str)
        {
            int bitsToPad = 8 - str.Length % 8;
            paddedBits = bitsToPad;

            if (bitsToPad != 8)
            {
                var neededLength = bitsToPad + str.Length;
                str = str.PadLeft(neededLength, '0');
            }

            int size = str.Length / 8;
            byte[] arr = new byte[size];

            for (int i = 0; i < size; i++)
                arr[i] = Convert.ToByte(str.Substring(i * 8, 8), 2);

            return arr;
        }
    }
}

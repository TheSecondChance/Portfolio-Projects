using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Magma
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_plainText_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TextBox_plainText.Text == "Введите открытый текст в это поле...")
            {
                TextBox_plainText.Text = "";
                TextBox_plainText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_plainText_MouseLeave(object sender, MouseEventArgs e)
        {
            if (TextBox_plainText.Text == "")
            {
                TextBox_plainText.Text = "Введите открытый текст в это поле...";
                TextBox_plainText.Foreground = new SolidColorBrush(Colors.Silver);
            }
        }

        private void TextBox_plainText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_plainText.Text != "Введите открытый текст в это поле...")
            {
                TextBox_plainText.Foreground = new SolidColorBrush(Colors.Black);
                TextBox_plainText.Text = TextBox_plainText.Text.Replace("Введите открытый текст в это поле...", "");
            }
        }

        private void TextBox_cipherText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_cipherText.Text != "Шифрованный текст появится в этом поле...")
            {
                TextBox_cipherText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_key_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TextBox_key.Text == "Ключ для шифрования...")
            {
                TextBox_key.Text = "";
                TextBox_key.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_key_MouseLeave(object sender, MouseEventArgs e)
        {
            if (TextBox_key.Text == "")
            {
                TextBox_key.Text = "Ключ для шифрования...";
                TextBox_key.Foreground = new SolidColorBrush(Colors.Silver);
            }
        }

        private void TextBox_key_TextChanged(object sender, TextChangedEventArgs e)
        {
            int keyBlocksNumber = 8;

            if (TextBox_key.Text != "Ключ для шифрования...")
            {
                TextBox_key.Foreground = new SolidColorBrush(Colors.Black);
                TextBox_key.Text = TextBox_key.Text.Replace("Ключ для шифрования...", "");

                Regex check = new Regex("[0-9a-f]{64}");

                if (check.IsMatch(TextBox_key.Text))
                {
                    string[] keyParts = new string[keyBlocksNumber];
                    for (int i = 0; i < keyBlocksNumber; i++)
                    {
                        keyParts[i] = TextBox_key.Text.Substring(i * 8, keyBlocksNumber);
                    }
                    TextBox_key.Text = string.Join("-", keyParts);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox_key.Text = KeyGenerator.Generate();
        }

        private void Button_encrypt_Click(object sender, RoutedEventArgs e)
        {
            string plaintext, key, ciphertext;
            Regex checkKey = new Regex("^([0-9a-f]{8}-){7}[0-9a-f]{8}$");
            plaintext = TextBox_plainText.Text;
            key = TextBox_key.Text;

            if (checkKey.IsMatch(key))
            {
                if (TextBox_plainText.Text != "Введите открытый текст в это поле...")
                {
                    ciphertext = SimpleReplacemant.Encrypt(plaintext, key);
                    TextBox_cipherText.Text = ciphertext;
                }
            }
            else MessageBox.Show("Введён некорректный ключ!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

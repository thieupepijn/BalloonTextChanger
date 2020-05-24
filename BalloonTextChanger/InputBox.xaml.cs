using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BalloonTextChanger
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        public string Text { get; set; }
        public int TextSize { get; set; }

        public InputBox(int defaultFontSize)
        {
            InitializeComponent();

            List<int> fontSizes = new List<int>();
            for (int fontSize = 8; fontSize < 26; fontSize++)
            {
                fontSizes.Add(fontSize);
            }
            cmbFontSizes.ItemsSource = fontSizes;
            cmbFontSizes.SelectedIndex = fontSizes.FindIndex(fs => fs == defaultFontSize);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Text = txtInput.Text;
            TextSize = (int)cmbFontSizes.SelectedItem;
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
            DialogResult = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
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

namespace Calculator {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            button3.Content = "<";
            row1.Height = new GridLength(0.6, GridUnitType.Star);
        }
        private void text1_TextChanged(object sender, TextChangedEventArgs e) => text2.Text = "";
        private void ButtonClick(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            switch (button.Content.ToString()) {
                case "C":
                    text1.Text = "";
                    break;
                case "CE":
                    if(text1.Text != "") {
                        for (int i = text1.Text.Length - 1; i >= 0; i--) {
                            if (text1.Text[i] == '+' || text1.Text[i] == '-'
                                || text1.Text[i] == '*' || text1.Text[i] == '/') break;
                            text1.Text = text1.Text = text1.Text.Substring(0, i);
                        }
                    }
                    break;
                case "=":
                    if (text1.Text.Contains("/0") && (text1.Text[text1.Text.Length - 1] == '0' ||
                        text1.Text[text1.Text.LastIndexOf("/0") + 2] != '.')) text2.Text = "На ноль делить нельзя";
                    else {
                        try { text2.Text = new DataTable().Compute(text1.Text, null).ToString(); }
                        catch (Exception ex) {
                            MessageBox.Show(ex.Message + "\nНеправильно введено выражение.", "!",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    break;
                case "<":
                    if(text1.Text != "") text1.Text = text1.Text.Substring(0, text1.Text.Length - 1);
                    break;
                case ".":
                    if (text1.Text[text1.Text.Length - 1] == '.') return;
                    else text1.Text += button.Content;
                    break;
                default:
                    if (text1.Text == "0") text1.Text = text1.Text.Substring(0, text1.Text.Length - 1);
                    text1.Text += button.Content;
                    break;
            }
        }
    }
}

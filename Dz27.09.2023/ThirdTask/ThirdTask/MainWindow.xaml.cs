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

namespace ThirdTask {
    public partial class MainWindow : Window {
        List<TextBox> textBoxes;
        int quan = 0;
        double res;
        double[] arr = new double[9];
        public MainWindow() {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            textBoxes = new List<TextBox>() { text1, text2, text3, text4, text5, text6, text7, text8, text9 };
            foreach (var box in textBoxes) if (box.Text != "") quan++;
            if (quan == 9) {
                for (int i = 0; i < arr.Length; i++) arr[i] = double.Parse(textBoxes[i].Text);
                res = (arr[0] * arr[4] * arr[8]) + (arr[1] * arr[5] * arr[6]) + (arr[2] * arr[3] * arr[7])
                    - (arr[2] * arr[4] * arr[6]) - (arr[0] * arr[5] * arr[7]) - (arr[1] * arr[3] * arr[8]);
                result.Text = res.ToString();
            }
            else MessageBox.Show("Не все квадратики заполнены!", "!");
        }
    }
}

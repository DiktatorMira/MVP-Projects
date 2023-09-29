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

namespace FourthTask {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            wrap1.Height = Height / 7;
            grid1.Height = Height - wrap1.Height;
            grid1.Width = Width / 2 - 10;
            wrap2.Width = Width / 2 - 10;
            textBox.Width = Width / 2 - 10;
            textBox.Height = Height - wrap1.Height;
            textBox.Text += "9:00 - Начало занятий\n";
            textBox.Text += "10:25 - Перерыв\n";
            textBox.Text += "10:25 - Начало второй пары\n";
            textBox.Text += "12:00 - Конец занятий";
        }
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e) {
            grid1.Width = Width / 2 - 10;
            grid1.Height = Height - wrap1.Height;
            wrap2.Width = Width / 2 - 10;
            textBox.Width = Width / 2 - 10;
            if (Width <= 650 && Width > 375) {
                grid1.Height = Height - (wrap1.Height * 1.5);
                textBox.Height = Height - (wrap1.Height * 1.5);
            }
            else if (Width <= 375) {
                grid1.Height = Height - (wrap1.Height * 2);
                textBox.Height = Height - (wrap1.Height * 2);
            }
            else {
                grid1.Height = Height - wrap1.Height;
                textBox.Height = Height - wrap1.Height;
            }
        }
    }
}

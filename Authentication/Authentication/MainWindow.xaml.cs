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

namespace Authentication{
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            row4.Height = new GridLength(0.6, GridUnitType.Star);
            row3.Height = new GridLength(0.5, GridUnitType.Star);
            row2.Height = new GridLength(0.6, GridUnitType.Star);
            but1.Width = Width / 3.5;
            but2.Width = Width / 3.5;
            text1.Height = Height / 7.5;
            text2.Height = Height / 7.5;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            but1.Width = Width / 3.5;
            but2.Width = Width / 3.5;
            text1.Height = Height / 7.5;
            text2.Height = Height / 7.5;
        }
    }
}

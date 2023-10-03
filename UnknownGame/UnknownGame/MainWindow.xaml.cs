using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace UnknownGame{
    public partial class MainWindow : Window {
        public int time, number, active = 0;
        public Random rand = new Random();
        public List<Button> buttons { get; set; }
        public List<int> nums { get; set; }
        public MainWindow() {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bar.Width = Width;
            bar.Height = Height * 0.05;
            text1.Width = Width * 0.225;
            text1.Height = Height * 0.45;
            buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9,
            button10, button11, button12, button13, button14, button15, button16 };
            foreach (var button in buttons) button.Click += ClickButton;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            bar.Width = Width;
            bar.Height = Height * 0.05;
            text1.Width = Width * 0.225;
            text1.Height = Height * 0.45;
            if (Width < 425) label1.FontSize = 8;
            else label1.FontSize = 10;
        }
        private void ClickButton(object sender, RoutedEventArgs e) {
            Button button = (Button)sender; ;
            if (nums[active] == int.Parse(button.Content.ToString())) {
                button.IsEnabled = false;
                text1.Text += button.Content + "\n";
                active++;
                if (active == buttons.Count) {
                    MessageBoxResult res = MessageBox.Show("Поздравляю, вы победили! Хотите начать заново?", "Молодец",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if(res == MessageBoxResult.Yes) Restart();
                    else Application.Current.Shutdown();
                }
            }
            else return;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        private void MenuItem_Click_1(object sender, RoutedEventArgs e) => Restart();
        private void Restart() {
            string temp = Application.ResourceAssembly.Location;
            var path = new StringBuilder(temp);
            path[path.Length - 1] = 'e';
            path[path.Length - 2] = 'x';
            path[path.Length - 3] = 'e';
            Process.Start(new ProcessStartInfo { FileName = path.ToString(), UseShellExecute = true });
            Application.Current.Shutdown();
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            if (text2.Text != "") time = int.Parse(text2.Text);
            else {
                MessageBox.Show("Введите время!", "!",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (time >= 30 && time <= 180) {
                menu1.Background = Brushes.White;
                menu1.IsEnabled = false;
                text2.IsEnabled = false;
                bar.Maximum = time;
                TimerBarTick();
                GenerateNumbers();
            }
            else MessageBox.Show("Необходимо ввести время от 30 до 180 секунд!", "!",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private async void TimerBarTick() {
            int min = time / 60, sec = time - (min * 60);
            label2.Content = string.Format("{0:D2}:{1:D2}", min, sec);
            await Task.Delay(1000);
            while (bar.Value != time) {
                if (active == 16) return;
                bar.Value++;
                sec--;
                if(sec < 0) {
                    min--;
                    sec = 59;
                }
                label2.Content = string.Format("{0:D2}:{1:D2}", min, sec);
                await Task.Delay(1000);
            }
            MessageBoxResult res = MessageBox.Show("Времени не хватило... Хотите начать заново?", "Не совсем молодец",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (res == MessageBoxResult.Yes) Restart();
            else Application.Current.Shutdown();
        }
        public void GenerateNumbers() {
            var numbers = Enumerable.Range(0, 100).ToList();
            var numbersCopy = new List<int>(numbers);
            nums = new List<int>();
            foreach (var button in buttons) {
                var pickIndex = rand.Next(numbersCopy.Count);
                button.Content = numbersCopy[pickIndex].ToString();
                nums.Add(numbersCopy[pickIndex]);
                numbersCopy.RemoveAt(pickIndex);
            }
            nums.Sort();
        }
    }
}

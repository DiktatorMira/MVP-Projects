using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Xml.Linq;

namespace Notebook {
    public partial class MainWindow : Window {
        int temp;
        public MainWindow() {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            row1.Height = new GridLength(0.5, GridUnitType.Star);
        }
        private void LoadFromFileClick(object sender, RoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            using (StreamReader reader = new StreamReader("persons.json")) {
                string json = reader.ReadToEnd();
                pers.Persons = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
                Dispatcher.Invoke(() => listBox.ItemsSource = pers.Persons);
            }
        }
        private void SaveToFileClick(object sender, RoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            using (StreamWriter writer = new StreamWriter("persons.json")) {
                string json = JsonSerializer.Serialize(pers.Persons);
                writer.Write(json);
            }
        }
        private void AddClick(object sender, RoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.Fio != "" && pers.Address != "" && pers.Phone != "") {
                pers.Persons.Add(pers.Fio + "  " + pers.Address + "  " + pers.Phone);
                pers.Fio = string.Empty;
                pers.Address = string.Empty;
                pers.Phone = string.Empty;
                //-----------------------> Не получилось подсключить стили(ошибка на этапе исполнения)
                //ListBoxItem item = (ListBoxItem)(listBox.ItemContainerGenerator.ContainerFromIndex(0));
                //item.Style = FindResource("ListBoxItemStyle") as Style;
            }
            else MessageBox.Show("Необходимо заполнить все поля!", "!");
        }
        private void ChangeClick(object sender, RoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.SelectedIndex != -1) {
                string[] arr = pers.Persons[pers.SelectedIndex].Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                pers.Fio = arr[0];
                pers.Address = arr[1];
                pers.Phone = arr[2];
                temp = pers.SelectedIndex;
                change.IsEnabled = true;
                menu.IsEnabled = false;
            }
            else MessageBox.Show("Необходимо вырбрать элемент для изменения!", "!");
        }
        private void DeleteClick(object sender, RoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.SelectedIndex != -1) {
                MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Точно?",
                      MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes) pers.Persons.RemoveAt(pers.SelectedIndex);
            }
            else MessageBox.Show("Необходимо вырбрать элемент для удаления!", "!");
        }
        private void SaveChangeClick(object sender, RoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.Fio != "" && pers.Address != "" && pers.Phone != "") {
                pers.Persons.RemoveAt(temp);
                pers.Persons.Insert(temp, pers.Fio + "   " + pers.Address + "   " + pers.Phone);
                pers.Fio = string.Empty;
                pers.Address = string.Empty;
                pers.Phone = string.Empty;
                change.IsEnabled = false;
                menu.IsEnabled = true;
            }
            else MessageBox.Show("Необходимо заполнить все поля!", "!");
        }
    }
    public class Person : INotifyPropertyChanged {
        private string fio, address, phone;
        public ObservableCollection<string> Persons { get; set; } = new ObservableCollection<string>();
        private int selectedIndex = -1;
        public int SelectedIndex  {
            get { return selectedIndex; }
            set {
                selectedIndex = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
        }
        public string Fio {
            get { return fio; }
            set {
                fio = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Fio)));
            }
        }
        public string Address {
            get { return address; }
            set {
                address = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Address)));
            }
        }
        public string Phone {
            get { return phone; }
            set {
                phone = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Phone)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    }
}

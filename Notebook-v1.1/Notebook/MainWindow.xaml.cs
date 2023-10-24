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
        private void LoadCommand(object sender, ExecutedRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            using (StreamReader reader = new StreamReader("persons.json")) {
                string json = reader.ReadToEnd();
                pers.Persons = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
                Dispatcher.Invoke(() => listBox.ItemsSource = pers.Persons);
            }
        }
        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            using (StreamWriter writer = new StreamWriter("persons.json")) {
                string json = JsonSerializer.Serialize(pers.Persons);
                writer.Write(json);
            }
        }
        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.Persons.Count > 0) e.CanExecute = true;
            else e.CanExecute = false;
        }
        private void AddCommand(object sender, ExecutedRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            pers.Persons.Add(pers.Fio + "  " + pers.Address + "  " + pers.Phone);
            pers.Fio = string.Empty;
            pers.Address = string.Empty;
            pers.Phone = string.Empty;
        }
        private void AddCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.Fio != "" && pers.Address != "" && pers.Phone != "") e.CanExecute = true;
            else e.CanExecute = false;
        }
        private void ChangeCommand(object sender, ExecutedRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            string[] arr = pers.Persons[pers.SelectedIndex].Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
            pers.Fio = arr[0];
            pers.Address = arr[1];
            pers.Phone = arr[2];
            temp = pers.SelectedIndex;
            change.IsEnabled = true;
            menu.IsEnabled = false;
        }
        private void ChangeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.SelectedIndex != -1) e.CanExecute = true;
            else e.CanExecute = false;
        }
        private void SaveChangeCommand(object sender, ExecutedRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            pers.Persons.RemoveAt(temp);
            pers.Persons.Insert(temp, pers.Fio + "   " + pers.Address + "   " + pers.Phone);
            pers.Fio = string.Empty;
            pers.Address = string.Empty;
            pers.Phone = string.Empty;
            change.IsEnabled = false;
            menu.IsEnabled = true;
        }
        private void SaveChangeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.Fio != "" && pers.Address != "" && pers.Phone != "") e.CanExecute = true;
            else e.CanExecute = false;
        }
        private void DeleteCommand(object sender, ExecutedRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Точно?",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) pers.Persons.RemoveAt(pers.SelectedIndex);
        }
        private void DeleteCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            Person pers = Resources["pers"] as Person;
            if (pers.SelectedIndex != -1) e.CanExecute = true;
            else e.CanExecute = false;
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
    public class CommandLoad {
        private static RoutedUICommand load;
        static CommandLoad() {
            InputGestureCollection input = new InputGestureCollection();
            input.Add(new KeyGesture(Key.L, ModifierKeys.Control, "Ctrl+L"));
            load = new RoutedUICommand(
              "Загрузить из файла", "Загрузить из файла", typeof(CommandLoad), input);
        }
        public static RoutedUICommand Load {
            get { return load; }
        }
    }
    public class CommandSave {
        private static RoutedUICommand save;
        static CommandSave() {
            InputGestureCollection input = new InputGestureCollection();
            input.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));
            save = new RoutedUICommand(
              "Сохранить в файл", "Сохранить в файл", typeof(CommandSave), input);
        }
        public static RoutedUICommand Save {
            get { return save; }
        }
    }
    public class CommandAdd {
        private static RoutedUICommand add;
        static CommandAdd() {
            InputGestureCollection input = new InputGestureCollection();
            input.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl+A"));
            add = new RoutedUICommand(
              "Добавить", "Добавить", typeof(CommandAdd), input);
        }
        public static RoutedUICommand Add {
            get { return add; }
        }
    }
    public class CommandChange {
        private static RoutedUICommand change;
        static CommandChange() {
            InputGestureCollection input = new InputGestureCollection();
            input.Add(new KeyGesture(Key.C, ModifierKeys.Control, "Ctrl+C"));
            change = new RoutedUICommand(
              "Изменить", "Изменить", typeof(CommandChange), input);
        }
        public static RoutedUICommand Change {
            get { return change; }
        }
    }
    public class CommandSaveChange {
        private static RoutedUICommand save_change;
        static CommandSaveChange() {
            InputGestureCollection input = new InputGestureCollection();
            input.Add(new KeyGesture(Key.D, ModifierKeys.Control, "Ctrl+D"));
            save_change = new RoutedUICommand(
              "Сохранить изменения", "Сохранить изменения", typeof(CommandSaveChange), input);
        }
        public static RoutedUICommand SaveChange {
            get { return save_change; }
        }
    }
    public class CommandDelete {
        private static RoutedUICommand delete;
        static CommandDelete() {
            InputGestureCollection input = new InputGestureCollection();
            input.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            delete = new RoutedUICommand(
              "Удалить", "Удалить", typeof(CommandDelete), input);
        }
        public static RoutedUICommand Delete {
            get { return delete; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ResumeDatabase {
    public abstract class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class MainViewModel : BaseViewModel {
        public ObservableCollection<string> ComboPersons { get; set; }
        public ObservableCollection<PersonViewModel> ListPersons { get; set; }
        private ObservableCollection<PersonViewModel> temp_list = new ObservableCollection<PersonViewModel>();
        private PersonViewModel current;
        private int combo_index;
        private Commands? add, select, clear, remove, save;
        public MainViewModel() {
            ComboPersons = new ObservableCollection<string>();
            ListPersons = new ObservableCollection<PersonViewModel>();
            Current = new PersonViewModel();
            LoadFromFile();
        }
        public PersonViewModel Current {
            get { return current; }
            set {
                current = value;
                OnPropertyChanged(nameof(Current));
            }
        }
        public int ComboIndex {
            get { return combo_index; }
            set {
                if (combo_index != value) {
                    combo_index = value;
                    OnPropertyChanged(nameof(ComboIndex));
                }
            }
        }
        private void LoadFromFile() {
            try {
                if (File.Exists("template.json") && File.Exists("combobox.json")) {
                    string json = File.ReadAllText("template.json");
                    temp_list = JsonConvert.DeserializeObject<ObservableCollection<PersonViewModel>>(json);
                    json = File.ReadAllText("combobox.json");
                    ComboPersons = JsonConvert.DeserializeObject<ObservableCollection<string>>(json);
                }
            }
            catch(Exception ex) { MessageBox.Show("Ошибка загрузки из файла: " + ex.Message); }
        }
        public ICommand AddCommand {
            get {
                if (add == null) add = new Commands(execute => Add(), can => CanAdd());
                return add;
            }
        }
        private void Add() {
            ComboPersons.Add(Current.ComboText());
            temp_list.Add(Current.Clone());
            Current.Fio = Current.Age = Current.FamilyStatus = Current.Address = Current.Email = string.Empty;
            Current.Check1 = Current.Check2 = Current.Check3 = false;
        }
        private bool CanAdd() { return !current.IsEmpty(); }
        public ICommand SelectCommand {
            get {
                if (select == null) select = new Commands(execute => Select(), can => CanSelect());
                return select;
            }
        }
        private void Select() {
            ListPersons.Clear();
            ListPersons.Add(temp_list[ComboIndex]);
        }
        private bool CanSelect() { return ComboPersons.Count > 0; }
        public ICommand ClearCommand {
            get {
                if (clear == null) clear = new Commands(execute => Clear(), can => CanClear());
                return clear;
            }
        }
        private void Clear() => ListPersons.Clear();
        private bool CanClear() { return ComboPersons.Count > 0 && ListPersons.Count > 0; }
        public ICommand RemoveCommand {
            get {
                if (remove == null) remove = new Commands(execute => Remove(), can => CanRemove());
                return remove;
            }
        }
        private void Remove() {
            MessageBoxResult res = MessageBox.Show("Вы точно хотите удалить резюме?", "?", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) {
                ComboPersons.RemoveAt(ComboIndex);
                ListPersons.Clear();
            }
        }
        private bool CanRemove() { return ComboPersons.Count > 0; }
        public ICommand SaveCommand {
            get {
                if (save == null) save = new Commands(execute => SaveToFile(), can => CanSave());
                return save;
            }
        }
        private void SaveToFile() {
            try {
                string json = JsonConvert.SerializeObject(temp_list, Formatting.Indented);
                File.WriteAllText("template.json", json);
                json = JsonConvert.SerializeObject(ComboPersons, Formatting.Indented);
                File.WriteAllText("combobox.json", json);
            }
            catch(Exception ex) { MessageBox.Show("Ошибка сохранения в файл: " + ex.Message); }
            MessageBox.Show("Пользователи сохранены в файл.");
        }
        private bool CanSave() { return temp_list.Count > 0; }
    }
    public class PersonViewModel : BaseViewModel {
        private ResumeModel model;
        private bool check1, check2, check3;
        public PersonViewModel() => model = new ResumeModel();
        public bool Check1 {
            get { return check1; }
            set {
                check1 = value;
                OnPropertyChanged(nameof(Check1));
            }
        }
        public bool Check2 {
            get { return check2; }
            set {
                check2 = value;
                OnPropertyChanged(nameof(Check2));
            }
        }
        public bool Check3 {
            get { return check3; }
            set {
                check3 = value;
                OnPropertyChanged(nameof(Check3));
            }
        }
        public ResumeModel Model {
            get { return model; }
            set {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
        public string Fio {
            get { return model.Fio; }
            set {
                model.Fio = value;
                OnPropertyChanged(nameof(Fio));
            }
        }
        public string Age {
            get { return model.Age; }
            set {
                model.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        public string FamilyStatus {
            get { return model.FamilyStatus; }
            set {
                model.FamilyStatus = value;
                OnPropertyChanged(nameof(FamilyStatus));
            }
        }
        public string Address {
            get { return model.Address; }
            set {
                model.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Email {
            get { return model.Email; }
            set {
                model.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string IsInfo {
            get { return model.IsInfo; }
            set {
                if (Check1) model.IsInfo = "Да";
                else model.IsInfo = "Нет";
                OnPropertyChanged(nameof(IsInfo));
            }
        }
        public string IsLanguage {
            get { return model.IsLanguage; }
            set {
                if (Check2) model.IsLanguage = "Да";
                else model.IsLanguage = "Нет";
                OnPropertyChanged(nameof(IsLanguage));
            }
        }
        public string IsCommunicate {
            get { return model.IsCommunicate; }
            set {
                if (Check3) model.IsCommunicate = "Да";
                else model.IsCommunicate = "Нет";
                OnPropertyChanged(nameof(IsCommunicate));
            }
        }
        public PersonViewModel Clone() {
            return new PersonViewModel { Fio = Fio, Age = Age, FamilyStatus = FamilyStatus, Address = Address,
            Email = Email, IsInfo = IsInfo, IsLanguage = IsLanguage, IsCommunicate = IsCommunicate }; 
        }
        public string ComboText() { return Fio + ", " + Age; }
        public bool IsEmpty() {
            return (string.IsNullOrEmpty(Fio) || string.IsNullOrEmpty(Age) || string.IsNullOrEmpty(FamilyStatus)
                || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Email));
        }
    }
}
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Notebook {
    public class MainVM : DependencyObject {
        public ObservableCollection<PersonsVM> persons { get; set; }
        public PersonsVM current { get; set; }
        private static readonly DependencyProperty selected_index;
        private Commands? load, save, add, change, save_change, remove;
        private int temp;
        private bool is_add = true, is_load = true, is_save = true, is_save_change = false;
        public int SelectedIndex {
            get { return (int)GetValue(selected_index); }
            set { SetValue(selected_index, value); }
        }
        public MainVM() {
            SelectedIndex = -1;
            current = new PersonsVM();
            persons = new ObservableCollection<PersonsVM>();
        }
        static MainVM() {
            selected_index = DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(MainVM));
        }
        public ICommand LoadCommand {
            get {
                if (load == null) { load = new Commands(exectute => Load(), can => CanLoad()); }
                return load;
            }

        }
        private void Load() {
            ObservableCollection<PersonsVM> pers = JsonDatabase.Pointer.Deserialize("persons.json");
            if (pers == null || pers.Count == 0) return;
            persons.Clear();
            foreach (var i in pers) {
                PersonsVM obj = new PersonsVM { Fio = i.Fio, Address = i.Address, Phone = i.Phone };
                persons.Add(obj);
            }
        }
        private bool CanLoad() { return is_load; }
        public ICommand SaveCommand {
            get {
                if (save == null) { save = new Commands(exectute => Save(), can => CanSave()); }
                return save;
            }
        }
        private void Save() {
            JsonDatabase.Pointer.Serialize(persons, "persons.json");
            MessageBox.Show("Записная книжка сохранена в файл.", "!");
        }
        private bool CanSave() { return persons.Count > 0 && is_save; }
        public ICommand AddCommand {
            get {
                if (add == null) { add = new Commands(exectute => Add(), can => CanAdd()); }
                return add;
            }
        }
        private void Add() {
            persons.Add(current.Clone());
            current.Fio = string.Empty;
            current.Address = string.Empty;
            current.Phone = string.Empty;
        }
        private bool CanAdd() { return !current.IsEmpty() && is_add; }
        public ICommand ChangeCommand {
            get {
                if (change == null) change = new Commands(exexcute => Change(), can => CanChange());
                return change;
            }
        }
        private void Change() {
            if (persons[SelectedIndex] == null) return;
            temp = SelectedIndex;
            current.Fio = persons[SelectedIndex].Fio;
            current.Address = persons[SelectedIndex].Address;
            current.Phone = persons[SelectedIndex].Phone;
            is_add = false;
            is_load = false;
            is_save = false;
            is_save_change = true;
        }
        private bool CanChange() { return SelectedIndex != -1 && current.IsEmpty(); }
        public ICommand SaveChangeCommand {
            get {
                if (save_change == null) save_change = new Commands(exexcute => SaveChange(), can => CanSaveChange());
                return save_change;
            }
        }
        private void SaveChange() {
            persons[temp].Fio = current.Fio;
            persons[temp].Address = current.Address;
            persons[temp].Phone = current.Phone;
            current.Fio = string.Empty;
            current.Address = string.Empty;
            current.Phone = string.Empty;
            is_add = true;
            is_load = true;
            is_save = true;
            is_save_change = false;
        }
        private bool CanSaveChange() { return !current.IsEmpty() && is_save_change; }
        public ICommand RemoveCommand {
            get {
                if (remove == null) remove = new Commands(exexcute => Remove(), can => CanRemove());
                return remove;
            }
        }
        private void Remove() {
            MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Точно?",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) persons.RemoveAt(SelectedIndex);
        }
        private bool CanRemove() { return SelectedIndex != -1 && current.IsEmpty(); ; }
    }
    public class PersonsVM : DependencyObject {
        private PersonModel person;
        private static readonly DependencyProperty fio, address, phone;
        public PersonModel Person {
            get { return person; }
            set { person = value; }
        }
        public string Fio {
            get { return (string)GetValue(fio); }
            set { SetValue(fio, value); }
        }
        public string Address {
            get { return (string)GetValue(address); }
            set { SetValue(address, value); }
        }
        public string Phone {
            get { return (string)GetValue(phone); }
            set { SetValue(phone, value); }
        }
        static PersonsVM() {
            fio = DependencyProperty.Register(nameof(Fio), typeof(string), 
                typeof(PersonsVM), new PropertyMetadata(null, OnFioChanged));
            address = DependencyProperty.Register(nameof(Address), typeof(string), 
                typeof(PersonsVM), new PropertyMetadata(null, OnAddressChanged));
            phone = DependencyProperty.Register(nameof(Phone), typeof(string), 
                typeof(PersonsVM), new PropertyMetadata(null, OnPhoneChanged));
        }
        private static void OnFioChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            var vm = (PersonsVM)sender;
            vm.person.Fio = (string)e.NewValue;
        }
        private static void OnAddressChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            var vm = (PersonsVM)sender;
            vm.person.Address = (string)e.NewValue;
        }
        private static void OnPhoneChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            var vm = (PersonsVM)sender;
            vm.person.Phone = (string)e.NewValue;
        }
        public PersonsVM() => person = new PersonModel();
        public PersonsVM Clone() { return new PersonsVM { Fio = Fio, Address = Address, Phone = Phone }; }
        public bool IsEmpty() {
            return (string.IsNullOrEmpty(Fio) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Phone));
        }
    }
}

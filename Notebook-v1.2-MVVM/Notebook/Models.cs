using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Notebook {
    public class PersonModel : DependencyObject {
        private static readonly DependencyProperty fio, address, phone;
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
        static PersonModel() {
            fio = DependencyProperty.Register(nameof(Fio), typeof(string), typeof(PersonModel));
            address = DependencyProperty.Register(nameof(Address), typeof(string), typeof(PersonModel));
            phone = DependencyProperty.Register(nameof(Phone), typeof(string), typeof(PersonModel));
        }
        public PersonModel Clone() {
            return new PersonModel { Fio = Fio, Address = Address, Phone = Phone };
        }
    }
}

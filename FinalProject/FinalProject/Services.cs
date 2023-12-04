using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalProject {
    public class Commands : ICommand {
        Action<object> execute;
        Predicate<object> can_execute;
        public Commands(Action<object> execute, Predicate<object> can_execute) {
            if (execute == null) throw new ArgumentNullException("execute");
            this.execute = execute;
            this.can_execute = can_execute;
        }
        public bool CanExecute(object param) {
            if (can_execute != null) return can_execute(param);
            return true;
        }
        public void Execute(object param) => execute(param);
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
    public class Navigation {
        public void NavigateRegistration(int value) {
            Registration view = new Registration();
            if (value == 1) view.Show();
            else view.Close();
        }
        public void NavigateSignIn(int value) {
            SignIn view = new SignIn();
            if (value == 1) view.Show();
            else view.Close();
        }
        public void NavigateGallery() {
            Gallery view = new Gallery();
            view.Show();
        }
    }
    public class KeyValue {
        public string Key { get; set; }
        public double Value { get; set; }
        public KeyValue() { }
        public KeyValue(string key, double value) {
            Key = key;
            Value = value;
        }
    }
}

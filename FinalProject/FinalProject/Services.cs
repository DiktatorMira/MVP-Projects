using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    public class WindowNavigate {
        public void WinOpen(int value) {
            Window view;
            if(value == 0) view = new SignIn();
            else if(value == 1) view = new Registration();
            else view = new Gallery();
            view.Show();
        }
        public void WinClose(Window view) {
            if (view == null) throw new ArgumentNullException(nameof(view));
            view.Close();
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

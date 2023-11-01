using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Notebook {
    public partial class App : Application {
        private void OnStartUp(object sender, StartupEventArgs e) {
            MainWindow view = new MainWindow();
            view.Show();
        }
    }
}

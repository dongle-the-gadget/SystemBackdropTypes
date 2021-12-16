using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SystemBackdropTypes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (Environment.OSVersion.Version.Build < 22523)
            {
                MessageBox.Show("This demonstration requires Windows 11 Insider Preview build 22523 or newer.", "Incompatible Windows build", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }
            new MainWindow().Show();
        }
    }
}

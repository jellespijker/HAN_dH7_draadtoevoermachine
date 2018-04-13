using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Diagnostics;

namespace SetCodeAccesSecurity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void SetCAS_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            info.Verb = "runas";
            info.Arguments = "C:/Windows/Microsoft.NET/Framework64/v4.0.30319/caspol.exe -m -q -ag 1.2 -url " + System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + "/HAN - Simulate TF/Matlab/* FullTrust";
            Process.Start(info);
        }

        private void CnlBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

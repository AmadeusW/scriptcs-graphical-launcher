using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace ScriptCs.Launcher.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScriptHost scriptHost = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task Execute()
        {
/*
if (scriptHost == null)
{
    scriptHost = new ScriptHost();
    scriptHost.Initialize();
}
*/
            String output = null;
            string path = Path.Text.Trim(' ', '"', '\'');
            await Task.Run(() =>
            {
                var processStartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    Arguments = $"{path} -cache",
                    CreateNoWindow = true,
                    FileName = "scriptcs",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                };
                var proc = System.Diagnostics.Process.Start(processStartInfo);
                output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

            });
            StatusText.Text = output?.ToString();
        }

        private async void Path_KeyUp(Object sender, KeyEventArgs e)
        {
            ExecuteButton.IsEnabled = false;
            try
            {
                if (e.Key == Key.Enter)
                {
                    await Execute();
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = ex.Message;
            }
            ExecuteButton.IsEnabled = true;
        }

        private async void Button_Click(Object sender, RoutedEventArgs e)
        {
            ExecuteButton.IsEnabled = false;
            try
            {
                await Execute();
            }
            catch (Exception ex)
            {
                StatusText.Text = ex.Message;
            }
            ExecuteButton.IsEnabled = true;
        }
    }
}

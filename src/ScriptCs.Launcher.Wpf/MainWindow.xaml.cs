using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        AppController controller = null;

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
            string parameters = String.IsNullOrWhiteSpace(Params.Text) ? String.Empty : $"-- {Params.Text}";
            string arguments = $"\"{path}\" {parameters}";

            await Task.Run(() =>
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    Arguments = arguments,
                    CreateNoWindow = true,
                    FileName = "scriptcs",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                };

                p.OutputDataReceived += new DataReceivedEventHandler(
                    (s, e) =>
                    {
                        StatusText.Dispatcher.BeginInvoke((Action)(() => {
                            StatusText.Text += e.Data;
                        }));
                    }
                );

                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();

            });
            //StatusText.Text = output?.ToString();
            await Task.Run(() =>
            {
                // Store the log
            });
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

        private async void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                controller = await AppController.Initialize();
                this.DataContext = controller.ViewModel;
            }
            catch
            {
                Debugger.Break();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                controller.Save();
            }
            catch
            {
                Debugger.Break();
            }
        }
    }
}

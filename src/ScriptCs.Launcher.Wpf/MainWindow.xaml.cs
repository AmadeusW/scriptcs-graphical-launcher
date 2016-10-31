using ScriptCs.Launcher.Wpf.ScriptList;
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

        private async void Path_KeyUp(Object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    await controller.Execute(allItems.SelectedItem as ScriptInfo);
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = ex.Message;
            }
        }

        private async void Button_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                await controller.Execute((sender as Button).Tag as ScriptInfo);
            }
            catch (Exception ex)
            {
                StatusText.Text = ex.Message;
            }
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

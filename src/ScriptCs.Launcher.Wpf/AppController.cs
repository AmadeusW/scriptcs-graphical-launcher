using ScriptCs.Launcher.Wpf.ScriptList;
using ScriptCs.Launcher.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher.Wpf
{
    public class AppController
    {
        public ApplicationVM ViewModel { get; private set; }
        // todo: jump list

        private AppController() { }

        public static async Task<AppController> Initialize()
        {
            ApplicationVM vm = null;

            await Task.Run(() =>
            {
                vm = FileHelpers.LoadConfiguration();
                if (vm == null)
                {
                    vm = new ApplicationVM { Scripts = new List<ScriptInfo>() { new ScriptInfo { Name = "hey" } } };
                }
            });

            return new AppController { ViewModel = vm };
        }

        public void Save()
        {
            FileHelpers.SaveConfiguration(this.ViewModel);
        }
    }
}

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

        public async Task Execute(ScriptInfo script)
        {
            if (script == null)
                throw new ArgumentNullException(nameof(script));

            try
            {
                script.Executing = true;
                script.UpdateProperty(nameof(script.Executing));

                switch (script.Host)
                {
                    case ExecutionHost.Commandline:
                    default:
                        await ExecuteCommandline(script);
                        break;
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                script.Executing = false;
                script.UpdateProperty(nameof(script.Executing));
            }
        }

        private async Task ExecuteCommandline(ScriptInfo script)
        {
            /*
            if (scriptHost == null)
            {
                scriptHost = new ScriptHost();
                scriptHost.Initialize();
            }
            */
            String output = null;
            string path = script.Path.Trim(' ', '"', '\'');
            string parameters = String.IsNullOrWhiteSpace(script.Arguments) ? String.Empty : $"-- {script.Arguments}";
            string arguments = $"\"{path}\" {parameters}";

            await Task.Run(() =>
            {
                var processStartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    Arguments = arguments,
                    CreateNoWindow = true,
                    FileName = "scriptcs",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                };
                var proc = System.Diagnostics.Process.Start(processStartInfo);
                output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

            });
            script.Output = output?.ToString();
            script.UpdateProperty(nameof(script.Output)); // fuck this shit

            await Task.Run(() =>
            {
                // Store the log
            });
        }
    }
}

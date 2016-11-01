using ScriptCs.Launcher.Wpf.ScriptList;
using ScriptCs.Launcher.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ScriptCs.Launcher.Wpf.Serialization;

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
                vm = SavedApplicationVM.MakeVM(FileHelpers.LoadConfiguration());
                if (vm == null)
                {
                    vm = new ApplicationVM { Scripts = new List<ScriptInfo>() { new ScriptInfo { Name = "hey" } } };
                }
            });

            return new AppController { ViewModel = vm };
        }

        public void Save()
        {
            FileHelpers.SaveConfiguration(SavedApplicationVM.FromVM(ViewModel));
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

        public async Task Stop(ScriptInfo script)
        {
            if (!script.Executing)
                throw new InvalidOperationException("Attempted to stop script that is not running.");
            if (script.Process == null)
                throw new InvalidOperationException("Attempted to stop script without associated process.");

            script.Process.Kill();
            script.Executing = false;
            script.Process = null;
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
                var p = new Process();
                script.Process = p;
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
                        script.Output += e.Data;
                        script.UpdateProperty(nameof(script.Output));
                    }
                );

                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                script.Process = null;
            });

            await Task.Run(() =>
            {
                // Store the log
            });
        }
    }
}

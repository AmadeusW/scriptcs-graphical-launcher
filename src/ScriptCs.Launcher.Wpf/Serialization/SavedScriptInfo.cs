using ScriptCs.Launcher.Wpf.ScriptList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher.Wpf.Serialization
{
    public class SavedScriptInfo
    {
        public static SavedScriptInfo FromScriptInfo(ScriptInfo script)
        {
            return new SavedScriptInfo
            {
                Name = script.Name,
                Path = script.Path,
                Arguments = script.Arguments,
                Host = script.Host,
                LogOutput = script.LogOutput,
                Icon = script.Icon,
                Shortcut = script.Shortcut,
                Color = script.Color,
            };
        }

        public static ScriptInfo MakeScriptInfo(SavedScriptInfo savedScript)
        {
            return new ScriptInfo
            {
                Name = savedScript.Name,
                Path = savedScript.Path,
                Arguments = savedScript.Arguments,
                Host = savedScript.Host,
                LogOutput = savedScript.LogOutput,
                Icon = savedScript.Icon,
                Shortcut = savedScript.Shortcut,
                Color = savedScript.Color,
            };
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }

        public ExecutionHost Host { get; set; }
        public bool LogOutput { get; set; }

        public string Icon { get; set; }
        public string Shortcut { get; set; }
        public string Color { get; set; }
    }
}

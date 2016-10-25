using ScriptCs.Launcher.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher.Wpf.ScriptList
{
    public class ScriptInfo : ViewModelBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }

        public string Host { get; set; }
        public bool LogOutput { get; set; }

        public string Icon { get; set; }
        public string Shortcut { get; set; }
        public string Color { get; set; }
    }
}

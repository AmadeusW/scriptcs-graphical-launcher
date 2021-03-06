﻿using ScriptCs.Launcher.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ExecutionHost Host { get; set; }
        public bool LogOutput { get; set; }

        public string Icon { get; set; }
        public string Shortcut { get; set; }
        public string Color { get; set; }

        // Runtime/viewmodel
        public bool Executing { get; set; } = false;
        public string Output { get; set; } = String.Empty;
        public Process Process { get; set; }
    }
}

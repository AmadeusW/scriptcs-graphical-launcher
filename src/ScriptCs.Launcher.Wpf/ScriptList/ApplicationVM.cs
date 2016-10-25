using ScriptCs.Launcher.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher.Wpf.ScriptList
{
    public class ApplicationVM : ViewModelBase
    {
        public IEnumerable<ScriptInfo> Scripts { get; set; }

        public ApplicationVM()
        {
            Scripts = new List<ScriptInfo>();
        }
    }
}

using ScriptCs.Launcher.Wpf.ScriptList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher.Wpf.Serialization
{
    public class SavedApplicationVM
    {
        public IEnumerable<SavedScriptInfo> Scripts { get; set; }

        public static SavedApplicationVM FromVM(ApplicationVM vm)
        {
            return new SavedApplicationVM
            {
                Scripts = vm.Scripts.Select(n => SavedScriptInfo.FromScriptInfo(n))
            };
        }

        public static ApplicationVM MakeVM(SavedApplicationVM savedVM)
        {
            return new ApplicationVM
            {
                Scripts = savedVM.Scripts.Select(n => SavedScriptInfo.MakeScriptInfo(n))
            };
        }
    }
}

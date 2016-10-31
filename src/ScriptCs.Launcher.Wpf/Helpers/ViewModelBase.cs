using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher.Wpf.Helpers
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void UpdateProperty(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException(nameof(propertyName));
            NotifyPropertyChanged(propertyName);
        }

        #endregion
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ManagerUsersGroups.WpfUI.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}

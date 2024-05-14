using System.Windows.Input;

namespace ManagerUsersGroups.WpfUI.ViewModels.Interfaces
{
    public interface IApplicationViewModel
    {
        IMainViewModel MainViewModel { get; }

        IConfigViewModel ConfigViewModel { get; }

        public ICommand FindCommand { get; }
        public ICommand OpenSettingCommand { get; }
        public ICommand ApplySettingCommand { get; }
        public ICommand SaveSettingCommand { get; }
    }
}

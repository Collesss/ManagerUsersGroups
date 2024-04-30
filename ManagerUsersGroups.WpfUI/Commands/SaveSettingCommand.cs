using ManagerUsersGroups.WpfUI.Command;
using System.Windows.Data;

namespace ManagerUsersGroups.WpfUI.Commands
{
    public class SaveSettingCommand : BaseCommand
    {
        public override void Execute(object parameter) =>
            ((BindingGroup)parameter).UpdateSources();
    }
}

using ManagerUsersGroups.WpfUI.Command;
using System.Linq;
using System.Windows.Data;

namespace ManagerUsersGroups.WpfUI.Commands
{
    public class SaveSettingCommand : BaseCommand
    {
        public override void Execute(object parameter) =>
            ((BindingExpression[])parameter)
            .ToList()
            .ForEach(binding => binding.UpdateSource());
    }
}

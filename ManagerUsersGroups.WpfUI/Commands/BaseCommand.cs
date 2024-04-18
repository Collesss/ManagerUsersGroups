using System;
using System.Windows.Input;

namespace ManagerUsersGroups.WpfUI.Command
{
    public abstract class BaseCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) =>
            true;

        public abstract void Execute(object parameter);
    }
}

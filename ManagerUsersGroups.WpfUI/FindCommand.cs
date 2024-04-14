using System;
using System.Windows.Input;

namespace ManagerUsersGroups.WpfUI
{
    public class FindCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _action;

        public FindCommand(Action action) 
        {
            _action = action ?? (() => { });
        }

        public bool CanExecute(object parameter) =>
            true;

        public void Execute(object parameter) =>
            _action();
    }
}

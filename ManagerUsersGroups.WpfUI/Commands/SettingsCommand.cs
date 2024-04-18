namespace ManagerUsersGroups.WpfUI.Command
{
    public class SettingsCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            new SettingsWindow().ShowDialog();
        }
    }
}

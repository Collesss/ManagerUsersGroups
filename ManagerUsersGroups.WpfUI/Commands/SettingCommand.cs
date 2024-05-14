namespace ManagerUsersGroups.WpfUI.Command
{
    public class OpenSettingCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            new SettingsWindow().ShowDialog();
        }
    }
}

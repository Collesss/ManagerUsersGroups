namespace ManagerUsersGroups.WpfUI.ViewModels.Interfaces
{
    public interface IMainViewModel
    {
        string EmailLoginsFIO { get; set; }

        string Logins { get; set; }

        string FIOs { get; set; }

        string Emails { get; set; }

        string EmailsFormattedForOutlook { get; set; }

        string EmailsFormattedForHelp { get; set; }

        string EmailsDatabase { get; set; }
    }
}

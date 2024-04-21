using ManagerUsersGroups.WpfUI.ViewModel;
using System;
using System.DirectoryServices;
using System.Globalization;
using System.Windows.Data;

namespace ManagerUsersGroups.WpfUI.Converters
{
    public class EnumFlagsConverter : IValueConverter
    {
        public ApplicationUsersModelView ViewModel { get; init; }

        public EnumFlagsConverter(/*ConfigModelView viewModel*/) 
        {
            //_viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AuthenticationTypes authenticationType = (AuthenticationTypes)value;
            AuthenticationTypes authenticationTypeParam = (AuthenticationTypes)parameter;

            return (authenticationType & authenticationTypeParam) == authenticationTypeParam;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AuthenticationTypes authenticationTypeParam = (AuthenticationTypes)parameter;
            bool isChecked = (bool)value;

            return isChecked ? (ViewModel.ConfigViewModel.AuthenticationType | authenticationTypeParam) : (ViewModel.ConfigViewModel.AuthenticationType & (~authenticationTypeParam));
        }
    }
}

using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManagerUsersGroups.WpfUI
{
    public class ApplicationUsersModelView : INotifyPropertyChanged, ICommand
    {
        private string _emailLoginsFIO = string.Empty;
        private string _logins = string.Empty;
        private string _FIOs = string.Empty;
        private string _emails = string.Empty;
        private string _emailsFormattedForOutlook = string.Empty;
        private string _emailsFormattedForHelp = string.Empty;

        public string EmailLoginsFIO
        {
            get => _emailLoginsFIO;
            set
            {
                _emailLoginsFIO = value;
                Logins = FIOs = Emails = value;
                OnPropertyChanged();
            }
        }

        public string Logins
        {
            get => _logins;
            set
            {
                _logins = value;
                OnPropertyChanged();
            }
        }

        public string FIOs
        {
            get => _FIOs;
            set
            {
                _FIOs = value;
                OnPropertyChanged();
            }
        }

        public string Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                OnPropertyChanged();
            }
        }

        public string EmailsFormattedForOutlook
        {
            get => _emailsFormattedForOutlook;
            set
            {
                _emailsFormattedForOutlook = value;
                OnPropertyChanged();
            }
        }

        public string EmailsFormattedForHelp
        {
            get => _emailsFormattedForHelp;
            set
            {
                _emailsFormattedForHelp = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged(this, new PropertyChangedEventArgs(prop));


        private readonly IServiceProvider _serviceProvider;

        public ApplicationUsersModelView(/*IServiceProvider serviceCollection*/)
        {
            //_serviceCollection = serviceCollection;
        }

        
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) =>
            true;


        private enum EnumFindResult
        {
            Find,
            NotFind,
            ManyFind
        }

        private record FindResult(EnumFindResult EnumFindResult, UserEntity UserEntity = null);

        public void Execute(object parameter)
        {
            using var scope = _serviceProvider.CreateScope();

            IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            string[] findStrings = Regex.Matches(EmailLoginsFIO, @"[^\r\n]+")
                .Select(match => match.Value)
                .ToArray();

            FindResult[] users = Task.WhenAll(findStrings.Select(async findStr =>
            {
                IEnumerable<UserEntity> users = await userRepository.Find(findStr);

                return users.Count() switch
                {
                    0 => new FindResult(EnumFindResult.NotFind),
                    1 => new FindResult(EnumFindResult.Find, users.Single()),
                    _ => new FindResult(EnumFindResult.ManyFind),
                };
            })).Result;


            EmailLoginsFIO = string.Join('\n', findStrings);
            

        }
    }
}

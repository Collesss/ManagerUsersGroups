using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Interfaces;
using ManagerUsersGroups.WpfUI.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManagerUsersGroups.WpfUI.Command
{
    public class FindCommand : BaseCommand
    {
        private readonly IMainViewModel _mainViewModel;
        private readonly IServiceProvider _serviceProvider;

        public FindCommand(IMainViewModel mainViewModel, IServiceProvider serviceProvider)
        {
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /*
        private enum EnumFindResult
        {
            Find,
            NotFind,
            ManyFind
        }
        */
        //private record FindResult(EnumFindResult EnumFindResult, UserEntity UserEntity = null);


        public override void Execute(object parameter)
        {
            using var scope = _serviceProvider.CreateScope();

            IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            
            string[] findStrings = Regex.Matches(_mainViewModel.EmailLoginsFIO, @"[^\r\n]+")
                .Select(match => match.Value)
                .ToArray();


            UserEntity NotFind = new UserEntity 
            {
                SID = "Not find.",
                CommonName = "Not find.",
                DisplayName = "Not find.",
                DistinguishedName = "Not find.",
                Email = "Not find.",
                Login = "Not find.",
                HomeMDB = "Not find."
            };

            UserEntity FindMoreOne = new UserEntity
            {
                SID = "Find more one.",
                CommonName = "Find more one.",
                DisplayName = "Find more one.",
                DistinguishedName = "Find more one.",
                Email = "Find more one.",
                Login = "Find more one.",
                HomeMDB = "Find more one."
            };

            UserEntity[] users = Task.WhenAll(findStrings.Select(async findStr =>
            {
                IEnumerable<UserEntity> users = await userRepository.Find(findStr);

                return users.Count() switch
                {
                    0 => NotFind,
                    1 => users.Single(),
                    _ => FindMoreOne,
                };
            })).Result;


            _mainViewModel.EmailLoginsFIO = string.Join('\n', findStrings);
            _mainViewModel.Logins = string.Join('\n', users.Select(user => user.Login));
            _mainViewModel.Emails = string.Join('\n', users.Select(user => user.Email));
            _mainViewModel.FIOs = string.Join('\n', users.Select(user => user.DisplayName));
            _mainViewModel.EmailsFormattedForOutlook = string.Join("; ", users.Select(user => user.Email));
            _mainViewModel.EmailsFormattedForHelp = string.Join(",", users.Select(user => user.Email));
            _mainViewModel.EmailsDatabase = string.Join('\n', users.Select(user => Regex.Match(user.HomeMDB, "(?<=^CN=)[^,]+").Value));

            /*
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

            string GetVal(FindResult findResult, string value) =>
                findResult.EnumFindResult switch 
                {
                    EnumFindResult.NotFind => "Not find.",
                    EnumFindResult.ManyFind => "Find more one.",
                    _ => value
                };

            _mainViewModel.EmailLoginsFIO = string.Join('\n', findStrings);
            _mainViewModel.Logins = string.Join('\n', users.Select(userFR => GetVal(userFR, userFR?.UserEntity?.Login)));
            _mainViewModel.Emails = string.Join('\n', users.Select(userFR => GetVal(userFR, userFR?.UserEntity?.Emails)));
            _mainViewModel.FIOs = string.Join('\n', users.Select(userFR => GetVal(userFR, userFR?.UserEntity?.DisplayName)));
            */
        }
    }
}

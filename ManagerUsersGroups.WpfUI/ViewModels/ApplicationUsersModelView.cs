using ManagerUsersGroups.Repository.AD.AutoMapperProfiles;
using ManagerUsersGroups.Repository.AD.Implementations;
using ManagerUsersGroups.Repository.AD.Options;
using ManagerUsersGroups.Repository.Interfaces;
using ManagerUsersGroups.WpfUI.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Input;

namespace ManagerUsersGroups.WpfUI.ViewModel
{
    public class ApplicationUsersModelView
    {
        public MainViewModel MainViewModel { get; } = new MainViewModel();
        public ConfigModelView ConfigViewModel { get; } = new ConfigModelView();


        public ICommand FindCommand { get; }
        public ICommand OpenSettingCommand { get; }


        private readonly IServiceProvider _serviceProvider;

        
        public ApplicationUsersModelView()
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

            hostBuilder.ConfigureServices(services => 
            {
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());
                services.AddOptions<ADOptions>().Configure(opts => 
                {
                    opts.UserName = ConfigViewModel.UserName;
                    opts.Path = ConfigViewModel.Path;
                    opts.Password = ConfigViewModel.Password;
                    opts.AuthenticationTypes = ConfigViewModel.AuthenticationType;
                    opts.LoginType = ConfigViewModel.LoginType;
                });
            });

            _serviceProvider = hostBuilder.Build().Services;

            FindCommand = new FindCommand(MainViewModel, _serviceProvider);
            OpenSettingCommand = new SettingsCommand();
        }
    }
}

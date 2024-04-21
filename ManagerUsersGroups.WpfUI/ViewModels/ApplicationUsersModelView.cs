using ManagerUsersGroups.Repository.AD.AutoMapperProfiles;
using ManagerUsersGroups.Repository.AD.Implementations;
using ManagerUsersGroups.Repository.AD.Options;
using ManagerUsersGroups.Repository.Interfaces;
using ManagerUsersGroups.WpfUI.Command;
using ManagerUsersGroups.WpfUI.Converters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Input;

namespace ManagerUsersGroups.WpfUI.ViewModel
{
    public class ApplicationUsersModelView
    {
        private readonly IServiceProvider _serviceProvider;


        public MainViewModel MainViewModel { get; }
        public ConfigModelView ConfigViewModel { get; }


        public ICommand FindCommand { get; }
        public ICommand OpenSettingCommand { get; }
        

        public ApplicationUsersModelView()
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

            hostBuilder.ConfigureServices(services => 
            {
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<ConfigModelView>();
                services.AddSingleton<FindCommand>();
                services.AddSingleton<SettingsCommand>();

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

            MainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            ConfigViewModel = _serviceProvider.GetRequiredService<ConfigModelView>();

            FindCommand = _serviceProvider.GetRequiredService<FindCommand>();
            OpenSettingCommand = _serviceProvider.GetRequiredService<SettingsCommand>();
        }
    }
}

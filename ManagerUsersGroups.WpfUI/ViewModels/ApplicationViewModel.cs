﻿using RADAMP = ManagerUsersGroups.Repository.AD.AutoMapperProfiles;
using WUAMP = ManagerUsersGroups.WpfUI.AutoMapperProfiles;
using ManagerUsersGroups.Repository.AD.Implementations;
using ManagerUsersGroups.Repository.AD.Options;
using ManagerUsersGroups.Repository.Interfaces;
using ManagerUsersGroups.WpfUI.Command;
using ManagerUsersGroups.WpfUI.Commands;
using ManagerUsersGroups.WpfUI.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Input;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ManagerUsersGroups.WpfUI.Options;

namespace ManagerUsersGroups.WpfUI.ViewModels
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        private readonly IServiceProvider _serviceProvider;


        public IMainViewModel MainViewModel { get; }
        public IConfigViewModel ConfigViewModel { get; }


        public ICommand FindCommand { get; }
        public ICommand OpenSettingCommand { get; }
        public ICommand ApplySettingCommand { get; }
        public ICommand SaveSettingCommand { get; }


        public ApplicationViewModel()
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();


            hostBuilder.ConfigureServices(services =>
            {
                var conf = new ConfigurationBuilder().AddJsonFile("configurations.json").Build();

                services.AddSingleton<IConfiguration>(conf);

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddAutoMapper(cfg =>
                {
                    cfg.AddProfile<RADAMP.AutoMapperProfile>();
                    cfg.AddProfile<WUAMP.AutoMapperProfile>();
                });
                services.AddSingleton<IMainViewModel, MainViewModel>();
                services.AddSingleton<IConfigViewModel, ConfigViewModel>();

                services.AddKeyedSingleton<ICommand, FindCommand>("FindCommand");
                services.AddKeyedSingleton<ICommand, OpenSettingCommand>("OpenSettingCommand");
                services.AddKeyedSingleton<ICommand, ApplySettingCommand>("ApplySettingCommand");
                services.AddKeyedSingleton<ICommand, SaveSettingCommand>("SaveSettingCommand");


                services.Configure<SettingOptions>(conf);
                services.AddOptions<ADOptions>().Configure<IMapper, IConfigViewModel>((opts, mapper, configViewModel) => mapper.Map(configViewModel, opts));
            });

            _serviceProvider = hostBuilder.Build().Services;

            MainViewModel = _serviceProvider.GetRequiredService<IMainViewModel>();
            ConfigViewModel = _serviceProvider.GetRequiredService<IConfigViewModel>();

            FindCommand = _serviceProvider.GetRequiredKeyedService<ICommand>("FindCommand");
            OpenSettingCommand = _serviceProvider.GetRequiredKeyedService<ICommand>("OpenSettingCommand");
            ApplySettingCommand = _serviceProvider.GetRequiredKeyedService<ICommand>("ApplySettingCommand");
            SaveSettingCommand = _serviceProvider.GetRequiredKeyedService<ICommand>("SaveSettingCommand");
        }
    }
}

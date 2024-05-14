using System;
using System.IO;
using System.Text.Json;
using AutoMapper;
using ManagerUsersGroups.WpfUI.Command;
using ManagerUsersGroups.WpfUI.Options;
using ManagerUsersGroups.WpfUI.ViewModels.Interfaces;

namespace ManagerUsersGroups.WpfUI.Commands
{
    public class SaveSettingCommand : BaseCommand
    {
        //private readonly IOptions<SaveSettingOptions> _options;
        private readonly IConfigViewModel _configViewModel;
        private readonly IMapper _mapper;

        public SaveSettingCommand(IConfigViewModel configViewModel, IMapper mapper)
        {
            //_options = options ?? throw new ArgumentNullException(nameof(options));
            _configViewModel = configViewModel ?? throw new ArgumentNullException(nameof(configViewModel));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override void Execute(object parameter) =>
            File.WriteAllText("configurations.json", JsonSerializer.Serialize(_mapper.Map<SettingOptions>(_configViewModel), new JsonSerializerOptions { WriteIndented = true }));
            
    }
}

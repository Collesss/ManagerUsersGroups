using AutoMapper;
using ManagerUsersGroups.Repository.AD.Options;
using ManagerUsersGroups.WpfUI.ViewModels;
using ManagerUsersGroups.WpfUI.ViewModels.Interfaces;
using System.DirectoryServices;

namespace ManagerUsersGroups.WpfUI.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ConfigLoginType, LoginType>();
            CreateMap<LoginType, ConfigLoginType>();

            CreateMap<IConfigViewModel, ADOptions>()
                .ForMember(ado => ado.AuthenticationTypes, opts => opts.MapFrom(new ValueResolverBoolsToAuthenticationType()));

            CreateMap<ADOptions, IConfigViewModel>()
                .ForMember(icvm => icvm.AuthenticationTypesSecure, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.Secure)))
                .ForMember(icvm => icvm.AuthenticationTypesEncryptionSecureSocketsLayer, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.Encryption)))
                .ForMember(icvm => icvm.AuthenticationTypesReadonlyServer, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.ReadonlyServer)))
                .ForMember(icvm => icvm.AuthenticationTypesAnonymous, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.Anonymous)))
                .ForMember(icvm => icvm.AuthenticationTypesFastBind, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.FastBind)))
                .ForMember(icvm => icvm.AuthenticationTypesSigning, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.Signing)))
                .ForMember(icvm => icvm.AuthenticationTypesSealing, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.Sealing)))
                .ForMember(icvm => icvm.AuthenticationTypesDelegation, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.Delegation)))
                .ForMember(icvm => icvm.AuthenticationTypesServerBind, opts => opts.MapFrom(new ValueResolverAuthenticationTypeToBool(AuthenticationTypes.ServerBind)));
        }

        private sealed class ValueResolverBoolsToAuthenticationType : IValueResolver<IConfigViewModel, ADOptions, AuthenticationTypes>
        {
            public AuthenticationTypes Resolve(IConfigViewModel source, ADOptions destination, AuthenticationTypes destMember, ResolutionContext context) =>
                (source.AuthenticationTypesSecure ? AuthenticationTypes.Secure : 0) |
                (source.AuthenticationTypesEncryptionSecureSocketsLayer ? AuthenticationTypes.Encryption : 0) |
                (source.AuthenticationTypesReadonlyServer ? AuthenticationTypes.ReadonlyServer : 0) |
                (source.AuthenticationTypesAnonymous ? AuthenticationTypes.Anonymous : 0) |
                (source.AuthenticationTypesFastBind ? AuthenticationTypes.FastBind : 0) |
                (source.AuthenticationTypesSigning ? AuthenticationTypes.Signing : 0) |
                (source.AuthenticationTypesSealing ? AuthenticationTypes.Sealing : 0) |
                (source.AuthenticationTypesDelegation ? AuthenticationTypes.Delegation : 0) |
                (source.AuthenticationTypesServerBind ? AuthenticationTypes.ServerBind : 0);
        }

        private sealed class ValueResolverAuthenticationTypeToBool : IValueResolver<ADOptions, IConfigViewModel, bool>
        {
            private readonly AuthenticationTypes _authenticationType;

            public ValueResolverAuthenticationTypeToBool(AuthenticationTypes authenticationType)
            {
                _authenticationType = authenticationType;
            }

            public bool Resolve(ADOptions source, IConfigViewModel destination, bool destMember, ResolutionContext context) =>
                (source.AuthenticationTypes & _authenticationType) == _authenticationType;
        }
    }
}

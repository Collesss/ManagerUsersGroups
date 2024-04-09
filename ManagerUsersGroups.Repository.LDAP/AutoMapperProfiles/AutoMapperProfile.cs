using AutoMapper;
using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.LDAP.Extensions;
using System.DirectoryServices.Protocols;

namespace ManagerUsersGroups.Repository.LDAP.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<SearchResultEntry, BaseEntity>()
                .ForMember(be => be.SID, opts => opts.MapFrom(sr => sr.GetSid()))
                .ForMember(be => be.DistinguishedName, opts => opts.MapFrom(sr => sr.GetProp("distinguishedName")))
                .ForMember(be => be.CanonicalName, opts => opts.MapFrom(sr => sr.GetProp("canonicalName")))
                .ForMember(be => be.CommonName, opts => opts.MapFrom(sr => sr.GetProp("cn")));

            CreateMap<SearchResultEntry, UserEntity>()
                .IncludeBase<SearchResultEntry, BaseEntity>()
                .ForMember(ue => ue.Login, opts => opts.MapFrom(sr => sr.GetProp("sAMAccountName")))
                .ForMember(ue => ue.Email, opts => opts.MapFrom(sr => sr.GetProp("mail")))
                .ForMember(ue => ue.DisplayName, opts => opts.MapFrom(sr => sr.GetProp("displayName")));

            CreateMap<SearchResultEntry, GroupEntity>()
                .IncludeBase<SearchResultEntry, BaseEntity>();
            
        }
    }
}

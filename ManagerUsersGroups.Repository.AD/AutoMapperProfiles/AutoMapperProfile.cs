using AutoMapper;
using ManagerUsersGroups.Repository.AD.Extensions;
using ManagerUsersGroups.Repository.Entities;
using System.DirectoryServices;

namespace ManagerUsersGroups.Repository.AD.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<SearchResult, BaseEntity>()
                .ForMember(be => be.SID, opts => opts.MapFrom(sr => sr.GetSid()))
                .ForMember(be => be.DistinguishedName, opts => opts.MapFrom(sr => sr.GetProp("distinguishedName")))
                //.ForMember(be => be.CanonicalName, opts => opts.MapFrom(sr => sr.GetProp("canonicalName")))
                .ForMember(be => be.CommonName, opts => opts.MapFrom(sr => sr.GetProp("cn")));

            CreateMap<SearchResult, UserEntity>()
                .IncludeBase<SearchResult, BaseEntity>()
                .ForMember(ue => ue.Login, opts => opts.MapFrom(sr => sr.GetProp("sAMAccountName")))
                .ForMember(ue => ue.Email, opts => opts.MapFrom(sr => sr.GetProp("mail")))
                .ForMember(ue => ue.DisplayName, opts => opts.MapFrom(sr => sr.GetProp("displayName")));

            CreateMap<SearchResult, GroupEntity>()
                .IncludeBase<SearchResult, BaseEntity>();
        }
    }
}

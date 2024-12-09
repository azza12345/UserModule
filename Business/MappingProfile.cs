using AutoMapper;
using Core.ViewModels;
using Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Action = Data.Action;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
             CreateMap<RegisterViewModel, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Fname, opt => opt.MapFrom(src => src.Fname))
            .ForMember(dest => dest.Lname, opt => opt.MapFrom(src => src.Lname))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile));

            CreateMap<LoginViewModel, User>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

           CreateMap<Group, GroupViewModel>().ReverseMap();

        CreateMap<ActionViewModel, Action>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.SystemId, opt => opt.MapFrom(src => src.SystemId))
               .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<GroupPermissionViewModel, GroupPermission>();
        CreateMap<ViewViewModel, View>();


    }
}

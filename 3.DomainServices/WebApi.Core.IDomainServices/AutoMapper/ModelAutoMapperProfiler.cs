using AutoMapper;
using WebApi.Core.EntityModels.Core;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.EntityModels.Queues;
using WebApi.Core.Utility;
using WebApi.Core.ViewModels;
using WebApi.Core.ViewModels.Core;
using WebApi.Core.ViewModels.Identity.WebApi;

namespace WebApi.Core.IDomainServices.AutoMapper
{
    public class ModelAutoMapperProfiler : Profile
    {
        public ModelAutoMapperProfiler()
        {
           CreateMap<BaseEntity, BaseViewModel>().ReverseMap();
           CreateMap<AuditableEntity, AuditableViewModel>().ReverseMap();
           CreateMap<IdentityColumnEntity, IdentityColumnViewModel>().ReverseMap();

           CreateMap<User, IdentityUserViewModel>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

           CreateMap<IdentityUserViewModel, User>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));


           CreateMap<Role, IdentityRoleViewModel>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
           CreateMap<IdentityRoleViewModel, Role>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

           CreateMap<EmailQueue, EmailQueueViewModel>().ReverseMap();
           CreateMap<RequestQueue, RequestQueueViewModel>().ReverseMap();
           CreateMap<PdfQueue, PdfQueueViewModel>().ReverseMap();


           CreateMap<Client, ClientViewModel>()
                .ForMember(dest => dest.ApplicationType, opt => opt.ResolveUsing<ApplicationTypeEnumResolver, string>(src => src.ApplicationType));

           CreateMap<ClientViewModel, Client>()
                            .ForMember(dest => dest.ApplicationType, opt => opt.ResolveUsing<ApplicationTypeIntResolver, ApplicationTypes>(src => src.ApplicationType));

           CreateMap<RefreshToken, RefreshTokenViewModel>().ReverseMap();

           CreateMap<ExternalLogin, ExternalLoginViewModel>().ReverseMap();

           CreateMap<ExternalLogin, UserLoginInfoViewModel>().ReverseMap();


        }
    }
}

using AutoMapper;
using Library.Dtos.Publisher;
using Library.Models;

namespace Library.Profiles;

public class PublishersProfile : Profile
{
    public PublishersProfile()
    {
        CreateMap<Publisher, PublisherReadDto>()
            .ForMember(p => p.Self, opt => opt.Ignore())
            .ForMember(p => p.Books, opt => opt.Ignore());
        CreateMap<PublisherCreateDto, Publisher>()
            .ForMember(p => p.Created, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(p => p.Modified, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(p => p.Id, opt => opt.MapFrom(x => new Guid()))
            .ForMember(p => p.Books, opt => opt.Ignore());
    }
}
using AutoMapper;
using Library.Dtos.Tag;
using Library.Models;

namespace Library.Profiles;

public class TagsProfile : Profile
{
    public TagsProfile()
    {
        CreateMap<Tag, TagReadDto>()
            .ForMember(t => t.Self, opt => opt.Ignore())
            .ForMember(t => t.Books, opt => opt.Ignore());
        CreateMap<TagCreateDto, Tag>()
            .ForMember(b => b.Created, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(b => b.Modified, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(b => b.Id, opt => opt.MapFrom(x => new Guid()))
            .ForMember(b => b.Books, opt => opt.Ignore());
    }
}
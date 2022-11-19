using AutoMapper;
using Library.Dtos.Author;
using Library.Models;

namespace Library.Profiles;

public class AuthorsProfile : Profile
{
    public AuthorsProfile()
    {
        CreateMap<Author, AuthorReadDto>()
            .ForMember(a => a.Self, opt => opt.Ignore())
            .ForMember(a => a.Books, opt => opt.Ignore());
        CreateMap<AuthorCreateDto, Author>()
            .ForMember(a => a.Created, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(a => a.Modified, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(a => a.Id, opt => opt.MapFrom(x => new Guid()))
            .ForMember(a => a.Books, opt => opt.Ignore());
    }
}

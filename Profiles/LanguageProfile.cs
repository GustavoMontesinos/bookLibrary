using AutoMapper;
using Library.Dtos.Language;
using Library.Models;

namespace Library.Profiles;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<Language, LanguageReadDto>()
            .ForMember(l => l.Self, opt => opt.Ignore())
            .ForMember(l => l.Books, opt => opt.Ignore());
    }
}
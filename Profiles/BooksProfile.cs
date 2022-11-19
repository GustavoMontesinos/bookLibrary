using AutoMapper;
using Library.Dtos.Book;
using Library.MappingResolvers;
using Library.Models;

namespace Library.Profiles;

public class BooksProfile : Profile
{
    public BooksProfile()
    {
        CreateMap<Book, BookReadDto>()
            .ForMember(b => b.Self, opt => opt.Ignore())
            .ForMember(b => b.Authors, opt => opt.Ignore())
            .ForMember(b => b.Publisher, opt => opt.Ignore())
            .ForMember(b => b.Tags, opt => opt.Ignore())
            .ForMember(b => b.Sections, opt => opt.Ignore());
        CreateMap<BookCreateDto, Book>()
            .ForMember(b => b.Created, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(b => b.Modified, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(b => b.Id, opt => opt.MapFrom(x => new Guid()))
            .ForMember(b => b.Authors, opt => opt.MapFrom<BookAuthorResolver>())
            .ForMember(b => b.Publisher, opt => opt.MapFrom<BookPublisherResolver>())
            .ForMember(b => b.Tags, opt => opt.MapFrom<BookTagResolver>())
            .ForMember(b => b.Sections, opt => opt.MapFrom<BookContentResolver>());
    }
}
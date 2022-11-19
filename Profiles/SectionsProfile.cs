using AutoMapper;
using Library.Dtos.Section;
using Library.MappingResolvers;
using Library.Models;

namespace Library.Profiles;

public class SectionsProfile : Profile
{
    public SectionsProfile()
    {
        CreateMap<Section, SectionReadDto>()
            .ForMember(s => s.Self, opt => opt.Ignore())
            .ForMember(s => s.HierarchyLevel, opt => opt.MapFrom(x => x.HierarchyLevel!.ToString()))
            .ForMember(s => s.Book, opt => opt.Ignore());
        CreateMap<SectionCreateDto, Section>()
            .ForMember(b => b.Created, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(b => b.Modified, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(b => b.Id, opt => opt.MapFrom(x => new Guid()))
            .ForMember(s => s.ParentId, opt => opt.MapFrom<SectionParentResolvers>())
            .ForMember(s => s.BookId, opt => opt.MapFrom<SectionBookResolvers>())
            .ForMember(s => s.HierarchyLevel, opt => opt.MapFrom<SectionHierarchyResolvers>());
      }
}
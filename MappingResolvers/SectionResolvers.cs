using AutoMapper;
using Library.Dtos.Section;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.MappingResolvers;

public class SectionHierarchyResolvers : IValueResolver<SectionCreateDto, Section, HierarchyId?>
{
    public HierarchyId Resolve(
        SectionCreateDto sectionCreateDto,
        Section section,
        HierarchyId? id,
        ResolutionContext context)
    {
        var parentId = sectionCreateDto.HierarchyLevel;
        return HierarchyId.Parse(parentId);
    }
}

public class SectionParentResolvers : IValueResolver<SectionCreateDto, Section, Guid?>
{
    public Guid? Resolve(
        SectionCreateDto sectionCreateDto,
        Section section,
        Guid? id,
        ResolutionContext context)
    {
        var parentId = sectionCreateDto.ParentId;
        return parentId == string.Empty ? null : Guid.Parse(parentId!);
    }
}

public class SectionBookResolvers : IValueResolver<SectionCreateDto, Section, Guid?>
{
    public Guid? Resolve(
        SectionCreateDto sectionCreateDto,
        Section section,
        Guid? id,
        ResolutionContext context)
    {
        var bookId = sectionCreateDto.BookId;
        return bookId == string.Empty ? null : Guid.Parse(bookId!);
    }
}


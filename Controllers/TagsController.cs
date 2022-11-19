using AutoMapper;
using Library.Data;
using Library.Dtos.Book;
using Library.Dtos.Tag;
using Library.Links;
using Library.Models;
using Library.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly IGenericRepo _repo;
    private readonly ILinksGenerator _linksGenerator;
    private readonly IMapper _mapper;

    public TagsController(
        IGenericRepo repo,
        ILinksGenerator linksGenerator,
        IMapper mapper)
    {
        _repo = repo;
        _linksGenerator = linksGenerator;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetAllTags")]
    public ActionResult<IEnumerable<Tag>> GetAllTags(
        [FromQuery] TagParams tagParams,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        return Ok(_linksGenerator
            .Mapping<Tag, TagReadDto>(
                modelList: _repo.GetAll<Tag, TagParams>(tagParams).ToList(),
                context: HttpContext));
    }

    [HttpGet(
        template: "{id:guid}",
        Name = "GetTagById")]
    public ActionResult<Tag> GetTagById(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        return Ok(_linksGenerator
            .Mapping<Tag, TagReadDto>(
                modelList: new List<Tag> { _repo.GetById<Tag>(id) },
                context: HttpContext)[0]);
    }

    [HttpGet(
        template: "{id:guid}/books",
        Name = "GetBooksOfTag")]
    public ActionResult<IEnumerable<Book>> GetBooksOfTag(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        return Ok(_linksGenerator
            .Mapping<Book, BookReadDto>(
                modelList: _repo.GetPropertyItemOfModel<Book, Tag>(id).ToList(),
                context: HttpContext));
    }

    [HttpPost(Name = "CreateTag")]
    public ActionResult<TagReadDto> CreateTag(
        [FromBody] TagCreateDto tagDto,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var tag = _mapper.Map<Tag>(tagDto);
        _repo.Create(tag);

        var tagWithLinks = _linksGenerator
            .Mapping<Tag, TagReadDto>(
                modelList: new List<Tag> { tag },
                context: HttpContext)[0];

        _repo.SaveChanges();

        return CreatedAtRoute(
            nameof(GetTagById),
            routeValues: new { id = tag.Id },
            value: tagWithLinks);
    }

    [HttpPut(template: "{id:guid}")]
    public ActionResult<Tag> UpdateTag(
        [FromRoute] Guid id,
        [FromBody] TagUpdateDto tag,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var tagExists = _repo.Exists<Tag>(id);
        if (tagExists == false) return NotFound();
        
        var tagUpdated = _repo.Update<Tag, TagUpdateDto>(tag, id);
        tagUpdated.Modified = DateTime.Now;
        _repo.SaveChanges();

        var response = _linksGenerator
            .Mapping<Tag, TagReadDto>(
                modelList: new List<Tag> { tagUpdated },
                context: HttpContext)[0];
        
        return Ok(response);
    }

    [HttpDelete(template: "{id:guid}")]
    public ActionResult<Tag> DeleteTag([FromRoute] Guid id)
    {
        var tagExists = _repo.Exists<Tag>(id);
        if (tagExists == false) return NotFound();
        _repo.Delete<Tag>(id);
        _repo.SaveChanges();
        return NoContent();
    }
}
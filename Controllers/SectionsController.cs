using AutoMapper;
using Library.Data;
using Library.Dtos.Section;
using Library.Links;
using Library.Models;
using Library.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class SectionsController : ControllerBase
{
    private readonly IGenericRepo _repo;
    private readonly ILinksGenerator _linksGenerator;
    private readonly IMapper _mapper;

    public SectionsController(

        IGenericRepo repo,
        ILinksGenerator linksGenerator,
        IMapper mapper)
    {
        _repo = repo;
        _linksGenerator = linksGenerator;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetAllSections")]
    public ActionResult<IEnumerable<Section>> GetAllSections(
        [FromQuery] SectionParams sectionParams,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        return Ok(_linksGenerator
            .Mapping<Section, SectionReadDto>(
                modelList: _repo.GetAll<Section, SectionParams>(sectionParams).ToList(),
                context: HttpContext));
    }

    [HttpGet(
        template: "{id:guid}",
        Name = "GetSectionById")]
    public ActionResult<Section> GetSectionById(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        return Ok(_linksGenerator
            .Mapping<Section, SectionReadDto>(
                modelList: new List<Section> { _repo.GetById<Section>(id) },
                context: HttpContext)[0]);
    }

    [HttpPost(Name = "CreateSection")]
    public ActionResult<SectionReadDto> CreateSection(
        [FromBody] SectionCreateDto sectionDto,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var section = _mapper.Map<Section>(sectionDto);
        _repo.Create(section);

        var sectionWithLinks = _linksGenerator
            .Mapping<Section, SectionReadDto>(
                modelList: new List<Section> { section },
                context: HttpContext)[0];

        _repo.SaveChanges();

        return CreatedAtRoute(
            routeName: nameof(GetSectionById),
            routeValues: new { id = section.Id },
            value: sectionWithLinks);
    }

    [HttpPut(
        template: "{id:guid}",
        Name = "UpdateSection")]
    public ActionResult<Section> UpdateSection(
        [FromRoute] Guid id,
        [FromBody] SectionUpdateDto section,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var sectionExists = _repo.Exists<Section>(id);
        if (sectionExists == false) return NotFound();

        var sectionUpdated = _repo.Update<Section, SectionUpdateDto>(section, id);
        sectionUpdated.Modified = DateTime.Now;
        _repo.SaveChanges();

        var response = _linksGenerator
            .Mapping<Section, SectionReadDto>(
                modelList: new List<Section> { sectionUpdated },
                context: HttpContext)[0];
        return Ok(response);
    }

    [HttpDelete(
        template: "{id:guid}",
        Name = "DeleteSection")]
    public IActionResult DeleteSection([FromRoute] Guid id)
    {
        var sectionExists = _repo.Exists<Section>(id);
        if (sectionExists == false) return NotFound();
        _repo.Delete<Section>(id);
        _repo.SaveChanges();
        return NoContent();
    }
}
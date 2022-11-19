using AutoMapper;
using Library.Data;
using Library.Dtos.Language;
using Library.Links;
using Library.Models;
using Library.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Route(template:"api/[controller]")]
[ApiController]
public class LanguagesController : ControllerBase
{
    private readonly IGenericRepo _repo;
    private readonly ILinksGenerator _linksGenerator;
    private readonly IMapper _mapper;

    public LanguagesController(
        IGenericRepo repo,
        ILinksGenerator linksGenerator,
        IMapper mapper)
    {
        _repo = repo;
        _linksGenerator = linksGenerator;
        _mapper = mapper;
    }
    
    [HttpGet(Name = "GetAllLanguages")]
    public ActionResult<IEnumerable<Language>> GetAllLanguages(
        [FromQuery] LanguageParams languageParams,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var languages = _repo
            .GetAll<Language, LanguageParams>(languageParams)
            .ToList();
        var response = _linksGenerator
            .Mapping<Language, LanguageReadDto>(languages, HttpContext);
        return Ok(response);
    }
    
    [HttpGet(
        template: "{id:guid}",
        Name = "GetLanguageById")]
    public ActionResult<Language> GetLanguageById(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var languages = new List<Language> { _repo.GetById<Language>(id) };
        var response = _linksGenerator
            .Mapping<Language, LanguageReadDto>(languages, HttpContext)[0];
        return Ok(response);
    }
}
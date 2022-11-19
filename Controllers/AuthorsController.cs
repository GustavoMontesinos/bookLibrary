using AutoMapper;
using Library.Data;
using Library.Dtos.Author;
using Library.Dtos.Book;
using Library.Links;
using Library.Models;
using Library.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IGenericRepo _repo;
    private readonly ILinksGenerator _linksGenerator;
    private readonly IMapper _mapper;

    public AuthorsController(
        IGenericRepo repo,
        ILinksGenerator linksGenerator,
        IMapper mapper)
    {
        _repo = repo;
        _linksGenerator = linksGenerator;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<Author> GetAllAuthors(
        [FromQuery] AuthorParams authorParams,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var authorList = _repo
            .GetAll<Author, AuthorParams>(authorParams)
            .ToList();
        var response = _linksGenerator
            .Mapping<Author, AuthorReadDto>(authorList, HttpContext);
        return Ok(response);
    }

    [HttpGet(
        template: "{id:guid}",
        Name = "GetAuthorById")]
    public ActionResult<Author> GetAuthorById(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var author = new List<Author> { _repo.GetById<Author>(id) };
        var response = _linksGenerator
            .Mapping<Author, AuthorReadDto>(author, HttpContext)[0];
        return Ok(response);
    }

    [HttpGet(template: "{id:guid}/books")]
    public ActionResult<Book> GetBooksOfAuthor(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var books = _repo
            .GetPropertyItemOfModel<Book, Author>(id)
            .ToList();
        var response = _linksGenerator
            .Mapping<Book, BookReadDto>(books, HttpContext);
        return Ok(response);
    }

    [HttpDelete(
        template: "{id:guid}",
        Name = "DeleteAuthor")]
    public ActionResult<Book> DeleteAuthor([FromRoute] Guid id)
    {
        var authorExists = _repo.Exists<Author>(id);
        if (authorExists == false) return NotFound();
        _repo.Delete<Author>(id);
        _repo.SaveChanges();
        return NoContent();
    }

    [HttpPost]
    public ActionResult<AuthorReadDto> CreateAuthor(
        [FromBody] AuthorCreateDto authorCreateDto,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var author = _mapper.Map<Author>(authorCreateDto);
        _repo.Create(author);

        var authorWithLinks = _linksGenerator
            .Mapping<Author, AuthorReadDto>(
                modelList: new List<Author> { author },
                context: HttpContext)[0];
        _repo.SaveChanges();

        return CreatedAtRoute(
            routeName: nameof(GetAuthorById),
            routeValues: new { id = author.Id },
            value: authorWithLinks);
    }

    [HttpPut(template: "{id:guid}")]
    public ActionResult<AuthorReadDto> UpdateAuthor(
        [FromRoute] Guid id,
        [FromBody] AuthorUpdateDto author,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var authorExists = _repo.Exists<Author>(id);
        if (authorExists == false) return NotFound();

        var authorUpdated = _repo.Update<Author, AuthorUpdateDto>(author, id);
        authorUpdated.Modified = DateTime.Now;
        _repo.SaveChanges();

        var authors = new List<Author> { authorUpdated };
        var response = _linksGenerator
            .Mapping<Author, AuthorReadDto>(authors, HttpContext)[0];
        return Ok(response);
    }
}
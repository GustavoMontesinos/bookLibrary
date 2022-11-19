using AutoMapper;
using Library.Data;
using Library.Dtos.Author;
using Library.Dtos.Book;
using Library.Dtos.Language;
using Library.Dtos.Section;
using Library.Dtos.Tag;
using Library.Links;
using Library.Models;
using Library.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;
[Route(template: "api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IGenericRepo _repo;
    private readonly ILinksGenerator _linksGenerator;
    private readonly IMapper _mapper;

    public BooksController(
        IGenericRepo repo,
        ILinksGenerator linksGenerator,
        IMapper mapper)
    {
        _repo = repo;
        _linksGenerator = linksGenerator;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetAllBooks")]
    public ActionResult<IEnumerable<Book>> GetAllBooks(
        [FromQuery] BookParams bookParams,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var books = _repo
            .GetAll<Book, BookParams>(bookParams)
            .ToList();
        var response = _linksGenerator
            .Mapping<Book, BookReadDto>(books, HttpContext);
        return Ok(response);
    }

    [HttpGet(
        template: "{id:guid}",
        Name = "GetBookById")]
    public ActionResult<Book> GetBookById(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var books = new List<Book> { _repo.GetById<Book>(id) };
        var response = _linksGenerator.Mapping<Book, BookReadDto>(books, HttpContext)[0];
        return Ok(response);
    }

    [HttpGet(
        template: "{id:guid}/authors",
        Name = "GetAuthorsOfBook")]
    public ActionResult<Author> GetAuthorsOfBook(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var bookExist = _repo.Exists<Book>(id);
        if (bookExist == false) return BadRequest();

        var authors = _repo
            .GetPropertyItemOfModel<Author, Book>(id)
            .ToList();
        var response = _linksGenerator.Mapping<Author, AuthorReadDto>(authors, HttpContext);
        return Ok(response);
    }

    [HttpGet(
        template: "{id:guid}/sections",
        Name = "GetSectionsOfBook")]
    public ActionResult<Author> GetSectionsOfBook(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var bookExist = _repo.Exists<Book>(id);
        if (bookExist == false) return BadRequest();

        var sections = _repo
            .GetPropertyItemOfModel<Section, Book>(id)
            .ToList();
        var response = _linksGenerator.Mapping<Section, SectionReadDto>(sections, HttpContext);
        return Ok(response);
    }

    [HttpGet(
        template: "{id:guid}/tags",
        Name = "GetTagsOfBook")]
    public ActionResult<Author> GetTagsOfBook(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var bookExist = _repo.Exists<Book>(id);
        if (bookExist == false) return NotFound();

        var tags = _repo
            .GetPropertyItemOfModel<Tag, Book>(id)
            .ToList();
        var response = _linksGenerator.Mapping<Tag, TagReadDto>(tags, HttpContext);
        return Ok(response);
    }

    [HttpPost(
        template: "{id:guid}/authors",
        Name = "CreateAuthorsOfBook")]
    public ActionResult<Author> CreateAuthorsOfBook(
        [FromRoute] Guid id,
        [FromBody] AuthorCreateDto authorCreateDto)
    {
        var bookExist = _repo.Exists<Book>(id);
        if (bookExist == false) return NotFound();

        var book = _repo.GetById<Book>(id);
        var author = _mapper.Map<Author>(authorCreateDto);
        author.Books = new List<Book> { book };
        _repo.Create(author);
        _repo.SaveChanges();
        var response = _linksGenerator.Mapping<Author, AuthorReadDto>(new List<Author> { author }, HttpContext)[0];
        return Ok(response);
    }

    [HttpDelete(
        template: "{bookId:guid}/authors/{authorId:guid}",
        Name = "DeleteAuthorOfBook")]
    public IActionResult DeleteAuthorOfBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid authorId)
    {
        var authorExist = _repo.Exists<Author>(authorId);
        var bookExist = _repo.Exists<Book>(bookId);
        if (authorExist == false || bookExist == false) return NotFound();

        _repo.DeletePropertyOfModel<Author, Book>(bookId, authorId);
        _repo.SaveChanges();
        return NoContent();
    }

    [HttpPost(
        template: "{bookId:guid}/authors/{authorId:guid}",
        Name = "AddAuthorToBook")]
    public ActionResult<Author> AddAuthorToBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid authorId)
    {
        var authorExist = _repo.Exists<Author>(authorId);
        var bookExist = _repo.Exists<Book>(bookId);
        if (authorExist == false || bookExist == false) return NotFound();

        var addedAuthors = _repo.AddPropertyItemToAModel<Author, Book>(bookId, authorId);
        if (addedAuthors == null) return Conflict();
        _repo.SaveChanges();
        return Ok(_linksGenerator.Mapping<Author, AuthorReadDto>(addedAuthors.ToList(), HttpContext));
    }

    // WARNING not tested for single property
    [HttpDelete(
        template: "{bookId:guid}/languages/{languageId:guid}",
        Name = "DeleteLanguageOfBook")]
    public IActionResult DeleteLanguageOfBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid languageId)
    {
        var languageExist = _repo.Exists<Language>(languageId);
        var bookExist = _repo.Exists<Book>(bookId);
        if (languageExist == false || bookExist == false) return NotFound();

        _repo.DeletePropertyOfModel<Language, Book>(bookId, languageId);
        _repo.SaveChanges();
        return NoContent();
    }

    // WARNING not tested for single property
    [HttpPost(
        template: "{bookId:guid}/languages/{languageId:guid}",
        Name = "AddLanguageToBook")]
    public ActionResult<Author> AddLanguageToBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid languageId)
    {
        var languageExist = _repo.Exists<Language>(languageId);
        var bookExist = _repo.Exists<Book>(bookId);
        if (languageExist == false || bookExist == false) return NotFound();

        var addedLanguages = _repo.AddPropertyItemToAModel<Language, Book>(bookId, languageId);
        if (addedLanguages == null) return Conflict();
        _repo.SaveChanges();
        return Ok(_linksGenerator.Mapping<Language, LanguageReadDto>(addedLanguages.ToList(), HttpContext));
    }


    [HttpPost(
        template: "{bookId:guid}/contents/{sectionId:guid}",
        Name = "AddContentsToBook")]
    public ActionResult<Section> AddContentsToBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid sectionId)
    {
        var sectionExist = _repo.Exists<Section>(sectionId);
        var bookExist = _repo.Exists<Book>(bookId);
        if (sectionExist == false || bookExist == false) return NotFound();

        var section = _repo.GetById<Section>(sectionId);
        section.BookId = bookId;

        var addedContents = _repo.AddPropertyItemToAModel<Section, Book>(bookId, sectionId);
        if (addedContents == null) return Conflict();
        _repo.SaveChanges();
        var response = _linksGenerator.Mapping<Section, SectionReadDto>(addedContents.ToList(), HttpContext);
        return Ok(response);
    }

    [HttpDelete(
        template: "{bookId:guid}/contents/{sectionId:guid}",
        Name = "DeleteSectionOfBook")]
    public IActionResult DeleteSectionOfBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid sectionId)
    {
        var sectionExist = _repo.Exists<Section>(sectionId);
        var bookExist = _repo.Exists<Book>(bookId);
        if (sectionExist == false || bookExist == false) return NotFound();

        _repo.DeletePropertyOfModel<Section, Book>(bookId, sectionId);
        _repo.SaveChanges();
        return NoContent();
    }

    [HttpPost(
        template: "{bookId:guid}/tags/{tagId:guid}",
        Name = "AddTagToBook")]
    public ActionResult<Tag> AddTagToBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid tagId)
    {
        var tagExists = _repo.Exists<Tag>(tagId);
        var bookExists = _repo.Exists<Book>(bookId);
        if (tagExists == false || bookExists == false) return NotFound();

        var addedTags = _repo.AddPropertyItemToAModel<Tag, Book>(bookId, tagId);
        if (addedTags == null) return Conflict();
        _repo.SaveChanges();
        return Ok(_linksGenerator.Mapping<Tag, TagReadDto>(addedTags.ToList(), HttpContext));
    }

    [HttpDelete(
        template: "{bookId:guid}/tag/{tagId:guid}",
        Name = "DeleteTagOfBook")]
    public IActionResult DeleteTagOfBook(
        [FromRoute] Guid bookId,
        [FromRoute] Guid tagId)
    {
        var authorExists = _repo.Exists<Tag>(tagId);
        var bookExists = _repo.Exists<Book>(bookId);
        if (authorExists == false || bookExists == false) return NotFound();

        _repo.DeletePropertyOfModel<Tag, Book>(bookId, tagId);
        _repo.SaveChanges();
        return NoContent();
    }

    [HttpPost]
    public ActionResult<BookReadDto> CreateBook([FromBody] BookCreateDto bookDto)
    {
        var authorIds = bookDto.AuthorsIds;
        var tagsIds = bookDto.TagsIds;

        var isAuthorIdsEmpty = authorIds!.Count == 0;
        var isTagsIdsEmpty = tagsIds!.Count == 0;

        var authorExists = isAuthorIdsEmpty || authorIds.Any(aId => _repo.Exists<Author>(Guid.Parse(aId)));
        var tagExists = isTagsIdsEmpty || tagsIds.Any(tId => _repo.Exists<Tag>(Guid.Parse(tId)));
        var publisherExists = _repo.Exists<Publisher>(Guid.Parse(bookDto.PublisherId!));

        if ( !authorExists || !tagExists || !publisherExists) return BadRequest();

        var book = _mapper.Map<Book>(bookDto);
        _repo.Create(book);
        var bookWithLinks = _linksGenerator
            .Mapping<Book, BookReadDto>(new List<Book> { book }, HttpContext)[0];
        _repo.SaveChanges();
        return CreatedAtRoute(
            routeName: nameof(GetBookById),
            routeValues: new { id = book.Id },
            value: bookWithLinks);
    }

    [HttpPut(template: "{id:guid}")]
    public ActionResult<BookReadDto> UpdateBook(
        [FromRoute] Guid id,
        [FromBody] BookUpdateDto book,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var bookExists = _repo.Exists<Book>(id);
        if (bookExists == false) return NotFound();

        var bookUpdated = _repo.Update<Book, BookUpdateDto>(book, id);
        bookUpdated.Modified = DateTime.Now;
        _repo.SaveChanges();
        var books = new List<Book> { bookUpdated };
        var response = _linksGenerator
            .Mapping<Book, BookReadDto>(books, HttpContext)[0];
        return Ok(response);
    }

    [HttpDelete(template: "{id:guid}")]
    public ActionResult<Book> DeleteBook([FromRoute] Guid id)
    {
        var bookExists = _repo.Exists<Book>(id);
        if (bookExists == false) return NotFound();
        _repo.Delete<Book>(id);
        _repo.SaveChanges();
        return NoContent();
    }
}
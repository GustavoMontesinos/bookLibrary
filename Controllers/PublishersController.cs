using AutoMapper;
using Library.Data;
using Library.Dtos.Book;
using Library.Dtos.Publisher;
using Library.Links;
using Library.Models;
using Library.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly IGenericRepo _repo;
    private readonly ILinksGenerator _linksGenerator;
    private readonly IMapper _mapper;

    public PublishersController(
        IGenericRepo repo,
        ILinksGenerator linksGenerator,
        IMapper mapper)
    {
        _repo = repo;
        _linksGenerator = linksGenerator;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<Publisher> GetPublishers(
        [FromQuery] PublisherParams publisherParams,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var publishers = _repo
            .GetAll<Publisher, PublisherParams>(publisherParams)
            .ToList();
        var response = _linksGenerator
            .Mapping<Publisher, PublisherReadDto>(publishers, HttpContext);
        return Ok(response);
    }

    [HttpGet(
        template: "{id:guid}",
        Name = "GetPublisherById")]
    public ActionResult<Publisher> GetPublisherById(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var publishers = new List<Publisher> { _repo.GetById<Publisher>(id) };
        var response = _linksGenerator
            .Mapping<Publisher, PublisherReadDto>(publishers, HttpContext)[0];
        return Ok(response);
    }

    [HttpGet(
        template: "{id:guid}/books",
        Name = "GetBooksOfPublisher")]
    public ActionResult<Book> GetBooksOfPublisher(
        [FromRoute] Guid id,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var books = _repo
            .GetPropertyItemOfModel<Book, Publisher>(id)
            .ToList();
        var response = _linksGenerator
            .Mapping<Book, BookReadDto>(books, HttpContext);
        return Ok(response);
    }

    [HttpPost]
    public ActionResult<PublisherReadDto> CreatePublisher(
        [FromBody] PublisherCreateDto publisherCreateDto,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var publisher = _mapper.Map<Publisher>(publisherCreateDto);
        _repo.Create(publisher);

        var publisherWithLinks = _linksGenerator
            .Mapping<Publisher, PublisherReadDto>(new List<Publisher> { publisher }, HttpContext)[0];
        _repo.SaveChanges();

        return CreatedAtRoute(
            nameof(GetPublisherById),
            routeValues: new { id = publisher.Id },
            value: publisherWithLinks);
    }

    [HttpPut(template: "{id:guid}")]
    public ActionResult<PublisherReadDto> UpdatePublisher(
        [FromRoute] Guid id,
        [FromBody] PublisherUpdateDto publisher,
        [FromHeader] string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var publisherExists = _repo.Exists<Publisher>(id);
        if (publisherExists == false) return NotFound();

        var publisherUpdated = _repo.Update<Publisher, PublisherUpdateDto>(publisher, id);
        publisherUpdated.Modified = DateTime.Now;
        _repo.SaveChanges();

        var publishers = new List<Publisher> { publisherUpdated };
        var response = _linksGenerator
            .Mapping<Publisher, PublisherReadDto>(publishers, HttpContext)[0];
        return Ok(response);
    }

    [HttpDelete(
        template: "{id:guid}",
        Name = "DeletePublisher")]
    public ActionResult<Publisher> DeletePublisher([FromRoute] Guid id)
    {
        var publisherExists = _repo.Exists<Publisher>(id);
        if (publisherExists == false) return NotFound();
        _repo.Delete<Publisher>(id);
        _repo.SaveChanges();
        return NoContent();
    }
}
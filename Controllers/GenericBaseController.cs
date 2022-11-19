using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using AutoMapper;
using Library.Data;
using Library.Links;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public abstract class GenericBaseController :
    ControllerBase,
    IGenericBaseController
{
    private readonly ILinksGenerator _linksGenerator;
    private readonly IGenericRepo _genericRepo;
    private readonly IMapper _mapper;
    private readonly PluralizationService _pluralizationService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));

    protected GenericBaseController(
        ILinksGenerator linksGenerator,
        IGenericRepo genericRepo,
        IMapper mapper)
    {
        _linksGenerator = linksGenerator;
        _genericRepo = genericRepo;
        _mapper = mapper;
    }

    public List<TReadDto> GetAll<T, TReadDto, TParams>(TParams qParams,
        string? linkType,
        HttpContext httpContext)
        where T : class
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var items = _linksGenerator
            .Mapping<T, TReadDto>(
                _genericRepo.GetAll<T, TParams>(qParams).ToList(),
                httpContext);
        return items;
        // return Ok(items);
    }

    public TReadDto GetById<T, TReadDto>(
        Guid id,
        string? linkType,
        HttpContext httpContext)
        where T : class
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var items = _linksGenerator
            .Mapping<T, TReadDto>(new List<T>
                { _genericRepo.GetById<T>(id) }, httpContext)[0];
        return items;
    }

    public List<TReadDto>? GetPropertyItemOfModel<T, TReadDto, TModel>(Guid id,
        string? linkType,
        HttpContext httpContext)
    {
        var bookExist = _genericRepo.Exists<TModel>(id);
        if (bookExist == false) return null;

        if (linkType != null) _linksGenerator.LinkType = linkType;
        var author = _linksGenerator
            .Mapping<T, TReadDto>(_genericRepo
                .GetPropertyItemOfModel<T, TModel>(id)
                .ToList(), httpContext);
        return author;
    }

    public object? CreatePropertyItemOfModel<T, TCreateDto, TReadDto, TModel>(Guid modelId,
        TCreateDto itemCreateDto,
        HttpContext httpContext)
    {
        var modelExist = _genericRepo.Exists<TModel>(modelId);
        if (modelExist == false) return null;

        var model = _genericRepo.GetById<TModel>(modelId);
        var item = _mapper.Map<T>(itemCreateDto);

        var prop = item?
            .GetType()
            .GetProperty(_pluralizationService.Pluralize(model?.GetType().Name!));
        prop?.SetValue(item, new List<TModel> { model });

        _genericRepo.Create(item);
        _genericRepo.SaveChanges();

        var items = _linksGenerator.Mapping<T, TReadDto>(new List<T> { item }, httpContext)[0];
        return items;
    }
    public IActionResult DeletePropertyOfModel<TItem, TModel>(
        Guid modelId,
        Guid itemId)
    {
        var itemExist = _genericRepo.Exists<TItem>(itemId);
        var modelExist = _genericRepo.Exists<TModel>(modelId);
        if (itemExist == false || modelExist == false) return NotFound();

        _genericRepo.DeletePropertyOfModel<TItem, TModel>(modelId, itemId);
        _genericRepo.SaveChanges();
        return NoContent();
    }

    public ActionResult<TItem> AddPropertyItemToAModel<TItem, TItemReadDto, TModel>(
        Guid itemId,
        Guid modelId)
    {
        var itemExist = _genericRepo.Exists<TItem>(itemId);
        var modelExist = _genericRepo.Exists<TModel>(modelId);
        if (modelExist == false || itemExist == false) return NotFound();

        var addedItems = _genericRepo.AddPropertyItemToAModel<TItem, TModel>(modelId, itemId);
        if (addedItems == null) return Conflict();
        _genericRepo.SaveChanges();
        return Ok(_linksGenerator.Mapping<TItem, TItemReadDto>(addedItems.ToList(), HttpContext));
    }

    public ActionResult<TReadDto> Update<T,TUpdateDto, TReadDto>(
        Guid id,
        TUpdateDto updateItem,
        string? linkType)
    {
        if (linkType != null) _linksGenerator.LinkType = linkType;
        var itemExists = _genericRepo.Exists<T>(id);
        if (itemExists == false) return NotFound();

        var itemUpdated = _genericRepo.Update<T, TUpdateDto>(updateItem, id);
        _genericRepo.SaveChanges();

        return Ok(_linksGenerator
            .Mapping<T, TReadDto>(
                modelList: new List<T> { itemUpdated },
                context: HttpContext)[0]);
    }

    public ActionResult<T> Delete<T>(Guid id)
    {
        _genericRepo.Delete<T>(id);
        _genericRepo.SaveChanges();
        return NoContent();
    }
}
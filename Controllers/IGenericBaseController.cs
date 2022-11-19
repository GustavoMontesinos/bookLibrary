using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public interface IGenericBaseController
{
    public List<TReadDto> GetAll<T, TReadDto, TParams>(TParams qParams,
        string? linkType,
        HttpContext httpContext)
        where T : class;

    public TReadDto GetById<T, TReadDto>(
        Guid id,
        string? linkType,
        HttpContext httpContext)
        where T : class;

    public List<TReadDto>? GetPropertyItemOfModel<T, TReadDto, TModel>(Guid id,
        string? linkType,
        HttpContext httpContext);

    public object? CreatePropertyItemOfModel<T, TCreateDto, TReadDto, TModel>(Guid modelId,
        TCreateDto itemCreateDto,
        HttpContext httpContext);

    public IActionResult DeletePropertyOfModel<TItem, TModel>(
        Guid modelId,
        Guid itemId);

    public ActionResult<TItem> AddPropertyItemToAModel<TItem, TItemReadDto, TModel>(
        Guid itemId,
        Guid modelId);

    public ActionResult<TReadDto> Update<T, TUpdateDto, TReadDto>(
        Guid id,
        TUpdateDto updateItem,
        string? linkType);

    public ActionResult<T> Delete<T>([FromRoute] Guid id);
}
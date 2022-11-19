namespace Library.Links;
public interface ILinksGenerator
{
    string LinkType { set; }
    public List<TDto> Mapping<TModel, TDto>(
        IReadOnlyList<TModel> modelList,
        HttpContext context);
}
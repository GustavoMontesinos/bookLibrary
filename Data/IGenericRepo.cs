namespace Library.Data;

public interface IGenericRepo
{
    bool SaveChanges();
    bool Exists<T>(Guid id);
    void Delete<T>(Guid id);
    T Update<T, TDto>(TDto item, Guid id);
    void Create<T>(T item);
    T GetById<T>(Guid id);
    IEnumerable<T> GetAll<T, TParams>(TParams queryParams) where T : class;
    IEnumerable<TItem>? AddPropertyItemToAModel<TItem, TModel>(Guid modelId, Guid itemId);
    ICollection<TItem> GetPropertyItemOfModel<TItem, TModel>(Guid id);
    void DeletePropertyOfModel<TItem, TModel>(Guid modelId, Guid itemId);
}
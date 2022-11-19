using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Library.Data;

public class GenericRepo : IGenericRepo
{
    private readonly AppDbContext _context;
    private readonly PluralizationService _pluralizationService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));

    public GenericRepo(AppDbContext context) { _context = context; }
    public bool SaveChanges() => _context.SaveChanges() > 0;
    private IQueryable<T> BaseQuery<T>() => (IQueryable<T>)Setup<T>()!.GetValue(_context)!;
    public bool Exists<T>(Guid id) => BaseQuery<T>().Any("Id.Equals(@0)", id);
    private PropertyInfo? Setup<T>()
    {
        var name = _pluralizationService
            .Pluralize(typeof(T).Name);
        return _context
            .GetType()
            .GetProperties()
            .FirstOrDefault(f => f.Name.Equals(name));
    }
    private void CrudFactory<T>(string operation, T item)
    {
        //Instance of a DbSet Object
        var instance = Setup<T>()!.GetValue(_context)!;
        var op = instance
            .GetType()
            .GetMethod(operation);
        op!.Invoke(
            obj: instance, 
            parameters: new object?[] { item });
    }

    //TODO Fix parse of query parameters
    public IEnumerable<T> GetAll<T, TParams>(TParams queryParams) 
        where T : class
    {
        var query = BaseQuery<T>();
        
        foreach (var property in queryParams!.GetType().GetProperties())
        {
            var value = property.GetValue(queryParams);
            if (value == null) continue;
            var operation = property.PropertyType == typeof(string) ? ".Contains(@0)": ".Equals(@0)";
            query = query.Where(property.Name + operation, value);
        }
        return query.ToList();
    }
    public ICollection<TItem> GetPropertyItemOfModel<TItem, TModel>(Guid id)
        => BaseQuery<TModel>()
            .Where("Id.Equals(@0)", id)
            .Select(_pluralizationService.Pluralize(typeof(TItem).Name))
            .First();
    public T GetById<T>(Guid id) 
        => BaseQuery<T>()
            .Where("Id.Equals(@0)", id)
            .Single()!;
    public void Delete<T>(Guid id) 
        => CrudFactory(
            operation:"Remove", 
            item: GetById<T>(id));
    public void Create<T>(T item)
        => CrudFactory(
            operation:"Add", 
            item: item);
    public T Update<T, TDto>(TDto item, Guid id)
    {
        var itemToUpdate = GetById<T>(id);
        var properties = item?.GetType().GetProperties();
        foreach (var property in properties!)
        {
            var value = property.GetValue(item);
            var propertyInfo = itemToUpdate?.GetType()
                .GetProperties()
                .Single(f => f.Name.ToLower().Equals(property.Name.ToLower()));
            if (value != null) propertyInfo!.SetValue(itemToUpdate, value);
        }
        return itemToUpdate;
    }
    public IEnumerable<TItem>? AddPropertyItemToAModel<TItem, TModel>(Guid modelId, Guid itemId)
    {
        var model = GetById<TModel>(modelId);
        var item = GetById<TItem>(itemId);
        var items = GetPropertyItemOfModel<TItem, TModel>(modelId);
        var itemAlreadyInList = items.Any(i => i != null && i.GetType().GetProperty("Id")!.GetValue(i)!.Equals(itemId));
        if (itemAlreadyInList) return null;
        items.Add(item);
        var prop = model?
            .GetType()
            .GetProperty(_pluralizationService.Pluralize(item?.GetType().Name!));
        prop?.SetValue(model, items);
        return items;
    }
    public void DeletePropertyOfModel<TItem, TModel>(Guid modelId, Guid itemId)
    {
        var item = GetById<TItem>(itemId);
        var model = GetById<TModel>(modelId);
        var items = GetPropertyItemOfModel<TItem, TModel>(modelId);
        items.Remove(item);
        var prop = model?
            .GetType()
            .GetProperty(_pluralizationService.Pluralize(item?.GetType().Name!));
        prop?.SetValue(model, items);
    }
}
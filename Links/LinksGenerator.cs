using System.Reflection;
using AutoMapper;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace Library.Links;

public class LinksGenerator : ILinksGenerator
{
    public string LinkType { get; set; } = "abs";
    private static string? LinkGeneratorMethod { get; set; }
    private readonly LinkGenerator _linkGenerator;
    private readonly IMapper _mapper;
    private MethodInfo _linkType = null!;
    private object[] _params = Array.Empty<object>();
    private readonly PluralizationService _pluralizationService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));

    public LinksGenerator(
        LinkGenerator linkGenerator,
        IMapper mapper)
    {
        _linkGenerator = linkGenerator;
        _mapper = mapper;
    }
    private void Config(string linkType)
    {
        switch (linkType)
        {
            case "rel" or "relative":
                LinkGeneratorMethod = "GetPathByAction";
                _params = new object[8];
                break;
            case "abs" or "absolute":
                LinkGeneratorMethod = "GetUriByAction";
                _params = new object[10];
                break;
        }
        _params[0] = _linkGenerator;
        _linkType = typeof(ControllerLinkGeneratorExtensions)
        .GetMethods()
        .First(x => x.Name.Equals(LinkGeneratorMethod));
    }

    private void PopulateUris<TModel, TDto>(
        List<PropertyInfo> uris,
        PropertyInfo[] modelProperties,
        string modelName,
        TModel model,
        TDto dto)
    {
        var id = modelProperties.First(f => f.Name.Equals("Id"));
        foreach (var uri in uris)
        {
            var name = uri.Name;
            string action;
            string controller;
            object values;
            string nameId;

            var property = modelProperties.FirstOrDefault(f => f.Name.Equals(name));
            var isGeneric = property != null && property.PropertyType.IsGenericType;

            if (isGeneric)
            {
                action = "Get" + name + "Of" + modelName;
                controller = _pluralizationService.Pluralize(modelName);
                values = new { id = id.GetValue(model) };
            }
            else
            {
                if (name == "Self")
                {
                    nameId = string.Empty;
                    name = modelName;
                }
                else if (name.Contains(modelName))
                {
                    nameId = name.Replace(modelName, "");
                    name = modelName;
                }
                else  nameId = name;

                action = "Get" + name + "ById";
                controller = _pluralizationService.Pluralize(name);
                var nonGenericId = modelProperties.First(f => f.Name.Equals(nameId+"Id"));
                values = new { id = nonGenericId.GetValue(model)};
            }
            _params[2] = action;
            _params[3] = controller;
            _params[4] = values;

            var link = _linkType?.Invoke(_linkGenerator,_params)?.ToString() ?? "about:blank";
            uri.SetValue(dto, new Uri(link));
        }
    }
    public List<TDto> Mapping<TModel, TDto> (
        IReadOnlyList<TModel> modelList,
        HttpContext context)
    {
        Config(LinkType);
        _params[1] = context;
        var listDto = _mapper.Map<IEnumerable<TDto>>(modelList).ToList();
        for (var i = 0; i < modelList.Count; i++)
        {
            var dto = listDto[i];
            var model = modelList[i];
            var modelName = model!.GetType().Name;
            var uris = dto!
                .GetType()
                .GetProperties()
                .Where(f => f.PropertyType == typeof(Uri))
                .ToList();
            var modelProperties = model
                .GetType()
                .GetProperties();
            PopulateUris<TModel, TDto>(uris, modelProperties, modelName, model, dto);
        }
        return listDto;
    }
}
namespace Moedelo.Common.AspNet.Mvc;

public sealed class MoedeloMvcOptions
{
    public bool InjectControllersAsSingleton { get; set; } = true;
    public bool? RespectBrowserAcceptHeader { get; set; } = null;
    public bool ConvertEnumToString { get; set; } = false;
    public bool UseJsonCamelCasePropertyNames { get; set; }
}

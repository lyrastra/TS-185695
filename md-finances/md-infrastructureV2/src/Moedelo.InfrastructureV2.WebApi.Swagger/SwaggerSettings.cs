namespace Moedelo.InfrastructureV2.WebApi.Swagger;

public class SwaggerSettings(string commentXmlFilePath)
{
    public string CommentsXmlFilePath { get; set; } = commentXmlFilePath;
    public string Version { get; set; } = "v1";
    public bool DisableUiValidator { get; set; } = true;
    public bool EnableMdApiKey { get; set; } = false;
}

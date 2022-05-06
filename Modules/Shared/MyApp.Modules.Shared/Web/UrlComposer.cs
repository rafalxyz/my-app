using Microsoft.Extensions.Configuration;

namespace MyApp.Modules.Shared.Web;

internal class UrlComposer : IUrlComposer
{
    private readonly IConfiguration configuration;

    public UrlComposer(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string Create(string relativePath)
        => $"{configuration["AppUrl"]}/{relativePath}";
}

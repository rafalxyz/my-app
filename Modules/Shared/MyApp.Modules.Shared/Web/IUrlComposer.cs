namespace MyApp.Modules.Shared.Web;

public interface IUrlComposer
{
    string Create(string relativePath);
}
namespace MyApp.Modules.Shared.DateTime;

internal class AppDateTime : IDateTime
{
    public System.DateTime Now()
    {
        return System.DateTime.UtcNow;
    }
}

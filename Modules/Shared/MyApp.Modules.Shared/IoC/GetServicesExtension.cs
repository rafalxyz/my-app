using System.Reflection;
using System.Runtime.CompilerServices;

namespace MyApp.Modules.Shared.IoC;

public static class GetServicesExtension
{
    public static IEnumerable<Type> GetServices(this Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(x =>
                // Not an interface.
                x.IsClass
                // Not a static or abstract class.
                && !x.IsAbstract
                // Not a compiler generated type.
                && x.GetCustomAttribute(typeof(CompilerGeneratedAttribute), true) == null
                // Declares at least one public instance method not being a property accessor / setter.
                && x.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Any(m => !m.IsSpecialName)
                // Not an endpoint.
                && !x.Name.EndsWith("Endpoint")
            ).ToList();
    }
}

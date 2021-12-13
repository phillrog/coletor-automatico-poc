using ColetaAutomatica.Core.Attributes;
using System.Reflection;

namespace ColetaAutomatica.Core.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo[] GetFilteredProperties(this Type type)
        {
            return type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(SkipPropertyAttribute), true).Length == 0).ToArray();
        }
    }
}

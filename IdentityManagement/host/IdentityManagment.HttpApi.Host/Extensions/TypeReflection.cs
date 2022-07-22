using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IdentityManagment.Extensions
{
    public static class TypeReflection
    {
        public static List<string> GetAllConstants(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(f => f.Name).ToList();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns whether the type can be treated as a primitive, even if in essence it is not.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool LikePrimitive(this Type t)
        {
            return t.IsPrimitive || t == typeof(string) || t == typeof(DateTime);
        }

        /// <summary>
        /// Returns whether the type is an enumeration, but not being tricked into treating a string as one.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsRealEnumerable(this Type t)
        {
            return typeof(IEnumerable).IsAssignableFrom(t) && t != typeof(string) && t != typeof(byte[]);
        }

        /// <summary>
        /// Returns the underlying type if t is Nullable. Else returns t.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetNonNullableType(this Type t)
        {
            var type = t;
            if (t.IsGenericType &&
               t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(t);
            }
            return type;
        }

        /// <summary> 
        /// Check if a type subclasses a generic type. Note that the standard IsSubClassOf method
        /// does not work on generic types.
        /// </summary> 
        /// <param name="genericType">The generic base type to check against</param> 
        /// <returns>True if this is indeed a subclass of the given generic type</returns> 
        public static bool IsSubclassOfGeneric(this Type type, Type genericType)
        {
            Type baseType = type.BaseType;

            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition() == genericType)
                    return true;
                else baseType = baseType.BaseType;
            }
            return false;
        }

        /// <summary>
        /// Return a typed enumeration of custom attributes that have been specified against this type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetCustomAttributes<T>(this Type type) where T : Attribute
        {
            foreach (var att in type.GetCustomAttributes(typeof(T), true))
            {
                yield return att as T;
            }
        }

        /// <summary>
        /// Search for the generic arguements for a given base type
        /// </summary>
        /// <param name="type">The derrived type</param>
        /// <param name="genericBaseType">The generic base type</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetBaseGenericArguements(this Type type, Type genericBaseType)
        {
            Type baseType = type.BaseType;

            //Loop until we find the base type
            while (baseType != null)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == genericBaseType)
                    //Found, so get the generic arguments for the type
                    return baseType.GetGenericArguments();
                else
                    //Get the next base type
                    baseType = baseType.BaseType;
            }

            return null;
        }

        public static dynamic TypeMapExpression(this Type type, string parameterName)
        {
            //Get a parameter expression for the type
            var parameter = System.Linq.Expressions.Expression.Parameter(type);
            //Get a lambda expression for the type
            return System.Linq.Expressions.Expression.Lambda(System.Linq.Expressions.Expression.Property(parameter, parameterName), parameter);
        }
    }
}

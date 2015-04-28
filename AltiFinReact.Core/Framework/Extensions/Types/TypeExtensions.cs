using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Altinet.Extensions.GenericEnumerable;

namespace AltiFinReact.Core.Framework.Extensions.Types
{
    public static class TypeExtensions
    {
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> propertyInfos =
    new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        private static object token = 0;


        public static bool IsGenericEnumerable(this Type type)
        {
            Type[] genericArgs = type.GetGenericArguments();
            return genericArgs.Length == 1 && typeof (IEnumerable<>).MakeGenericType(genericArgs).IsAssignableFrom(type);
        }

        public static void CallOn<T>(this object target, Action<T> action) where T : class
        {
            var subject = target as T;
            if (subject == null) return;

            try
            {
                action(subject);
            }
            catch (InvalidOperationException e)
            {
                if (!e.ToString().Contains("The calling thread"))
                {
                    throw;
                }
            }
        }

        public static bool IsNullable(this Type theType)
        {
            return (theType.IsGenericType && theType.
                GetGenericTypeDefinition() == typeof (Nullable<>));
        }

        public static Type GetNonGenericType(this Type type)
        {
            return type.IsGenericType ? type.GetGenericArguments()[0] : type;
        }


        public static void CallOnEach<T>(this IEnumerable enumerable, Action<T> action) where T : class
        {
            foreach (object o in enumerable)
            {
                o.CallOn(action);
            }
        }

        /// <summary>
        /// Gets value of object property in property string
        /// </summary>
        /// <param name="o">Object for which we are getting the value</param>
        /// <param name="propertyString">Name of property which we are getting.</param>
        /// <remarks>Property string can be name of a nested property with '.' separator</remarks>
        /// <returns>Value of object property</returns>
        public static object GetPropertyValue(this object o, string propertyString)
        {
            if (o == null)
                return null;

            var result = o;
            var properties = propertyString.Split('.');

            foreach (var property in properties)
            {
                var type = result.GetType();
                PropertyInfo propertyInfo;
                Dictionary<string, PropertyInfo> infos;

                if (!propertyInfos.TryGetValue(type, out infos))
                    lock (token)
                    {
                        if (!propertyInfos.TryGetValue(type, out infos))
                            propertyInfos.Add(type, (infos = new Dictionary<string, PropertyInfo>()));
                    }

                if (!infos.TryGetValue(property, out propertyInfo))
                    lock (token)
                    {
                        if (!infos.TryGetValue(property, out propertyInfo))
                            infos.Add(property, (propertyInfo = type.GetProperty(property)));
                    }

                if (propertyInfo == null)
                    throw new ArgumentException(string.Format("Property {0}.{1} doesn't exist",
                        type.FullName, property));

                result = propertyInfo.GetValue(result, null);

                if (result == null)
                    return null;
            }

            return result;
        }

        /// <summary>
        /// Indicates whether object has provided property
        /// </summary>
        /// <param name="o">Object for which we are checking if property exists</param>
        /// <param name="propertyString">Name of property which is to be checked for existance</param>
        /// <remarks>Property string can be name of a nested property with '.' separator</remarks>
        /// <returns>True if object has provided property</returns>
        public static bool HasProperty(this object o, string propertyString)
        {
            if (o == null)
                return false;

            var result = o.GetType();
            var properties = propertyString.Split('.');

            foreach (var property in properties)
            {
                PropertyInfo propertyInfo;
                Dictionary<string, PropertyInfo> infos;

                if (!propertyInfos.TryGetValue(result, out infos))
                    lock (token)
                    {
                        if (!propertyInfos.TryGetValue(result, out infos))
                            propertyInfos.Add(result, (infos = new Dictionary<string, PropertyInfo>()));
                    }

                if (!infos.TryGetValue(property, out propertyInfo))
                    lock (token)
                    {
                        if (!infos.TryGetValue(property, out propertyInfo))
                            infos.Add(property, (propertyInfo = result.GetProperty(property)));
                    }

                if (propertyInfo == null)
                    return false;

                result = propertyInfo.PropertyType;
            }

            return true;
        }

        /// <summary>
        /// Sets the value of specified property. If property has different return types, you can provide
        /// which of the return value types you want to set.
        /// </summary>
        /// <param name="o">object</param>
        /// <param name="propertyName">Name of the property whose value is to be set</param>
        /// <param name="propertyType">Type of the property return value</param>
        /// <param name="value">Value to assign to the property</param>
        public static void SetPropertyValue(this object o, string propertyName, Type propertyType, object value)
        {
            o.GetType().UnderlyingSystemType.GetProperty(propertyName, propertyType).SetValue(o, value, null);
        }

        /// <summary>
        /// Sets value of object property in property string to provided value
        /// </summary>
        /// <param name="o">Object for which we are setting the value</param>
        /// <param name="propertyString">Name of property which we are setting.</param>
        /// <param name="value">Value of object property to set</param>
        /// <remarks>Property string can be name of a nested property with '.' separator</remarks>
        public static void SetPropertyValue(this object o, string propertyString, object value)
        {
            if (o == null)
                return;

            PropertyInfo propertyInfo = null;
            var property = o;
            var propertyNames = propertyString.Split('.');

            foreach (var propertyName in propertyNames)
            {
                if (propertyInfo != null)
                    property = propertyInfo.GetValue(property, null);

                if (property == null)
                    return;

                var type = property.GetType();
                Dictionary<string, PropertyInfo> infos;

                if (!propertyInfos.TryGetValue(type, out infos))
                    lock (token)
                    {
                        if (!propertyInfos.TryGetValue(type, out infos))
                            propertyInfos.Add(type, (infos = new Dictionary<string, PropertyInfo>()));
                    }

                if (!infos.TryGetValue(propertyName, out propertyInfo))
                    lock (token)
                    {
                        if (!infos.TryGetValue(propertyName, out propertyInfo))
                            infos.Add(propertyName, (propertyInfo = type.GetProperty(propertyName)));
                    }

                if (propertyInfo == null)
                    throw new ArgumentException(String.Format("Property {0}.{1} doesn't exist",
                        type.FullName, property));
            }

            if (propertyInfo != null)
                propertyInfo.SetValue(property, value, null);
        }

        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static Type GetPropertyType(this object o, string propertyString)
        {
            if (o == null)
                return null;

            Type obj = o.GetType();
            string[] properties = propertyString.Split('.');

            obj = properties.Aggregate(obj, (current, property) => current.GetProperty(property).PropertyType);

            if (obj == null)
                throw new ArgumentException(String.Format("Argument {0} has invalid value {1}", "propertyString",
                                                          propertyString));
            return obj;
        }

        public static string GetMemberName<TMember>(this Expression<Func<TMember>> expression)
        {
            return GetMemberName(expression.Body as MemberExpression);
        }

        public static string GetMemberName<T, TMember>(this Expression<Func<T, TMember>> expression)
        {
            return GetMemberName(expression.Body as MemberExpression);
        }

        private static string GetMemberName(MemberExpression expression)
        {
            return GetMembersOnPath(expression)
                            .Select(m => m.Member.Name)
                            .Reverse().Join(".");
        }

        private static IEnumerable<MemberExpression> GetMembersOnPath(MemberExpression expression)
        {
            while (expression != null)
            {
                yield return expression;
                expression = expression.Expression as MemberExpression;
            }
        }



        public static string GetPropertyValueToCode(this object o, string propertyString)
        {
            var val = o.GetPropertyValue(propertyString);
            var propType = o.GetPropertyType(propertyString);
            if (val == null && propType.IsNullable() && propType.GetNonGenericType().IsValueType)
            {
                val = propType.GetNonGenericType().GetDefault();
            }
            return val is string ? "\"" + val.ToString().Replace("\"", "\\\"").Replace(";", ",") + "\"" : val != null ? val.ToString() : "null";
        }

    }
}
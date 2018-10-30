//-----------------------------------------------------------------------
// <copyright file="ClToolsEnum.cs" company="Ideal Software">
//     Copyright (c) Ideal Software. All rights reserved.
// </copyright>
// <author>Stephane Amoa</author>
//-----------------------------------------------------------------------
namespace Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;

    /// <summary>
    /// This class deals with enum type <see cref="Enum"/>
    /// <remarks>Used to check enum, get description, ect...</remarks>
    /// </summary>
    public static class CLToolsEnumHelper
    {
        /// <summary>
        /// Get an enum values by its description.
        /// </summary>
        /// <typeparam name="T">The Enum type</typeparam>
        /// <param name="description">The enum description</param>
        /// <returns>The enum value otherwise exceptions are raised</returns>
        /// <remarks>Parameter T has to match the search</remarks>
        /// <exception cref="ArgumentNullException">Argument is null or empty</exception>
        /// <exception cref="ArgumentException">The type is not an enum.</exception>
        public static Enum GetEnumValueByDescription<T>(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description));
            }

            Type type = typeof(T);
            var baseType = type.GetTypeInfo().BaseType;
            if (baseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            Enum EnumToReturn = null;
            var arrayEnumType = Enum.GetValues(type).Cast<Enum>();
            foreach (Enum enumField in arrayEnumType)
            {
                if (enumField.GetDescription() == description)
                {
                    EnumToReturn = enumField;
                    break;
                }
            }

            return EnumToReturn;
        }

        /// <summary>
        /// get an array of enum value given the enum type.
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>The enum value for this type of enum.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <remarks>if the base type is not an enum type an exception is thrown <see cref="ArgumentException"/> </remarks>
        public static IEnumerable<T> GetEnumValues<T>()
        {
            if (typeof(T).GetTypeInfo().BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Parse an enum type given a string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns>The enum value otherwise the default enum value.</returns>
        /// <remarks>Default enum value is returned when parsing failed <see cref="Enum.TryParse{TEnum}(string, bool, out TEnum)"/> </remarks>
        public static T ParseEnum<T>(string value, T defaultValue) where T : struct
        {
            try
            {
                T enumValue;
                if (!Enum.TryParse(value, true, out enumValue))
                {
                    return defaultValue;
                }

                return enumValue;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("T must be of type System.Enum");
            }
        }
    }

    /// <summary>
    /// This class is an helper for enum <see cref="System.Enum"/>
    /// </summary>
    /// <remarks>Well used in applications</remarks>
    public static class ToolEnumsHelper
    {
        /// <summary>
        /// Get an attribute type  given an enum value
        /// </summary>
        /// <typeparam name="T">The attribute.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The enum type, otherwise null</returns>
        /// <remarks>This is useful to find if an enum value is of a certain enum type</remarks>
        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            if (enumValue != null)
            {
                var typeInfo = enumValue.GetType();
                //typeof(T).GetTypeInfo().BaseType
                FieldInfo fi = typeInfo.GetTypeInfo().GetField(enumValue.ToString());
                IEnumerable<Attribute> tabObj = (IEnumerable < Attribute > ) fi.GetCustomAttributes(false);
                //object[] tabObj = fi.GetCustomAttributes(false);
                //object[] tabObj = fi..GetCustomAttributes(false);
                //return (T)tabObj[0];
                return (T)tabObj.ElementAt(0);
            }

            return null;
        }

        /// <summary>
        /// Retreive the description given to an enum value as an attribute.
        /// </summary>
        /// <param name="enumVal">The enum value.</param>
        /// <returns>The enum value's description otherwise an empty string</returns>
        /// <remarks>Very useful</remarks>
        public static string GetDescription(this Enum enumVal)
        {
            var attr = GetAttributeOfType<EnumDescriptionAttribute>(enumVal);
            return attr != null ? attr.Text : string.Empty;
        }

    }

    /// <summary>
    /// This class install an attribute used for enum description.
    /// Its base class is <see cref="Attribute"/>
    /// </summary>
    /// <remarks>This is where we manage the description of an enum value prior to setting attributes to this class.</remarks>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// property Text
        /// </summary>
        /// <remarks>The description is set and get here.</remarks>
        public string Text { get; set; }
    }
}

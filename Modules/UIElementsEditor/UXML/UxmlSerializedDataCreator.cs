// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor.UIElements
{
    /// <summary>
    /// Editor utility methods for dealing with UxmlSerializedData.
    /// </summary>
    public static class UxmlSerializedDataCreator
    {
        /// <summary>
        /// Creates a <see cref="UxmlSerializedData"/> instance for the given type with default values applied.
        /// </summary>
        /// <param name="type">The type that contains the [[UxmlSerializedData]].
        /// The type must belong to a class that's actively decorated with either the [[UxmlElementAttribute]] 
        /// or [[UxmlObjectAttribute]] attribute.
        /// </param>
        public static UxmlSerializedData CreateUxmlSerializedData(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var desc = UxmlSerializedDataRegistry.GetDescription(type.FullName);
            if (desc == null)
            {
                Debug.LogError($"Could not find UxmlSerializedData for type {type}");
                return null;

            }
            return desc.CreateDefaultSerializedData();
        }
    }
}

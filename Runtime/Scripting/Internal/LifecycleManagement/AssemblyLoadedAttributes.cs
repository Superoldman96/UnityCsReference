// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using UnityEngine.Bindings;

namespace Unity.Scripting.LifecycleManagement
{
    [VisibleToOtherModules]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class AfterAssemblyLoadedAttribute : LifecycleAttributeBase
    {
        public AfterAssemblyLoadedAttribute() { }
    }

    [VisibleToOtherModules]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class BeforeAssemblyUnloadingAttribute : LifecycleAttributeBase
    {
        public BeforeAssemblyUnloadingAttribute() { }
    }
}

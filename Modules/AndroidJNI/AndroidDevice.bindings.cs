// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using UnityEngine.Bindings;

namespace UnityEngine.Android
{
    public enum AndroidHardwareType
    {
        Generic,
        [Obsolete("ChromeOS is no longer supported.")]
        ChromeOS
    }

    public class AndroidDevice
    {
        static public AndroidHardwareType hardwareType => AndroidHardwareType.Generic;
        static public void SetSustainedPerformanceMode(bool enabled) {}
    }
}

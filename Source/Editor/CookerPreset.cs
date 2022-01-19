// AlwaysTooLate.GameCooker (c) 2018-2022 Always Too Late. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEditor;

namespace AlwaysTooLate.GameCooker
{
    /// <summary>
    ///     Cooker preset class.
    /// </summary>
    [Serializable]
    public class CookerPreset
    {
        public string Name;

        public List<Target> Targets;

        [Serializable]
        public class Target
        {
            public enum BuildType
            {
                Debug,
                Release,
                Shipping
            }

            public bool BuildContent;

            internal int BuildNumber;

            public BuildTarget BuildTarget;

            public BuildAssetBundleOptions ContentBuildOptions;

            public string ContentDirectoryName;

            public string[] DefineSymbols;

            public string ExecutableName;

            public bool Headless;

            public string Name;

            public string OutputName;

            public string PostBuildAction;

            public string PreBuildAction;

            public BuildType Type;

            public bool DoNotCleanDefines;
        }
    }
}
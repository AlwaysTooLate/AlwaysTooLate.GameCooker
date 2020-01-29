// AlwaysTooLate.Core (c) 2018-2019 Always Too Late. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AlwaysTooLate.GameCooker
{
    /// <summary>
    ///     The GameCooker manager class.
    /// </summary>
    public class CookerManager
    {
        /// <summary>
        ///     All created presets.
        /// </summary>
        public List<CookerPreset> Presets = new List<CookerPreset>();

        /// <summary>
        ///     The current selected preset.
        /// </summary>
        public CookerPreset SelectedPreset;

        /// <summary>
        ///     The current selected target.
        /// </summary>
        public CookerPreset.Target SelectedTarget;

        /// <summary>
        ///     Saves all configuration changes.
        /// </summary>
        public void Save()
        {
            // build save object
            var obj = new SaveObject
            {
                Presets = Presets
            };
            obj.SelectedPreset = obj.Presets.IndexOf(SelectedPreset);

            var data = JsonUtility.ToJson(obj, true);
            File.WriteAllText("gamecooker.json", data); // TODO: check if this wokrks on mac/linux
        }

        /// <summary>
        ///     Loads previous configuration.
        /// </summary>
        public void Load()
        {
            if (File.Exists("gamecooker.json"))
            {
                var data = File.ReadAllText("gamecooker.json");
                try
                {
                    var obj = JsonUtility.FromJson<SaveObject>(data);

                    Presets = obj.Presets;

                    if (Presets.Count > 0 && obj.SelectedPreset >= 0)
                        SelectedPreset = obj.Presets[obj.SelectedPreset];
                }
                catch
                {
                    Debug.LogWarning("Failed to load GameCooker configuration");
                }
            }
            else
            {
                Debug.LogWarning("Cooker configuration not found, created new one");
                File.WriteAllText("gamecooker.json", "{}");
                Presets = new List<CookerPreset>();
            }
        }

        /// <summary>
        ///     Builds the selected preset.
        /// </summary>
        public void Build()
        {
            InternalBuild();
        }

        /// <summary>
        ///     Builds scripts of the selected preset.
        /// </summary>
        public void BuildScripts()
        {
            InternalBuild();
        }

        /// <summary>
        ///     Resets defines.
        /// </summary>
        public void ResetDefines()
        {
            BuildPipelineHelper.SetDefines(Presets.ToArray());
        }

        private void InternalBuild()
        {
            // TODO: auto start option(warning: use proper working dir!)

            foreach (var target in SelectedPreset.Targets)
                try
                {
                    BuildPipelineHelper.Build(target);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to build target: " + target.Name + " error: " + ex);
                }

            // reset the directives
            ResetDefines();

            // increase build number TODO: build counting
        }

        private struct SaveObject
        {
            public List<CookerPreset> Presets;
            public int SelectedPreset;
        }
    }
}
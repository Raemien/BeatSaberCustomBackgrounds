using BeatSaberMarkupLanguage.Attributes;
using BS_Utils.Utilities;
using UnityEngine;

namespace CustomBackgrounds
{
    internal class PluginConfig
    {
        public bool RegenerateConfig = true;
    }


    class Settings : PersistentSingleton<Settings>
    {
        private Config config;

        [UIAction("trigger-togglebackgrounds")]
        public void ToggleBackgrounds(bool val)
        {
            GameObject skyboxObject = GameObject.Find("_SkyBGObject");
            if (skyboxObject != null)
            {
                //val ? LoadBackground(string bgname) : 
                skyboxObject.SetActive(val);
            }
        }
        [UIAction("hideobj-refresh")] private void RefreshHiddenObjects(bool val) => EnvironmentHider.HideMenuEnv();

        //General
        [UIValue("enabled")]
        public bool EnableBackgrounds
        {
            get => config.GetBool("General", "Enabled", true);
            set => config.SetBool("General", "Enabled", value);
        }
        public bool EnablePreviews
        {
            get => config.GetBool("General", "Sprite Previews", true);
            set => config.SetBool("General", "Sprite Previews", value);
        }
        [UIValue("enable-reflections")]
        public bool EnableReflections
        {
            get => config.GetBool("General", "Reflect Skybox", true);
            set => config.SetBool("General", "Reflect Skybox", value);
        }
        public string SelectedBackground
        {
            get => config.GetString("General", "Selected Skybox", "Default");
            set => config.SetString("General", "Selected Skybox", value);
        }

        //Hidden Objects
        [UIValue("hideobj-platform")]
        public bool HidePlatform
        {
            get => config.GetBool("Hidden Objects", "Hide Platform", false);
            set => config.SetBool("Hidden Objects", "Hide Platform", value);
        }
        public bool HideRings
        {
            get => config.GetBool("Hidden Objects", "Hide Rings", true);
            set => config.SetBool("Hidden Objects", "Hide Rings", value);
        }
        public bool HideMenuEnv
        {
            get => config.GetBool("Hidden Objects", "Hide Menu Decorations", true);
            set => config.SetBool("Hidden Objects", "Hide Menu Decorations", value);
        }
        public bool HideGameEnv
        {
            get => config.GetBool("Hidden Objects", "Hide GameCore Decorations", true);
            set => config.SetBool("Hidden Objects", "Hide GameCore Decorations", value);
        }

        //Offsets
        public int RotationOffset
        {
            get => config.GetInt("Offsets", "Rotation Offset", 0);
            set => config.SetInt("Offsets", "Rotation Offset", value);
        }
        public bool RegenerateConfig { get; internal set; }

        public void Awake()
        {
            config = new Config("CustomBackgrounds");
        }
    }

}
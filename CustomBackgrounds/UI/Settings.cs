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
        [UIAction("hideobj-refresh")] private void RefreshHiddenObjects() => EnvironmentHider.HideEnvironmentObjects(true);

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
        [UIValue("hideobj-fog")]
        public bool HideFog
        {
            get => config.GetBool("Hidden Objects", "Hide Smoke", false);
            set => config.SetBool("Hidden Objects", "Hide Smoke", value);
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

        public bool RegenerateConfig { get; internal set; }

        public void Awake()
        {
            config = new Config("CustomBackgrounds");
        }
    }

}
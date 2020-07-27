using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Utilities;
using IPA;
using IPA.Config;
using IPA.Utilities;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace CustomBackgrounds
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class CustomBackgrounds
    {
        public const string assemblyName = "CustomBackgrounds";

        public static BackgroundBehaviour skyBehaviour;

        [Init]
        public CustomBackgrounds(IPALogger logger, [IPA.Config.Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
        }


        [OnEnable]
        public void Start()
        {
            PersistentSingleton<Settings>.TouchInstance();
            PersistentSingleton<UISingleton>.TouchInstance();
            UISingleton.RegMenuButton();

            BSMLSettings.instance.AddSettingsMenu("Backgrounds", "CustomBackgrounds.Views.SettingsMenu.bsml", Settings.instance);

            SceneManager.sceneLoaded += OnSceneLoaded;
            SetupDirectories();
            InitBackground();

        }

        [OnDisable]
        public void Disable()
        {
            BSMLSettings.instance.RemoveSettingsMenu(Settings.instance);
            UISingleton.RemoveMenuButton();
        }

        private void OnSceneLoaded(Scene newScene, LoadSceneMode sceneMode)
        {
            var config = Settings.instance;
            EnvironmentHider.hiddenObjs = 0;
            InitBackground();
            switch (newScene.name)
            {
                case "MenuViewControllers":
                case "GameCore":
                case "Credits":
                case "HealthWarning":
                    if (config.EnableBackgrounds && (skyBehaviour.skyTextureName != config.SelectedBackground)) // Prevent unnessesary loading of textures
                    {
                        skyBehaviour.LoadBackground(config.SelectedBackground);
                    }
                    break;
            }

        }

        private void SetupDirectories() 
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "CustomBackgrounds/SkyShaders");
            if (!File.Exists(Environment.CurrentDirectory + "/CustomBackgrounds/SkyShaders/CustomBG"))
            {
                Logger.Log("WARNING: 'CustomBG' SkyShader file is missing! Try redownloading CustomBackgrounds from MA or github.", IPALogger.Level.Critical);
                Disable();
            }

        }

        private void InitBackground()
        {
            string curscene = SceneManager.GetActiveScene().name;
            var config = Settings.instance;
            if (skyBehaviour == null)
            {
                skyBehaviour = new GameObject(nameof(BackgroundBehaviour)).AddComponent<BackgroundBehaviour>();
                GameObject.DontDestroyOnLoad(skyBehaviour);
            }
            skyBehaviour.enabled = config.EnableBackgrounds;
        }

    }
}
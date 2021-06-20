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
using HarmonyLib;

namespace CustomBackgrounds
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class CustomBackgrounds
    {
        public const string assemblyName = "CustomBackgrounds";
        public static BackgroundBehaviour skyBehaviour;
        static Harmony harmPatcher;

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

            SceneManager.sceneLoaded += OnSceneLoaded;
            SetupDirectories();
            InitBackground();
            ApplyPatches();
        }

        public static void ApplyPatches()
        {
            if (harmPatcher == null)
            {
                harmPatcher = new Harmony(assemblyName);
                harmPatcher.PatchAll();
            }
        }

        [OnDisable]
        public void Disable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            harmPatcher.UnpatchAll(assemblyName);
            harmPatcher = null;
        }

        private void OnSceneLoaded(Scene newScene, LoadSceneMode sceneMode)
        {
            var config = Settings.instance;
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
                UnityEngine.Object.DontDestroyOnLoad(skyBehaviour);
            }
            skyBehaviour.enabled = config.EnableBackgrounds;
        }

    }
}
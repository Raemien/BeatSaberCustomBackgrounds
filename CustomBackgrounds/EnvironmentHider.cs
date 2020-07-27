using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomBackgrounds
{
    internal static class EnvironmentHider
    {
        public static int hiddenObjs;
        public static void HideEnvironmentObjects(bool forceHide = false)
        {
            if (hiddenObjs < 65536 || forceHide) // Tacky workaround for onSceneLoaded events not reliably triggering with environments
            {
                var config = Settings.instance;
                Scene curScene = SceneManager.GetActiveScene();
                GameObject environmentRoot;
                Renderer[] appendedRenderers = { };
                Renderer[] excludedRenderers = { };
                switch (curScene.name)
                {
                    case "GameCore":
                        environmentRoot = GameObject.Find("Environment/PlayersPlace").transform.parent.gameObject;
                        foreach (var obj in curScene.GetRootGameObjects().Where(obj => obj.name.Contains("Ring")))
                        {
                            appendedRenderers = appendedRenderers.Concat(obj.GetComponentsInChildren<Renderer>()).ToArray();
                        }
                        excludedRenderers = excludedRenderers.AddRangeToArray(!config.HidePlatform && !GameObject.Find("Environment/SpawnRotationChevron") ? GameObject.Find("Environment/PlayersPlace").GetComponentsInChildren<Renderer>() : Array.Empty<Renderer>());
                        excludedRenderers = excludedRenderers.AddRangeToArray(!config.HideFog ? GameObject.Find("Environment/BigSmokePS").GetComponentsInChildren<Renderer>() : Array.Empty<Renderer>());
                        break;
                    case "MenuViewControllers":
                        environmentRoot = GameObject.Find("Wrapper/MenuPlayersPlace").transform.parent.gameObject;
                        appendedRenderers = GameObject.Find("Wrapper/Arrows").GetComponentsInChildren<Renderer>();
                        excludedRenderers = !config.HidePlatform ? GameObject.Find("Wrapper/MenuPlayersPlace").GetComponentsInChildren<Renderer>() : Array.Empty<Renderer>();
                        break;
                    default:
                        environmentRoot = null;
                        break;
                }

                if (environmentRoot != null)
                {
                    Renderer[] renderers = environmentRoot.GetComponentsInChildren<Renderer>();
                    if (appendedRenderers.Length > 0)
                    {
                        renderers = renderers.Concat(appendedRenderers).ToArray();
                    }
                    renderers = renderers.Except(excludedRenderers).ToArray();
                    bool hiddensetting = curScene.name == "MenuViewControllers" ? !config.HideMenuEnv : !config.HideGameEnv;

                    foreach (Renderer renderer in renderers)
                    {
                        renderer.enabled = hiddensetting;
                        hiddenObjs += 1;
                    }
                }
            }
        }
    }
}

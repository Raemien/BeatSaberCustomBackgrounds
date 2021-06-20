using HarmonyLib;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using BS_Utils.Utilities;

namespace CustomBackgrounds
{
    internal class EnvironmentHider
    {
        private static bool GetBGActive()
        {
            return (CustomBackgrounds.skyBehaviour.skyboxObject && CustomBackgrounds.skyBehaviour.skyboxObject.activeSelf);
        }

        private static GameObject FindMultiplayerPlatform()
        {
            MultiplayerLobbyAvatarPlace[] platforms = Resources.FindObjectsOfTypeAll<MultiplayerLobbyAvatarPlace>();
            for (int i = 0; i < platforms.Length; i++)
            {
                Transform plat = platforms[i].transform;
                if (plat.GetChild(0).eulerAngles.x == 270.0f && plat.name == "LobbyAvatarPlace(Clone)")
                {
                    return plat.gameObject;
                }
            }
            return null;
        }

        private static void HideChildRenderers(GameObject obj, bool onlymeshes, bool unhide = false)
        {
            if (obj == null) return;
            bool enabled = !GetBGActive() || unhide;
            var rendarray = onlymeshes ? obj.GetComponentsInChildren<MeshRenderer>() : obj.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rendarray.Length; i++)
            {
                Renderer renderer = rendarray[i];
                if (!renderer) continue;
                renderer.enabled = enabled;
            }
        }

        private static void HideChildLights(GameObject obj)
        {
            if (obj == null) return;
            var rendarray = obj.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rendarray.Length; i++)
            {
                Renderer renderer = rendarray[i];
                if (!renderer || renderer.GetComponent<LightManager>()) continue;
                if (!Regex.IsMatch(renderer.name, "bloom|light", RegexOptions.IgnoreCase)) continue;
                renderer.forceRenderingOff = true;
            }
        }

        public static void HideMenuEnv()
        {
            bool ismulti = Resources.FindObjectsOfTypeAll<MenuEnvironmentManager>()[0].GetField<int>("_prevMenuEnvironmentType") == 2;
            bool bgActive = GetBGActive();
            if (!ismulti)
            {
                // Find Objects
                var floorObj = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/BasicMenuGround");
                var notesObj = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/Notes");
                var notePileObj = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/PileOfNotes");
                var multiEnvObj = FindMultiplayerPlatform();

                // Apply Visibility
                multiEnvObj.SetActive(bgActive);
                HideChildRenderers(notesObj, false);
                HideChildRenderers(notePileObj, false);

                if (floorObj) floorObj.GetComponent<MeshRenderer>().enabled = !bgActive;
                if (bgActive)
                {
                    //HideChildRenderers(multiEnvObj, false);
                    //HideChildRenderers(GameObject.Find("LobbyAvatarPlace(Clone)"), false, true);
                }
            }
        }

        public static void HideGameEnv()
        {
            GameObject environmentObj = GameObject.Find("Environment");
            GameObject playersPlaceObj = GameObject.Find("Environment/PlayersPlace");
            GameObject coreLightingObj = GameObject.Find("Environment/CoreLighting");

            HideChildLights(environmentObj);
            if (Settings.instance.HideGameEnv)
            {
                HideChildRenderers(environmentObj, false);
                HideChildRenderers(playersPlaceObj, false, true);
            }
            HideChildRenderers(coreLightingObj, true, true);
        }
    }
}

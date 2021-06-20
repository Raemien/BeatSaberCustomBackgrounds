using BeatSaberMarkupLanguage;
using HarmonyLib;
using BS_Utils.Utilities;
using UnityEngine;
using System.Text.RegularExpressions;

namespace CustomBackgrounds.HarmonyPatches
{
    [HarmonyPatch(typeof(TrackLaneRing), "Init")]
    internal class TrackLaneRing_Init
    {
        internal static void Postfix(TrackLaneRing __instance)
        {
            if (Settings.instance.EnableBackgrounds)
            {
                Renderer[] renderers = __instance.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.forceRenderingOff = Settings.instance.HideRings;
                }
            }
        }
    }

}
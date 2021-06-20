using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.IO;
using UnityEngine;

namespace CustomBackgrounds
{
    class EnvironmentMenuViewController : BSMLResourceViewController
    {
        public override string ResourceName => "CustomBackgrounds.Views.EnvironmentMenu.bsml";

        //Values
        [UIValue("enabled")] private bool _EnableSky => Settings.instance.EnableBackgrounds;
        [UIValue("hideobj-menu")] private bool _HideMenu => Settings.instance.HideMenuEnv;
        [UIValue("hideobj-game")] private bool _HideGame => Settings.instance.HideGameEnv;
        [UIValue("hideobj-rings")] private bool _HideRings => Settings.instance.HideRings;


        //Actions
        [UIAction("hideobj-menu-onchange")]
        private void _SetHideMenuEnvs(bool newval)
        {
            Settings.instance.HideMenuEnv = newval;
            EnvironmentHider.HideMenuEnv();
        }

        [UIAction("hideobj-game-onchange")]
        private void _SetHideGameEnvs(bool newval)
        {
            Settings.instance.HideGameEnv = newval;
            EnvironmentHider.HideMenuEnv();
        }
		
        [UIAction("hideobj-rings-onchange")]
        private void _SetHideRings(bool newval) => Settings.instance.HideRings = newval;
    }
}

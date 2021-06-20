using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.IO;
using UnityEngine;

namespace CustomBackgrounds
{
    class SideConfigMenuViewController : BSMLResourceViewController
    {
        public override string ResourceName => "CustomBackgrounds.Views.SideConfigMenu.bsml";

        //Values
        [UIValue("enabled")] private bool _EnableSky => Settings.instance.EnableBackgrounds;
        [UIValue("menu-previews")] private bool _EnablePreviews => Settings.instance.EnablePreviews;
        [UIValue("offset-rotation")] private int _RotationOffset = Settings.instance.RotationOffset;

        //Actions
        [UIAction("enabled-onchange")]
        private void _SetEnableSky(bool newval)
        {
            Settings.instance.EnableBackgrounds = newval;
            Settings.instance.ToggleBackgrounds(newval);
        }
        [UIAction("previews-onchange")]
        private void _SetEnablePreviews(bool newval) => Settings.instance.EnablePreviews = newval;

        [UIAction("rotation-onchange")]
        private void _SetRotationOffset(int newval) => Settings.instance.RotationOffset = newval;
    }
}

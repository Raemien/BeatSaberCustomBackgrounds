﻿using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;

namespace CustomBackgrounds
{
    public class UISingleton : PersistentSingleton<UISingleton>
    {
        public static MenuButton menuButton = new MenuButton("Backgrounds", "Custom 360 backgrounds", OpenBGMenu);
        private static BackgroundMenuFlowCoordinator backgroundMenuFlowCoordinator;

        private static void OpenBGMenu()
        {
            if (backgroundMenuFlowCoordinator == null)
            {
                backgroundMenuFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<BackgroundMenuFlowCoordinator>();
            }
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(backgroundMenuFlowCoordinator, null, HMUI.ViewController.AnimationDirection.Horizontal, false);
        }
        public static void RegMenuButton()
        {
            MenuButtons.instance.RegisterButton(menuButton);
        }
        public static void RemoveMenuButton()
        {
            MenuButtons.instance.UnregisterButton(menuButton);
        }
    }
}

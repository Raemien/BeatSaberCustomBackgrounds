using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using BS_Utils.Utilities;
using HMUI;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CustomBackgrounds
{
    class BackgroundMenuViewController : BSMLResourceViewController
    {
        public override string ResourceName => "CustomBackgrounds.Views.BackgroundMenu.bsml";
        [UIComponent("background-list")] public CustomListTableData backgroundList;
        private List<string> _BackgroundList = new List<string>();

        [UIAction("on-select-bg")]
        private void SetBackground(TableView table, int selectedrow)
        {
            if (CustomBackgrounds.skyBehaviour != null)
            {
                string chosenfile = backgroundList.data[selectedrow].text;
                Settings.instance.SelectedBackground = chosenfile;
                CustomBackgrounds.skyBehaviour.LoadBackground(chosenfile);
                Logger.Log("Set skybox to " + chosenfile);
            }
        }
        [UIAction("#post-parse")]
        private void GetBGDir()
        {
            if (!_BackgroundList.Contains("Default"))
            {
                CustomListTableData.CustomCellInfo defaultskyCellInfo = new CustomListTableData.CustomCellInfo("Default", "Default Skybox");
                backgroundList.data.Add(defaultskyCellInfo);
                _BackgroundList.Add("Default");
            }
            RefreshSkyboxList();
        }

        public void RefreshSkyboxList() 
        {
            try
            {
                string backgroundDir = Environment.CurrentDirectory + "/CustomBackgrounds/";
                string[] installedBackgrounds = Directory.GetFiles(backgroundDir, "*.*");

                foreach (var background in installedBackgrounds)
                {
                    string backgroundName = background.Substring(backgroundDir.Length);
                    if (background.EndsWith(".png") || background.EndsWith(".jpeg") || background.EndsWith(".jpg") || background.EndsWith(".gif") && !_BackgroundList.Contains(background))
                    {
                        Sprite backgroundIcon = null;
                        if (Settings.instance.EnablePreviews)
                        {
                            backgroundIcon = UIUtilities.LoadSpriteFromFile(background);
                            backgroundIcon.texture.Compress(false);
                            backgroundIcon.texture.Apply();
                        }

                        CustomListTableData.CustomCellInfo customCellInfo = new CustomListTableData.CustomCellInfo(backgroundName, "Image File", backgroundIcon);

                        backgroundList.data.Add(customCellInfo);
                        _BackgroundList.Add(background);
                    }
                }
                backgroundList.tableView.ReloadData();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, IPA.Logging.Logger.Level.Error);
                throw;
            }

        }

    }
}

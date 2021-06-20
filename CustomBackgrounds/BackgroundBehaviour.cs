using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomBackgrounds
{

    public class BackgroundBehaviour : MonoBehaviour
    {
        private AssetBundle shaderAssetBundle;
        private Texture skyboxTexture;
        public GameObject skyboxObject;
        public Shader skyboxShader;
        public string skyTextureName;

        private void Update()
        {
            try
            {
                string curscene = SceneManager.GetActiveScene().name;
                var config = Settings.instance;
                if (skyboxObject == null)
                {
                    LoadBackground(Settings.instance.SelectedBackground);
                }
                skyboxObject.transform.eulerAngles = new Vector3(0, config.RotationOffset - 90, 180);
                switch (curscene)
                {
                    case "GameCore":
                        EnvironmentHider.HideGameEnv();
                        break;
                    case "MainMenu":
                    case "MenuCore":
                        EnvironmentHider.HideMenuEnv();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void LoadBackground(string bgname)
        {
            var config = Settings.instance;
            if (skyboxObject == null)
            {
                skyboxObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                skyboxObject.transform.position = Vector3.zero;
                skyboxObject.layer = config.EnableReflections ? 13 : 0;
                skyboxObject.name = "_SkyBGObject";
            }
            string skyDirectory = Environment.CurrentDirectory + "/CustomBackgrounds/";

            if (bgname == "Default" || !File.Exists(skyDirectory + bgname))
            {
                skyboxObject.SetActive(false);
                return;
            }

            skyboxObject.SetActive(true);
            Renderer skyboxRenderer = skyboxObject.GetComponent<Renderer>();
            Material skyboxMat = null;
            skyboxTexture = BS_Utils.Utilities.UIUtilities.LoadTextureFromFile(skyDirectory + bgname);
            shaderAssetBundle = AssetBundle.LoadFromFile(skyDirectory + "/SkyShaders/CustomBG");

            skyboxShader = shaderAssetBundle.LoadAllAssets<Shader>().First();
            skyboxMat = shaderAssetBundle.LoadAllAssets<Material>().First();

            skyboxRenderer.material = skyboxMat;
            skyboxMat.SetTexture("_Tex", skyboxTexture);
            skyboxMat.color.ColorWithAlpha(0);

            skyTextureName = bgname;

            skyboxObject.transform.localScale = Vector3.one * -800;
            skyboxObject.transform.rotation = Quaternion.Euler(0, config.RotationOffset - 90, 180);

            shaderAssetBundle.Unload(false);
        }

        private void UnloadBackground()
        {
            if (shaderAssetBundle != null)
            {
                shaderAssetBundle.Unload(false);
            }
            skyboxObject.SetActive(false);
        }

    }
}

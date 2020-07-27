using BeatSaberMarkupLanguage;
using HMUI;

namespace CustomBackgrounds
{
    public class BackgroundMenuFlowCoordinator : FlowCoordinator
    {
        private BackgroundMenuViewController bgMenuView;
        private SideConfigMenuViewController sideConfigView;

        private void Awake()
        {
            if (!bgMenuView)
            {
                bgMenuView = BeatSaberUI.CreateViewController<BackgroundMenuViewController>();
            }
            if (!sideConfigView)
            {
                sideConfigView = BeatSaberUI.CreateViewController<SideConfigMenuViewController>();
            }
        }

        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            if (firstActivation)
            {
                title = "Custom Skyboxes";
                showBackButton = true;
                ProvideInitialViewControllers(bgMenuView, sideConfigView);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, false);
        }


    }
}

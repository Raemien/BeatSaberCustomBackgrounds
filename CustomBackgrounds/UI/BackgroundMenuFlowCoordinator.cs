using BeatSaberMarkupLanguage;
using HMUI;

namespace CustomBackgrounds
{
    public class BackgroundMenuFlowCoordinator : FlowCoordinator
    {
        private BackgroundMenuViewController bgMenuView;
        private SideConfigMenuViewController sideConfigView;
        private EnvironmentMenuViewController environmentView;

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
            if (!environmentView)
            {
                environmentView = BeatSaberUI.CreateViewController<EnvironmentMenuViewController>();
            }
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("Backgrounds");
                showBackButton = true;
                ProvideInitialViewControllers(bgMenuView, sideConfigView, environmentView);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal);
        }


    }
}

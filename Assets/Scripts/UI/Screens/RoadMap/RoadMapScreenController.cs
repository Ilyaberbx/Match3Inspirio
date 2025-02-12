using System;
using Better.Locators.Runtime;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.Popups.LevelStart;
using EndlessHeresy.UI.Services.Popups;
using EndlessHeresy.UI.ViewComponents;

namespace EndlessHeresy.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenController : BaseController<RoadMapScreenModel, RoadMapScreenView>
    {
        private IPopupsService _popupsService;

        protected override void Show(RoadMapScreenModel model, RoadMapScreenView view)
        {
            base.Show(model, view);

            _popupsService = ServiceLocator.Get<PopupsService>();

            for (var i = 0; i < View.Nodes.Length; i++)
            {
                var nodeView = View.Nodes[i];
                nodeView.OnClick += OnNodeClicked;
                var hasLevel = i < model.LevelsCount;
                nodeView.SetLevel(i + 1);
                nodeView.SetActive(hasLevel);
            }
        }

        protected override void Hide()
        {
            base.Hide();

            foreach (var nodeView in View.Nodes)
            {
                nodeView.OnClick -= OnNodeClicked;
            }
        }

        private void OnNodeClicked(RoadMapNodeView node)
        {
            var levelIndex = Array.IndexOf(View.Nodes, node);
            _popupsService.Show<LevelStartPopupController, LevelStartPopupModel>(new LevelStartPopupModel(levelIndex));
        }
    }
}
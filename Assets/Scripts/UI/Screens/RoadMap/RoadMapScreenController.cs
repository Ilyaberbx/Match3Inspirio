using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Global.Services.User;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.Popups.LevelStart;
using EndlessHeresy.UI.Services.Popups;
using EndlessHeresy.UI.ViewComponents;

namespace EndlessHeresy.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenController : BaseController<RoadMapScreenModel, RoadMapScreenView>
    {
        private IPopupsService _popupsService;
        private IUserService _userService;

        protected override void Show(RoadMapScreenModel model, RoadMapScreenView view)
        {
            base.Show(model, view);

            _popupsService = ServiceLocator.Get<PopupsService>();
            _userService = ServiceLocator.Get<UserService>();

            foreach (var nodeView in View.Nodes)
            {
                nodeView.OnClick += OnNodeClicked;
            }

            Model.LastLevelIndex.Subscribe(OnLastLevelIndexChanged);
            Model.LastLevelIndex.Value = _userService.LastLevelIndex.Value;
        }

        protected override void Hide()
        {
            base.Hide();

            foreach (var nodeView in View.Nodes)
            {
                nodeView.OnClick -= OnNodeClicked;
            }

            Model.LastLevelIndex.Unsubscribe(OnLastLevelIndexChanged);
        }

        private void OnNodeClicked(RoadMapNodeView node)
        {
            var levelIndex = Array.IndexOf(View.Nodes, node);
            _popupsService
                .ShowAsync<LevelStartPopupController, LevelStartPopupModel>(new LevelStartPopupModel(levelIndex))
                .Forget();
        }

        private void OnLastLevelIndexChanged(int index)
        {
            for (var i = 0; i < View.Nodes.Length; i++)
            {
                var nodeView = View.Nodes[i];

                nodeView.SetAvailable(i <= index);
                var level = i + 1;
                nodeView.SetLevelText(level.ToString());
            }
        }
    }
}
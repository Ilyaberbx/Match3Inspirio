using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Data.Persistent;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Global.Services.Persistence;
using Inspirio.UI.Core;
using Inspirio.UI.Popups.LevelStart;
using Inspirio.UI.Services.Popups;
using Inspirio.UI.ViewComponents;

namespace Inspirio.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenController : BaseController<RoadMapScreenModel, RoadMapScreenView>
    {
        private IPopupsService _popupsService;
        private IUserService _userService;
        private ILevelService _levelService;

        protected override void Show(RoadMapScreenModel model, RoadMapScreenView view)
        {
            base.Show(model, view);

            _popupsService = ServiceLocator.Get<PopupsService>();
            _userService = ServiceLocator.Get<UserService>();
            _levelService = ServiceLocator.Get<LevelService>();

            foreach (var nodeView in View.Nodes)
            {
                nodeView.OnClick += OnNodeClicked;
            }

            Model.LastLevelIndex.Subscribe(OnLastLevelIndexChanged);
            Model.Levels.Subscribe(OnLevelsChanged);
            Model.LastLevelIndex.Value = _userService.LastLevelIndex.Value;
            Model.Levels.Value = _userService.Levels.Value;
        }

        protected override void Hide()
        {
            base.Hide();

            foreach (var nodeView in View.Nodes)
            {
                nodeView.OnClick -= OnNodeClicked;
            }

            Model.LastLevelIndex.Unsubscribe(OnLastLevelIndexChanged);
            Model.Levels.Unsubscribe(OnLevelsChanged);
        }

        private void OnNodeClicked(RoadMapNodeView node)
        {
            var levelIndex = Array.IndexOf(View.Nodes, node);
            _levelService.FireSelectLevel(levelIndex);
            _popupsService
                .ShowAsync<LevelStartPopupController, LevelStartPopupModel>(LevelStartPopupModel.New())
                .Forget();
        }

        private void OnLevelsChanged(List<LevelData> levels)
        {
            for (var i = 0; i < View.Nodes.Length; i++)
            {
                var nodeView = View.Nodes[i];
                var levelData = levels[i];
                var stars = levelData.Stars;

                for (var j = 0; j < nodeView.StarViews.Length; j++)
                {
                    var starView = nodeView.StarViews[j];
                    starView.SetFilled(stars - 1 >= j);
                }
            }
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
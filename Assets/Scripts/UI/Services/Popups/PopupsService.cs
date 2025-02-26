using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Inspirio.Global.Services.AssetsManagement;
using Inspirio.UI.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inspirio.UI.Services.Popups
{
    [Serializable]
    public sealed class PopupsService : PocoService, IPopupsService
    {
        private const string ControllerPostfix = "Controller";
        private const string ViewPathFormat = "Popups/{0}";

        [SerializeField] private CanvasGroup _backgroundGroup;
        [SerializeField] private Transform _root;

        private BaseController _currentController;
        private IAssetsService _assetsService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            HideBackground();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _assetsService = ServiceLocator.Get<ResourcesAssetsService>();
            return Task.CompletedTask;
        }

        public async Task<TController> ShowAsync<TController, TModel>(TModel model, bool showBackground = true)
            where TController : BaseController<TModel>, new()
            where TModel : IModel
        {
            var controllerName = typeof(TController).Name;
            var viewKey = controllerName.Replace(ControllerPostfix, string.Empty);
            var viewPrefab = await _assetsService.Load<BaseView>(string.Format(ViewPathFormat, viewKey));

            if (viewPrefab == null)
            {
                return null;
            }

            Hide();

            var at = _root.GetComponent<RectTransform>().position;
            var view = Object.Instantiate(viewPrefab, at, Quaternion.identity, _root);
            var controller = new TController();

            controller.Initialize(view, model);
            _currentController = controller;

            if (showBackground)
            {
                ShowBackground();
            }
            else
            {
                HideBackground();
            }

            return controller;
        }

        public void Hide()
        {
            if (_currentController == null)
            {
                return;
            }

            var viewGameObject = _currentController.DerivedView.gameObject;
            _currentController.Dispose();
            _currentController = null;
            Object.Destroy(viewGameObject);
            HideBackground();
        }

        private void ShowBackground()
        {
            _backgroundGroup.alpha = 1;
            _backgroundGroup.interactable = true;
            _backgroundGroup.blocksRaycasts = true;
        }

        private void HideBackground()
        {
            _backgroundGroup.alpha = 0;
            _backgroundGroup.interactable = false;
            _backgroundGroup.blocksRaycasts = false;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Inspirio.Global.Services.AssetsManagement;
using Inspirio.UI.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inspirio.UI.Services.Screens
{
    [Serializable]
    public sealed class ScreensService : PocoService, IScreensService
    {
        private const string ControllerPostfix = "Controller";
        private const string ViewPathFormat = "Screens/{0}";

        [SerializeField] private Transform _root;

        private BaseController _currentController;
        private IAssetsService _assetsService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _assetsService = ServiceLocator.Get<ResourcesAssetsService>();
            return Task.CompletedTask;
        }

        public async Task<TController> ShowAsync<TController, TModel>(TModel model)
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
        }
    }
}
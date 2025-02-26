using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Inspirio.Global.Services.AssetsManagement;
using Inspirio.UI.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inspirio.UI.Services.Huds
{
    [Serializable]
    public sealed class HudsService : PocoService, IHudsService
    {
        private const string ControllerPostfix = "Controller";
        private const string ViewPathFormat = "Huds/{0}";

        [SerializeField] private Transform _root;

        private List<BaseController> _controllers = new();
        private IAssetsService _assetsService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _assetsService = ServiceLocator.Get<ResourcesAssetsService>();
            return Task.CompletedTask;
        }

        public async Task<TController> ShowAsync<TController, TModel>(TModel model, ShowType showType)
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

            if (showType == ShowType.Single)
            {
                HideAll();
            }

            var at = _root.GetComponent<RectTransform>().position;
            var view = Object.Instantiate(viewPrefab, at, Quaternion.identity, _root);
            var controller = new TController();

            controller.Initialize(view, model);
            _controllers.Add(controller);

            return controller;
        }

        public void HideAll()
        {
            if (_controllers.IsNullOrEmpty())
            {
                return;
            }

            foreach (var controller in _controllers)
            {
                var viewGameObject = controller.DerivedView.gameObject;
                controller.Dispose();
                Object.Destroy(viewGameObject);
            }

            _controllers.Clear();
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using DG.Tweening;
using Inspirio.Extensions;
using Inspirio.Gameplay.Services.StaticDataManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inspirio.Gameplay.Services.Vfx
{
    [Serializable]
    public sealed class VfxService : PocoService, IVfxService
    {
        [SerializeField] private RectTransform _root;

        private IGameplayStaticDataService _gameplayStaticDataService;

        public async Task PlayAsync(VfxType vfxType, RectTransform rectTransform, float duration)
        {
            var data = _gameplayStaticDataService.GetVfxData(vfxType);

            if (data == null)
                return;

            await PlayVfxAsync(data.Prefab, rectTransform, duration);
        }

        private async Task PlayVfxAsync(RectTransform prefab, RectTransform targetTransform, float duration)
        {
            var vfxObj = CreateVfxObject(prefab, targetTransform);
            await AnimateVfxAsync(vfxObj, duration);
            Object.Destroy(vfxObj.gameObject);
        }

        private RectTransform CreateVfxObject(RectTransform prefab, RectTransform targetTransform)
        {
            var vfxObj = Object.Instantiate(prefab, _root);
            vfxObj.SetAsLastSibling();
            vfxObj.position = targetTransform.position;
            return vfxObj;
        }

        private async Task AnimateVfxAsync(RectTransform vfxObj, float duration)
        {
            var halfDuration = duration / 2;
            await vfxObj.transform.DOScale(Vector3.zero, halfDuration).From().AsTask(CancellationToken.None);
            await vfxObj.transform.DOScale(Vector3.zero, halfDuration).AsTask(CancellationToken.None);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            InitializeServices();
            return Task.CompletedTask;
        }

        private void InitializeServices()
        {
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
        }
    }
}
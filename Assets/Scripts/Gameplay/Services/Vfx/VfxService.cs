using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using DG.Tweening;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Services.StaticDataManagement;
using EndlessHeresy.Gameplay.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EndlessHeresy.Gameplay.Services.Vfx
{
    [Serializable]
    public sealed class VfxService : PocoService, IVfxService
    {
        [SerializeField] private RectTransform _root;

        private IGameplayStaticDataService _gameplayStaticDataService;
        private VfxConfiguration _vfxConfiguration;

        public async Task PlayAsync(VfxType vfxType, RectTransform rectTransform, float duration)
        {
            var data = _vfxConfiguration.Vfx.FirstOrDefault(temp => temp.Type == vfxType);

            if (data == null)
            {
                return;
            }

            var halfDuration = duration / 2;
            var prefab = data.Prefab;
            var vfxObj = Object.Instantiate(prefab, _root);
            vfxObj.SetAsLastSibling();
            vfxObj.position = rectTransform.position;

            await vfxObj
                .transform
                .DOScale(Vector3.zero, halfDuration)
                .From()
                .AsTask(CancellationToken.None);

            await vfxObj
                .transform
                .DOScale(Vector3.zero, halfDuration)
                .AsTask(CancellationToken.None);
            Object.Destroy(vfxObj.gameObject);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _vfxConfiguration = _gameplayStaticDataService.GetVfxConfiguration();
            return Task.CompletedTask;
        }
    }
}
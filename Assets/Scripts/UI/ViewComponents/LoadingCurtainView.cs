using System.Threading.Tasks;
using Better.Commons.Runtime.Components.UI;
using DG.Tweening;
using Inspirio.Extensions;
using UnityEngine;

namespace Inspirio.UI.ViewComponents
{
    public sealed class LoadingCurtainView : UIMonoBehaviour
    {
        [SerializeField] private CanvasGroup _group;

        public async Task ShowAsync(float duration)
        {
            await _group
                .DOFade(1, duration)
                .AsTask(destroyCancellationToken);

            _group.alpha = 1;
            _group.interactable = true;
            _group.blocksRaycasts = true;
        }

        public async Task HideAsync(float duration)
        {
            await _group
                .DOFade(0, duration)
                .AsTask(destroyCancellationToken);

            _group.alpha = 0;
            _group.interactable = false;
            _group.blocksRaycasts = false;
        }
    }
}
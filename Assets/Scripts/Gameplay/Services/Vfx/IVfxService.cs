using System.Threading.Tasks;
using UnityEngine;

namespace Inspirio.Gameplay.Services.Vfx
{
    public interface IVfxService
    {
        public Task PlayAsync(VfxType vfxType, RectTransform rectTransform, float duration);
    }
}
using System.Threading.Tasks;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Vfx
{
    public interface IVfxService
    {
        public Task PlayAsync(VfxType vfxType, RectTransform rectTransform, float duration);
    }
}
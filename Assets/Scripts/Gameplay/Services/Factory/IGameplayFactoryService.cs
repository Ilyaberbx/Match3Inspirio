using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Actors;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        Task<GameBoardActor> CreateGameBoardAsync(int index);
        Task<ItemActor> CreateItemAsync(int index, Transform parent);
        Task<TileActor> CreateTileAsync(int x, int y, Transform parent);
        void Dispose(MonoActor actor);
    }
}
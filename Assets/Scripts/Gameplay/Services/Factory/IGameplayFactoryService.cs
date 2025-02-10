using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Actors;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        Task<GameBoardActor> CreateGameBoardAsync(int levelId);
        Task<ItemActor> CreateItemAsync(int index, Transform parent);
        Task<TileActor> CreateTileAsync(int x, int y, Transform parent);
    }
}
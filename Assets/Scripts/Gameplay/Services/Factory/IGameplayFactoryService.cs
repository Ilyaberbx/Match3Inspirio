using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Actors;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        Task<GameBoardActor> CreateGameBoardActor();
        Task<TileActor> CreateTileActor(int x, int y, Transform parent);
    }
}
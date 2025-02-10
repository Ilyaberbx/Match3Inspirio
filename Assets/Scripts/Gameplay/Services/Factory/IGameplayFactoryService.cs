using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Actors.GameBoard;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        Task<GameBoardActor> CreateGameBoardActor();
    }
}
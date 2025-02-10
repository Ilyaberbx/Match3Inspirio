using System.Threading.Tasks;
using EndlessHeresy.Global.States;

namespace EndlessHeresy.Global.Services.StatesManagement
{
    public interface IGameStatesService
    {
        Task ChangeStateAsync<TState>() where TState : BaseGameState, new();
        void Dispose();
    }
}
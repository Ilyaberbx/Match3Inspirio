using System.Threading.Tasks;
using Inspirio.Global.States;

namespace Inspirio.Global.Services.StatesManagement
{
    public interface IGameStatesService
    {
        Task ChangeStateAsync<TState>() where TState : BaseGameState, new();
        Task ChangeStateAsync<TState>(TState state) where TState : BaseGameState;
        void Dispose();
    }
}
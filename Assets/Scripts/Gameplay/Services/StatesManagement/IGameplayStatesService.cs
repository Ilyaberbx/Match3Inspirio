using System.Threading.Tasks;
using Inspirio.Gameplay.States;

namespace Inspirio.Gameplay.Services.StatesManagement
{
    public interface IGameplayStatesService
    {
        Task ChangeStateAsync<TState>() where TState : BaseGameplayState, new();
        void Dispose();
    }
}
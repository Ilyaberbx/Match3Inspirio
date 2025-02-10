using System.Threading.Tasks;
using EndlessHeresy.Gameplay.States;

namespace EndlessHeresy.Gameplay.Services.StatesManagement
{
    public interface IGameplayStatesService
    {
        Task ChangeStateAsync<TState>() where TState : BaseGameplayState, new();
        void Dispose();
    }
}
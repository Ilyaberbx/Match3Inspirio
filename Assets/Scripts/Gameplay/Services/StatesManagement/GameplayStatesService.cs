using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Better.StateMachine.Runtime;
using Inspirio.Commons;
using Inspirio.Gameplay.States;

namespace Inspirio.Gameplay.Services.StatesManagement
{
    [Serializable]
    public sealed class GameplayStatesService : PocoService, IDisposable, IGameplayStatesService
    {
        private IStateMachine<BaseGameplayState> _stateMachine;
        private CancellationTokenSource _tokenSource;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stateMachine = new StateMachine<BaseGameplayState>();
            _tokenSource = new CancellationTokenSource();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _stateMachine.AddModule(new LoggerModule<BaseGameplayState>());
            _stateMachine.Run();
            return Task.CompletedTask;
        }

        public Task ChangeStateAsync<TState>() where TState : BaseGameplayState, new()
        {
            var state = new TState();
            return _stateMachine.ChangeStateAsync(state, _tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
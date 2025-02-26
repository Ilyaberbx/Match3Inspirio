using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Better.StateMachine.Runtime;
using Inspirio.Commons;
using Inspirio.Global.States;

namespace Inspirio.Global.Services.StatesManagement
{
    [Serializable]
    public sealed class GameStatesService : PocoService, IDisposable, IGameStatesService
    {
        private IStateMachine<BaseGameState> _stateMachine;
        private CancellationTokenSource _tokenSource;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stateMachine = new StateMachine<BaseGameState>();
            _stateMachine.AddModule(new LoggerModule<BaseGameState>());
            _stateMachine.Run();
            _tokenSource = new CancellationTokenSource();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task ChangeStateAsync<TState>() where TState : BaseGameState, new()
        {
            var state = new TState();
            return _stateMachine.ChangeStateAsync(state, _tokenSource.Token);
        }

        public Task ChangeStateAsync<TState>(TState state) where TState : BaseGameState =>
            _stateMachine.ChangeStateAsync(state, _tokenSource.Token);

        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Implementations
{
    public sealed class InitializingState : GameBoardState
    {
        public InitializingState(GameBoardActor context) : base(context)
        {
        }

        public override Task EnterAsync(CancellationToken token) => Context.InitializeAsync();

        public override void OnEntered()
        {
            Context.HandlePostInitialize();
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}
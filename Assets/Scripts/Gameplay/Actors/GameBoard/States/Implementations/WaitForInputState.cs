using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Implementations
{
    public sealed class WaitForInputState : GameBoardState
    {
        public WaitForInputState(GameBoardActor context) : base(context)
        {
        }

        public override Task EnterAsync(CancellationToken token)
        {
            Context.EnableInput();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            Context.DisableInput();
            return Task.CompletedTask;
        }
    }
}
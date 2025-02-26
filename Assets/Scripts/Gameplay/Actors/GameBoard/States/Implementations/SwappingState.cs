using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Implementations
{
    public sealed class SwappingState : GameBoardState
    {
        private readonly ItemActor _first;
        private readonly ItemActor _second;

        public SwappingState(GameBoardActor context, ItemActor first, ItemActor second) : base(context)
        {
            _first = first;
            _second = second;
        }
        

        public override Task EnterAsync(CancellationToken token)
        {
            return Context.SwapAsync(_first, _second);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override void OnEntered()
        {
            Context.HandleSwapFinished();
        }
    }
}
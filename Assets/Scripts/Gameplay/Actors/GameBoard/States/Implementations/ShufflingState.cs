using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Implementations
{
    public sealed class ShufflingState : GameBoardState
    {
        private readonly IReadOnlyList<TileActor> _tilesToShuffle;

        public ShufflingState(GameBoardActor context, IReadOnlyList<TileActor> tilesToShuffle) : base(context)
        {
            _tilesToShuffle = tilesToShuffle;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            return Context.ShuffleTilesAsync(_tilesToShuffle);
        }

        public override void OnEntered()
        {
            Context.HandlePostShuffle();
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
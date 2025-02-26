using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Implementations
{
    public sealed class InflatingState : GameBoardState
    {
        private readonly IReadOnlyList<TileActor> _tilesToInflate;

        public InflatingState(GameBoardActor context, IReadOnlyList<TileActor> tilesToInflate) : base(context)
        {
            _tilesToInflate = tilesToInflate;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var items = _tilesToInflate.Select(temp => temp.Item).ToList();
            return Context.InflateItemsAsync(items);
        }

        public override void OnEntered()
        {
           Context.HandlePostInflate(_tilesToInflate);
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}
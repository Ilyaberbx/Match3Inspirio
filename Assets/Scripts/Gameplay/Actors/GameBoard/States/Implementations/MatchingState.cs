using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Implementations
{
    public sealed class MatchingState : GameBoardState
    {
        private readonly IReadOnlyList<TileActor> _tilesToMatch;

        public MatchingState(GameBoardActor context, IReadOnlyList<TileActor> tilesToMatch) : base(context) =>
            _tilesToMatch = tilesToMatch;

        public override Task EnterAsync(CancellationToken token)
        {
            var itemsToMatch = _tilesToMatch.Select(temp => temp.Item).ToList();
            Context.HandlePreMatch(_tilesToMatch);
            return Context.MatchItemsAsync(itemsToMatch);
        }

        public override void OnEntered() => Context.HandlePostMatch(_tilesToMatch);

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}
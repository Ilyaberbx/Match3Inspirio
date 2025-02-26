using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Implementations
{
    public sealed class DeflatingState : GameBoardState
    {
        private readonly IReadOnlyList<TileActor> _tilesToDeflate;

        public DeflatingState(GameBoardActor context, IReadOnlyList<TileActor> tilesToDeflate) : base(context)
        {
            _tilesToDeflate = tilesToDeflate;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var items = _tilesToDeflate.Select(temp => temp.Item).ToList();
            return Context.DeflateItemsAsync(items);
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;

        public override async void OnEntered()
        {
            try
            {
                await Context.RewriteTilesAsync(_tilesToDeflate);
                Context.HandlePostDeflate(_tilesToDeflate);
            }
            catch (Exception e)
            {
                DebugUtility.LogException(e);
            }
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Modules;

namespace Inspirio.Gameplay.States
{
    public sealed class RoadMapState : BaseGameplayState
    {
        public override async Task EnterAsync(CancellationToken token)
        {
            await AddModuleAsync<RoadMapModule>();
        }

        public override Task ExitAsync(CancellationToken token)
        {
            DisposeAllModules();
            return Task.CompletedTask;
        }
    }
}
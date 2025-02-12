using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Modules;

namespace EndlessHeresy.Gameplay.States
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
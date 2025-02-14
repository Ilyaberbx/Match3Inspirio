using System.Threading;
using System.Threading.Tasks;
using Inspirio.Gameplay.Modules;

namespace Inspirio.Gameplay.States
{
    public sealed class MainMenuState : BaseGameplayState
    {
        public override async Task EnterAsync(CancellationToken token)
        {
            await AddModuleAsync<MainMenuModule>();
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
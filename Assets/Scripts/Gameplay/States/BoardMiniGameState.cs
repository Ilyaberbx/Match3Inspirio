using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Modules;

namespace EndlessHeresy.Gameplay.States
{
    public sealed class BoardMiniGameState : BaseGameplayState
    {
        public override async Task EnterAsync(CancellationToken token)
        {
            await AddModuleAsync<InitializeBoardModule>();
            await AddModuleAsync<PauseModule>();
            await AddModuleAsync<ScoreModule>();
            await AddModuleAsync<GameExodusModule>();
        }

        public override Task ExitAsync(CancellationToken token)
        {
            DisposeAllModules();
            return Task.CompletedTask;
        }
    }
}
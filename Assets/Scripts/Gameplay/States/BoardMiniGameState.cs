using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Modules;
using Inspirio.UI.Services.Huds;
using Inspirio.UI.Services.Popups;

namespace Inspirio.Gameplay.States
{
    public sealed class BoardMiniGameState : BaseGameplayState
    {
        private IPopupsService _popupsService;
        private IHudsService _hudsService;

        public override async Task EnterAsync(CancellationToken token)
        {
            InitializeServices();
            await AddModuleAsync<GameBoardModule>();
            await AddModuleAsync<PauseModule>();
            await AddModuleAsync<ScoreModule>();
            await AddModuleAsync<GameExodusModule>();
            await AddModuleAsync<ReturnModule>();
            await AddModuleAsync<EffectsModule>();
            await AddModuleAsync<AnalyticsModule>();
        }

        private void InitializeServices()
        {
            _popupsService = ServiceLocator.Get<PopupsService>();
            _hudsService = ServiceLocator.Get<HudsService>();
        }

        public override Task ExitAsync(CancellationToken token)
        {
            DisposeAllModules();
            _popupsService.Hide();
            _hudsService.HideAll();
            return Task.CompletedTask;
        }
    }
}
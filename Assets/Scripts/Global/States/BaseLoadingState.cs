using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Inspirio.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inspirio.Global.States
{
    public abstract class BaseLoadingState : BaseGameState
    {
        private MonoContextAdapter _monoContext;
        protected abstract string GetSceneName();

        public sealed override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);
            await LoadRequiredScene();
            await InstallDependencies();
            await OnSceneLoaded();
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            var sceneName = GetSceneName();
            _monoContext.Exit();
            await SceneManager.UnloadSceneAsync(sceneName);
        }

        protected abstract Task OnSceneLoaded();

        private async Task InstallDependencies()
        {
            _monoContext = Object.FindObjectOfType<MonoContextAdapter>();
            await _monoContext.EnterAsync();
        }

        private async Task LoadRequiredScene()
        {
            var sceneName = GetSceneName();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            var loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(loadedScene);
        }
    }
}
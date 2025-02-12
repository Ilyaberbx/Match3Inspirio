using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndlessHeresy.Global.States
{
    public abstract class BaseLoadingState : BaseGameState
    {
        private MonoContextAdapter _monoContext;
        protected abstract string GetSceneName();

        public sealed override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);

            var sceneName = GetSceneName();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            var loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(loadedScene);
            _monoContext = Object.FindObjectOfType<MonoContextAdapter>();
            await _monoContext.EnterAsync();
            await OnSceneLoaded();
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            var sceneName = GetSceneName();
            _monoContext.Exit();
            await SceneManager.UnloadSceneAsync(sceneName);
        }

        protected abstract Task OnSceneLoaded();
    }
}
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Global.Services.StatesManagement;
using Inspirio.Global.States;
using UnityEngine;

namespace Inspirio
{
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private IGameStatesService _gameStatesService;

        private void Start()
        {
            _gameStatesService = ServiceLocator.Get<GameStatesService>();
            _gameStatesService.ChangeStateAsync<AppInitializationState>().Forget();
        }

        private void OnDestroy()
        {
            _gameStatesService.Dispose();
        }
    }
}
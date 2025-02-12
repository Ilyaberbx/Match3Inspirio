using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Global.Services.StatesManagement;
using EndlessHeresy.Global.States;
using UnityEngine;

namespace EndlessHeresy
{
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private IGameStatesService _gameStatesService;

        private void Start()
        {
            _gameStatesService = ServiceLocator.Get<GameStatesService>();
            _gameStatesService.ChangeStateAsync<GameInitializationState>().Forget();
        }

        private void OnDestroy()
        {
            _gameStatesService.Dispose();
        }
    }
}
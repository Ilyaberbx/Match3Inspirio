using System.Collections.Generic;
using System.Threading.Tasks;
using Better.StateMachine.Runtime.States;
using Inspirio.Gameplay.Modules;

namespace Inspirio.Gameplay.States
{
    public abstract class BaseGameplayState : BaseState
    {
        private readonly List<BaseGameplayModule> _modules = new();

        protected async Task AddModuleAsync<TModule>() where TModule : BaseGameplayModule, new()
        {
            var module = new TModule();
            await module.InitializeAsync();
            _modules.Add(module);
        }

        protected void DisposeAllModules()
        {
            foreach (var module in _modules)
            {
                module.Dispose();
            }

            _modules.Clear();
        }

        public sealed override void OnEntered()
        {
        }

        public sealed override void OnExited()
        {
        }
    }
}
using System;
using System.Threading.Tasks;

namespace EndlessHeresy.Gameplay.Modules
{
    public abstract class BaseGameplayModule : IDisposable
    {
        public abstract Task InitializeAsync();

        public abstract void Dispose();
    }
}
using System;
using System.Threading.Tasks;

namespace Inspirio.Gameplay.Modules
{
    public abstract class BaseGameplayModule : IDisposable
    {
        public abstract Task InitializeAsync();

        public abstract void Dispose();
    }
}
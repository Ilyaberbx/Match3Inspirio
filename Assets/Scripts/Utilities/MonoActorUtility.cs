using Inspirio.Gameplay.Core;
using Inspirio.Gameplay.Core.Builder;

namespace Inspirio.Utilities
{
    public static class MonoActorUtility
    {
        public static MonoActorBuilder<TActor> GetBuilder<TActor>() where TActor : MonoActor =>
            new(new ComponentsLocator());
    }
}
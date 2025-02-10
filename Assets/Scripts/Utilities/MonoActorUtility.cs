using EndlessHeresy.Core;
using EndlessHeresy.Core.Builder;

namespace EndlessHeresy.Utilities
{
    public static class MonoActorUtility
    {
        public static MonoActorBuilder<TActor> GetBuilder<TActor>() where TActor : MonoActor =>
            new(new ComponentsLocator());
    }
}
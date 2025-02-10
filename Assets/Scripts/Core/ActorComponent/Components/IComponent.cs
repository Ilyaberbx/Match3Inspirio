using System.Threading.Tasks;

namespace EndlessHeresy.Core
{
    public interface IComponent
    {
        public void SetActor(IActor actor);
        public Task InitializeAsync();
        public void Dispose();
    }
}
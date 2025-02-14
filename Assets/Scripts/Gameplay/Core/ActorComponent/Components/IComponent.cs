using System.Threading.Tasks;

namespace Inspirio.Gameplay.Core
{
    public interface IComponent
    {
        public void SetActor(IActor actor);
        public Task InitializeAsync();
        public void Dispose();
    }
}
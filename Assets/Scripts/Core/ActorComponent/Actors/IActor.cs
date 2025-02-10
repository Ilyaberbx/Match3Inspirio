using System.Threading.Tasks;
using UnityEngine;

namespace EndlessHeresy.Core
{
    public interface IActor : IComponentsLocator
    {
        public Task InitializeAsync(IComponentsLocator locator);
        public void Dispose();
        GameObject GameObject { get; }
        Transform Transform { get; }
        bool ActiveSelf { get; }
    }
}
using System.Collections.Generic;

namespace Inspirio.Gameplay.Core
{
    public interface IComponentsLocator
    {
        public IEnumerable<IComponent> GetAllComponents();
        public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : IComponent;
        public TComponent GetComponent<TComponent>() where TComponent : IComponent;
        public bool TryAddComponent<TComponent>(TComponent component) where TComponent : IComponent;
        public bool TryRemoveComponent<TComponent>(TComponent component) where TComponent : IComponent;
    }
}
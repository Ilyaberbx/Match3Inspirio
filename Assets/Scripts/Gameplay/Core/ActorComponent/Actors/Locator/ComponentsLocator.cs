using System;
using System.Collections.Generic;
using Better.Locators.Runtime;

namespace Inspirio.Gameplay.Core
{
    public class ComponentsLocator : Locator<Type, IComponent>, IComponentsLocator
    {
        public IEnumerable<IComponent> GetAllComponents() => GetElements();

        public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : IComponent
        {
            var componentType = typeof(TComponent);
            var hasComponent = TryGet(componentType, out var derivedComponent);

            if (!hasComponent)
            {
                component = default;
                return false;
            }

            if (derivedComponent is TComponent castedComponent)
            {
                component = castedComponent;
                return true;
            }

            component = default;
            return false;
        }

        public TComponent GetComponent<TComponent>() where TComponent : IComponent =>
            TryGetComponent(out TComponent component) ? component : default;

        public bool TryAddComponent<TComponent>(TComponent component) where TComponent : IComponent =>
            TryAdd(component.GetType(), component);

        public bool TryRemoveComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            if (Remove(component))
            {
                component.Dispose();
                return true;
            }

            return false;
        }
    }
}
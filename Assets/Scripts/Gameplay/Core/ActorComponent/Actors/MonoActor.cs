using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Inspirio.Gameplay.Core
{
    public abstract class MonoActor : MonoBehaviour, IActor
    {
        [SerializeField] protected MonoComponent[] MonoComponents;

        private IComponentsLocator _componentsLocator;
        private IEnumerable<IComponent> _components;
        private Transform _transform;
        private GameObject _gameObject;

        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;
        public bool ActiveSelf => GameObject.activeSelf;

        public async Task InitializeAsync(IComponentsLocator locator)
        {
            _componentsLocator = locator;
            InitializeMonoComponents();
            _components = _componentsLocator.GetAllComponents();
            
            var initializationTasks = new List<Task>();

            foreach (var component in _components)
            {
                component.SetActor(this);
                var initializationTask = component.InitializeAsync();
                initializationTasks.Add(initializationTask);
            }

            if (initializationTasks.IsNullOrEmpty())
            {
                return;
            }

            await Task.WhenAll(initializationTasks);
            await OnInitializeAsync();
        }

        public void Dispose()
        {
            OnDispose();
            _componentsLocator = null;
        }

        public IEnumerable<IComponent> GetAllComponents() => _componentsLocator.GetAllComponents();

        public new bool TryGetComponent<TComponent>(out TComponent component) where TComponent : IComponent =>
            _componentsLocator.TryGetComponent(out component);

        public new TComponent GetComponent<TComponent>() where TComponent : IComponent =>
            _componentsLocator.GetComponent<TComponent>();

        public bool TryAddComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            if (_componentsLocator.TryAddComponent(component))
            {
                component.SetActor(this);
                return true;
            }

            return false;
        }

        public bool TryRemoveComponent<TComponent>(TComponent component) where TComponent : IComponent =>
            _componentsLocator.TryRemoveComponent(component);

        protected virtual Task OnInitializeAsync() => Task.CompletedTask;

        protected virtual void OnDispose()
        {
            if (_components.IsNullOrEmpty())
            {
                return;
            }

            foreach (var component in _components)
            {
                component.Dispose();
            }
        }

        private void InitializeMonoComponents()
        {
            _transform = transform;
            _gameObject = gameObject;

            foreach (var monoComponent in MonoComponents)
            {
                TryAddComponent(monoComponent);
            }
        }
    }
}
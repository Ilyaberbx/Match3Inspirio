using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inspirio.Gameplay.Core.Builder
{
    public sealed class MonoActorBuilder<TActor> where TActor : MonoActor
    {
        private const string NoPrefabProvidedMessage = "No prefab provided";

        private Vector2 _at;
        private Component _prefab;
        private Transform _parent;
        private TActor _actor;
        private Quaternion _rotation;
        private readonly IComponentsLocator _forComponents;

        private TActor Actor
        {
            get
            {
                if (_actor == null)
                {
                    var actorObject = CreateActorObject();
                    _actor = actorObject.GetComponent<TActor>();
                }

                return _actor;
            }
        }

        public MonoActorBuilder(IComponentsLocator forComponents)
        {
            _forComponents = forComponents;
        }

        public MonoActorBuilder<TActor> ForPrefab(Component prefab)
        {
            _prefab = prefab;
            return this;
        }

        public MonoActorBuilder<TActor> WithParent(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public MonoActorBuilder<TActor> WithComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            _forComponents.TryAddComponent(component);
            return this;
        }

        public async Task<TActor> Build()
        {
            await Actor.InitializeAsync(_forComponents);
            return Actor;
        }

        private Component CreateActorObject()
        {
            if (_prefab == null)
            {
                DebugUtility.LogException<NullReferenceException>(NoPrefabProvidedMessage);
            }

            return Object.Instantiate(_prefab, _parent);
        }
    }
}
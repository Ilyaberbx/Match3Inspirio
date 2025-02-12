using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;

namespace EndlessHeresy.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenView : BaseView
    {
        [SerializeField] private RoadMapNodeView[] _nodes;

        public RoadMapNodeView[] Nodes => _nodes;
    }
}
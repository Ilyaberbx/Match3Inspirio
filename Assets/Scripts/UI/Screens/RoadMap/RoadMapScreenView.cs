using Inspirio.UI.MVC;
using Inspirio.UI.ViewComponents;
using UnityEngine;

namespace Inspirio.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenView : BaseView
    {
        [SerializeField] private RoadMapNodeView[] _nodes;

        public RoadMapNodeView[] Nodes => _nodes;
    }
}
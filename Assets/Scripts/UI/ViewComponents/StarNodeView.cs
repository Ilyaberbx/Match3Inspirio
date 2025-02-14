using Better.Commons.Runtime.Components.UI;
using UnityEngine;

namespace Inspirio.UI.ViewComponents
{
    public sealed class StarNodeView : UIMonoBehaviour
    {
        [SerializeField] private GameObject _filledContainer;
        [SerializeField] private GameObject _emptyContainer;

        public void SetFilled(bool filled)
        {
            _filledContainer.SetActive(filled);
            _emptyContainer.SetActive(!filled);
        }
    }
}
using Better.Commons.Runtime.Components.UI;
using UnityEngine;

namespace EndlessHeresy.UI.ViewComponents
{
    public sealed class StarView : UIMonoBehaviour
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
using System;
using Better.Commons.Runtime.Components.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.ViewComponents
{
    public sealed class RoadMapNodeView : UIMonoBehaviour
    {
        public event Action<RoadMapNodeView> OnClick;

        [SerializeField] private StarNodeView[] _starViews;
        [SerializeField] private GameObject _activeContainer;
        [SerializeField] private GameObject _inactiveContainer;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _actionButton;

        public StarNodeView[] StarViews => _starViews;
        private void OnEnable() => _actionButton.onClick.AddListener(OnActionButtonClicked);
        private void OnDisable() => _actionButton.onClick.RemoveListener(OnActionButtonClicked);
        public void SetLevelText(string levelText) => _levelText.text = levelText;
        public void SetAvailable(bool active)
        {
            _activeContainer.SetActive(active);
            _inactiveContainer.SetActive(!active);
            _actionButton.interactable = active;
        }

        private void OnActionButtonClicked() => OnClick?.Invoke(this);
    }
}
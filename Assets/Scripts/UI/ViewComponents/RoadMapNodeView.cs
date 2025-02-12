using System;
using Better.Commons.Runtime.Components.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.ViewComponents
{
    public sealed class RoadMapNodeView : UIMonoBehaviour
    {
        public event Action<RoadMapNodeView> OnClick;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _actionButton;

        private void OnEnable() => _actionButton.onClick.AddListener(OnActionButtonClicked);
        private void OnDisable() => _actionButton.onClick.RemoveListener(OnActionButtonClicked);

        public void SetLevel(int level) => _levelText.text = level.ToString();
        public void SetActive(bool active) => gameObject.SetActive(active);
        private void OnActionButtonClicked() => OnClick?.Invoke(this);
    }
}
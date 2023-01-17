using UnityEngine;
using UnityEngine.UI;

namespace UI.Animations
{
    public class ToggleButtonAnimator : MonoBehaviour
    {
        [SerializeField] private Toggle _toggleButton;
        [SerializeField] private RectTransform _movableToggleCheck;
        [SerializeField] private Image _toggleCheckBg;
        [SerializeField] private Color _toggleCheckBgEnabledColor = Color.green;

        private Color _toggleCheckBgDisabledColor;
        
        private void OnValidate()
        {
            if (_toggleButton == null)
                _toggleButton = GetComponentInChildren<Toggle>();
        }

        private void Awake()
        {
            _toggleCheckBgDisabledColor = _toggleCheckBg.color;
            _toggleButton.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnEnable()
        {
            OnToggleValueChanged(_toggleButton.isOn);
        }

        private void OnDestroy()
        {
            _toggleButton.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool newValue)
        {
            var currentX = newValue ? 1f : 0f;
            _movableToggleCheck.anchorMin = new Vector2(currentX, _movableToggleCheck.anchorMin.y);
            _movableToggleCheck.anchorMax = new Vector2(currentX, _movableToggleCheck.anchorMax.y);
            _toggleCheckBg.color = newValue ? _toggleCheckBgEnabledColor : _toggleCheckBgDisabledColor;
        }

    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SFX
{
    public class ToggleWithSfx : AbstractUiButtonWithSfx
    {
        protected override event Action PlaySfxEvent;
        
        [SerializeField] private Toggle _toggle;

        private void OnValidate()
        {
            if(_toggle ==null)
                _toggle = GetComponent<Toggle>();
        }

        protected override void Awake()
        {
            base.Awake();
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool newValue)
        {
            PlaySfxEvent?.Invoke();
        }
    }
}
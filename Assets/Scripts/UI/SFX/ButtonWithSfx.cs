using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SFX
{
    public class ButtonWithSfx : AbstractUiButtonWithSfx
    {
        protected override event Action PlaySfxEvent;
        
        [SerializeField] private Button _button;

        private void OnValidate()
        {
            if(_button ==null)
                _button = GetComponent<Button>();
        }

        protected override void Awake()
        {
            base.Awake();
            _button.onClick.AddListener(OnButtonClick);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            PlaySfxEvent?.Invoke();
        }

    }
}
using Installers.GlobalManagers;
using Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.SkinShopButtons
{
    public abstract class SkinShopButtonAbstractState : MonoBehaviour
    {
        [SerializeField, Required] protected Button button;
        [SerializeField, Required] protected Image buttonBackground;

        protected Color buttonBgDefaultColor;

        protected SignalBus signalBus;
        protected PlayerSkinData playerSkinData;
        protected PlayerSkinsManager playerSkinsManager;
        
        public virtual void Initialize(SignalBus signalBus, 
            PlayerSkinData playerSkinData, PlayerSkinsManager playerSkinsManager)
        {
            this.signalBus = signalBus;
            this.playerSkinData = playerSkinData;
            this.playerSkinsManager = playerSkinsManager;
            
            buttonBgDefaultColor = buttonBackground.color;
            
            Enable(false);
        }

        public virtual void Enter()
        {
            Enable(true);
            button.onClick.AddListener(OnButtonClick);
        }

        public virtual void Exit()
        {
            button.onClick.RemoveListener(OnButtonClick);
            Enable(false);
        }
        
        protected abstract void ChangeActiveState(bool state);
        protected abstract void OnButtonClick();

        private void Enable(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}

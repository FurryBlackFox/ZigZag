using DefaultNamespace;
using Signals;
using UnityEngine;
using Utils.SavableData;
using Zenject;

namespace Installers.GlobalManagers
{
    public class SettingsManager
    {
        public BooleanSavableData MusicEnabledSavableData { get; private set; }
            = new BooleanSavableData(StaticData.EnableMusicKey);

        public BooleanSavableData AiInputSavableData { get; private set; }
            = new BooleanSavableData(StaticData.EnableAiInputKey);

        private SignalBus _signalBus;

        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;

            LoadData();

            AiInputSavableData.OnValueChanged += AiInputValueChanged;
            MusicEnabledSavableData.OnValueChanged += MusicEnabledValueChanged;
        }

        ~SettingsManager()
        {
            AiInputSavableData.OnValueChanged -= AiInputValueChanged;
            MusicEnabledSavableData.OnValueChanged -= MusicEnabledValueChanged;
        }

        private void AiInputValueChanged()
        {
            _signalBus.Fire(new OnPlayerAiInputEnabledStateChanged(AiInputSavableData.Value));
        }

        private void MusicEnabledValueChanged()
        {
            _signalBus.Fire(new OnMusicEnabledStateChanged(MusicEnabledSavableData.Value));
        }

        private void LoadData()
        {
            MusicEnabledSavableData.Load(true);
            AiInputSavableData.Load();
        }
        
    }
}
using UnityEngine;

namespace Utils.SavableData
{
    public class BooleanSavableData : SavableData<bool>
    {
        protected override LoadDelegate LoadMethodDelegate => LoadBoolean;
        protected override SaveDelegate SaveMethodDelegate => SaveBoolean;
        
        public BooleanSavableData(string saveKey, bool autoSave = true) : base(saveKey, autoSave)
        {
        }

        private static bool LoadBoolean(string key)
        {
            var savedIntValue = PlayerPrefs.GetInt(key);
            return savedIntValue == 1;
        }
        
        private static void SaveBoolean(string key, bool value)
        {
            var intValue = value ? 1 : 0;
            PlayerPrefs.SetInt(key, intValue);
        }
    }
}
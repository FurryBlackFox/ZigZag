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

        private static bool LoadBoolean(string key, bool defaultValue = false)
        {
            var defaultInt = defaultValue ? 1 : 0;
            var savedIntValue = PlayerPrefs.GetInt(key, defaultInt);
            return savedIntValue == 1;
        }
        
        private static void SaveBoolean(string key, bool value)
        {
            var intValue = value ? 1 : 0;
            PlayerPrefs.SetInt(key, intValue);
        }
    }
}
using UnityEngine;

namespace Utils.SavableData
{
    public class IntegerSavableData : SavableData<int>
    {
        protected override LoadDelegate LoadMethodDelegate => PlayerPrefs.GetInt;
        protected override SaveDelegate SaveMethodDelegate => PlayerPrefs.SetInt;

        public IntegerSavableData(string saveKey, bool autoSave = true) : base(saveKey, autoSave)
        {
        }
    }
}
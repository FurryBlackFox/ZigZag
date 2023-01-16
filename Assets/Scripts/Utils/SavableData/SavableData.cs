using System;
using UnityEngine;

namespace Utils.SavableData
{
    public abstract class SavableData<T>
    {
        public event Action OnValueChanged;
        
        protected delegate T LoadDelegate(string key);
        protected delegate void SaveDelegate(string key, T value);

        protected abstract LoadDelegate LoadMethodDelegate { get;}
        protected abstract SaveDelegate SaveMethodDelegate { get;}
        
        public T Value { get; private set; }
        
        public readonly string saveKey;
        public readonly bool autoSave = false;

        public SavableData(string saveKey, bool autoSave = true)
        {
            this.saveKey = saveKey;
            this.autoSave = autoSave;
        }

        public void Load()
        {
            Value = LoadMethodDelegate(saveKey);
        }

        public void Save()
        {
            SaveMethodDelegate(saveKey, Value);
        }

        public void SetValue(T newValue)
        {
            var cashedValue = Value;
            
            Value = newValue;
            
            if(!cashedValue.Equals(Value))
                OnValueChanged?.Invoke();
                
            Save();
        }
    }
}
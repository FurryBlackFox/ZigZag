using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Utils.SavableData;
using Zenject;

public class UiPlayerScorePanel : MonoBehaviour
{
    public enum TargetScoreType
    {
        None = 0,
        CurrentScore = 1,
        HighScore = 2,
        Jewels = 3,
        GamesPlayed= 4
    }
    
    [SerializeField, Required] private TextMeshProUGUI _text;
    [SerializeField] private string _prefix;
    [SerializeField] private TargetScoreType _targetScoreType;
    
    private PlayerResourcesManager _playerResourcesManager;
    private IntegerSavableData _targetIntegerSavableData;

    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponent<TextMeshProUGUI>();
    }

    [Inject]
    private void Init(PlayerResourcesManager playerResourcesManager)
    {
        _playerResourcesManager = playerResourcesManager;

        _targetIntegerSavableData = GetTargetIntSavableData();
    }

    private void OnEnable()
    {
        _targetIntegerSavableData.OnValueChanged += UpdateTextValue;
        UpdateTextValue();
    }

    private void OnDisable()
    {
        _targetIntegerSavableData.OnValueChanged -= UpdateTextValue;
    }

    private void UpdateTextValue()
    {
        if (!gameObject.activeInHierarchy)
            return;

        var resultString = _prefix + _targetIntegerSavableData.Value;
        _text.SetText(resultString);
    }
    

    private IntegerSavableData GetTargetIntSavableData()
    {
        switch (_targetScoreType)
        {
            case TargetScoreType.CurrentScore:
                return _playerResourcesManager.CurrentScore;
            case TargetScoreType.HighScore:
                return _playerResourcesManager.HighScoreData;
            case TargetScoreType.Jewels:
                return _playerResourcesManager.CollectedJewelsCount;
            case TargetScoreType.GamesPlayed:
                return _playerResourcesManager.PlayedGamesCount;
            default:
                return null;
        }
    }
}

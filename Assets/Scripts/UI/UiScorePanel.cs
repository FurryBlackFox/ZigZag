using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

public class UiScorePanel : MonoBehaviour
{
    [SerializeField, Required] private TextMeshProUGUI _text;
    
    private PlayerResourcesManager _playerResourcesManager;

    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponent<TextMeshProUGUI>();
    }

    [Inject]
    private void Init(PlayerResourcesManager playerResourcesManager)
    {
        _playerResourcesManager = playerResourcesManager;
    }

    private void OnEnable()
    {
        _playerResourcesManager.CurrentScore.OnValueChanged += UpdateTextValue;
        UpdateTextValue();
    }

    private void OnDisable()
    {
        _playerResourcesManager.CurrentScore.OnValueChanged -= UpdateTextValue;
    }

    private void UpdateTextValue()
    {
        _text.SetText(_playerResourcesManager.CurrentScore.Value.ToString());
    }
}

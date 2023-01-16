using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UIPulsingText : MonoBehaviour
{
    [SerializeField, Required] private TextMeshProUGUI _text;
    [SerializeField] private float _pulseCycleDuration = 1f;
    [SerializeField] private Ease _ease = Ease.Linear;

    private Tween _pulseTween;
    private Color _defaultTextColor;
    private Color _zeroAlphaTextColor;
    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        StartPulse();
    }

    private void OnDisable()
    {
        StopPulse();
    }

    private void Init()
    {
        _defaultTextColor = _text.color;
        _zeroAlphaTextColor = _defaultTextColor;
        _zeroAlphaTextColor.a = 0;
    }

    public void StartPulse()
    {
        _pulseTween = _text
            .DOColor(_zeroAlphaTextColor, _pulseCycleDuration * 0.5f)
            .SetEase(_ease)
            .SetLoops(-1, LoopType.Yoyo);

        _pulseTween.Play();
    }

    public void StopPulse()
    {
        _pulseTween?.Kill();
        _text.color = _defaultTextColor;
    }
}

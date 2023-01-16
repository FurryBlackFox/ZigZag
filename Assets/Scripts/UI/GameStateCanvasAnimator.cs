using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

public class GameStateCanvasAnimator : MonoBehaviour
{
    [SerializeField] private List<UiAnimatedPanel> _animatedPanelsList;
    [SerializeField] private float _delayBeforeShowPanelsInSecs = 0f;
    [SerializeField] private float _delayBeforeHidePanelsInSecs = 0f;
    [SerializeField] private float _delayBetweenShowPanelsInSecs = 0f;
    [SerializeField] private float _delayBetweenHidePanelsInSecs = 0f;

    private bool _initialized;

    private int _maxShowAnimDurationInMs;
    private int _maxHideAnimDurationInMs;

    private void TryToInit()
    {
        if (_initialized)
            return;

        _maxShowAnimDurationInMs =
            (int)TimeSpan.FromSeconds(_animatedPanelsList.Max(p => p.ShowDuration)).TotalMilliseconds;
        _maxHideAnimDurationInMs =
            (int)TimeSpan.FromSeconds(_animatedPanelsList.Max(p => p.HideDuration)).TotalMilliseconds;
        

        _initialized = true;
    }

    [Button]
    public async UniTask ShowPanels()
    {
        TryToInit();
        
        var startDelay = (int) TimeSpan.FromSeconds(_delayBeforeShowPanelsInSecs).TotalMilliseconds;
        await UniTask.Delay(startDelay);
        
        
        var delayInMs = (int) TimeSpan.FromSeconds(_delayBetweenShowPanelsInSecs).TotalMilliseconds;

        for (var i = 0; i < _animatedPanelsList.Count; i++)
        {
            var animatedPanel = _animatedPanelsList[i];
            animatedPanel.Show();
            
            if(i < _animatedPanelsList.Count - 1)
                await UniTask.Delay(delayInMs);
        }

        await UniTask.Delay(_maxShowAnimDurationInMs + delayInMs * (_animatedPanelsList.Count - 1));
    }

    [Button]
    public async UniTask HidePanels()
    {
        TryToInit();
        
        var startDelay = (int) TimeSpan.FromSeconds(_delayBeforeHidePanelsInSecs).TotalMilliseconds;
        await UniTask.Delay(startDelay);
        
        var delayInMs = (int) TimeSpan.FromSeconds(_delayBetweenHidePanelsInSecs).TotalMilliseconds; 
        
        for (var i = 0; i < _animatedPanelsList.Count; i++)
        {
            var animatedPanel = _animatedPanelsList[i];
            animatedPanel.Hide();
            
            if(i < _animatedPanelsList.Count - 1)
                await UniTask.Delay(delayInMs);
        }
        
        await UniTask.Delay(_maxHideAnimDurationInMs + delayInMs * (_animatedPanelsList.Count - 1));
    }
    
    [Button]
    public void InstantShowPanels()
    {
        TryToInit();
        
        foreach (var animatedPanel in _animatedPanelsList)
        {
            animatedPanel.InstantShow();
        }
    }

    [Button]
    public void InstantHidePanels()
    {
        TryToInit();
        
        foreach (var animatedPanel in _animatedPanelsList)
        {
            animatedPanel.InstantHide();
        }
    }
}

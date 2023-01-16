using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UiAnimatedPanel : MonoBehaviour
{
    private enum Direction
    {
        None = 0,
        TopToBottom = 1,
        BottomToTop = -1,
        RightToLeft = 2,
        LeftToRight = -2,
    }
    
    private enum PanelState
    {
        None = 0,
        Shown = 1,
        Hidden = 2,
        ShownToHiddenTransition = 3,
        HiddenToShownTransition = 4,
    }
    
    [field: SerializeField] public float ShowDuration { get; private set; } = 1f;
    [field: SerializeField] public float HideDuration { get; private set; } = 1f;
    
    [SerializeField] private Direction _showDirection;
    [SerializeField] private float _anchorMoveValue = 1f;
    [SerializeField] private Ease _showEase = Ease.Linear;
    [SerializeField] private Ease _hideEase = Ease.Linear;

    private RectTransform _rectTransform;

    private Vector2 _shownStateAnchorMin;
    private Vector2 _shownStateAnchorMax;
    
    private Vector2 _hidenStateAnchorMin;
    private Vector2 _hidenStateAnchorMax;

    private Sequence _moveSequence;

    private bool _initialized = false;

    private PanelState _currentPanelState;

    private void TryToInit()
    {
        if (_initialized)
            return;
        
        _rectTransform = GetComponent<RectTransform>();

        _shownStateAnchorMin = _rectTransform.anchorMin;
        _shownStateAnchorMax = _rectTransform.anchorMax;
        
        GetDirectionModifiedResultAnchors(InverseDirection(_showDirection), out _hidenStateAnchorMin, out _hidenStateAnchorMax, 
        _anchorMoveValue);

        _currentPanelState = PanelState.Shown;

        _initialized = true;
    }


    [Button]
    public float Show()
    {
        TryToInit();

        if (_currentPanelState == PanelState.Shown)
            return 0;

        if (_currentPanelState == PanelState.HiddenToShownTransition)
            return 0;

        var transitionDuration = _currentPanelState == PanelState.Hidden
            ? ShowDuration
            : _moveSequence?.position ?? 0;

        _moveSequence?.Kill();

        if (transitionDuration == 0)
        {
            InstantShow();
            return 0;
        }

        _currentPanelState = PanelState.HiddenToShownTransition;
        
        _moveSequence = DOTween.Sequence();
        _moveSequence.Join(_rectTransform
            .DOAnchorMax(_shownStateAnchorMax, transitionDuration)
            .SetEase(_showEase));
        _moveSequence.Join(_rectTransform
            .DOAnchorMin(_shownStateAnchorMin, transitionDuration)
            .SetEase(_showEase));
        _moveSequence.OnComplete(() =>
        {
            _currentPanelState = PanelState.Shown;
        });
        
        _moveSequence.Play();
        
        //await _moveSequence.AsyncWaitForCompletion();
        return _moveSequence.Duration();
    }

    [Button]
    public float Hide()
    {
        TryToInit();

        if (_currentPanelState == PanelState.Hidden)
            return 0;
        
        if (_currentPanelState == PanelState.ShownToHiddenTransition)
            return 0;
        
        var transitionDuration = _currentPanelState == PanelState.Shown
            ? HideDuration
            : _moveSequence?.position ?? 0;
        
        _moveSequence?.Kill();

        if (transitionDuration == 0)
        {
            InstantHide();
            return 0;
        }
        
        _currentPanelState = PanelState.ShownToHiddenTransition;

        _moveSequence = DOTween.Sequence();
        _moveSequence.Join(_rectTransform
            .DOAnchorMax(_hidenStateAnchorMax, transitionDuration)
            .SetEase(_hideEase));
        _moveSequence.Join(_rectTransform
            .DOAnchorMin(_hidenStateAnchorMin, transitionDuration)
            .SetEase(_hideEase));
        _moveSequence.OnComplete(() =>
        {
            _currentPanelState = PanelState.Hidden;
        });

        _moveSequence.Play();
        
        //await _moveSequence.AsyncWaitForCompletion();
        return _moveSequence.Duration();
    }
    

    [Button]
    public void InstantShow()
    {
        TryToInit();
        
        if(_currentPanelState == PanelState.Shown)
            return;
        
        _moveSequence?.Kill();
        
        _rectTransform.anchorMin = _shownStateAnchorMin;
        _rectTransform.anchorMax = _shownStateAnchorMax;

        _currentPanelState = PanelState.Shown;
    }
    
    [Button]
    public void InstantHide()
    {
        TryToInit();
        
        if(_currentPanelState == PanelState.Hidden)
            return;
        
        _moveSequence?.Kill();
        
        _rectTransform.anchorMin = _hidenStateAnchorMin;
        _rectTransform.anchorMax = _hidenStateAnchorMax;

        _currentPanelState = PanelState.Hidden;
    }
    
    private Direction InverseDirection(Direction direction)
    {
        return (Direction) (- (int) direction);
    }
    
    private void GetDirectionModifiedResultAnchors(Direction direction, out Vector2 min, out Vector2 max, float additiveValue)
    {
        min = _rectTransform.anchorMin;
        max = _rectTransform.anchorMax;

        switch (direction)
        {
            case Direction.TopToBottom:
                min.y -= additiveValue;
                max.y -= additiveValue;
                break;
            case Direction.BottomToTop:
                min.y += additiveValue;
                max.y += additiveValue;
                break;
            case Direction.LeftToRight:
                min.x += additiveValue;
                max.x += additiveValue;
                break;
            case Direction.RightToLeft:
                min.x -= additiveValue;
                max.x -= additiveValue;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }


}

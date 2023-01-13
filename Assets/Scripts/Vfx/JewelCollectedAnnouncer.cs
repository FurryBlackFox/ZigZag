using System;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Vfx
{
    public class JewelCollectedAnnouncer : MonoBehaviour
    {
        [SerializeField, Required] private CanvasGroup _canvasGroup;
        
        private Sequence _tweenSequence;
        private float _cashedCanvasAlpha = 1f;

        private bool _initialized = false;

        private void OnValidate()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        public void Play(float duration, float distance, Ease ease)
        {
            TryToInitialize();
            
            ResetState();

            _tweenSequence = DOTween.Sequence();
            _tweenSequence
                .Join(transform
                    .DOMoveY(transform.position.y + distance, duration)
                    .SetEase(ease));
            _tweenSequence
                .Join(_canvasGroup
                    .DOFade(0, duration)
                    .SetEase(ease));
            _tweenSequence
                .OnComplete(Despawn);
            
            _tweenSequence.Play();
        }

        private void TryToInitialize()
        {
            if (_initialized)
                return;

            _cashedCanvasAlpha = _canvasGroup.alpha;

            _initialized = true;
        }

        private void ResetState()
        {
            if (_tweenSequence == null)
                return;

            _tweenSequence.Kill();
            
            _canvasGroup.alpha = _cashedCanvasAlpha;
        }

        private void Despawn()
        {
            LeanPool.Despawn(this);
        }
    }
}
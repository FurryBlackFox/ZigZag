using System;
using Player;
using UnityEngine;
using Zenject;

public class PlayerShadowController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _shadowMeshGameObject;

    private SignalBus _signalBus;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnValidate()
    {
        if (_playerController == null)
            _playerController = GetComponentInParent<PlayerController>();
    }

    private void Awake()
    {
        _signalBus.Subscribe<OnPlayerDeath>(OnPlayerDeath);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<OnPlayerDeath>(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        _shadowMeshGameObject.SetActive(false);
    }
}

using System;
using GameStateMachine.GameStates;
using Player;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class PlayerShadowController : MonoBehaviour
{
    [SerializeField, Required] private PlayerController _playerController;
    [SerializeField, Required] private GameObject _shadowMeshGameObject;

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
        _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
    }

    private void OnGameStateChanged(OnGameStateChanged stateChangedEvent)
    {
        _shadowMeshGameObject.SetActive(stateChangedEvent.currentStateType != GameStateType.Defeat);
    }
}

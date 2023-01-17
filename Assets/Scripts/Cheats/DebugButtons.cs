using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class DebugButtons : MonoBehaviour
{
    private PlayerResourcesManager _playerResourcesManager;

    [Inject]
    private void Init(PlayerResourcesManager playerResourcesManager)
    {
        _playerResourcesManager = playerResourcesManager;
    }

    [Button]
    private void AppendJewels()
    {
        _playerResourcesManager.DebugAddJewels(1000);
    }
}

using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera _activeCamera;

    private void Awake()
    {
        Debug.Break();
        FindObjectOfType<PlayManager>().SubscribeOnPlayerSpawned(OnPlayerSpawned);
        _activeCamera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();

        var player = FindObjectOfType<Player>();

        if (player != null)
            _activeCamera.Follow = player.transform;
    }

    public void OnPlayerSpawned(Player player)
    {
        Debug.Log("On Player Spawned: CameraManager");
        _activeCamera.Follow = player.transform;
    }
}
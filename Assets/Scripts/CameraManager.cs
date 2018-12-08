using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviourSingleton<CameraManager>
{
    public CinemachineVirtualCamera _activeCamera;

    private void Awake()
    {
        Debug.Log("Awake CM - " + frameCount);
    }

    private void Start()
    {
        Debug.Log("Start CM");
        StartCoroutine(InitCall());
    }
    public void Init()
    {
        Debug.Log("Init CM - " + frameCount);
        _initialized = true;
    }

    private void OnEnable()
    {
        FindObjectOfType<PlayManager>().SubscribeOnPlayerSpawned(OnPlayerSpawned);
        Debug.Log("Enable CM - " + frameCount);
    }

    private void Update()
    {
        frameCount++;
    }

    public void OnPlayerSpawned(Player player)
    {
        Debug.Log("On Player Spawned: CameraManager");
        _activeCamera.Follow = player.transform;
    }

    private IEnumerator InitCall()
    {
        yield return null;
        Init();
    }
}
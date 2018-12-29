using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviourSingleton<CameraManager>
{
    public CinemachineVirtualCamera _activeCamera;

    public CinemachineVirtualCamera _followCamera;
    public CinemachineVirtualCamera _deathCamera;

    private CinemachineBrain _cameraBrain;

    private void Awake()
    {
        _cameraBrain = GetComponentInChildren<CinemachineBrain>();
    }

    private void Start()
    {
        StartCoroutine(InitCall());
    }

    public void Init()
    {
        //Debug.Log("Init CM - " + frameCount);
        _initialized = true;
    }

    private void OnEnable()
    {
        FindObjectOfType<PlayManager>().SubscribeOnPlayerSpawned(OnPlayerSpawned);
    }

    private void Update()
    {
        frameCount++;
    }

    public void OnPlayerSpawned(Player player)
    {
        _activeCamera.Follow = player.transform;
        _followCamera.Follow = player.transform;
        _deathCamera.LookAt = player.transform;
    }

    public void ChangeCamera (CinemachineVirtualCamera camera)
    {
        camera.gameObject.SetActive(true);
        _activeCamera.gameObject.SetActive(false);
        _activeCamera = camera;
    }

    private IEnumerator InitCall()
    {
        yield return null;
        Init();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviourSingleton<PlayManager>
{
    public Player ActivePlayer;
    private List<Action<Player>> OnPlayerSpawnedCallbacks = new List<Action<Player>>();

    public float FillRange = 50;
    public float ModuleSpan = 10;

    public Transform SceneModuleContainer;
    public List<LevelBlock> SceneModules = new List<LevelBlock>();

    public GameObject SceneModulePrefab;
    public Transform SpawnPoint;
    public GameObject PlayerPrefab;

    private bool _hasPlayerReference
    {
        get
        {
            return ActivePlayer;
        }
    }
    private GameObject _populateAroundTarget;

    private void Awake()
    {
    }
    private void Start()
    {
        StartCoroutine(InitCall());
    }

    public void Init()
    {
        _initialized = true;
        ActivePlayer = SpawnPlayer();
        _populateAroundTarget = ActivePlayer.gameObject;
        OnPlayerSpawned(ActivePlayer);
    }

    public void SubscribeOnPlayerSpawned(Action<Player> callbackMethod)
    {
        if (!OnPlayerSpawnedCallbacks.Contains(callbackMethod))
            OnPlayerSpawnedCallbacks.Add(callbackMethod);
        else
            Debug.LogWarning("Ignored subscription to OnPlayerSpawned - A subscription for this action is already active");
    }

    public void UnsubscribeOnPlayerSpawned(Action<Player> callbackMethod)
    {
        if (OnPlayerSpawnedCallbacks.Contains(callbackMethod))
            OnPlayerSpawnedCallbacks.Remove(callbackMethod);
        else
            Debug.LogWarning("Ignored unsubsciption to OnPlayerSpawned - The action didn't have an existing subscription");
    }

    private void OnPlayerSpawned(Player player)
    {
        foreach (Action<Player> action in OnPlayerSpawnedCallbacks)
        {
            action.Invoke(player);
        }
    }

    private void Update()
    {
        frameCount++;
        if (!_hasPlayerReference)
            return;

        FillCheck();
        ClearCheck();
    }

    private void FillCheck()
    {
        if (!IsLeftMostRangeCovered())
        {
            PlaceNewSceneModule(false);
        }
        else if (!IsRightMostRangeCovered())
        {
            PlaceNewSceneModule(true);
        }
    }

    private void ClearCheck()
    {
        List<LevelBlock> removeList = new List<LevelBlock>();
        foreach (LevelBlock module in SceneModules)
        {
            var distance = Mathf.Abs(_populateAroundTarget.transform.position.x - module.transform.position.x);
            if (distance > FillRange + 1)
            {
                removeList.Add(module);
            }
        }

        foreach (LevelBlock module in removeList)
        {
            SceneModules.Remove(module);
            Destroy(module.gameObject);
        }
    }

    private void PlaceNewSceneModule(bool right = true)
    {
        var module = SpawnSceneModule();

        bool isInitModule = false;

        if (SceneModules.Count < 1)
            isInitModule = true;

        if (isInitModule)
        {
            var modulePos = _populateAroundTarget.transform.position;
            module.transform.position = new Vector3(modulePos.x, 0, 0);
        }
        else if (right)
        {
            var modulePos = GetRightMost().transform.position;
            module.transform.position = new Vector3(modulePos.x + ModuleSpan, 0, 0);
        }
        else
        {
            var modulePos = GetLeftMost().transform.position;
            module.transform.position = new Vector3(modulePos.x - ModuleSpan, 0, 0);
        }

        SceneModules.Add(module);
    }

    private LevelBlock SpawnSceneModule()
    {
        var go = Instantiate(SceneModulePrefab, SceneModuleContainer);
        var returnValue = go.GetComponent<LevelBlock>();
        return returnValue;
    }

    private LevelBlock GetLeftMost()
    {
        LevelBlock returnValue = null;

        if (SceneModules.Count < 1)
            return null;

        var tempX = 99999f;
        foreach (LevelBlock module in SceneModules)
        {
            if (module.transform.position.x < tempX)
            {
                returnValue = module;
                tempX = module.transform.position.x;
            }
        }

        return returnValue;
    }

    private LevelBlock GetRightMost()
    {
        LevelBlock returnValue = null;

        if (SceneModules.Count < 1)
            return null;

        var tempX = -99999f;
        foreach (LevelBlock module in SceneModules)
        {
            if (module.transform.position.x > tempX)
            {
                returnValue = module;
                tempX = module.transform.position.x;
            }
        }
        return returnValue;
    }

    private bool IsLeftMostRangeCovered()
    {
        var leftMost = GetLeftMost();
        if (leftMost == null)
            return false;

        var leftMostDistance = Mathf.Abs(_populateAroundTarget.transform.position.x - (leftMost.transform.position.x - ModuleSpan));

        if (leftMostDistance < FillRange)
            return false;

        return true;
    }

    private bool IsRightMostRangeCovered()
    {
        var rightMost = GetRightMost();
        if (rightMost == null)
            return false;

        var rightMostDistance = Mathf.Abs(_populateAroundTarget.transform.position.x - (rightMost.transform.position.x + ModuleSpan));

        if (rightMostDistance < FillRange)
            return false;

        return true;
    }

    public Player SpawnPlayer()
    {
        var player = Instantiate(PlayerPrefab);
        player.transform.position = SpawnPoint.position;

        var collider = player.GetComponentInChildren<Collider>();
        var newMatValues = collider.sharedMaterial;
        newMatValues.bounciness = 0.0f;
        newMatValues.dynamicFriction = 0.05f;
        newMatValues.staticFriction = 0.05f;
        newMatValues.frictionCombine = PhysicMaterialCombine.Minimum;
        collider.sharedMaterial = newMatValues;

        return player.GetComponent<Player>();
    }

    private IEnumerator InitCall()
    {
        yield return null;
        Init();
    }
}

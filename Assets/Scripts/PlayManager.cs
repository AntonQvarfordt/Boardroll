using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviourSingletonPersistent<PlayManager>
{
    private List<Action<Player>> OnPlayerSpawnedCallbacks = new List<Action<Player>>();

    public float FillRange = 50;
    public float ModuleSpan = 10;

    public Transform SceneModuleContainer;
    public List<LevelBlock> SceneModules = new List<LevelBlock>();

    public GameObject SceneModulePrefab;
    public Transform SpawnPoint;
    public GameObject PlayerPrefab;

    private bool _initialized;
    private GameObject _populateAroundTarget;

    public override void Awake()
    {
        base.Awake();
    }

    public void Init(GameObject target)
    {
        _populateAroundTarget = target;
        _initialized = true;
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
        if (!_initialized)
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

    //private void CalculateSceneModuleOrder()
    //{
    //	var modulePositions = new List<KeyValuePair<GameObject , float>>();
    //	foreach (LevelBlock module in SceneModules)
    //	{
    //		modulePositions.Add(new KeyValuePair<GameObject , float>(gameObject , module.transform.position.x));
    //	}
    //}

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
        return player.GetComponent<Player>();
    }
}

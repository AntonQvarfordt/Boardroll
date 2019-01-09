using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

[System.Serializable]
public struct IKGroup
{
    public IKTargetGroup TargetGroup;
    public int TargetIdentity;
    public Transform Joint;
}

[RequireComponent(typeof(FullBodyBipedIK))]
public class IKInit : MonoBehaviour
{
    public IKGroup[] TargetIKGroup;

    private FullBodyBipedIK _targetIK;

    private void Awake()
    {
        _targetIK = GetComponent<FullBodyBipedIK>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (IKGroup ikG in TargetIKGroup) {
            var matchingTarget = GetMatchingTarget(ikG);
            if (matchingTarget == null)
                return;

            var effector = new IKEffector();

            if (ikG.TargetGroup == IKTargetGroup.LFoot)
                effector = _targetIK.solver.GetEffector(FullBodyBipedEffector.LeftFoot);
            else if (ikG.TargetGroup == IKTargetGroup.RFoot)
                effector = _targetIK.solver.GetEffector(FullBodyBipedEffector.RightFoot);
            else if (ikG.TargetGroup == IKTargetGroup.LHand)
                effector = _targetIK.solver.GetEffector(FullBodyBipedEffector.LeftHand);
            else if (ikG.TargetGroup == IKTargetGroup.RHand)
                effector = _targetIK.solver.GetEffector(FullBodyBipedEffector.RightHand);

            effector.target = matchingTarget.gameObject.transform;
        }

    }

    private IKTarget GetMatchingTarget (IKGroup ikG)
    {
        var ikTargets = FindObjectsOfType<IKTarget>();
        var matchingTargets = new List<IKTarget>();

        foreach (IKTarget target in ikTargets)
        {
            var validMatch = true;

            if (target.TargetIKGroup.TargetGroup != ikG.TargetGroup)
                validMatch = false;
            if (target.TargetIKGroup.TargetIdentity != ikG.TargetIdentity)
                validMatch = false;

            if (validMatch)
                matchingTargets.Add(target);
        }

        if (matchingTargets.Count < 1)
        {
            Debug.LogWarning("IKInit has found multiple matches, this shouldn't happen, find the duplicate IKTarget in your scene... GRABBING FIRST MATCH");
        }

        if (matchingTargets.Count == 0)
        {
            Debug.LogError("IKInit failed to find a matching TargetIK, make sure any TargetIK is correctly configured and spawned into the scene");
            return null;
        }

        return matchingTargets[0];
     
    }

}

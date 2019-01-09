using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IKTargetGroup
{
    LHand,
    RHand,
    LFoot,
    RFoot
}

public class IKTarget : MonoBehaviour
{
    public IKGroup TargetIKGroup;
}

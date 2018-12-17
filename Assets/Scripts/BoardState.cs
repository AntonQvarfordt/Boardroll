using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoardStateInformation
{
    public BoardStates ThisState;
    public List<BoardStates> IncompatibleWith;
}

//public enum BoardStates
//{
//    Grounded,
//    FreeFall,
//    CatchWindow
//}

public class BoardState : MonoBehaviour
{
    public bool IsGrounded
    {
        get
        {
            return _grounded;
        }
    }

    public bool IsRolling
    {
        get
        {
            if (GetVelocity.x < 1)
                return false;
            else if (IsGrounded)
                return true;

            return false;

        }
    }

    public bool InFreeFall
    {
        get
        {
            if (!IsGrounded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool InCatchWindow
    {
        get
        {
            return _inCatchWindow;
        }
    }

    public bool Bailed
    {
        get; set;
    }

    public Transform GroundProbe;
    public string GroundLayer;

    public Vector3 GetVelocity
    {
        get
        {
            var returnValue = new Vector3();
            if (_rigidbody != null)
                returnValue = _rigidbody.velocity;

            return returnValue;
        }
    }

    [HideInInspector]
    public List<BoardStates> ActiveStates = new List<BoardStates>();

    private List<Action> _onGroundConnectCallbacks = new List<Action>();
    private Rigidbody _rigidbody;
    private bool _grounded;
    private bool _inCatchWindow;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var isGrounded = IsGroundConnected();

        if (_grounded != isGrounded)
        {
            if (!isGrounded && ActiveStates.Contains(BoardStates.Grounded))
            {
                ActiveStates.Remove(BoardStates.Grounded);
            }
            else if (isGrounded && !ActiveStates.Contains(BoardStates.Grounded))
            {
                FromFreeFallToStreet();
                ActiveStates.Add(BoardStates.Grounded);
            }


            _grounded = isGrounded;
        }
    }

    private void FixedUpdate()
    {
        VelocityClamp();
    }

    //private void AddActiveState (BoardStates state)
    //{
    //    if ()
    //}

    private bool IsGroundConnected()
    {
        RaycastHit[] hitColliders = Physics.SphereCastAll(GroundProbe.position, 0.01f, Vector3.down, 0.1f);

        var returnValue = false;

        foreach (RaycastHit hit in hitColliders)
        {
            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == GroundLayer)
                returnValue = true;
        }

        return returnValue;
    }

    private void FromFreeFallToStreet()
    {
        Debug.Log("Air to Land, valid: " + InCatchWindow);
    }

    public void OnGroundConnectSubscribe(Action action)
    {
        if (_onGroundConnectCallbacks.Contains(action))
            return;

        _onGroundConnectCallbacks.Add(action);
    }

    public void OnGroundConnectUnSubscribe(Action action)
    {
        if (!_onGroundConnectCallbacks.Contains(action))
            return;

        _onGroundConnectCallbacks.Remove(action);
    }

    private void VelocityClamp()
    {
        if (_rigidbody.velocity.x > 10)
        {
            _rigidbody.velocity = new Vector3(10, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }
    }

    #region catch

    public void Catch(float windowOfOpporunity = 0.2f)
    {
        Debug.Log("Catch 2");
        SetCatchWindowOpen(windowOfOpporunity);
    }

    private void SetCatchWindowOpen(float closeAfter)
    {
        Debug.Log("Catch CR 1");
        if (_inCatchWindow)
            return;
        Debug.Log("Catch CR 2");
        _inCatchWindow = true;
        StartCoroutine(CatchCoroutine(closeAfter));
    }

    private IEnumerator CatchCoroutine(float closeAfterTime)
    {
        float timePool;
        timePool = closeAfterTime;
        var timeStep = Time.fixedDeltaTime;

        Debug.Log(timePool);
        Debug.Log(timeStep);

        while (timePool > 0)
        {
            Debug.Log(timePool);
            timePool -= timeStep;
            yield return new WaitForFixedUpdate();
        }

        SetCatchWindowClosed();
    }

    private void SetCatchWindowClosed()
    {
        Debug.Log("SetClosed");
        _inCatchWindow = false;
    }

    #endregion

}

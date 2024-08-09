using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct BoardStateInformation
{
    public BoardStates ThisState;
    public List<BoardStates> IncompatibleWith;
}

public class BoardState : MonoBehaviour
{
    public bool IsIncapacitated
    {
        get; set;
    }

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

    public Transform GroundProbe;
    public string GroundLayer;
	public LandParticles LandParticle;
	public AudioClip LandSound;

    public Vector3 GetVelocity
    {
        get
        {
            var returnValue = new Vector3();
            if (_rigidbody != null)
                returnValue = _rigidbody.linearVelocity;

            return returnValue;
        }
    }

    [HideInInspector]
    public List<BoardStates> ActiveStates = new List<BoardStates>();

    private List<Action> _onGroundConnectCallbacks = new List<Action>();
    private List<Action> _onIncapacitatedCallback = new List<Action>();

    private Rigidbody _rigidbody;
    private float _rBodyBaseDrag = 0.1f;
    private float _velocityClamp = 15;
    private bool _grounded;
    private bool _inCatchWindow;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.linearDamping = _rBodyBaseDrag;
    }

    private void Start()
    {
        Catch(1);
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
		AudioManager.Instance.PlayOneShot(LandSound, AudioManager.Instance.SFXMixer, 0.3f);
		if (!InCatchWindow)
            Bail();
        else
		{
			_rigidbody.AddForce(Vector3.right * GetVelocity.x * 20);
			LandParticle.Activate();
		}
    }

    private void VelocityClamp()
    {
        if (_rigidbody.linearVelocity.x > _velocityClamp)
        {
            _rigidbody.linearVelocity = new Vector3(_velocityClamp, _rigidbody.linearVelocity.y, _rigidbody.linearVelocity.z);
        }
    }

    #region catch

    public void Catch(float windowOfOpporunity = 0.2f)
    {
        _rigidbody.AddForce(new Vector3(0, -10, 0), ForceMode.Impulse);
        SetCatchWindowOpen(windowOfOpporunity);
    }

    private void SetCatchWindowOpen(float closeAfter)
    {
        if (_inCatchWindow)
            return;

        _inCatchWindow = true;
        StartCoroutine(CatchCoroutine(closeAfter));
    }

    private IEnumerator CatchCoroutine(float closeAfterTime)
    {
        float timePool;
        timePool = closeAfterTime;
        var timeStep = Time.fixedDeltaTime;

        while (timePool > 0)
        {
            timePool -= timeStep;
            yield return new WaitForFixedUpdate();
        }

        SetCatchWindowClosed();
    }

    private void SetCatchWindowClosed()
    {
        _inCatchWindow = false;
    }

    public void Bail()
    {
        if (IsIncapacitated)
            return;

        IsIncapacitated = true;

        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.linearDamping = 0.75f;

        var collider = GetComponentInChildren<Collider>();
        var newMatValues = collider.sharedMaterial;

        newMatValues.bounciness = 0.4f;
        newMatValues.dynamicFriction = 0.05f;
        newMatValues.staticFriction = 0.05f;
        collider.sharedMaterial = newMatValues;

        var randomForce = new Vector3();
        randomForce.y = Random.Range(0.5f, 1.5f) * GetVelocity.x;
        randomForce.x = Random.Range(0.75f, 1.5f) * GetVelocity.x;
        randomForce.z = Random.Range(-4, 4);

        var randomTorqueForce = new Vector3();
        randomTorqueForce.y = randomForce.x * 8;
        randomTorqueForce.x = Random.Range(-70, 70) * GetVelocity.x + 1;
        randomTorqueForce.z = Random.Range(-70, 70) * GetVelocity.x + 1;

        _rigidbody.AddForce(randomForce, ForceMode.VelocityChange);
        _rigidbody.AddTorque(randomTorqueForce * 2, ForceMode.VelocityChange);

        foreach (Action cb in _onIncapacitatedCallback)
        {
            cb.Invoke();
        }

        CameraManager.Instance.ChangeCamera(CameraManager.Instance._deathCamera);

        Invoke("BailDelayedEffects", 0.1f);
    }

    private void BailDelayedEffects()
    {
        var collider = GetComponentInChildren<Collider>();
        var newMatValues = collider.sharedMaterial;
        newMatValues.bounciness = 0.1f;
        newMatValues.dynamicFriction = 0.2f;
        newMatValues.staticFriction = 0.2f;
        newMatValues.frictionCombine = PhysicsMaterialCombine.Average;
        collider.sharedMaterial = newMatValues;
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

    public void OnIncapacitatedSubscribe(Action action)
    {
        if (_onIncapacitatedCallback.Contains(action))
            return;

        _onIncapacitatedCallback.Add(action);
    }

    public void OnIncapacitatedUnsubscribe(Action action)
    {
        if (!_onIncapacitatedCallback.Contains(action))
            return;

        _onIncapacitatedCallback.Remove(action);
    }

    #endregion
}

using System;
using System.Collections.Generic;
using UnityEngine;

public enum BoardStates
{
	Grounded,
	FreeFall
}

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

	public Transform GroundProbe;
	public string GroundLayer;

	private Rigidbody _rigidbody;

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

	private bool _grounded;
	private List<Action> _onGroundConnectCallbacks = new List<Action>();

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
				ActiveStates.Add(BoardStates.Grounded);
			}
			_grounded = isGrounded;
		}
	}

	private void FixedUpdate()
	{
		VelocityClamp();
	}

	private bool IsGroundConnected()
	{
		RaycastHit[] hitColliders = Physics.SphereCastAll(GroundProbe.position , 0.01f , Vector3.down , 0.1f);

		var returnValue = false;

		foreach (RaycastHit hit in hitColliders)
		{
			if (LayerMask.LayerToName(hit.collider.gameObject.layer) == GroundLayer)
				returnValue = true;
		}

		return returnValue;
	}

	public void OnGroundConnectSubscribe( Action action )
	{
		if (_onGroundConnectCallbacks.Contains(action))
			return;

		_onGroundConnectCallbacks.Add(action);
	}

	public void OnGroundConnectUnSubscribe( Action action )
	{
		if (!_onGroundConnectCallbacks.Contains(action))
			return;

		_onGroundConnectCallbacks.Remove(action);
	}

	private void VelocityClamp()
	{
		if (_rigidbody.velocity.x > 10)
		{
			_rigidbody.velocity = new Vector3(10 , _rigidbody.velocity.y , _rigidbody.velocity.z);
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAction_Break : MonoBehaviour
{

	[Header("Skate Params")]
	public float BreakPower = 10;

	private Rigidbody _rigidbody;
	private BoardState _boardStateScript;
	public Animator BoardAnimator;
	private float _startDrag;
	public float BoardVelocity;
	public AudioClip BreakSound;

	public BreakParticle BreakParticles;

	bool isBreaking;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_boardStateScript = GetComponent<BoardState>();
		_startDrag = _rigidbody.drag;
		
	}

	private void Update()
	{
		if (_boardStateScript.IsIncapacitated)
			return;

		BoardVelocity = _boardStateScript.GetVelocity.magnitude;

		if (Input.GetKeyDown("q") && _boardStateScript.IsGrounded)
		{
			if (isBreaking)
				return;

			Break();
		}
		
		if (Input.GetKeyUp("q") && isBreaking)
		{
			CancelBreaking();
		}

		if ((_boardStateScript.GetVelocity.magnitude < 2) && isBreaking)
		{
			CancelBreaking();
		}
	}

	public void Break()
	{

		isBreaking = true;
		AudioManager.Instance.PlayOneShot(BreakSound, AudioManager.Instance.SFXMixer, 0.3f);
		//E.DOColor(Color.blue , 1f).From();
		BoardAnimator.SetBool("Breaking", true);
		_rigidbody.drag = _startDrag * BreakPower;
		BreakParticles.Activate();
	}

	private void FixedUpdate()
	{
		
	}

	public void CancelBreaking()
	{
		isBreaking = false;
		BreakParticles.Deactivate();
		_rigidbody.drag = _startDrag ;
		BoardAnimator.SetBool("Breaking", false);
	}


}

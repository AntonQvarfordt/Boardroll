using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAction_Break : MonoBehaviour
{

	[Header("Skate Params")]
	public float BreakPower = 10;
	public AnimationCurve BreakForceCurve = new AnimationCurve();
	public float BreakApplyTime = 2;

	private Rigidbody _rigidbody;
	private BoardState _boardStateScript;
	public bool fUpdate = false;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_boardStateScript = GetComponent<BoardState>();
		Invoke("StartFUpdate", 1);
	}

	private void StartFUpdate()
	{
		fUpdate = true;
	}

	private void Update()
	{
		if (_boardStateScript.IsIncapacitated)
			return;

		if (Input.GetKey("q") && _boardStateScript.IsGrounded)
		{
			Break();
		}
	}

	public void Break()
	{
		StartCoroutine(BreakCoroutine(BreakApplyTime, BreakForceCurve));
		//AudioManager.Instance.PlayOneShot(GetComponent<AudioClipContainer>().GetClip("Air") , AudioManager.Instance.SFXMixer , 0.3f);
		//E.DOColor(Color.blue , 1f).From();
	}

	private void FixedUpdate()
	{
		if (fUpdate)
			_rigidbody.AddTorque(new Vector3(0, 100 * BreakPower, 0));
	}

	private IEnumerator BreakCoroutine(float time, AnimationCurve curve)
	{
		var tickTime = Time.fixedDeltaTime;
		var timePool = 0f;

		while (timePool < time)
		{
			var curveStep = timePool / time;
			timePool += tickTime;
			_rigidbody.AddForce(new Vector2(BreakPower * curve.Evaluate(curveStep), 0));
			yield return new WaitForFixedUpdate();
		}
		yield return null;
	}
}

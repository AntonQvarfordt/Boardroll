using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct KickValues
{
    public float KickPower;
    public AnimationCurve KickForceCurve;
    public float KickApplyTime;
    public AudioClip KickSound;
}

public class BoardAction_Kick : MonoBehaviour {

	public KickValues[] KValues;

	[Header("Skate Params")]
	public float KickPower = 10;
	public AnimationCurve KickForceCurve;
	public float KickApplyTime = 2;
	public AudioClip KickSound;
	private Rigidbody _rigidbody;
    private BoardState _boardStateScript;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boardStateScript = GetComponent<BoardState>();
    }
    private void Update()
    {
        if (_boardStateScript.IsIncapacitated)
            return;

        if (Input.GetKeyDown("e") && _boardStateScript.IsGrounded)
        {
            Kick();
        }
    }

	public void Kick()
	{
		StartCoroutine(KickCoroutine(KickApplyTime , KickForceCurve));
		AudioManager.Instance.PlayOneShot(KickSound, AudioManager.Instance.SFXMixer , 0.3f);
		//E.DOColor(Color.blue , 1f).From();
	}

	private IEnumerator KickCoroutine( float time , AnimationCurve curve )
	{
		var tickTime = Time.fixedDeltaTime;
		var timePool = 0f;

		while (timePool < time)
		{
			var curveStep = timePool / time;
			timePool += tickTime;
			_rigidbody.AddForce(new Vector2(KickPower * curve.Evaluate(curveStep) , 0));
			yield return new WaitForFixedUpdate();
		}
		yield return null;
	}
}

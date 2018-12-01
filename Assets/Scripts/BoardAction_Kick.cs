using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAction_Kick : MonoBehaviour {

	[Header("Skate Params")]
	public float KickPower = 10;
	public AnimationCurve KickForceCurve = new AnimationCurve();
	public float KickApplyTime = 2;

	private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Kick();
        }
    }

	public void Kick()
	{
		StartCoroutine(KickCoroutine(KickApplyTime , KickForceCurve));
		//AudioManager.Instance.PlayOneShot(GetComponent<AudioClipContainer>().GetClip("Air") , AudioManager.Instance.SFXMixer , 0.3f);
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

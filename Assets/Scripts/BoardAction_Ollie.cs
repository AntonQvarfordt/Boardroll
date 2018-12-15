using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAction_Ollie : MonoBehaviour {

    [Header("Ollie Params")]
    public float OlliePower = 10;
    public AnimationCurve OllieForceCurve = new AnimationCurve();
    public float OllieApplyTime = 2;
    public Animator BoardPrimaryAnimator;

    private Rigidbody _rigidbody;
    private BoardState _boardStateScript;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boardStateScript = GetComponent<BoardState>();
      
    }

    private void Update()
    {
        if (Input.GetKeyDown("space")  && _boardStateScript.IsGrounded)
        {
            Ollie();
        }

        if (!_boardStateScript.IsGrounded)
        {
            BoardPrimaryAnimator.SetBool("AirHold", true);
        }
        else
        {
            BoardPrimaryAnimator.SetBool("AirHold", false);
        }
    }

    public void Ollie()
    {
        StartCoroutine(OllieCoroutine(OllieApplyTime, OllieForceCurve));
        BoardPrimaryAnimator.SetTrigger("Ollie");
        //AudioManager.Instance.PlayOneShot(GetComponent<AudioClipContainer>().GetClip("Air") , AudioManager.Instance.SFXMixer , 0.3f);
        //E.DOColor(Color.blue , 1f).From();
    }

    private IEnumerator OllieCoroutine(float time, AnimationCurve curve)
    {
        var tickTime = Time.fixedDeltaTime;
        var timePool = 0f;

        while (timePool < time)
        {
            var curveStep = timePool / time;
            timePool += tickTime;
            _rigidbody.AddForce(new Vector2(0, OlliePower * curve.Evaluate(curveStep)), ForceMode.Impulse);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardAction_Ollie : MonoBehaviour {

    [Header("Ollie Params")]
    public float OlliePower = 10;
    public AnimationCurve OllieForceCurve = new AnimationCurve();
    public float OllieApplyTime = 2;
    public Animator BoardPrimaryAnimator;

    private Rigidbody _rigidbody;
    private BoardState _boardStateScript;

    private List<Action> _ollieCallback = new List<Action>();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boardStateScript = GetComponent<BoardState>();
      
    }

    private void Update()
    {
        if (_boardStateScript.IsIncapacitated)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown("space")  && _boardStateScript.IsGrounded)
        {
            Ollie();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && _boardStateScript.InFreeFall)
        {
            Debug.Log("Catch");
            _boardStateScript.Catch();
        }

        //if (!_boardStateScript.IsGrounded)
        //{
        //    BoardPrimaryAnimator.SetBool("AirHold", true);
        //}
        //else
        //{
        //    BoardPrimaryAnimator.SetBool("AirHold", false);
        //}
    }

    public void OllieSubscribe(Action callback)
    {
        if (_ollieCallback.Contains(callback))
            return;

        _ollieCallback.Add(callback);
    }

    public void OllieUnscubscribe(Action callback)
    {
        if (!_ollieCallback.Contains(callback))
            return;

        _ollieCallback.Remove(callback);
    }


    public void Ollie()
    {
        StartCoroutine(OllieCoroutine(OllieApplyTime, OllieForceCurve));

        foreach (Action cb in _ollieCallback)
        {
            cb.Invoke();
        }
        
        //BoardPrimaryAnimator.SetTrigger("Ollie");
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

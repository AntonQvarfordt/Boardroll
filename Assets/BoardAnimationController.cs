using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAnimationController : MonoBehaviour {

    public Animator DeckAnimator;
    public Animator BoardTopLayer;

    private BoardState _boardStateScript;
    private BoardAction_Ollie _actionOllieScript;

    private void Awake()
    {
        _boardStateScript = GetComponent<BoardState>();
        _actionOllieScript = GetComponent<BoardAction_Ollie>();
    }

    private void OnEnable()
    {
        _actionOllieScript.OllieSubscribe(OllieCallback);
    }

    private void OnDisable()
    {
        _actionOllieScript.OllieUnscubscribe(OllieCallback);
    }

    private void Update()
    {
        DeckAnimator.SetBool("IsRolling", _boardStateScript.IsRolling);
        DeckAnimator.SetFloat("Turbulence", _boardStateScript.GetVelocity.x *0.5f);
        BoardTopLayer.SetBool("InFreeFall", _boardStateScript.InFreeFall);
    }

    private void OllieCallback ()
    {
        BoardTopLayer.SetTrigger("Ollie");
    }

}

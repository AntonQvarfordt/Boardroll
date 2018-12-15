using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAnimationController : MonoBehaviour {

    public Animator DeckAnimator;
    public Animator TrucksAnimator;
    public Animator BoardTopLayer;
    public Animator BoardSecondLayer;

    private BoardState _boardStateScript;

    private void Awake()
    {
        _boardStateScript = GetComponent<BoardState>();
    }

    private void Update()
    {
        DeckAnimator.SetBool("IsRolling", _boardStateScript.IsRolling);
        DeckAnimator.SetFloat("Turbulence", _boardStateScript.GetVelocity.x *0.25f);
    }

}

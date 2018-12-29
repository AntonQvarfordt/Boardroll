using UnityEngine;
using DG.Tweening;
public class BoardAnimationController : MonoBehaviour
{

    public Animator DeckAnimator;
    public Animator BoardTopLayer;

    private Vector3 _deckAnimatorInitPosition;
    private Quaternion _deckAnimatorInitRotation;

    private Vector3 _boardTopLayerInitPosition;
    private Quaternion _boardTopLayerInitRotation;

    private BoardState _boardStateScript;
    private BoardAction_Ollie _actionOllieScript;

    private bool _disabled;

    private void Awake()
    {
        _boardStateScript = GetComponent<BoardState>();
        _actionOllieScript = GetComponent<BoardAction_Ollie>();

        _deckAnimatorInitPosition = DeckAnimator.transform.localPosition;
        _deckAnimatorInitRotation = DeckAnimator.transform.localRotation;

        _boardTopLayerInitPosition = BoardTopLayer.transform.localPosition;
        _boardTopLayerInitRotation = BoardTopLayer.transform.localRotation;
    }

    private void OnEnable()
    {
        _boardStateScript.OnIncapacitatedSubscribe(OnIncapacitated);
        _actionOllieScript.OllieSubscribe(OllieCallback);
    }

    private void OnDisable()
    {
        _boardStateScript.OnIncapacitatedUnsubscribe(OnIncapacitated);
        _actionOllieScript.OllieUnscubscribe(OllieCallback);
    }

    private void Update()
    {
        if (_disabled)
            return;

        DeckAnimator.SetBool("IsRolling", _boardStateScript.IsRolling);
        DeckAnimator.SetFloat("Turbulence", _boardStateScript.GetVelocity.x * 0.5f);
        BoardTopLayer.SetBool("InFreeFall", _boardStateScript.InFreeFall);
    }

    private void OllieCallback()
    {
        BoardTopLayer.SetTrigger("Ollie");
    }

    private void OnIncapacitated()
    {
        _disabled = true;

        DeckAnimator.enabled = false;
        BoardTopLayer.enabled = false;

        DeckAnimator.transform.DOLocalMove(_deckAnimatorInitPosition, 1);
        DeckAnimator.transform.DOLocalRotateQuaternion(_deckAnimatorInitRotation, 1);

        BoardTopLayer.transform.DOLocalMove(_boardTopLayerInitPosition, 1);
        BoardTopLayer.transform.DOLocalRotateQuaternion(_boardTopLayerInitRotation, 1);
    }

}

using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class AVizBeatPunch : MonoBehaviour
{

    public RectTransform Target;
    public VisualizationBrain VisBrain;

    private RectTransform _proxy;

    private void OnEnable()
    {
        VisBrain.OnSetSubscribe(BeatAction);
    }

    private void OnDisable()
    {
        VisBrain.OnSetUnsubscribe(BeatAction);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _proxy = CreateProxyTransform(Target);
        Target.SetParent(_proxy);
    }

    public void Jump(float strength)
    {
        var newShakeStrength = Random.Range(strength - (strength * 0.5f), strength + strength);

        var isTweening = DOTween.IsTweening(_proxy);

        if (isTweening)
            DOTween.Complete(_proxy);

        _proxy.DOShakeScale(0.2f, newShakeStrength*0.3f, 25);
    }

    private void BeatAction(BeatEventData eventData)
    {
        if (eventData.volume < 50)
            return;

        Jump(eventData.volume / 100);
    }

    private RectTransform CreateProxyTransform(RectTransform target)
    {
        var duplicate = Instantiate(target.gameObject, transform);
        var duplicateComponents = duplicate.GetComponents<Component>();

        var removeList = new List<Component>();

        foreach (Component comp in duplicateComponents)
        {
            if (comp.GetType() != typeof(RectTransform))
            {

                removeList.Add(comp);
            }
        }

        foreach (Component comp in removeList)
        {
            Destroy(comp);
        }

        foreach (RectTransform child in duplicate.transform)
        {
            if (child == duplicate.GetComponent<RectTransform>())
                continue;

            Destroy(child.gameObject);
        }

        duplicate.name = target.name + "+PROXY";

        return duplicate.GetComponent<RectTransform>();
    }
}

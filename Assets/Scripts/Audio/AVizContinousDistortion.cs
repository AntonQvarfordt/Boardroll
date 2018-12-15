using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVizContinousDistortion : MonoBehaviour
{
    public RectTransform Target;
    public float DeformScaleMagnitude;
    private bool _isDeformingScale;
    public Vector3 TargetOriginalScale = new Vector3(1, 1, 1);
    public VisualizationBrain VisBrain;

    private RectTransform _proxy;

    private void Start()
    {
        TargetOriginalScale = Target.localScale;
        VisBrain.eventProvider.SongLoaded += Init;
    }

    private void Init()
    {
        var targetSiblingIndex = Target.GetSiblingIndex();
        _proxy = CreateProxyTransform(Target);
        Target.SetParent(_proxy);
        _proxy.SetSiblingIndex(targetSiblingIndex);
        OpenDeformScale();
    }

    private void OnDisable()
    {
        CloseDeformScale();
    }

    public void OpenDeformScale()
    {
        if (_isDeformingScale)
            return;

        _isDeformingScale = true;
        StartCoroutine(DeformScaleUpdate());
    }

    public void CloseDeformScale()
    {
        if (!_isDeformingScale)
            return;

        _isDeformingScale = false;
        var newScale = TargetOriginalScale;
        _proxy.localScale = newScale;
    }

    private IEnumerator DeformScaleUpdate()
    {
        while (_isDeformingScale)
        {
            var newScale = TargetOriginalScale;
            var addition = 1 + (VisBrain.GetVolume * 0.001f);

            newScale *= addition;
            _proxy.localScale = newScale;

            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
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

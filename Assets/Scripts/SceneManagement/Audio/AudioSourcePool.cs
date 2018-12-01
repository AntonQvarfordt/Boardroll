using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioSourcePool : ObjectPool<AudioSourcePool, AudioSourceObject>
{
    public AudioSourceObject GetAudoSourceObject ()
    {
        var poolObj = Pop();
        poolObj.instance.name = poolObj.instance.name + "-Playing...";
        return poolObj;
    }

    public void ReturnToPoolOnNotPlaying (AudioSourceObject audioObj)
    {
        StartCoroutine(InvokeOnFinishedPlaying(audioObj.GetAudioSource, audioObj.ReturnToPool));
    }

    private IEnumerator InvokeOnFinishedPlaying (AudioSource source, Action action)
    {
        while (source.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        action.Invoke();
    }
}

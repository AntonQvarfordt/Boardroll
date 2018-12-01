using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceObject : PoolObject<AudioSourcePool, AudioSourceObject>
{
    private AudioSource _audioSource;

    public AudioSource GetAudioSource
    {
        get { return _audioSource; }
    }

    protected override void SetReferences()
    {
        base.SetReferences();
        _audioSource = instance.GetComponent<AudioSource>();
    }
    public override void ReturnToPool ()
    {
        base.ReturnToPool();
        instance.name = "audioSource(Clone)";
    }
}

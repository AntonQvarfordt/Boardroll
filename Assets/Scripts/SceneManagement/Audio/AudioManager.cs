﻿using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSourcePool))]
public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
	public AudioMixerGroup SFXMixer;
	private AudioSourcePool _audioSourcePool;

    private void Awake()
    {
        _audioSourcePool = GetComponent<AudioSourcePool>();
        Debug.Log("Awake AM - " + frameCount);

    }

    private void Start()
    {
        Debug.Log("Start AM - " + frameCount);
        StartCoroutine(InitCall());
    }
    private void Update()
    {
        frameCount++;
    }
    private IEnumerator InitCall()
    {
        yield return null;
        Init();
    }
    public void Init()
    {
        Debug.Log("Init AM - " + frameCount);
        _initialized = true;
    }

    public void PlayClipTrigger (AudioClip clip)
    {
        PlayOneShot(clip);
    }

    public void PlayOneShot (AudioClip clip, AudioMixerGroup mixerGroup = null, float volume = 1, float pitch = 1)
    {
        var audioSourceObject = _audioSourcePool.GetAudoSourceObject();
        var audioSource = audioSourceObject.GetAudioSource;

		if (mixerGroup == null)
			mixerGroup = SFXMixer;

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = mixerGroup;

        audioSource.Play();

        _audioSourcePool.ReturnToPoolOnNotPlaying(audioSourceObject);
    }
}
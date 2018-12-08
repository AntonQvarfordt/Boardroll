using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BeatEventData
{
    public float volume;
    public Intensity strength;
}

public enum Intensity
{
    Low,
    Mid,
    High
}

public class VisualizationBrain : MonoBehaviour {

    public List<Action<BeatEventData>> OnSetCallbacks = new List<Action<BeatEventData>>();

	public RhythmTool rhythmTool;
	public RhythmEventProvider eventProvider;
	public float Volume;

	public List<AudioClip> audioClips = new List<AudioClip>();
	private int currentSong;

    public float GetVolume
    {
        get
        {
            AnalysisData all = rhythmTool.all;
            return all.magnitude[rhythmTool.currentFrame];
        }
    }

    public float GetSmoothVolume
	{
		get
		{
			AnalysisData all = rhythmTool.all;
            return all.magnitudeSmooth[rhythmTool.currentFrame];
		}
	}

	public bool IsSongLoaded
	{
		get
		{
			return rhythmTool.songLoaded;
		}
	}

	private void Start()
	{
		currentSong = -1;

		eventProvider.Onset += OnOnset;
		eventProvider.Beat += OnBeat;
		eventProvider.Change += OnChange;
		eventProvider.SongLoaded += OnSongLoaded;
		eventProvider.SongEnded += OnSongEnded;

		if (audioClips.Count <= 0)
			Debug.LogWarning("no songs configured");
		else
			NextSong();
	}

	private void OnSongLoaded()
	{
		Debug.Log("On Song Loaded");
		rhythmTool.Play();
	}

	private void OnSongEnded()
	{
		Debug.Log("On Song Ended");
		NextSong();
	}

	private void NextSong()
	{
		currentSong++;

		if (currentSong >= audioClips.Count)
			currentSong = 0;

		rhythmTool.audioClip = audioClips[currentSong];
	}

	private void OnChange( int index , float change )
	{
		Debug.Log("Change| " + index + "| " + change);
	}

	private void OnBeat( Beat beat )
	{
		//Debug.Log("Beat| " + beat.length + "|");
	}

    public void OnSetSubscribe (Action<BeatEventData> action)
    {
        if (OnSetCallbacks.Contains(action))
            return;

        OnSetCallbacks.Add(action);
    }

    public void OnSetUnsubscribe(Action<BeatEventData> action)
    {
        if (!OnSetCallbacks.Contains(action))
            return;

        OnSetCallbacks.Remove(action);
    }

    private void OnOnset( OnsetType type , Onset onset )
	{
		if (onset.rank < 4 && onset.strength < 5)
			return;

		switch (type)
		{
			case OnsetType.Low:
				//Debug.Log("OnSet|low");
				break;
			case OnsetType.Mid:
				//Debug.Log("OnSet|mid");
				break;
			case OnsetType.High:
				//Debug.Log("OnSet|high");
				break;
			case OnsetType.All:
				//Debug.Log("OnSet|all");
                foreach (Action<BeatEventData> callback in OnSetCallbacks)
                {
                    callback.Invoke(GetBeatData());
                }
				break;
		}
	}

    private BeatEventData GetBeatData ()
    {
        var returnValue = new BeatEventData();
        returnValue.volume = GetSmoothVolume;
        returnValue.strength = Intensity.Mid;
        return returnValue;
    }

}

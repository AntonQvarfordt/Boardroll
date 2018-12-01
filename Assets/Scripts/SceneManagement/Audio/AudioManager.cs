using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSourcePool))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<AudioManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    protected static AudioManager instance;

	public AudioMixerGroup SFXMixer;

	private AudioSourcePool _audioSourcePool;

    public static AudioManager Create()
    {
        GameObject sceneControllerGameObject = new GameObject("AudioManager");
        instance = sceneControllerGameObject.AddComponent<AudioManager>();

        return instance;
    }

    private void Awake()
    {
        _audioSourcePool = GetComponent<AudioSourcePool>();
    }

    private void Start()
    {
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

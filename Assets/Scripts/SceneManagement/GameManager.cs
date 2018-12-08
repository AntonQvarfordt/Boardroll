using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Play
}
public class GameManager : MonoBehaviourSingleton<GameManager>
{

    public GameState ActiveGameState;

    public string PlayScene;
    public string MenuScene;

    private void Awake()
    {
        Debug.Log("Awake GM - " + frameCount);
    }

    private void Start()
    {
        Debug.Log("Start GM - " + frameCount);
        StartCoroutine(InitCall());
    }

    private IEnumerator InitCall()
    {
        yield return null;
        Init();
    }

    public void Init ()
    {
        Debug.Log("Init GM - " + frameCount);
        _initialized = true;
    }
    private void Update()
    {
        frameCount++;
    }
    public void StartPlayScene()
    {
        SceneManager.sceneLoaded += OnPlaySceneLoaded;
        SceneManager.LoadScene(PlayScene);
    }

    private void OnPlaySceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnPlaySceneLoaded;

        var playManager = FindObjectOfType<PlayManager>();

        if (playManager == null)
        {
            throw new System.ArgumentException("Parameter cannot be null");

        }

        ActiveGameState = GameState.Play;
    }

    public void OpenMenu()
    {

    }
}

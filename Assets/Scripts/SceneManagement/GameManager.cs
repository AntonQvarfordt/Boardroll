using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Play
}
public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    public Player ActivePlayer;
    public GameState ActiveGameState;

    public string PlayScene;
    public string MenuScene;

    public override void Awake()
    {
        base.Awake();
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
        var newPlayer = playManager.SpawnPlayer();
        playManager.Init(newPlayer.gameObject);
    }

    public void OpenMenu()
    {

    }
}

using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public string mapSceneName;
    
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    public void OnPlay()
    {
        _gameManager.ResetGame();
        SceneFader.Instance.LoadSceneWithFade(mapSceneName);
    }
}

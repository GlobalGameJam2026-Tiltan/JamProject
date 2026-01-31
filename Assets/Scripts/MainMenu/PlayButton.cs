using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public string mapSceneName;
    
    public void OnPlay()
    {
        SceneFader.instance.LoadSceneWithFade(this.mapSceneName);
    }
}

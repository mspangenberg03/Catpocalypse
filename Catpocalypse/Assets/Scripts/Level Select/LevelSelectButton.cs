using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string sceneToLoad)
    {
        try
        {
            SceneLoader_Async.LoadSceneAsync("CutScene" + sceneToLoad);
        } catch
        {
            SceneLoader_Async.LoadSceneAsync(sceneToLoad);
        }
    }
}

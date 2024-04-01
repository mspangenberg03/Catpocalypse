using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class SceneLoader_Async : MonoBehaviour
{
    public static SceneLoader_Async Instance;


    [Header("UI References")]
    [SerializeField] private Slider _ProgressBar;
    [SerializeField] private TextMeshProUGUI _ProgressBarText;


    private static bool IsLoadingScreenActive;



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("A SceneLoader_Async already exists! Self destructing.");
            Destroy(gameObject);
            return;
        }


        Instance = this;

        // Tell Unity to not destroy this game object when a new scene is loaded.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!IsLoadingScreenActive)
        {
            // Disable the loading screen.
            gameObject.SetActive(false);
        }
    }


    public static void LoadSceneAsync(string sceneToLoad)
    {
        if (Instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Loading Screen");
            GameObject loadingScreen = Instantiate(prefab, Vector3.zero, Quaternion.identity);
           
            loadingScreen.GetComponent<SceneLoader_Async>().LoadScene_Async(sceneToLoad);
        }
        else
        {
            Instance.LoadScene_Async(sceneToLoad);
        }
    }


    public void LoadScene_Async(string sceneToLoad)
    {
        if (IsLoadingScreenActive)
        {
            Debug.LogError($"Cannot load the scene \"{sceneToLoad}\", because the loading screen is already loading another scene!");
            return;
        }


        gameObject.SetActive(true);
        
        StartCoroutine(LoadScene(sceneToLoad));        
    }

    private IEnumerator LoadScene(string sceneToLoad)
    {
        IsLoadingScreenActive = true;

        _ProgressBar.value = 0f;
        _ProgressBarText.text = "0%";

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loadOperation.isDone)
        {
            float progress = loadOperation.progress / 1.0f;
            _ProgressBar.value = progress;
            _ProgressBarText.text = progress.ToString("P0");
            yield return null;  
        }


        IsLoadingScreenActive = false;
        gameObject.SetActive(false);
    }
}

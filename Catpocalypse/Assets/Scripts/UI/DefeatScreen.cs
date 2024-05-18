using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatScreen : MonoBehaviour
{
    [Tooltip("The list of random defeat text images that can appear on this screen.")]
    [SerializeField]
    private List<Sprite> _DefeatTextImages;

    [Tooltip("This is the image where the random defeat message gets displayed.")]
    [SerializeField]
    private Image _DefeatTextImage;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // Select a random display text every time this panel is opened.
        SelectRandomDisplayText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectRandomDisplayText()
    {
        int index = Random.Range(0, _DefeatTextImages.Count);

        _DefeatTextImage.sprite = _DefeatTextImages[index];
    }

    public void OnRetryClicked()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("TowerBase");
        foreach (GameObject tb in bases)
        {
            if (tb.GetComponent<TowerBase>().hasTower == true)
            {
                tb.GetComponent<TowerBase>().DestroyTower();
            }
        }


        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneLoader_Async.LoadSceneAsync("SceneManager.GetActiveScene().name");
    }

    public void OnMainMenuClicked()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("TowerBase");
        foreach (GameObject tb in bases)
        {
            if (tb.GetComponent<TowerBase>().hasTower == true)
            {
                tb.GetComponent<TowerBase>().DestroyTower();
            }
        }

        //SceneManager.LoadScene("MainMenu");
        SceneLoader_Async.LoadSceneAsync("MainMenu");
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
